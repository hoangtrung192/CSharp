using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LuxuryBridalStudio
{
    // Bound forms share these collections; SQL refresh preserves their object identities.
    public sealed class StudioWorkspace
    {
        public DataTable Customers { get; private set; }
        public DataTable Products { get; private set; }
        public DataTable Suppliers { get; private set; }
        public List<RentalContract> Contracts { get; private set; } = new List<RentalContract>();
        public List<StaffMember> Employees { get; private set; } = new List<StaffMember>();
        public List<StockReceipt> Receipts { get; private set; } = new List<StockReceipt>();
        public List<GiaoDichThongKe> Transactions { get; private set; } = new List<GiaoDichThongKe>();
        public object SyncRoot { get; private set; } = new object();
        internal SqlStudioStore Store { get; set; }
        internal Dictionary<string, byte[]> Versions { get; private set; } = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
        public bool IsPersistent { get { return Store != null; } }

        public void Refresh() { if (Store != null) Store.Refresh(this); }
        internal T Commit<T>(Func<StudioWorkspace, T> operation) { return Store.Write(this, operation); }
        internal void Commit(Action<StudioWorkspace> operation)
        { Store.Write(this, current => { operation(current); return true; }); }

        internal void ReplaceFrom(StudioWorkspace fresh)
        {
            CopyTable(Customers, fresh.Customers); CopyTable(Suppliers, fresh.Suppliers); CopyTable(Products, fresh.Products);
            Contracts.Clear(); Contracts.AddRange(fresh.Contracts);
            Employees.Clear(); Employees.AddRange(fresh.Employees);
            Receipts.Clear(); Receipts.AddRange(fresh.Receipts);
            Transactions.Clear(); Transactions.AddRange(fresh.Transactions);
            Versions.Clear(); foreach (var item in fresh.Versions) Versions.Add(item.Key, item.Value);
        }

        private static void CopyTable(DataTable target, DataTable source)
        {
            target.BeginLoadData();
            try { target.Clear(); foreach (DataRow row in source.Rows) target.ImportRow(row); }
            finally { target.EndLoadData(); }
        }

        public StudioWorkspace()
        {
            Customers = Table("Mã KH", "Họ và tên", "Số điện thoại", "Số CCCD", "Địa chỉ");
            Suppliers = Table("Mã NCC", "Tên nhà cung cấp", "Số điện thoại", "Địa chỉ", "Ghi chú");
            Products = Table("Mã SP", "Tên trang phục", "Loại", "Màu sắc", "Nơi SX");
            Products.Columns.Add("Tồn kho", typeof(int));
            Products.Columns.Add("Giá thuê", typeof(decimal));
            Products.Columns.Add("Giá nhập", typeof(decimal));
            Products.Columns.Add("HinhAnh");
        }

        private static DataTable Table(params string[] names)
        {
            var table = new DataTable();
            foreach (string name in names) table.Columns.Add(name);
            table.PrimaryKey = new[] { table.Columns[0] };
            return table;
        }

        public void SeedDemoCatalog()
        {
            if (Customers.Rows.Count != 0 || Products.Rows.Count != 0 || Suppliers.Rows.Count != 0) return;
            Customers.Rows.Add("KH001", "Nguyễn Thu Hà", "0901234567", "079186001234", "123 Lê Lợi, TP. HCM");
            Customers.Rows.Add("KH002", "Trần Thị Mai Anh", "0987654321", "001206012345", "45 Cầu Giấy, Hà Nội");
            Customers.Rows.Add("KH003", "Hoàng Thùy Linh", "0963111222", "038195007890", "78 Trần Phú, Hà Nội");
            Customers.Rows.Add("KH004", "Lê Hoàng Yến", "0912345678", "025198009876", "12 Kim Mã, Hà Nội");
            Customers.Rows.Add("KH005", "Phạm Minh Đức", "0935678901", "001099003456", "89 Nguyễn Trãi, Hà Nội");
            Products.Rows.Add("SP001", "Váy cưới công chúa đính đá", "Váy cưới", "Trắng", "Việt Nam", 2, 3500000m, 10000000m, "");
            Products.Rows.Add("SP002", "Váy cưới cúp ngực Satin", "Váy cưới", "Trắng kem", "Hàn Quốc", 1, 2800000m, 8000000m, "");
            Products.Rows.Add("SP003", "Vest chú rể classic đen", "Vest", "Đen", "Việt Nam", 4, 1500000m, 3000000m, "");
            Products.Rows.Add("SP004", "Áo dài cưới Long Phụng", "Áo dài", "Đỏ thẫm", "Việt Nam", 0, 2000000m, 4000000m, "");
            Products.Rows.Add("SP005", "Váy cưới đuôi cá phối ren", "Váy cưới", "Trắng", "Pháp", 3, 4200000m, 12000000m, "");
            Suppliers.Rows.Add("NCC001", "Xưởng May Bella Bridal", "02438225678", "102 Bà Triệu, Hà Nội", "Váy cưới ren cao cấp");
            Suppliers.Rows.Add("NCC002", "Thời Trang HERA", "02839301234", "45 Nam Kỳ Khởi Nghĩa, TP. HCM", "Váy cưới satin");
            Suppliers.Rows.Add("NCC003", "Xưởng Vest Hoàng Gia", "0918222333", "88 Nguyễn Đình Chiểu, TP. HCM", "Vest chú rể");
            Suppliers.Rows.Add("NCC004", "Áo Dài Truyền Thống", "0905456789", "12 Lê Lợi, Huế", "Áo dài gấm");
            Suppliers.Rows.Add("NCC005", "Phụ Kiện Diamond Studio", "0977888999", "25 Kim Mã, Hà Nội", "Voan, vương miện");
        }

        // Capacity is the maximum concurrent quantity, not the sum of all reservations in a range.
        // Both end dates are inclusive. Overdue unreturned items block availability until returned.
        public int Available(string productCode, DateTime start, DateTime end)
        {
            var product = Products.Rows.Find(productCode);
            if (product == null) throw new InvalidOperationException("Sản phẩm không tồn tại.");
            if (end.Date < start.Date) throw new InvalidOperationException("Khoảng ngày không hợp lệ.");
            var events = new SortedDictionary<DateTime, int>();
            foreach (var rental in Contracts.Where(x => x.TrangThai == RentalStatus.Active))
            {
                int quantity = rental.Lines.Where(x => x.ProductCode == productCode).Sum(x => x.Quantity);
                if (quantity == 0) continue;
                DateTime until = rental.HenTra.Date < DateTime.Today ? DateTime.MaxValue.Date : rental.HenTra.Date;
                DateTime from = rental.NgayThue.Date > start.Date ? rental.NgayThue.Date : start.Date;
                DateTime to = until < end.Date ? until : end.Date;
                if (from > to) continue;
                Change(events, from, quantity);
                if (to < DateTime.MaxValue.Date) Change(events, to.AddDays(1), -quantity);
            }
            int used = 0, peak = 0;
            foreach (var e in events) { used += e.Value; peak = Math.Max(peak, used); }
            return Math.Max(0, (int)product["Tồn kho"] - peak);
        }

        private static void Change(IDictionary<DateTime, int> events, DateTime date, int quantity)
        {
            int old; events.TryGetValue(date, out old); events[date] = old + quantity;
        }

        internal void RecordPayment(RentalContract rental, decimal amount, DateTime date, string description, bool refund = false)
        {
            if (amount == 0) return;
            decimal remaining = amount;
            for (int i = 0; i < rental.Lines.Count; i++)
            {
                var line = rental.Lines[i];
                decimal part = i == rental.Lines.Count - 1 ? remaining : decimal.Floor(amount * line.Total / rental.TienThue);
                remaining -= part;
                Transactions.Add(new GiaoDichThongKe { Ma = "GD" + (Transactions.Count + 1).ToString("D6"), ContractCode = rental.Ma, LineNumber = i + 1, Ngay = date.Date,
                    KhachHang = rental.KhachHang, Nhom = line.Category, NoiDung = description + " · " + rental.Ma, Thu = refund ? 0 : part, Hoan = refund ? part : 0 });
            }
        }
    }

    public static class RentalStatus
    {
        public const string Active = "Đang thuê";
        public const string Returned = "Đã trả";
        public const string Cancelled = "Đã hủy";
    }
}
