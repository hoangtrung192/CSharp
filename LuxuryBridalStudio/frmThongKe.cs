using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace LuxuryBridalStudio
{
    public partial class frmThongKe : Form
    {
        private readonly List<GiaoDichThongKe> duLieu;
        private readonly bool laDuLieuMau;
        private readonly CultureInfo vi = CultureInfo.GetCultureInfo("vi-VN");
        private DataTable bangHienThi;

        // Constructor không tham số dành cho Designer và chạy demo.
        public frmThongKe() : this(TaoDuLieuMau(), true) { }

        // Khi có DB: truyền danh sách giao dịch thực vào constructor này.
        public frmThongKe(IEnumerable<GiaoDichThongKe> giaoDich) : this(giaoDich, false) { }

        private frmThongKe(IEnumerable<GiaoDichThongKe> giaoDich, bool duLieuMau)
        {
            if (giaoDich == null) throw new ArgumentNullException("giaoDich");
            duLieu = giaoDich.ToList();
            if (duLieu.Any(x => x == null || x.Thu < 0 || x.Hoan < 0))
                throw new ArgumentException("Giao dịch không được null; số tiền thu và hoàn phải không âm.");
            laDuLieuMau = duLieuMau;
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            lblNguon.Text = laDuLieuMau ? "DỮ LIỆU MẪU • Chưa kết nối database" : "Dữ liệu được cung cấp từ ứng dụng";
            cboNhom.SelectedIndex = 0;
            cboGom.SelectedIndex = 0;
            VeThangNay();
        }

        private void VeThangNay()
        {
            dtpTuNgay.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDenNgay.Value = DateTime.Today;
            ApDung();
        }

        private void btnLoc_Click(object sender, EventArgs e) { ApDung(); }
        private void btnThangNay_Click(object sender, EventArgs e) { VeThangNay(); }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboNhom.SelectedIndex = 0;
            cboGom.SelectedIndex = 0;
            VeThangNay();
        }

        private void ApDung()
        {
            DateTime tu = dtpTuNgay.Value.Date, den = dtpDenNgay.Value.Date;
            if (tu > den)
            {
                MessageBox.Show(this, "Từ ngày phải nhỏ hơn hoặc bằng đến ngày.", "Kiểm tra bộ lọc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var loc = duLieu.Where(x => x.Ngay.Date >= tu && x.Ngay.Date <= den
                && (cboNhom.SelectedIndex <= 0 || x.Nhom == cboNhom.Text)).OrderBy(x => x.Ngay).ToList();
            decimal thu = loc.Sum(x => x.Thu), hoan = loc.Sum(x => x.Hoan);
            lblThu.Text = Tien(thu);
            lblHoan.Text = Tien(hoan);
            lblThucThu.Text = Tien(thu - hoan);
            lblSoGD.Text = loc.Count.ToString("N0", vi);

            DataTable bang = new DataTable();
            bang.Columns.Add("Mã giao dịch", typeof(string));
            bang.Columns.Add("Ngày", typeof(DateTime));
            bang.Columns.Add("Khách hàng", typeof(string));
            bang.Columns.Add("Nhóm", typeof(string));
            bang.Columns.Add("Nội dung", typeof(string));
            bang.Columns.Add("Tiền thu", typeof(decimal));
            bang.Columns.Add("Hoàn tiền", typeof(decimal));
            bang.Columns.Add("Thực thu", typeof(decimal));
            foreach (var x in loc)
                bang.Rows.Add(x.Ma, x.Ngay, x.KhachHang, x.Nhom, x.NoiDung, x.Thu, x.Hoan, x.Thu - x.Hoan);
            DataTable cu = bangHienThi;
            bangHienThi = bang;
            dgvChiTiet.DataSource = bang;
            if (cu != null) cu.Dispose();
            dgvChiTiet.Columns["Ngày"].DefaultCellStyle.Format = "dd/MM/yyyy";
            foreach (string cot in new[] { "Tiền thu", "Hoàn tiền", "Thực thu" })
            {
                dgvChiTiet.Columns[cot].DefaultCellStyle.Format = "N0";
                dgvChiTiet.Columns[cot].DefaultCellStyle.FormatProvider = vi;
                dgvChiTiet.Columns[cot].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            lblTrangThai.Text = string.Format("{0:dd/MM/yyyy} – {1:dd/MM/yyyy} | {2} giao dịch | Đơn vị: VNĐ. Thực thu = Thu − Hoàn; không phải lợi nhuận.", tu, den, loc.Count);
            btnXuat.Enabled = loc.Count > 0;
            VeBieuDo(loc, tu, den);
        }

        private string Tien(decimal so) { return so.ToString("N0", vi) + " đ"; }

        private void VeBieuDo(List<GiaoDichThongKe> loc, DateTime tu, DateTime den)
        {
            chartDongTien.Series.Clear();
            chartCoCau.Series.Clear();
            chartDongTien.Titles.Clear();
            chartCoCau.Titles.Clear();
            // Trên 93 ngày tự gom tháng để biểu đồ không bị hàng nghìn cột.
            bool thang = cboGom.SelectedIndex == 1 || (den - tu).TotalDays > 93;
            chartDongTien.Titles.Add("THU / HOÀN THEO " + (thang ? "THÁNG" : "NGÀY") + " (triệu đồng)");
            chartCoCau.Titles.Add("TIỀN THU THEO NHÓM");
            var sThu = new Series("Thu") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(197, 112, 126) };
            var sHoan = new Series("Hoàn") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(62, 39, 51) };
            chartDongTien.Series.Add(sThu);
            chartDongTien.Series.Add(sHoan);
            var nhom = loc.GroupBy(x => thang ? new DateTime(x.Ngay.Year, x.Ngay.Month, 1) : x.Ngay.Date)
                .ToDictionary(g => g.Key, g => new { Thu = g.Sum(x => x.Thu), Hoan = g.Sum(x => x.Hoan) });
            DateTime moc = thang ? new DateTime(tu.Year, tu.Month, 1) : tu;
            // Giới hạn số điểm cho khoảng thời gian quá dài.
            if ((den.Year - tu.Year) * 12 + den.Month - tu.Month > 120)
            {
                foreach (var p in nhom.OrderBy(x => x.Key))
                {
                    sThu.Points.AddXY(p.Key.ToString("MM/yyyy"), (double)(p.Value.Thu / 1000000m));
                    sHoan.Points.AddXY(p.Key.ToString("MM/yyyy"), (double)(p.Value.Hoan / 1000000m));
                }
            }
            else while (moc <= den)
            {
                string ten = moc.ToString(thang ? "MM/yyyy" : "dd/MM");
                sThu.Points.AddXY(ten, nhom.ContainsKey(moc) ? (double)(nhom[moc].Thu / 1000000m) : 0);
                sHoan.Points.AddXY(ten, nhom.ContainsKey(moc) ? (double)(nhom[moc].Hoan / 1000000m) : 0);
                if (thang && moc.Year == 9999 && moc.Month == 12 || !thang && moc == DateTime.MaxValue.Date) break;
                moc = thang ? moc.AddMonths(1) : moc.AddDays(1);
            }
            var tron = new Series("Cơ cấu") { ChartType = SeriesChartType.Doughnut };
            tron["DoughnutRadius"] = "65";
            tron.Label = "#PERCENT{P0}";
            tron.LegendText = "#VALX";
            chartCoCau.Series.Add(tron);
            foreach (var g in loc.GroupBy(x => x.Nhom).Where(g => g.Sum(x => x.Thu) > 0))
                tron.Points.AddXY(g.Key, (double)g.Sum(x => x.Thu));
            if (tron.Points.Count == 0) chartCoCau.Titles.Add("Không có tiền thu trong kỳ");
            if (loc.Count == 0) chartDongTien.Titles.Add("Không có giao dịch trong kỳ");
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (bangHienThi == null || bangHienThi.Rows.Count == 0) return;
            using (var hop = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "ThongKe_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv" })
            {
                if (hop.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    using (var w = new StreamWriter(hop.FileName, false, new UTF8Encoding(true)))
                    {
                        w.WriteLine(string.Join(",", bangHienThi.Columns.Cast<DataColumn>().Select(c => Csv(c.ColumnName))));
                        // Xuất đúng thứ tự đang sắp xếp trên bảng.
                        foreach (DataRowView row in bangHienThi.DefaultView)
                            w.WriteLine(string.Join(",", row.Row.ItemArray.Select(v => Csv(v is DateTime ? ((DateTime)v).ToString("dd/MM/yyyy") : Convert.ToString(v, CultureInfo.InvariantCulture)))));
                    }
                    MessageBox.Show(this, "Đã xuất bảng chi tiết đang hiển thị.", "Xuất CSV");
                }
                catch (IOException ex) { MessageBox.Show(this, "Không thể ghi file. Hãy đóng file đang mở hoặc chọn nơi khác.\n" + ex.Message); }
                catch (UnauthorizedAccessException ex) { MessageBox.Show(this, "Không có quyền ghi tại vị trí này.\n" + ex.Message); }
            }
        }

        private static string Csv(string s)
        {
            s = s ?? "";
            if (s.Length > 0 && "=+-@\t\r\n".IndexOf(s[0]) >= 0) s = "'" + s;
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }

        private static List<GiaoDichThongKe> TaoDuLieuMau()
        {
            var ds = new List<GiaoDichThongKe>();
            string[] nhom = { "Váy cưới", "Vest", "Áo dài", "Phụ kiện" };
            string[] khach = { "Nguyễn Mai", "Trần Linh", "Đỗ Hà", "Lê Minh", "Phạm Anh" };
            DateTime dau = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-5);
            int i = 0;
            for (DateTime ngay = dau; ngay <= DateTime.Today; ngay = ngay.AddDays(1))
            {
                if (ngay.Day % 4 == 0) continue;
                for (int j = 0; j < 2; j++)
                {
                    i++;
                    bool hoan = i % 9 == 0;
                    ds.Add(new GiaoDichThongKe { Ma = "GD" + i.ToString("D5"), Ngay = ngay,
                        KhachHang = khach[i % khach.Length], Nhom = nhom[i % 4],
                        NoiDung = hoan ? "Hoàn tiền khách hàng" : "Thu tiền thuê trang phục",
                        Thu = hoan ? 0 : 500000m + i % 8 * 350000m, Hoan = hoan ? 350000m : 0 });
                }
            }
            return ds;
        }
    }

    public sealed class GiaoDichThongKe
    {
        public string Ma { get; set; }
        public DateTime Ngay { get; set; }
        public string KhachHang { get; set; }
        public string Nhom { get; set; }
        public string NoiDung { get; set; }
        public decimal Thu { get; set; }
        public decimal Hoan { get; set; }
    }
}
