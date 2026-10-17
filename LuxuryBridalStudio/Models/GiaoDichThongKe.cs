using System;
using System.ComponentModel;

namespace LuxuryBridalStudio
{
    public sealed class GiaoDichThongKe
    {
        public string Ma { get; set; }
        [Browsable(false)]
        public string ContractCode { get; set; }
        [Browsable(false)]
        public int LineNumber { get; set; }
        public DateTime Ngay { get; set; }
        public string KhachHang { get; set; }
        public string Nhom { get; set; }
        public string NoiDung { get; set; }
        public decimal Thu { get; set; }
        public decimal Hoan { get; set; }
    }
}
