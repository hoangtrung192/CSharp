using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LuxuryBridalStudio
{
    public sealed class SqlAccountService : IAccountService
    {
        private readonly string connectionString;
        public SqlAccountService(string connectionString) { this.connectionString = connectionString; }
        public bool HasAccounts
        {
            get
            {
                try
                {
                    using (var connection = SqlStudioStore.Open(connectionString))
                    using (var command = new SqlCommand("SELECT COUNT(*) FROM dbo.Accounts", connection)) return (int)command.ExecuteScalar() != 0;
                }
                catch (SqlException ex) { throw SqlStudioStore.DatabaseError(ex); }
            }
        }

        public void Register(string userName, string password) { Create(userName, password, false); }
        public void CreateInitialManager(string userName, string password) { Create(userName, password, true); }
        private void Create(string userName, string password, bool initialManager)
        {
            userName = (userName ?? "").Trim();
            if (!Regex.IsMatch(userName, @"^[A-Za-z0-9_.-]{3,50}$"))
                throw new InvalidOperationException("Tài khoản cần 3–50 ký tự: chữ, số, dấu chấm, gạch dưới hoặc gạch ngang.");
            if (password == null || password.Length < 10 || password.Length > 128 || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
                throw new InvalidOperationException("Mật khẩu cần 10–128 ký tự, gồm chữ và số.");
            var salt = new byte[16];
            using (var random = RandomNumberGenerator.Create()) random.GetBytes(salt);
            byte[] hash = Hash(password, salt, 100000);
            try
            {
                using (var connection = SqlStudioStore.Open(connectionString))
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    using (var command = new SqlCommand(@"DECLARE @result int;
                        EXEC @result=sys.sp_getapplock @Resource=N'LuxuryBridalStudio.Accounts',
                            @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=15000;
                        IF @result < 0 THROW 51011, 'Account setup is busy; please retry.', 1;
                        SELECT COUNT(*) FROM dbo.Accounts;", connection, transaction))
                    {
                        int count = (int)command.ExecuteScalar();
                        if (initialManager && count != 0) throw new InvalidOperationException("Đã có tài khoản. Không thể tạo lại quản lý ban đầu.");
                        if (!initialManager && count == 0) throw new InvalidOperationException("Cần tạo tài khoản quản lý ban đầu trước khi đăng ký nhân viên.");
                    }
                    using (var command = new SqlCommand(@"INSERT dbo.Accounts
                        (UserName, DisplayName, RoleName, PasswordHash, PasswordSalt, PasswordIterations, PasswordAlgorithm)
                        VALUES (@name, @display, @role, @hash, @salt, 100000, 'PBKDF2-SHA256');", connection, transaction))
                    {
                        command.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = userName;
                        command.Parameters.Add("@display", SqlDbType.NVarChar, 150).Value = userName;
                        command.Parameters.Add("@role", SqlDbType.NVarChar, 30).Value = initialManager ? Roles.Manager : Roles.Employee;
                        command.Parameters.Add("@hash", SqlDbType.Binary, 32).Value = hash;
                        command.Parameters.Add("@salt", SqlDbType.Binary, 16).Value = salt;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            catch (SqlException ex) { throw SqlStudioStore.DatabaseError(ex); }
        }

        public UserSession Authenticate(string userName, string password)
        {
            userName = (userName ?? "").Trim();
            if (userName.Length > 50 || password == null || password.Length > 128)
                throw new InvalidOperationException("Tài khoản hoặc mật khẩu không đúng.");
            try
            {
                using (var connection = SqlStudioStore.Open(connectionString))
                using (var command = new SqlCommand(@"SELECT UserName, DisplayName, RoleName, PasswordHash, PasswordSalt, PasswordIterations, PasswordAlgorithm
                    FROM dbo.Accounts WHERE UserName=@name AND IsActive=1;", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = userName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new InvalidOperationException("Tài khoản hoặc mật khẩu không đúng.");
                        int iterations = reader.GetInt32(5);
                        if (reader.GetString(6) != "PBKDF2-SHA256" || iterations < 100000 || iterations > 2000000)
                            throw new InvalidOperationException("Cấu hình mật khẩu không được hỗ trợ.");
                        byte[] expected = (byte[])reader[3], supplied = Hash(password, (byte[])reader[4], iterations);
                        int difference = 0;
                        for (int i = 0; i < expected.Length; i++) difference |= expected[i] ^ supplied[i];
                        if (difference != 0) throw new InvalidOperationException("Tài khoản hoặc mật khẩu không đúng.");
                        return new UserSession(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
            catch (SqlException ex) { throw SqlStudioStore.DatabaseError(ex); }
        }

        private static byte[] Hash(string password, byte[] salt, int iterations)
        { using (var hash = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)) return hash.GetBytes(32); }
    }
}
