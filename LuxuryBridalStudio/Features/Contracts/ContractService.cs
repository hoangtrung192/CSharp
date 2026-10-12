using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
public sealed class ContractService
    {
        private readonly List<RentalContract> rentals;
        public ContractService(List<RentalContract> data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Any(x => x == null || string.IsNullOrWhiteSpace(x.Ma)) || data.Select(x => x.Ma).Distinct().Count() != data.Count)
                throw new ArgumentException("Dữ liệu phải có mã không rỗng, không trùng và không chứa phần tử null.", "data");
            rentals = data;
        }

        private string NextCode()
        {
            int number = 1;
            while (rentals.Any(x => x.Ma == "HD" + number.ToString("0000"))) number++;
            return "HD" + number.ToString("0000");
        }


        public List<RentalContract> Search(string query)
        {

            return rentals.Where(x => (x.Ma + " " + x.KhachHang).IndexOf(query ?? "", StringComparison.CurrentCultureIgnoreCase) >= 0).Select(Clone).ToList();
        }
        internal static RentalContract Clone(RentalContract x)
        {
            return new RentalContract { Ma=x.Ma, KhachHang=x.KhachHang, SanPham=x.SanPham, NgayThue=x.NgayThue, HenTra=x.HenTra, TienThue=x.TienThue, DaThu=x.DaThu, TrangThai=x.TrangThai, NgayTra=x.NgayTra, PhuPhi=x.PhuPhi, ThuKhiTra=x.ThuKhiTra, GhiChu=x.GhiChu };
        }
        public void Create(string customer, string product, DateTime start, DateTime end, decimal price, decimal paid)
        {

            if (string.IsNullOrWhiteSpace(customer) || string.IsNullOrWhiteSpace(product)) throw new InvalidOperationException("Nhập khách hàng và sản phẩm.");
            if (end.Date < start.Date || price <= 0 || paid < 0 || paid > price) throw new InvalidOperationException("Ngày hoặc số tiền không hợp lệ.");
            rentals.Add(new RentalContract { Ma=NextCode(), KhachHang=customer.Trim(), SanPham=product.Trim(), NgayThue=start.Date, HenTra=end.Date, TienThue=price, DaThu=paid, TrangThai="Đang thuê" });
        }
        public void Cancel(string code)
        {

            var r=rentals.SingleOrDefault(x=>x.Ma==code);
            if (r==null || r.TrangThai!="Đang thuê" || r.DaThu!=0) throw new InvalidOperationException("Chỉ hủy hợp đồng đang thuê và chưa thu tiền.");
            r.TrangThai="Đã hủy";
        }
    }
}
