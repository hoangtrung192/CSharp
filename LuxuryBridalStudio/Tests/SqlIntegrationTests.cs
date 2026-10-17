using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuxuryBridalStudio;

internal static class SqlIntegrationTests
{
    private static string database, connectionString;
    private static int assertions;
    private static readonly DateTime Today = DateTime.Today;
    private const string Password = "Integration123!";
    private const string Master = "Data Source=.;Initial Catalog=master;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=10";

    [STAThread]
    private static int Main(string[] args)
    {
        database = "LuxuryBridalStudio_Test_" + Guid.NewGuid().ToString("N");
        bool created = false;
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Execute(Master, "CREATE DATABASE [" + database + "] COLLATE Latin1_General_100_CI_AS;");
            created = true;
            var builder = new SqlConnectionStringBuilder(Master) { InitialCatalog = database };
            connectionString = builder.ConnectionString;
            string root = args.Length == 0 ? Directory.GetCurrentDirectory() : Path.GetFullPath(args[0]);
            foreach (string name in new[] { "002_Schema.sql", "003_SeedDemoCatalog.sql" })
            {
                string script = File.ReadAllText(Path.Combine(root, "Database", name)).Replace("[LuxuryBridalStudio]", "[" + database + "]");
                foreach (string batch in Regex.Split(script, @"(?im)^\s*GO\s*\r?$"))
                    if (!string.IsNullOrWhiteSpace(batch)) Execute(connectionString, batch);
            }
            Accounts();
            Persistence();
            Atomicity();
            Concurrency();
            Forms();
            Execute(connectionString, "DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS;");
            Console.WriteLine("PASS: " + assertions + " SQL integration assertions.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally
        {
            AppSession.User = null;
            SqlConnection.ClearAllPools();
            // Only this run's newly created, GUID-named test database may be removed.
            if (created && Regex.IsMatch(database, @"^LuxuryBridalStudio_Test_[a-f0-9]{32}$"))
            {
                Execute(Master, "ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + database + "];");
                Console.WriteLine("Removed isolated test database.");
            }
        }
    }

    private static void Assert(bool condition, string message) { assertions++; if (!condition) throw new Exception(message); }
    private static void Reject(Action action)
    {
        try { action(); }
        catch (InvalidOperationException) { assertions++; return; }
        throw new Exception("Expected rejection.");
    }
    private static StudioWorkspace Load() { return new SqlStudioStore(connectionString).Load(); }
    private static RentalLine Line(string code, int quantity = 1, decimal price = 100000)
    { return new RentalLine { ProductCode = code, Quantity = quantity, UnitPrice = price }; }
    private static object Scalar(string sql)
    {
        using (var connection = Open(connectionString))
        using (var command = new SqlCommand(sql, connection)) return command.ExecuteScalar();
    }
    private static SqlConnection Open(string connectionString)
    {
        var connection = new SqlConnection(connectionString); connection.Open();
        using (var command = new SqlCommand(@"SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; SET ANSI_PADDING ON;
            SET ANSI_WARNINGS ON; SET CONCAT_NULL_YIELDS_NULL ON; SET ARITHABORT ON; SET NUMERIC_ROUNDABORT OFF;", connection)) command.ExecuteNonQuery();
        return connection;
    }
    private static void Execute(string connectionString, string sql)
    { using (var connection = Open(connectionString)) using (var command = new SqlCommand(sql, connection)) { command.CommandTimeout = 60; command.ExecuteNonQuery(); } }

    private static void Accounts()
    {
        var accounts = new SqlAccountService(connectionString);
        Assert(!accounts.HasAccounts, "Initial database must have no shared default login.");
        Reject(() => accounts.Register("staff", Password));
        accounts.CreateInitialManager("manager", Password);
        Reject(() => accounts.CreateInitialManager("othermanager", Password));
        Reject(() => accounts.Authenticate("manager", "incorrect"));
        Reject(() => accounts.Register("bad name", Password));
        accounts.Register("staff", " Password123 ");
        Reject(() => accounts.Register("STAFF", Password));
        Reject(() => accounts.Authenticate("staff", "Password123"));
        Assert(new SqlAccountService(connectionString).Authenticate(" STAFF ", " Password123 ").Role == Roles.Employee, "Employee account and untrimmed password must persist.");
        AppSession.User = accounts.Authenticate("MANAGER", Password);
        Assert(AppSession.User.UserName == "manager" && AppSession.User.Role == Roles.Manager, "SQL login must supply canonical name and stored role.");
        Assert((int)Scalar("SELECT COUNT(*) FROM dbo.Accounts WHERE DATALENGTH(PasswordHash)=32 AND DATALENGTH(PasswordSalt)=16 AND PasswordIterations>=100000") == 2, "Stored passwords must be salted hashes.");
        Console.WriteLine("PASS: SQL account provisioning, registration, password validation and restart login.");
    }

    private static void Persistence()
    {
        var data = Load(); AppSession.Data = data;
        Assert(data.IsPersistent && data.Customers.Rows.Count == 5, "SQL catalogs must load.");
        var table = data.Customers; var employees = data.Employees;
        var catalog = new CatalogService(data);
        string customer = catalog.SaveCustomer(null, "Lê O'Neil [100%]", "0909990001", "", "Địa chỉ thử nghiệm");
        Assert(ReferenceEquals(table, data.Customers), "SQL saves must preserve bound DataTables.");
        Assert((string)Load().Customers.Rows.Find(customer)["Họ và tên"] == "Lê O'Neil [100%]", "Unicode and apostrophes must survive SQL round-trip.");
        catalog.SaveCustomer(customer, "Khách đã sửa", "0909990001", "", "Địa chỉ mới");
        Assert((string)Load().Customers.Rows.Find(customer)["Địa chỉ"] == "Địa chỉ mới", "Catalog edits must persist.");
        string disposable = catalog.SaveCustomer(null, "Khách tạm", "0909990002", "", "");
        catalog.Delete(Feature.Customers, disposable);
        Assert(Load().Customers.Rows.Find(disposable) == null, "Catalog deletion must persist.");

        var employeeService = new EmployeeService(data.Employees);
        employeeService.Save(null, "Nhân sự thử", "0909990003", Roles.Warehouse, true);
        Assert(ReferenceEquals(employees, data.Employees), "SQL refresh must preserve employee lists.");
        string employee = data.Employees.Single().Ma;
        employeeService.Deactivate(employee);
        Assert(!Load().Employees.Single().DangLam, "Staff changes and deactivation must persist.");
        Assert(new SqlAccountService(connectionString).Authenticate("staff", " Password123 ").Role == Roles.Employee, "Editing staff profiles must not alter login permissions.");

        int stock = (int)data.Products.Rows.Find("SP001")["Tồn kho"];
        new ReceiptService(data).Save("NCC001", Today, new[] { Line("SP001", 1, 100000), Line("SP001", 1, 200000) }, "Phiếu thử");
        var restarted = Load();
        Assert(restarted.Receipts.Single().Lines.Count == 2 && restarted.Receipts.Single().Total == 300000, "Receipt price lines and total must persist.");
        Assert((int)restarted.Products.Rows.Find("SP001")["Tồn kho"] == stock + 2, "Stock increment must persist with the receipt.");

        string rental = new ContractService(data).Create(customer, new[] { Line("SP001"), Line("SP003") }, Today, Today.AddDays(2), 1000000);
        restarted = Load();
        Assert(restarted.Contracts.Single().Lines.Count == 2 && restarted.Contracts.Single().DaThu == 1000000, "Rental detail and deposit must survive restart.");
        Assert(restarted.Transactions.Sum(t => t.Thu) == 1000000 && restarted.Transactions.All(t => t.ContractCode == rental), "Payment allocations and rental references must persist.");
        Reject(() => new CatalogService(data).Delete(Feature.Customers, customer));
        new ReturnService(data).Complete(rental, Today, 100000, "Phụ phí thử");
        restarted = Load();
        Assert(restarted.Contracts.Single().TrangThai == RentalStatus.Returned && restarted.Contracts.Single().DaThu == 5100000, "Return settlement must survive restart.");
        Assert(restarted.Transactions.Sum(t => t.Thu - t.Hoan) == 5100000, "Return collections must match reports.");
        Assert((decimal)Scalar("SELECT SUM(ThucThu) FROM dbo.vw_ThongKe") == 5100000, "SQL reporting view must match app totals.");

        string cancelled = new ContractService(data).Create(customer, new[] { Line("SP002") }, Today, Today, 200000);
        new ContractService(data).Cancel(cancelled, "Khách hủy");
        restarted = Load();
        Assert(restarted.Contracts.Single(r => r.Ma == cancelled).TrangThai == RentalStatus.Cancelled, "Cancellation must persist.");
        Assert(restarted.Transactions.Where(t => t.ContractCode == cancelled).Sum(t => t.Thu - t.Hoan) == 0, "Cancellation must retain a separate refund movement.");

        AppSession.User = new SqlAccountService(connectionString).Authenticate("staff", " Password123 ");
        Reject(() => new ReceiptService(data).Save("NCC001", Today, new[] { Line("SP001") }, ""));
        AppSession.User = new SqlAccountService(connectionString).Authenticate("manager", Password);
        Console.WriteLine("PASS: catalog/staff/stock/rental/return/refund persistence and SQL report totals.");
    }

    private static void Atomicity()
    {
        var data = Load();
        int count = data.Contracts.Count, payments = data.Transactions.Count;
        Execute(connectionString, "CREATE TRIGGER dbo.IntegrationRejectCash ON dbo.CashTransactions AFTER INSERT AS THROW 53000, 'Simulated payment failure', 1;");
        try
        {
            Reject(() => new ContractService(data).Create("KH001", new[] { Line("SP001") }, Today, Today, 100000));
            Assert(data.Contracts.Count == count && data.Transactions.Count == payments, "Failed SQL save must leave the displayed workspace unchanged.");
            var restarted = Load();
            Assert(restarted.Contracts.Count == count && restarted.Transactions.Count == payments, "A failed payment must roll back its rental and lines.");
        }
        finally { Execute(connectionString, "DROP TRIGGER dbo.IntegrationRejectCash;"); }
        Execute(connectionString, "UPDATE dbo.Accounts SET IsActive=0 WHERE UserName='manager';");
        try { Reject(() => new CatalogService(data).SaveCustomer(null, "Blocked User", "0909990004", "", "")); }
        finally { Execute(connectionString, "UPDATE dbo.Accounts SET IsActive=1 WHERE UserName='manager';"); }
        Assert(Load().Customers.Rows.Count == data.Customers.Rows.Count, "A revoked account must not write.");
        Console.WriteLine("PASS: database failures roll back the entire operation; disabled logins cannot save.");
    }

    private static void Concurrency()
    {
        var first = Load(); var second = Load();
        new CatalogService(first).SaveCustomer("KH001", "Latest Customer", "0901234567", "079186001234", "Latest address");
        Reject(() => new CatalogService(second).SaveCustomer("KH001", "Stale Customer", "0901234567", "079186001234", "Old address"));
        Assert((string)Load().Customers.Rows.Find("KH001")["Họ và tên"] == "Latest Customer", "A stale editor must not overwrite a newer row.");
        second.Refresh();
        new CatalogService(second).SaveCustomer("KH001", "Refreshed Customer", "0901234567", "079186001234", "Latest address");

        first = Load(); second = Load();
        var taskA = Task.Run(() => new CatalogService(first).SaveCustomer(null, "Concurrent A", "0909990005", "", ""));
        var taskB = Task.Run(() => new CatalogService(second).SaveCustomer(null, "Concurrent B", "0909990006", "", ""));
        Task.WaitAll(taskA, taskB);
        Assert(taskA.Result != taskB.Result, "Concurrent sessions must allocate different codes.");

        first = Load(); second = Load();
        int success = 0, rejected = 0;
        Action<StudioWorkspace> reserve = workspace =>
        {
            try { new ContractService(workspace).Create("KH001", new[] { Line("SP002") }, Today.AddDays(5), Today.AddDays(6), 0); Interlocked.Increment(ref success); }
            catch (InvalidOperationException) { Interlocked.Increment(ref rejected); }
        };
        Task.WaitAll(Task.Run(() => reserve(first)), Task.Run(() => reserve(second)));
        Assert(success == 1 && rejected == 1, "Concurrent bookings must not over-rent the only available unit.");
        Assert(Load().Available("SP002", Today.AddDays(5), Today.AddDays(6)) == 0, "Concurrent booking must persist exactly once.");

        first = Load(); second = Load();
        new CatalogService(first).SaveProduct("SP001", "Updated Dress", "Váy cưới", "Trắng", "Việt Nam", 4, 3600000, 200000, "");
        Reject(() => new ContractService(second).Create("KH001", new[] { Line("SP001") }, Today, Today, 0));
        Console.WriteLine("PASS: stale edit protection, concurrent code allocation and stock reservation.");
    }

    private static T Field<T>(object owner, string name)
    { return (T)owner.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(owner); }
    private static void Prepare(Form form)
    {
        form.StartPosition = FormStartPosition.Manual; form.Location = new Point(-30000, -30000);
        form.ShowInTaskbar = false; form.Opacity = 0; form.Show(); Application.DoEvents();
    }
    private static void Forms()
    {
        AppSession.Data = Load();
        using (var main = new frmMain())
        {
            Prepare(main);
            Field<Button>(main, "btnKhachHang").PerformClick();
            var customers = Field<Form>(main, "activeForm");
            Field<Button>(customers, "btnThemKhach").PerformClick();
            Field<TextBox>(customers, "txtHoTen").Text = "Khách tạo từ giao diện";
            Field<TextBox>(customers, "txtDienThoai").Text = "0909990007";
            customers.GetType().GetMethod("Save", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(customers, null);
            Assert(Load().Customers.AsEnumerable().Any(r => (string)r["Số điện thoại"] == "0909990007"), "Customer form must persist through the SQL service.");
            Assert(Field<TextBox>(customers, "txtHoTen").Text == "Khách tạo từ giao diện", "Bound controls must select the saved row after SQL refresh.");
            Field<Button>(main, "btnBaoCao").PerformClick();
            var report = Field<Form>(main, "activeForm");
            decimal total = AppSession.Data.Transactions.Sum(t => t.Thu - t.Hoan);
            Assert(Field<Label>(report, "lblThucThu").Text == InputRules.Money(total), "Report screen must show stored SQL payments.");
            new ContractService(Load()).Create("KH001", new[] { Line("SP001") }, Today, Today, 50000);
            Field<Button>(report, "btnLamMoi").PerformClick();
            Assert(Field<Label>(report, "lblThucThu").Text == InputRules.Money(total + 50000), "Report refresh must fetch payments from another session.");
        }
        Type setupType = typeof(StudioWorkspace).Assembly.GetType("LuxuryBridalStudio.frmInitialSetup");
        using (var setup = (Form)Activator.CreateInstance(setupType, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { new SqlAccountService(connectionString) }, null))
        {
            Prepare(setup);
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "previews"); Directory.CreateDirectory(directory);
            using (var bitmap = new Bitmap(setup.Width, setup.Height))
            { setup.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height)); bitmap.Save(Path.Combine(directory, "initial-setup.png")); }
        }
        Console.WriteLine("PASS: SQL-backed customer form, live report refresh and initial setup form.");
    }
}
