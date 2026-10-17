using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxuryBridalStudio
{
    public sealed class ContractService
    {
        private readonly StudioWorkspace data;
        public ContractService(StudioWorkspace workspace) { data = workspace ?? throw new ArgumentNullException("workspace"); }

        public List<RentalContract> Search(string query)
        {
            AppSession.Require(Feature.Contracts);
            return data.Contracts.Where(x => (x.Ma + " " + x.KhachHang + " " + x.SanPham).IndexOf(query ?? "", StringComparison.CurrentCultureIgnoreCase) >= 0)
                .Select(Clone).ToList();
        }

        internal static RentalContract Clone(RentalContract x)
        {
            return new RentalContract { Ma = x.Ma, CustomerCode = x.CustomerCode, KhachHang = x.KhachHang, SanPham = x.SanPham,
                Lines = x.Lines.Select(l => l.Clone()).ToList(), NgayThue = x.NgayThue, HenTra = x.HenTra,
                TienThue = x.TienThue, DaThu = x.DaThu, TrangThai = x.TrangThai, NgayTra = x.NgayTra,
                PhuPhi = x.PhuPhi, ThuKhiTra = x.ThuKhiTra, GhiChu = x.GhiChu };
        }

        public string Create(string customerCode, IEnumerable<RentalLine> requested, DateTime start, DateTime end, decimal paid)
        {
            AppSession.Require(Feature.Contracts, true);
            if (data.IsPersistent) return data.Commit(current => new ContractService(current).Create(customerCode, requested, start, end, paid));
            if (start.Date < DateTime.Today || end.Date < start.Date) throw new InvalidOperationException("Ngày thuê phải từ hôm nay; ngày trả không trước ngày thuê.");
            if (requested == null) throw new InvalidOperationException("Thêm sản phẩm vào hợp đồng.");
            var input = requested.ToList();
            if (input.Count == 0 || input.Any(x => x == null || x.Quantity <= 0 || x.Quantity > 1000 || string.IsNullOrWhiteSpace(x.ProductCode)))
                throw new InvalidOperationException("Mỗi sản phẩm cần số lượng từ 1 đến 1.000.");

            lock (data.SyncRoot)
            {
                var customer = data.Customers.Rows.Find(customerCode ?? "");
                if (customer == null) throw new InvalidOperationException("Chọn khách hàng có trong danh mục.");
                var lines = new List<RentalLine>();
                foreach (var group in input.GroupBy(x => x.ProductCode))
                {
                    var product = data.Products.Rows.Find(group.Key);
                    int quantity = group.Sum(x => x.Quantity);
                    if (product == null) throw new InvalidOperationException("Sản phẩm không tồn tại: " + group.Key);
                    if (quantity > data.Available(group.Key, start, end)) throw new InvalidOperationException("Không đủ " + product["Tên trang phục"] + " trong khoảng thuê đã chọn.");
                    decimal price = (decimal)product["Giá thuê"];
                    if (price <= 0) throw new InvalidOperationException("Sản phẩm chưa có giá thuê hợp lệ.");
                    lines.Add(new RentalLine { ProductCode = group.Key, ProductName = (string)product["Tên trang phục"], Category = (string)product["Loại"], Quantity = quantity, UnitPrice = price });
                }
                decimal total = lines.Sum(x => x.Total);
                if (total > 1000000000m || paid < 0 || paid > total || decimal.Truncate(paid) != paid)
                    throw new InvalidOperationException("Tổng tiền tối đa 1 tỷ; thu trước phải là số nguyên từ 0 đến tiền thuê.");
                int sequence = data.Contracts.Select(x => { int n; return x.Ma != null && x.Ma.StartsWith("HD") && int.TryParse(x.Ma.Substring(2), out n) ? n : 0; }).DefaultIfEmpty().Max() + 1;
                var rental = new RentalContract { Ma = "HD" + sequence.ToString("D4"), CustomerCode = customerCode, KhachHang = (string)customer["Họ và tên"],
                    Lines = lines, SanPham = string.Join("; ", lines.Select(x => x.ProductName + " × " + x.Quantity)), NgayThue = start.Date, HenTra = end.Date,
                    TienThue = total, DaThu = paid, TrangThai = RentalStatus.Active };
                data.Contracts.Add(rental);
                data.RecordPayment(rental, paid, DateTime.Today, "Thu trước tiền thuê");
                return rental.Ma;
            }
        }

        public void Cancel(string code, string reason = "")
        {
            AppSession.Require(Feature.Contracts, true);
            if (data.IsPersistent) { data.Commit(current => new ContractService(current).Cancel(code, reason)); return; }
            lock (data.SyncRoot)
            {
                var rental = data.Contracts.SingleOrDefault(x => x.Ma == code);
                if (rental == null || rental.TrangThai != RentalStatus.Active)
                    throw new InvalidOperationException("Chỉ hủy hợp đồng đang thuê.");
                if (rental.DaThu > 0)
                {
                    if (AppSession.User.Role != Roles.Manager) throw new InvalidOperationException("Hợp đồng đã thu tiền cần quản lý xác nhận hoàn tiền.");
                    reason = InputRules.Required(reason, "Lý do hoàn tiền", 1000);
                    data.RecordPayment(rental, rental.DaThu, DateTime.Today, "Hoàn tiền khi hủy: " + reason, true);
                    rental.DaThu = 0;
                }
                rental.GhiChu = (reason ?? "").Trim();
                rental.TrangThai = RentalStatus.Cancelled;
            }
        }
    }
}
