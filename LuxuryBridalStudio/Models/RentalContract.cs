using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public sealed class RentalContract
    {
        public string Ma { get; set; }
        public string KhachHang { get; set; }
        public string SanPham { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime HenTra { get; set; }
        public decimal TienThue { get; set; }
        public decimal DaThu { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayTra { get; set; }
        public decimal PhuPhi { get; set; }
        public decimal ThuKhiTra { get; set; }
        public string GhiChu { get; set; }
    }
}
