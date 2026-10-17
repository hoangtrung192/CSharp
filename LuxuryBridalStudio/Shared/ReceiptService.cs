using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxuryBridalStudio
{
    public sealed class ReceiptService
    {
        private readonly StudioWorkspace data;
        public ReceiptService(StudioWorkspace workspace) { data = workspace ?? throw new ArgumentNullException("workspace"); }

        public string Save(string supplierCode, DateTime date, IEnumerable<RentalLine> requested, string notes)
        {
            AppSession.Require(Feature.Receipts, true);
            if (data.IsPersistent) return data.Commit(current => new ReceiptService(current).Save(supplierCode, date, requested, notes));
            if (date.Date > DateTime.Today) throw new InvalidOperationException("Ngày nhập không được sau hôm nay.");
            if ((notes ?? "").Length > 1000) throw new InvalidOperationException("Ghi chú tối đa 1.000 ký tự.");
            var input = requested == null ? new List<RentalLine>() : requested.ToList();
            if (input.Count == 0 || input.Any(x => x == null || x.Quantity <= 0 || x.Quantity > 1000000 || x.UnitPrice <= 0 || x.UnitPrice > 1000000000m || decimal.Truncate(x.UnitPrice) != x.UnitPrice))
                throw new InvalidOperationException("Phiếu cần sản phẩm, số lượng và đơn giá nguyên dương hợp lệ.");
            lock (data.SyncRoot)
            {
                if (data.Suppliers.Rows.Find(supplierCode ?? "") == null) throw new InvalidOperationException("Chọn nhà cung cấp có trong danh mục.");
                var lines = new List<RentalLine>();
                foreach (var item in input)
                {
                    var product = data.Products.Rows.Find(item.ProductCode ?? "");
                    if (product == null) throw new InvalidOperationException("Sản phẩm không còn tồn tại: " + item.ProductCode);
                    lines.Add(new RentalLine { ProductCode = item.ProductCode, ProductName = (string)product["Tên trang phục"], Category = (string)product["Loại"], Quantity = item.Quantity, UnitPrice = item.UnitPrice });
                }
                decimal total = lines.Sum(x => x.Total);
                if (total > 1000000000m) throw new InvalidOperationException("Tổng phiếu nhập tối đa 1 tỷ đồng.");
                foreach (var group in lines.GroupBy(x => x.ProductCode))
                    if ((int)data.Products.Rows.Find(group.Key)["Tồn kho"] + group.Sum(x => (long)x.Quantity) > 1000000)
                        throw new InvalidOperationException("Tổng số bộ tối đa 1 triệu cho một sản phẩm.");
                string code = "HDN" + (data.Receipts.Count + 1).ToString("D4");
                // All validation finishes before any stock mutation.
                foreach (var group in lines.GroupBy(x => x.ProductCode))
                {
                    var product = data.Products.Rows.Find(group.Key);
                    product["Tồn kho"] = (int)product["Tồn kho"] + group.Sum(x => x.Quantity);
                    product["Giá nhập"] = group.Last().UnitPrice;
                }
                data.Receipts.Add(new StockReceipt { Code = code, SupplierCode = supplierCode, Employee = AppSession.User.UserName,
                    Date = date.Date, Notes = (notes ?? "").Trim(), Lines = lines, Total = total });
                return code;
            }
        }
    }
}
