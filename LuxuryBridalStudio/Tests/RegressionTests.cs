using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using LuxuryBridalStudio;

internal static class RegressionTests
{
    private static int assertions;
    private static readonly DateTime Today = DateTime.Today;

    [STAThread]
    private static int Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        try
        {
            Run("Authentication and role boundaries", Authentication);
            Run("Catalog validation, IDs and literal search", Catalog);
            Run("Receipt, multi-item rental, return and reporting", Lifecycle);
            Run("Availability across overlapping intervals", Availability);
            Run("Cancellation and refunds", Cancellation);
            Run("Invalid operations preserve state", Atomicity);
            Run("Independent views and form smoke checks", Forms);
            Console.WriteLine("PASS: " + assertions + " assertions across 7 suites.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { AppSession.User = null; }
    }

    private static void Run(string name, Action test) { Reset(); test(); Console.WriteLine("PASS: " + name); }
    private static void Assert(bool condition, string message) { assertions++; if (!condition) throw new Exception(message); }
    private static void Reject(Action action)
    {
        try { action(); }
        catch (InvalidOperationException) { assertions++; return; }
        throw new Exception("Expected operation to be rejected.");
    }
    private static void Reset()
    {
        AppSession.User = new UserSession("manager", "Manager", Roles.Manager);
        AppSession.Data = new StudioWorkspace(); AppSession.Data.SeedDemoCatalog();
    }
    private static RentalLine Line(string code, int quantity = 1, decimal cost = 100000m)
    {
        return new RentalLine { ProductCode = code, Quantity = quantity, UnitPrice = cost };
    }
    private static string Rent(string code, DateTime start, DateTime end, int quantity = 1, decimal paid = 0)
    {
        return new ContractService(AppSession.Data).Create("KH001", new[] { Line(code, quantity) }, start, end, paid);
    }

    private static void Authentication()
    {
        var accounts = new MemoryAccountService();
        accounts.Add("admin", "Admin@12345", Roles.Manager);
        accounts.Register("staff", " Password123 ");
        Reject(() => accounts.Authenticate("admin", "anything"));
        Reject(() => accounts.Authenticate("missing", "Admin@12345"));
        Reject(() => accounts.Authenticate("staff", "Password123"));
        Assert(accounts.Authenticate(" STAFF ", " Password123 ").Role == Roles.Employee, "Registration cannot grant manager role; passwords preserve spaces.");
        Reject(() => accounts.Register("STAFF", "Password123"));
        Reject(() => accounts.Register("bad name", "Password123"));
        Reject(() => accounts.Register("newuser", "short"));
        Assert(accounts.Authenticate("admin", "Admin@12345").Role == Roles.Manager, "Stored role must be used.");
        AppSession.User = new UserSession("staff", "Staff", Roles.Employee);
        Assert(AppSession.User.Can(Feature.Contracts, true) && !AppSession.User.Can(Feature.Products, true), "Employee product access must be read-only.");
        Reject(() => new ReceiptService(AppSession.Data).Save("NCC001", Today, new[] { Line("SP001") }, ""));
        Reject(() => new CatalogService(AppSession.Data).SaveProduct(null, "Test", "Vest", "Đen", "Việt Nam", 1, 1, 1, ""));
        Reject(() => new EmployeeService(AppSession.Data.Employees).Save(null, "Test", "0901234567", Roles.Manager, true));
        AppSession.User = new UserSession("stock", "Stock", Roles.Warehouse);
        Reject(() => Rent("SP001", Today, Today));
        Reject(() => new ReturnService(AppSession.Data).List(false));
        Assert(!AppSession.User.Can(Feature.Reports) && !AppSession.User.Can(Feature.Customers), "Warehouse cannot read financial reports or customer records.");
        AppSession.User = null;
        Reject(() => new ContractService(AppSession.Data).Search(""));
    }

    private static void Catalog()
    {
        var catalog = new CatalogService(AppSession.Data);
        catalog.Delete(Feature.Customers, "KH002");
        string code = catalog.SaveCustomer(null, "Người mới", "090-999-1234", "", "");
        Assert(code == "KH006", "Deleting a middle row must not duplicate the final ID.");
        Assert(AppSession.Data.Customers.Rows.Find(code)["Số điện thoại"].ToString() == "0909991234", "Normalize phone numbers.");
        Reject(() => catalog.SaveCustomer(null, "Trùng số", "0909991234", "", ""));
        Reject(() => catalog.SaveCustomer(null, "Sai CCCD", "0909991235", "123", ""));
        Reject(() => catalog.SaveProduct(null, "Test", "Vest", "Đen", "Việt Nam", 1, 0, 1, ""));
        string literal = "O'Brien [x] % *";
        code = catalog.SaveCustomer(null, literal, "0909991236", "", "");
        using (var view = new DataView(AppSession.Data.Customers))
            foreach (string token in new[] { "O'Brien", "[", "]", "%", "*", literal })
            {
                view.RowFilter = InputRules.SearchFilter(token, "Họ và tên", "Số điện thoại");
                Assert(view.Count == 1 && (string)view[0][0] == code, "Search must treat special characters literally: " + token);
            }
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            foreach (string culture in new[] { "en-US", "vi-VN" })
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
                foreach (string value in new[] { "3500000", "3.500.000 đ", "3,500,000" }) Assert(InputRules.ParseMoney(value) == 3500000m, "Money must not depend on OS culture.");
            }
        }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
        foreach (string value in new[] { "-1", "abc", "1.5", "1,23", "1.000,000", "1000000001" }) Reject(() => InputRules.ParseMoney(value));
    }

    private static void Lifecycle()
    {
        var data = AppSession.Data;
        int stock = (int)data.Products.Rows.Find("SP001")["Tồn kho"];
        var receipt = new ReceiptService(data);
        receipt.Save("NCC001", Today, new[] { Line("SP001", 2, 200000), Line("SP001", 1, 300000) }, "Test");
        Assert((int)data.Products.Rows.Find("SP001")["Tồn kho"] == stock + 3, "Receipt must increase stock.");
        Assert(data.Receipts.Single().Total == 700000m && (decimal)data.Products.Rows.Find("SP001")["Giá nhập"] == 300000m, "Different unit costs must be preserved.");
        var contracts = new ContractService(data);
        string code = contracts.Create("KH001", new[] { Line("SP001", 2), Line("SP003") }, Today, Today.AddDays(1), 1000000m);
        Assert(data.Contracts.Single().TienThue == 8500000m, "Rental must derive its price from real catalog entries.");
        Assert(data.Available("SP001", Today, Today) == stock + 1, "Rental must reserve each quantity.");
        var copy = contracts.Search(code).Single(); copy.Lines[0].Quantity = 100; copy.TienThue = 1;
        Assert(data.Contracts.Single().Lines[0].Quantity == 2 && data.Contracts.Single().TienThue == 8500000m, "Search results must be deep copies.");
        Reject(() => new CatalogService(data).Delete(Feature.Customers, "KH001"));
        Reject(() => new CatalogService(data).Delete(Feature.Products, "SP001"));
        Reject(() => new CatalogService(data).Delete(Feature.Suppliers, "NCC001"));
        var returns = new ReturnService(data);
        Assert(returns.List(false).Single().Ma == code, "Return service must see newly created contracts.");
        returns.Complete(code, Today, 100000m, "Giặt đặc biệt");
        var completed = returns.List(true).Single();
        Assert(completed.ThuKhiTra == 7600000m && completed.DaThu == 8600000m, "Return settlement must be correct.");
        Assert(data.Transactions.Sum(x => x.Thu - x.Hoan) == 8600000m && data.Transactions.All(x => x.Thu >= 0 && x.Hoan >= 0), "Report totals must equal actual receipts, including fees.");
        Assert(data.Available("SP001", Today, Today) == stock + 3, "Return must release reserved quantities.");
        int payments = data.Transactions.Count;
        Reject(() => returns.Complete(code, Today, 0, ""));
        Assert(data.Transactions.Count == payments, "Duplicate return must not duplicate revenue.");
    }

    private static void Availability()
    {
        var data = AppSession.Data;
        Rent("SP001", Today, Today.AddDays(1));
        Rent("SP001", Today.AddDays(3), Today.AddDays(4));
        Assert(data.Available("SP001", Today, Today.AddDays(4)) == 1, "Non-overlapping reservations must not be summed together.");
        Rent("SP001", Today, Today.AddDays(4));
        Reject(() => Rent("SP001", Today.AddDays(1), Today.AddDays(2)));
        Assert(data.Available("SP001", Today.AddDays(2), Today.AddDays(2)) == 1, "Capacity must recover between reservations.");
        Rent("SP001", Today.AddDays(5), Today.AddDays(5), 2);
        Assert(data.Available("SP001", Today.AddDays(5), Today.AddDays(5)) == 0, "Quantity capacity must be enforced.");
        data.Contracts[0].NgayThue = Today.AddDays(-3); data.Contracts[0].HenTra = Today.AddDays(-1);
        Assert(data.Available("SP001", Today.AddDays(10), Today.AddDays(10)) == 1, "Overdue items must block new bookings until returned.");
        Reject(() => new CatalogService(data).SaveProduct("SP001", "Váy cưới", "Váy cưới", "Trắng", "Việt Nam", 1, 3500000, 1000000, ""));
    }

    private static void Cancellation()
    {
        var data = AppSession.Data;
        string code = Rent("SP001", Today, Today, 1, 1000000);
        AppSession.User = new UserSession("staff", "Staff", Roles.Employee);
        Reject(() => new ContractService(data).Cancel(code, "Hoàn tiền"));
        Assert(data.Contracts[0].DaThu == 1000000m, "Rejected refund must not mutate payment.");
        AppSession.User = new UserSession("manager", "Manager", Roles.Manager);
        Reject(() => new ContractService(data).Cancel(code));
        new ContractService(data).Cancel(code, "Khách đổi lịch");
        Assert(data.Contracts[0].TrangThai == RentalStatus.Cancelled && data.Contracts[0].DaThu == 0, "Cancellation must settle refund.");
        Assert(data.Transactions.Sum(x => x.Thu - x.Hoan) == 0m, "Refund must offset reported cash.");
        Assert(data.Available("SP001", Today, Today) == 2, "Cancelled reservations must release stock.");
        Reject(() => new ContractService(data).Cancel(code, "Hoàn lần hai"));
        string unpaid = Rent("SP001", Today, Today);
        AppSession.User = new UserSession("staff", "Staff", Roles.Employee);
        new ContractService(data).Cancel(unpaid);
        Assert(data.Contracts[1].TrangThai == RentalStatus.Cancelled, "Employee can cancel unpaid contract.");
    }

    private static void Atomicity()
    {
        var data = AppSession.Data;
        Reject(() => new ReceiptService(data).Save("NCC001", Today, new[] { Line("SP001", 1), Line("missing", 1) }, ""));
        Assert(data.Receipts.Count == 0 && (int)data.Products.Rows.Find("SP001")["Tồn kho"] == 2, "Invalid receipt must not partially update stock.");
        Reject(() => new ReceiptService(data).Save("missing", Today, new[] { Line("SP001") }, ""));
        Reject(() => new ReceiptService(data).Save("NCC001", Today.AddDays(1), new[] { Line("SP001") }, ""));
        Reject(() => new ContractService(data).Create("KH001", new[] { Line("SP001"), Line("SP004") }, Today, Today, 100));
        Reject(() => Rent("SP001", Today.AddDays(-1), Today));
        Reject(() => Rent("SP001", Today, Today, 1, 3500001));
        Assert(data.Contracts.Count == 0 && data.Transactions.Count == 0, "Invalid contract must not reserve stock or record cash.");
        string code = Rent("SP001", Today, Today, 1, 1000);
        Reject(() => new ReturnService(data).Complete(code, Today, 1, ""));
        Reject(() => new ReturnService(data).Complete(code, Today.AddDays(1), 0, ""));
        Assert(data.Contracts.Single().TrangThai == RentalStatus.Active && data.Transactions.Sum(x => x.Thu) == 1000, "Invalid return must leave contract and cash unchanged.");
    }

    private static T Field<T>(object form, string name)
    {
        return (T)form.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(form);
    }
    private static void Prepare(Form form)
    {
        form.BindingContext = new BindingContext();
        // Exercise actual WinForms lifecycle without showing windows on the user's desktop.
        form.ShowInTaskbar = false;
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(-30000, -30000);
        form.Opacity = 0;
        form.Show();
        Application.DoEvents();
        form.PerformLayout();
    }
    private static void Call(object target, string method)
    {
        target.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(target, null);
    }
    private static void SelectProduct(Form form, string code)
    {
        var grid = Field<DataGridView>(form, "dgvSanPham");
        foreach (DataGridViewRow row in grid.Rows)
        {
            if ((string)((DataRowView)row.DataBoundItem)[0] != code) continue;
            grid.CurrentCell = row.Cells[0]; row.Selected = true;
            Call(form, "SelectRow"); return;
        }
        throw new Exception("Product row missing.");
    }
    private static void Capture(Form form, string name)
    {
        string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "previews");
        Directory.CreateDirectory(directory);
        using (var bitmap = new Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bitmap.Save(Path.Combine(directory, name + ".png"));
        }
    }
    private static void Forms()
    {
        AppSession.Accounts = new MemoryAccountService();
        using (var login = new frmDangNhap()) { Prepare(login); Capture(login, "login"); Assert(!Field<ComboBox>(login, "cboVaiTro").Enabled, "User must not select login role."); }
        using (var main = new frmMain())
        {
            Prepare(main); Capture(main, "overview");
            Assert(Field<Label>(main, "lblDoanhThu").Text == InputRules.Money(0), "Overview must not show invented revenue.");
            Field<Button>(main, "btnHopDong").PerformClick();
            Assert(Field<Form>(main, "activeForm") is frmHopDong, "Navigation must open contracts.");
            string navigationCode = Rent("SP001", Today, Today, 1, 1000);
            Field<Button>(main, "btnTraDo").PerformClick();
            Assert(Field<Form>(main, "activeForm") is frmTraDo, "Navigation must open returns after closing contracts.");
            var picker = Field<ComboBox>(Field<Form>(main, "activeForm"), "contracts");
            Assert(((RentalContract)picker.Items[0]).Ma == navigationCode, "Return UI must see contracts created before navigation.");
            new ReturnService(AppSession.Data).Complete(navigationCode, Today, 0, "");
            Field<Button>(main, "btnBaoCao").PerformClick();
            var report = Field<Form>(main, "activeForm");
            Assert(Field<Label>(report, "lblThucThu").Text == InputRules.Money(3500000m), "Report UI must reflect actual collected money.");
            Field<Button>(main, "btnTongQuan").PerformClick();
            Assert(Field<Form>(main, "activeForm") == null && Field<Label>(main, "lblDoanhThu").Text == InputRules.Money(3500000m), "Returning to overview must refresh totals.");
        }
        Reset();
        var forms = new Form[] { new frmKhachHang(), new frmNhaCungCap(), new frmSanPham(), new frmHopDong(), new frmTraDo(), new frmNhanSu(), new frmHoaDonNhap(), new frmThongKe() };
        try
        {
            foreach (var form in forms) { Prepare(form); Capture(form, form.GetType().Name); assertions++; }
            using (var other = new frmKhachHang())
            {
                Prepare(other);
                var firstView = Field<DataView>(forms[0], "view");
                var secondView = Field<DataView>(other, "view");
                firstView.RowFilter = "[Mã KH] = 'KH001'";
                Assert(secondView.Count == 5 && object.ReferenceEquals(firstView.Table, secondView.Table), "Forms share data but must have independent filters.");
            }
            Assert(Field<DataGridView>(forms[6], "dgvChiTietNhap").Rows.Count == 0, "New receipt must start empty.");
            Assert(AppSession.Data.Transactions.Count == 0, "Opening reports must not generate fake transactions.");
            var rentalForm = forms[3];
            Field<ComboBox>(rentalForm, "product").SelectedValue = "SP001";
            Call(rentalForm, "AddItem");
            Assert(Field<NumericUpDown>(rentalForm, "price").Value == 3500000m, "Rental cart must calculate catalog price in the UI.");
            Call(rentalForm, "ResetDraft");
            var receiptForm = forms[6];
            Field<ComboBox>(receiptForm, "cboSanPham").SelectedValue = "SP001";
            Field<TextBox>(receiptForm, "txtDonGiaNhap").Text = "100.000";
            Call(receiptForm, "AddLine");
            Field<TextBox>(receiptForm, "txtDonGiaNhap").Text = "200.000";
            Call(receiptForm, "AddLine");
            var details = Field<DataTable>(receiptForm, "details");
            Assert(details.Rows.Count == 2 && details.AsEnumerable().Sum(x => (decimal)x["Thành tiền"]) == 300000m, "Receipt cart must preserve different unit prices.");
            Call(receiptForm, "ResetDraft");

            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "product-test.png");
            using (var bitmap = new Bitmap(10, 10)) bitmap.Save(imagePath);
            AppSession.Data.Products.Rows.Find("SP001")["HinhAnh"] = imagePath;
            var productForm = forms[2];
            SelectProduct(productForm, "SP001");
            using (var unlocked = new FileStream(imagePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                Assert(unlocked.Length > 0, "Loading an image must not lock the source file.");
            Assert(Convert.ToString(Field<PictureBox>(productForm, "picAnhSP").Tag) == imagePath, "Selected product image must retain its path.");
            SelectProduct(productForm, "SP002");
            Assert(Convert.ToString(Field<PictureBox>(productForm, "picAnhSP").Tag) == "", "Selecting another product must clear stale image paths.");
            Call(productForm, "Save");
            Assert((string)AppSession.Data.Products.Rows.Find("SP002")["HinhAnh"] == "", "Saving another product must not copy the previous image.");
            var catalog = new CatalogService(AppSession.Data);
            string code = catalog.SaveCustomer(null, "Shared Customer", "0909998888", "", "");
            Assert(Field<DataView>(forms[0], "view").Table.Rows.Find(code) != null, "Changes remain available to existing forms.");
        }
        finally { foreach (var form in forms) form.Dispose(); }
        AppSession.User = new UserSession("staff", "Staff", Roles.Employee);
        using (var product = new frmSanPham())
        {
            Prepare(product);
            Assert(!Field<Button>(product, "btnLuu").Enabled && !Field<Button>(product, "btnXoa").Enabled, "Employee UI must not offer catalog writes.");
            Assert(!Field<TextBox>(product, "txtGiaNhap").Visible, "Employee UI must hide purchase prices.");
        }
    }
}
