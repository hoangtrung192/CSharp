using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
public sealed class ReturnService
    {
        private readonly List<RentalContract> rentals;
        public ReturnService(List<RentalContract> data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Any(x => x == null || string.IsNullOrWhiteSpace(x.Ma)) || data.Select(x => x.Ma).Distinct().Count() != data.Count)
                throw new ArgumentException("Dữ liệu phải có mã không rỗng, không trùng và không chứa phần tử null.", "data");
            rentals = data;
        }


        public List<RentalContract> List(bool completed)
        {

            return rentals.Where(x=>x.TrangThai==(completed ? "Đã trả" : "Đang thuê")).Select(ContractService.Clone).ToList();
        }
        public void Complete(string code, DateTime date, decimal extra, string notes)
        {

            var r=rentals.SingleOrDefault(x=>x.Ma==code);
            if (r==null || r.TrangThai!="Đang thuê") throw new InvalidOperationException("Hợp đồng không còn ở trạng thái đang thuê.");
            if (date.Date<r.NgayThue || date.Date>DateTime.Today || extra<0 || (extra>0 && string.IsNullOrWhiteSpace(notes))) throw new InvalidOperationException("Kiểm tra ngày trả, phụ phí và lý do.");
            decimal due=r.TienThue+extra-r.DaThu;
            if (due<0) throw new InvalidOperationException("Cần xử lý hoàn tiền riêng.");
            r.NgayTra=date.Date; r.PhuPhi=extra; r.ThuKhiTra=due; r.DaThu+=due; r.GhiChu=(notes ?? "").Trim(); r.TrangThai="Đã trả";
        }
    }
}
