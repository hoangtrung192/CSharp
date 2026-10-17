using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LuxuryBridalStudio
{
    public static class Roles
    {
        public const string Manager = "Quản lý";
        public const string Employee = "Nhân viên";
        public const string Warehouse = "Thủ kho";
    }

    public enum Feature { Overview, Customers, Products, Suppliers, Receipts, Contracts, Returns, Employees, Reports }

    public sealed class UserSession
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string Role { get; private set; }

        public UserSession(string userName, string displayName, string role)
        {
            if (string.IsNullOrWhiteSpace(userName) || !new[] { Roles.Manager, Roles.Employee, Roles.Warehouse }.Contains(role))
                throw new ArgumentException("Thông tin phiên đăng nhập không hợp lệ.");
            UserName = userName; DisplayName = displayName; Role = role;
        }

        public bool Can(Feature feature, bool write = false)
        {
            if (Role == Roles.Manager) return true;
            if (feature == Feature.Overview) return true;
            if (feature == Feature.Products) return !write || Role == Roles.Warehouse;
            if (Role == Roles.Warehouse) return feature == Feature.Suppliers || feature == Feature.Receipts;
            return feature == Feature.Customers || feature == Feature.Contracts || feature == Feature.Returns;
        }
    }

    // Runtime uses SqlAccountService; the memory implementation supports isolated tests.
    public interface IAccountService
    {
        UserSession Authenticate(string userName, string password);
        void Register(string userName, string password);
    }

    public sealed class MemoryAccountService : IAccountService
    {
        private sealed class Account
        {
            public string Name, Role;
            public byte[] Salt, Hash;
        }
        private readonly Dictionary<string, Account> accounts = new Dictionary<string, Account>(StringComparer.OrdinalIgnoreCase);

        public void Add(string name, string password, string role)
        {
            name = (name ?? "").Trim();
            if (!Regex.IsMatch(name, @"^[A-Za-z0-9_.-]{3,50}$")) throw new InvalidOperationException("Tài khoản cần 3–50 ký tự: chữ, số, dấu chấm, gạch dưới hoặc gạch ngang.");
            if (password == null || password.Length < 10 || password.Length > 128 || !password.Any(char.IsLetter) || !password.Any(char.IsDigit))
                throw new InvalidOperationException("Mật khẩu cần 10–128 ký tự, gồm chữ và số.");
            new UserSession(name, name, role);
            if (accounts.ContainsKey(name)) throw new InvalidOperationException("Tên tài khoản đã được sử dụng.");
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            accounts.Add(name, new Account { Name = name, Role = role, Salt = salt, Hash = Hash(password, salt) });
        }

        public void Register(string userName, string password) { Add(userName, password, Roles.Employee); }

        public UserSession Authenticate(string userName, string password)
        {
            Account account;
            if (!accounts.TryGetValue((userName ?? "").Trim(), out account) || password == null || password.Length > 128)
                throw new InvalidOperationException("Tài khoản hoặc mật khẩu không đúng.");
            byte[] supplied = Hash(password, account.Salt);
            int difference = 0;
            for (int i = 0; i < supplied.Length; i++) difference |= supplied[i] ^ account.Hash[i];
            if (difference != 0) throw new InvalidOperationException("Tài khoản hoặc mật khẩu không đúng.");
            return new UserSession(account.Name, account.Name, account.Role);
        }

        private static byte[] Hash(string password, byte[] salt)
        {
            using (var hash = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256)) return hash.GetBytes(32);
        }
    }

    public static class AppSession
    {
        public static StudioWorkspace Data { get; set; } = new StudioWorkspace();
        public static IAccountService Accounts { get; set; }
        public static UserSession User { get; set; }

        public static void Require(Feature feature, bool write = false)
        {
            if (User == null || !User.Can(feature, write)) throw new InvalidOperationException("Bạn không có quyền thực hiện thao tác này.");
        }
    }
}
