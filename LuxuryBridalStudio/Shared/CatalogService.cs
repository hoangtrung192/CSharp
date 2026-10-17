using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace LuxuryBridalStudio
{
    public sealed class CatalogService
    {
        private readonly StudioWorkspace data;
        public CatalogService(StudioWorkspace workspace) { data = workspace ?? throw new ArgumentNullException("workspace"); }

        public string SaveCustomer(string code, string name, string phone, string identity, string address)
        {
            AppSession.Require(Feature.Customers, true);
            if (data.IsPersistent) return data.Commit(current => new CatalogService(current).SaveCustomer(code, name, phone, identity, address));
            name = InputRules.Required(name, "Họ tên"); phone = InputRules.Phone(phone); identity = (identity ?? "").Trim();
            if (identity.Length > 0 && !Regex.IsMatch(identity, @"^[0-9]{12}$")) throw new InvalidOperationException("CCCD cần đúng 12 chữ số hoặc để trống.");
            if ((address ?? "").Length > 300) throw new InvalidOperationException("Địa chỉ tối đa 300 ký tự.");
            lock (data.SyncRoot)
            {
                if (data.Customers.AsEnumerable().Any(x => (string)x[0] != code && ((string)x["Số điện thoại"] == phone || identity.Length > 0 && (string)x["Số CCCD"] == identity)))
                    throw new InvalidOperationException("Số điện thoại hoặc CCCD đã được sử dụng.");
                return Save(data.Customers, code, "KH", name, phone, identity, (address ?? "").Trim());
            }
        }

        public string SaveSupplier(string code, string name, string phone, string address, string notes)
        {
            AppSession.Require(Feature.Suppliers, true);
            if (data.IsPersistent) return data.Commit(current => new CatalogService(current).SaveSupplier(code, name, phone, address, notes));
            name = InputRules.Required(name, "Tên nhà cung cấp"); phone = InputRules.Phone(phone);
            if ((address ?? "").Length > 300 || (notes ?? "").Length > 1000) throw new InvalidOperationException("Địa chỉ tối đa 300, ghi chú tối đa 1.000 ký tự.");
            lock (data.SyncRoot)
            {
                if (data.Suppliers.AsEnumerable().Any(x => (string)x[0] != code && (string)x["Số điện thoại"] == phone)) throw new InvalidOperationException("Số điện thoại đã được sử dụng.");
                return Save(data.Suppliers, code, "NCC", name, phone, (address ?? "").Trim(), (notes ?? "").Trim());
            }
        }

        public string SaveProduct(string code, string name, string category, string color, string origin, int stock, decimal rent, decimal cost, string image)
        {
            AppSession.Require(Feature.Products, true);
            if (data.IsPersistent) return data.Commit(current => new CatalogService(current).SaveProduct(code, name, category, color, origin, stock, rent, cost, image));
            name = InputRules.Required(name, "Tên sản phẩm");
            if (!new[] { "Váy cưới", "Vest", "Áo dài", "Phụ kiện" }.Contains(category)) throw new InvalidOperationException("Chọn loại sản phẩm hợp lệ.");
            color = InputRules.Required(color, "Màu sắc", 50); origin = InputRules.Required(origin, "Nơi sản xuất", 50);
            if (stock < 0 || stock > 1000000 || rent <= 0 || rent > 1000000000m || cost < 0 || cost > 1000000000m || decimal.Truncate(rent) != rent || decimal.Truncate(cost) != cost)
                throw new InvalidOperationException("Số lượng phải từ 0 đến 1 triệu; giá thuê phải lớn hơn 0, giá nhập không âm, tiền là số nguyên tối đa 1 tỷ.");
            lock (data.SyncRoot)
            {
                var row = code == null ? null : data.Products.Rows.Find(code);
                if (row != null && stock < (int)row["Tồn kho"] - data.Available(code, DateTime.Today, DateTime.MaxValue.Date))
                    throw new InvalidOperationException("Không thể giảm tổng số bộ xuống dưới số lượng đã cho thuê hoặc đặt lịch.");
                return Save(data.Products, code, "SP", name, category, color, origin, stock, rent, cost, image ?? "");
            }
        }

        private static string Save(DataTable table, string code, string prefix, params object[] values)
        {
            bool add = code == null;
            var row = add ? table.NewRow() : table.Rows.Find(code);
            if (row == null) throw new InvalidOperationException("Bản ghi không còn tồn tại. Hãy tải lại danh sách.");
            code = add ? InputRules.NextCode(table, prefix) : code;
            row.ItemArray = new object[] { code }.Concat(values).ToArray();
            if (add) table.Rows.Add(row);
            return code;
        }

        public void Delete(Feature feature, string code)
        {
            if (feature != Feature.Customers && feature != Feature.Products && feature != Feature.Suppliers) throw new ArgumentException("Danh mục không hợp lệ.");
            AppSession.Require(feature, true);
            if (data.IsPersistent) { data.Commit(current => new CatalogService(current).Delete(feature, code)); return; }
            lock (data.SyncRoot)
            {
                DataTable table = feature == Feature.Customers ? data.Customers : feature == Feature.Products ? data.Products : data.Suppliers;
                if (string.IsNullOrWhiteSpace(code) || table.Rows.Find(code) == null) throw new InvalidOperationException("Chọn bản ghi cần xóa.");
                bool referenced = feature == Feature.Customers ? data.Contracts.Any(x => x.CustomerCode == code)
                    : feature == Feature.Products ? data.Contracts.Any(x => x.Lines.Any(l => l.ProductCode == code)) || data.Receipts.Any(x => x.Lines.Any(l => l.ProductCode == code))
                    : data.Receipts.Any(x => x.SupplierCode == code);
                if (referenced) throw new InvalidOperationException("Bản ghi đã phát sinh giao dịch, không thể xóa để bảo toàn lịch sử.");
                table.Rows.Remove(table.Rows.Find(code));
            }
        }
    }
}
