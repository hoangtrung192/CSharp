using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LuxuryBridalStudio
{
    // A unit of work loads fresh data under a database-wide application lock, runs
    // the existing domain service, then writes only changed rows in one transaction.
    // This deliberately favors correctness over throughput for a small studio.
    public sealed class SqlStudioStore
    {
        private readonly string connectionString;
        private sealed class Mapping
        {
            public string Table;
            public string[] Keys, Columns;
            public bool Versioned;
            public Mapping(string table, string keys, string columns, bool versioned = false)
            { Table = table; Keys = keys.Split(','); Columns = columns.Split(','); Versioned = versioned; }
        }
        private static readonly Mapping[] Maps = {
            new Mapping("Customers", "CustomerCode", "CustomerCode,FullName,Phone,IdentityNumber,Address", true),
            new Mapping("Suppliers", "SupplierCode", "SupplierCode,SupplierName,Phone,Address,Notes", true),
            new Mapping("Products", "ProductCode", "ProductCode,ProductName,Category,Color,Origin,TotalQuantity,RentalPrice,PurchasePrice,ImagePath", true),
            new Mapping("Employees", "EmployeeCode", "EmployeeCode,FullName,Phone,RoleName,IsActive", true),
            new Mapping("RentalContracts", "ContractCode", "ContractCode,CustomerCode,CustomerName,StartDate,DueDate,RentalTotal,AmountCollected,Status,ReturnedDate,ExtraCharge,CollectedOnReturn,Notes", true),
            new Mapping("RentalLines", "ContractCode,LineNumber", "ContractCode,LineNumber,ProductCode,ProductName,Category,Quantity,UnitPrice"),
            new Mapping("StockReceipts", "ReceiptCode", "ReceiptCode,SupplierCode,RecordedBy,ReceiptDate,Notes,Total", true),
            new Mapping("StockReceiptLines", "ReceiptCode,LineNumber", "ReceiptCode,LineNumber,ProductCode,ProductName,Category,Quantity,UnitPrice"),
            new Mapping("CashTransactions", "TransactionCode", "TransactionCode,ContractCode,LineNumber,TransactionDate,CustomerName,Category,Description,Collected,Refunded")
        };

        public SqlStudioStore(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Thiếu chuỗi kết nối SQL Server.");
            this.connectionString = connectionString;
        }

        internal static SqlConnection Open(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                using (var command = new SqlCommand(@"SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; SET ANSI_PADDING ON;
                    SET ANSI_WARNINGS ON; SET CONCAT_NULL_YIELDS_NULL ON; SET ARITHABORT ON; SET NUMERIC_ROUNDABORT OFF;
                    SET XACT_ABORT ON;", connection)) command.ExecuteNonQuery();
                return connection;
            }
            catch (SqlException ex) { connection.Dispose(); throw DatabaseError(ex); }
        }

        internal static InvalidOperationException DatabaseError(SqlException ex)
        {
            string message = ex.Number == 2601 || ex.Number == 2627 ? "Mã, số điện thoại, CCCD hoặc tài khoản đã được sử dụng."
                : ex.Number == 547 ? "Dữ liệu vi phạm quy tắc hoặc đang được giao dịch khác sử dụng."
                : "Không thể đọc hoặc lưu dữ liệu SQL Server. Kiểm tra kết nối và cấu hình cơ sở dữ liệu; thao tác chưa được xác nhận thành công.";
            return new InvalidOperationException(message, ex);
        }

        public StudioWorkspace Load()
        {
            var workspace = new StudioWorkspace();
            Refresh(workspace);
            workspace.Store = this;
            return workspace;
        }

        public void Refresh(StudioWorkspace workspace)
        {
            lock (workspace.SyncRoot)
            {
                try
                {
                    using (var connection = Open(connectionString))
                    using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                    {
                        Lock(connection, transaction, "Shared");
                        using (var snapshot = Read(connection, transaction))
                        {
                            var fresh = Materialize(snapshot);
                            transaction.Commit();
                            workspace.ReplaceFrom(fresh);
                        }
                    }
                }
                catch (SqlException ex) { throw DatabaseError(ex); }
            }
        }

        internal T Write<T>(StudioWorkspace displayed, Func<StudioWorkspace, T> operation)
        {
            lock (displayed.SyncRoot)
            {
                try
                {
                    using (var connection = Open(connectionString))
                    using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                    {
                        Lock(connection, transaction, "Exclusive");
                        CheckActor(connection, transaction);
                        using (var before = Read(connection, transaction))
                        {
                            var current = Materialize(before);
                            T result = operation(current);
                            using (var after = Capture(current, before))
                            {
                                CheckVersions(displayed, before, after);
                                Save(connection, transaction, before, after);
                            }
                            // Read generated rowversions before commit; no SQL calls can fail after commit.
                            using (var saved = Read(connection, transaction))
                            {
                                var fresh = Materialize(saved);
                                transaction.Commit();
                                displayed.ReplaceFrom(fresh);
                            }
                            return result;
                        }
                    }
                }
                catch (SqlException ex) { throw DatabaseError(ex); }
            }
        }

        private static void Lock(SqlConnection connection, SqlTransaction transaction, string mode)
        {
            using (var command = new SqlCommand(@"DECLARE @result int;
                EXEC @result = sys.sp_getapplock @Resource=N'LuxuryBridalStudio.Workspace',
                    @LockMode=@mode, @LockOwner='Transaction', @LockTimeout=15000;
                IF @result < 0 THROW 51010, 'Workspace is busy; please retry.', 1;", connection, transaction))
            {
                command.Parameters.Add("@mode", SqlDbType.VarChar, 32).Value = mode;
                command.ExecuteNonQuery();
            }
        }

        private static void CheckActor(SqlConnection connection, SqlTransaction transaction)
        {
            if (AppSession.User == null) throw new InvalidOperationException("Vui lòng đăng nhập lại.");
            using (var command = new SqlCommand("SELECT RoleName FROM dbo.Accounts WHERE UserName=@name AND IsActive=1", connection, transaction))
            {
                command.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = AppSession.User.UserName;
                if (!Equals(command.ExecuteScalar(), AppSession.User.Role))
                    throw new InvalidOperationException("Tài khoản hoặc quyền đã thay đổi. Vui lòng đăng nhập lại.");
            }
        }

        private static DataSet Read(SqlConnection connection, SqlTransaction transaction)
        {
            var data = new DataSet();
            try
            {
                foreach (var map in Maps)
                {
                    string columns = string.Join(",", map.Columns.Select(Quote)) + (map.Versioned ? ",[RowVersion]" : "");
                    using (var command = new SqlCommand("SELECT " + columns + " FROM dbo." + Quote(map.Table) + " ORDER BY " + string.Join(",", map.Keys.Select(Quote)), connection, transaction))
                    using (var adapter = new SqlDataAdapter(command)) adapter.Fill(data, map.Table);
                    var table = data.Tables[map.Table];
                    table.PrimaryKey = map.Keys.Select(k => table.Columns[k]).ToArray();
                }
                return data;
            }
            catch { data.Dispose(); throw; }
        }

        private static string Text(DataRow row, string name) { return row.IsNull(name) ? "" : Convert.ToString(row[name]); }
        private static StudioWorkspace Materialize(DataSet data)
        {
            var workspace = new StudioWorkspace();
            foreach (DataRow r in data.Tables["Customers"].Rows)
                workspace.Customers.Rows.Add(r["CustomerCode"], r["FullName"], r["Phone"], Text(r, "IdentityNumber"), r["Address"]);
            foreach (DataRow r in data.Tables["Suppliers"].Rows)
                workspace.Suppliers.Rows.Add(r["SupplierCode"], r["SupplierName"], r["Phone"], r["Address"], r["Notes"]);
            foreach (DataRow r in data.Tables["Products"].Rows)
                workspace.Products.Rows.Add(r["ProductCode"], r["ProductName"], r["Category"], r["Color"], r["Origin"], r["TotalQuantity"], r["RentalPrice"], r["PurchasePrice"], r["ImagePath"]);
            foreach (DataRow r in data.Tables["Employees"].Rows)
                workspace.Employees.Add(new StaffMember { Ma = Text(r, "EmployeeCode"), HoTen = Text(r, "FullName"), DienThoai = Text(r, "Phone"), VaiTro = Text(r, "RoleName"), DangLam = (bool)r["IsActive"] });
            var rentals = new Dictionary<string, RentalContract>();
            foreach (DataRow r in data.Tables["RentalContracts"].Rows)
            {
                var rental = new RentalContract { Ma = Text(r, "ContractCode"), CustomerCode = Text(r, "CustomerCode"), KhachHang = Text(r, "CustomerName"),
                    NgayThue = (DateTime)r["StartDate"], HenTra = (DateTime)r["DueDate"], TienThue = (decimal)r["RentalTotal"], DaThu = (decimal)r["AmountCollected"],
                    TrangThai = Text(r, "Status"), NgayTra = r.IsNull("ReturnedDate") ? (DateTime?)null : (DateTime)r["ReturnedDate"],
                    PhuPhi = (decimal)r["ExtraCharge"], ThuKhiTra = (decimal)r["CollectedOnReturn"], GhiChu = Text(r, "Notes") };
                rentals.Add(rental.Ma, rental); workspace.Contracts.Add(rental);
            }
            foreach (DataRow r in data.Tables["RentalLines"].Rows) rentals[Text(r, "ContractCode")].Lines.Add(ReadLine(r));
            foreach (var rental in workspace.Contracts) rental.SanPham = string.Join("; ", rental.Lines.Select(x => x.ProductName + " × " + x.Quantity));
            var receipts = new Dictionary<string, StockReceipt>();
            foreach (DataRow r in data.Tables["StockReceipts"].Rows)
            {
                var receipt = new StockReceipt { Code = Text(r, "ReceiptCode"), SupplierCode = Text(r, "SupplierCode"), Employee = Text(r, "RecordedBy"),
                    Date = (DateTime)r["ReceiptDate"], Notes = Text(r, "Notes"), Total = (decimal)r["Total"], Lines = new List<RentalLine>() };
                receipts.Add(receipt.Code, receipt); workspace.Receipts.Add(receipt);
            }
            foreach (DataRow r in data.Tables["StockReceiptLines"].Rows) receipts[Text(r, "ReceiptCode")].Lines.Add(ReadLine(r));
            foreach (DataRow r in data.Tables["CashTransactions"].Rows)
                workspace.Transactions.Add(new GiaoDichThongKe { Ma = Text(r, "TransactionCode"), ContractCode = Text(r, "ContractCode"), LineNumber = (int)r["LineNumber"],
                    Ngay = (DateTime)r["TransactionDate"], KhachHang = Text(r, "CustomerName"), Nhom = Text(r, "Category"), NoiDung = Text(r, "Description"), Thu = (decimal)r["Collected"], Hoan = (decimal)r["Refunded"] });
            foreach (var map in Maps.Where(x => x.Versioned))
                foreach (DataRow row in data.Tables[map.Table].Rows)
                    workspace.Versions.Add(VersionKey(map, row), (byte[])row["RowVersion"]);
            return workspace;
        }

        private static RentalLine ReadLine(DataRow r)
        {
            return new RentalLine { ProductCode = Text(r, "ProductCode"), ProductName = Text(r, "ProductName"), Category = Text(r, "Category"), Quantity = (int)r["Quantity"], UnitPrice = (decimal)r["UnitPrice"] };
        }

        private static DataSet Capture(StudioWorkspace workspace, DataSet schema)
        {
            var data = schema.Clone();
            foreach (DataRow r in workspace.Customers.Rows)
                data.Tables["Customers"].Rows.Add(r[0], r[1], r[2], string.IsNullOrEmpty(Convert.ToString(r[3])) ? (object)DBNull.Value : r[3], r[4]);
            foreach (DataRow r in workspace.Suppliers.Rows) data.Tables["Suppliers"].Rows.Add(r.ItemArray);
            foreach (DataRow r in workspace.Products.Rows) data.Tables["Products"].Rows.Add(r.ItemArray);
            foreach (var e in workspace.Employees) data.Tables["Employees"].Rows.Add(e.Ma, e.HoTen, e.DienThoai, e.VaiTro, e.DangLam);
            foreach (var r in workspace.Contracts)
            {
                data.Tables["RentalContracts"].Rows.Add(r.Ma, r.CustomerCode, r.KhachHang, r.NgayThue, r.HenTra, r.TienThue, r.DaThu, r.TrangThai, (object)r.NgayTra ?? DBNull.Value, r.PhuPhi, r.ThuKhiTra, r.GhiChu ?? "");
                for (int i = 0; i < r.Lines.Count; i++) AddLine(data.Tables["RentalLines"], r.Ma, i + 1, r.Lines[i]);
            }
            foreach (var r in workspace.Receipts)
            {
                data.Tables["StockReceipts"].Rows.Add(r.Code, r.SupplierCode, r.Employee, r.Date, r.Notes ?? "", r.Total);
                for (int i = 0; i < r.Lines.Count; i++) AddLine(data.Tables["StockReceiptLines"], r.Code, i + 1, r.Lines[i]);
            }
            foreach (var t in workspace.Transactions)
                data.Tables["CashTransactions"].Rows.Add(t.Ma, t.ContractCode, t.LineNumber, t.Ngay, t.KhachHang, t.Nhom, t.NoiDung, t.Thu, t.Hoan);
            return data;
        }

        private static void AddLine(DataTable table, string code, int number, RentalLine line)
        { table.Rows.Add(code, number, line.ProductCode, line.ProductName, line.Category, line.Quantity, line.UnitPrice); }
        private static object[] Key(Mapping map, DataRow row) { return map.Keys.Select(k => row[k]).ToArray(); }
        private static string VersionKey(Mapping map, DataRow row) { return map.Table + ":" + Convert.ToString(row[map.Keys[0]]); }
        private static bool Same(Mapping map, DataRow left, DataRow right)
        { return left != null && right != null && map.Columns.All(c => Equals(left[c], right[c])); }

        private static void CheckVersions(StudioWorkspace displayed, DataSet before, DataSet after)
        {
            // A rental confirmed from an old picker must not silently use a changed price.
            var productMap = Maps.Single(x => x.Table == "Products");
            foreach (DataRow line in after.Tables["RentalLines"].Rows)
            {
                if (before.Tables["RentalLines"].Rows.Find(Key(Maps.Single(x => x.Table == "RentalLines"), line)) != null) continue;
                var product = before.Tables["Products"].Rows.Find(line["ProductCode"]);
                byte[] productVersion;
                if (product != null && displayed.Versions.TryGetValue(VersionKey(productMap, product), out productVersion)
                    && !productVersion.SequenceEqual((byte[])product["RowVersion"]))
                    throw new InvalidOperationException("Thông tin sản phẩm đã thay đổi. Mở lại Hợp đồng để kiểm tra giá và số lượng mới trước khi lưu.");
            }
            foreach (var map in Maps.Where(x => x.Versioned))
                foreach (DataRow old in before.Tables[map.Table].Rows)
                {
                    var next = after.Tables[map.Table].Rows.Find(Key(map, old));
                    byte[] version;
                    if (!Same(map, old, next) && displayed.Versions.TryGetValue(VersionKey(map, old), out version)
                        && !version.SequenceEqual((byte[])old["RowVersion"]))
                        throw new InvalidOperationException("Dữ liệu đã được người khác thay đổi. Mở lại chức năng để tải dữ liệu mới rồi thử lại.");
                }
        }

        private static void Save(SqlConnection connection, SqlTransaction transaction, DataSet before, DataSet after)
        {
            // Foreign-key order for deletes is the reverse of inserts.
            foreach (var map in Maps.Reverse())
                foreach (DataRow old in before.Tables[map.Table].Rows)
                    if (after.Tables[map.Table].Rows.Find(Key(map, old)) == null)
                        ExecuteChange(connection, transaction, map, old, null);
            foreach (var map in Maps)
                foreach (DataRow next in after.Tables[map.Table].Rows)
                {
                    var old = before.Tables[map.Table].Rows.Find(Key(map, next));
                    if (!Same(map, old, next)) ExecuteChange(connection, transaction, map, old, next);
                }
        }

        private static string Quote(string name) { return "[" + name + "]"; }
        private static void ExecuteChange(SqlConnection connection, SqlTransaction transaction, Mapping map, DataRow old, DataRow next)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                string table = "dbo." + Quote(map.Table);
                string where = string.Join(" AND ", map.Keys.Select(c => Quote(c) + "=@key_" + c));
                if (old == null)
                    command.CommandText = "INSERT " + table + " (" + string.Join(",", map.Columns.Select(Quote)) + ") VALUES (" + string.Join(",", map.Columns.Select(c => "@" + c)) + ")";
                else
                {
                    foreach (string key in map.Keys) Parameter(command, "@key_" + key, old.Table.Columns[key], old[key]);
                    if (map.Versioned)
                    {
                        where += " AND [RowVersion]=@version";
                        command.Parameters.Add("@version", SqlDbType.Binary, 8).Value = old["RowVersion"];
                    }
                    command.CommandText = next == null ? "DELETE FROM " + table + " WHERE " + where
                        : "UPDATE " + table + " SET " + string.Join(",", map.Columns.Where(c => !map.Keys.Contains(c)).Select(c => Quote(c) + "=@" + c)) + " WHERE " + where;
                }
                if (next != null) foreach (string column in map.Columns) Parameter(command, "@" + column, next.Table.Columns[column], next[column]);
                if (command.ExecuteNonQuery() != 1) throw new InvalidOperationException("Dữ liệu vừa thay đổi. Vui lòng mở lại chức năng rồi thử lại.");
            }
        }

        private static void Parameter(SqlCommand command, string name, DataColumn column, object value)
        {
            SqlDbType type = column.DataType == typeof(string) ? SqlDbType.NVarChar : column.DataType == typeof(int) ? SqlDbType.Int
                : column.DataType == typeof(decimal) ? SqlDbType.Decimal : column.DataType == typeof(bool) ? SqlDbType.Bit : SqlDbType.Date;
            var parameter = command.Parameters.Add(name, type);
            if (type == SqlDbType.NVarChar) parameter.Size = column.MaxLength > 0 ? column.MaxLength : -1;
            if (type == SqlDbType.Decimal) { parameter.Precision = 18; parameter.Scale = 0; }
            parameter.Value = value;
        }
    }
}
