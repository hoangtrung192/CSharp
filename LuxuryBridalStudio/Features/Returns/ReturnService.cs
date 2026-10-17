using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxuryBridalStudio
{
    public sealed class ReturnService
    {
        private readonly StudioWorkspace data;
        public ReturnService(StudioWorkspace workspace) { data = workspace ?? throw new ArgumentNullException("workspace"); }

        public List<RentalContract> List(bool completed)
        {
            AppSession.Require(Feature.Returns);
            return data.Contracts.Where(x => x.TrangThai == (completed ? RentalStatus.Returned : RentalStatus.Active))
                .Select(ContractService.Clone).ToList();
        }

        public void Complete(string code, DateTime date, decimal extra, string notes)
        {
            AppSession.Require(Feature.Returns, true);
            if (data.IsPersistent) { data.Commit(current => new ReturnService(current).Complete(code, date, extra, notes)); return; }
            lock (data.SyncRoot)
            {
                var rental = data.Contracts.SingleOrDefault(x => x.Ma == code);
                if (rental == null || rental.TrangThai != RentalStatus.Active) throw new InvalidOperationException("Hợp đồng không còn ở trạng thái đang thuê.");
                if (date.Date < rental.NgayThue || date.Date > DateTime.Today || extra < 0 || extra > 1000000000m || decimal.Truncate(extra) != extra || (extra > 0 && string.IsNullOrWhiteSpace(notes)))
                    throw new InvalidOperationException("Kiểm tra ngày trả, phụ phí và lý do.");
                if ((notes ?? "").Length > 1000) throw new InvalidOperationException("Ghi chú tối đa 1.000 ký tự.");
                decimal due = rental.TienThue + extra - rental.DaThu;
                if (due < 0) throw new InvalidOperationException("Cần xử lý hoàn tiền riêng.");
                rental.NgayTra = date.Date; rental.PhuPhi = extra; rental.ThuKhiTra = due;
                rental.DaThu += due; rental.GhiChu = (notes ?? "").Trim(); rental.TrangThai = RentalStatus.Returned;
                // Cash is recorded on collection day, even when a historical return date is entered.
                data.RecordPayment(rental, due, DateTime.Today, "Thu khi trả đồ, gồm phụ phí");
            }
        }
    }
}
