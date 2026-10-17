using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LuxuryBridalStudio
{
    public partial class frmMain : Form
    {
        private Form activeForm;

        public frmMain() { InitializeComponent(); }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (AppSession.User == null) { Close(); return; }
            lblAdmin.Text = AppSession.User.DisplayName + " (" + AppSession.User.Role + ")";
            Text = "Luxury Bridal Studio";
            btnTongQuan.Click += btnTongQuan_Click;
            btnKhachHang.Click += (s, a) => OpenFeature(Feature.Customers, btnKhachHang, () => new frmKhachHang());
            btnSanPham.Click += (s, a) => OpenFeature(Feature.Products, btnSanPham, () => new frmSanPham());
            btnNhaCungCap.Click += (s, a) => OpenFeature(Feature.Suppliers, btnNhaCungCap, () => new frmNhaCungCap());
            btnNhapKho.Click += (s, a) => OpenFeature(Feature.Receipts, btnNhapKho, () => new frmHoaDonNhap());
            btnHopDong.Click += (s, a) => OpenFeature(Feature.Contracts, btnHopDong, () => new frmHopDong());
            btnTraDo.Click += (s, a) => OpenFeature(Feature.Returns, btnTraDo, () => new frmTraDo());
            btnNhanSu.Click += (s, a) => OpenFeature(Feature.Employees, btnNhanSu, () => new frmNhanSu());
            btnBaoCao.Click += (s, a) => OpenFeature(Feature.Reports, btnBaoCao, () => new frmThongKe(AppSession.Data.Transactions));
            btnDangXuat.Click += (s, a) => { if (BridalFeatureUi.Confirm("Bạn có muốn đăng xuất?")) Close(); };
            btnKhachHang.Visible = AppSession.User.Can(Feature.Customers);
            btnSanPham.Visible = AppSession.User.Can(Feature.Products);
            btnNhaCungCap.Visible = AppSession.User.Can(Feature.Suppliers);
            btnNhapKho.Visible = AppSession.User.Can(Feature.Receipts);
            btnHopDong.Visible = AppSession.User.Can(Feature.Contracts);
            btnTraDo.Visible = AppSession.User.Can(Feature.Returns);
            btnNhanSu.Visible = AppSession.User.Can(Feature.Employees);
            btnBaoCao.Visible = AppSession.User.Can(Feature.Reports);
            // Do not expose customer or financial information through the overview to warehouse staff.
            panelLeftChart.Visible = pnlCard1.Visible = AppSession.User.Can(Feature.Reports);
            panelTable.Visible = AppSession.User.Can(Feature.Customers);
            FormClosing += (s, a) => { if (!CloseActiveForm()) a.Cancel = true; };
            BridalFeatureUi.Run(NapDuLieuTongQuan);
        }

        private bool CloseActiveForm()
        {
            if (activeForm == null) return true;
            activeForm.Close();
            if (!activeForm.IsDisposed) return false;
            activeForm = null; return true;
        }

        private void OpenFeature(Feature feature, Button button, Func<Form> create)
        {
            BridalFeatureUi.Run(() =>
            {
                AppSession.Require(feature);
                if (!CloseActiveForm()) return;
                AppSession.Data.Refresh();
                var child = create();
                activeForm = child;
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;
                panelContent.Controls.Add(child);
                child.BringToFront();
                child.Show();
                ChonNutMenu(button);
            });
        }

        private void ChonNutMenu(Button selected)
        {
            foreach (var button in new[] { btnTongQuan, btnKhachHang, btnSanPham, btnNhaCungCap, btnNhapKho, btnHopDong, btnTraDo, btnNhanSu, btnBaoCao })
                button.BackColor = button == selected ? Color.FromArgb(194, 94, 114) : Color.FromArgb(74, 21, 37);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            if (!CloseActiveForm()) return;
            ChonNutMenu(btnTongQuan);
            BridalFeatureUi.Run(NapDuLieuTongQuan);
        }

        private void NapDuLieuTongQuan()
        {
            var data = AppSession.Data;
            data.Refresh();
            DateTime today = DateTime.Today;
            lblToday.Text = today.ToString("dd/MM/yyyy");
            lblTitle.Text = "Tổng quan";
            lblCardTitle1.Text = "Thực thu tháng";
            lblLeftChartTitle.Text = "Thực thu 7 ngày gần nhất";
            lblTableTitle.Text = "Lịch trả hôm nay và quá hạn";
            lblDoanhThu.Text = InputRules.Money(data.Transactions.Where(x => x.Ngay.Year == today.Year && x.Ngay.Month == today.Month).Sum(x => x.Thu - x.Hoan));
            lblHopDong.Text = data.Contracts.Count(x => x.TrangThai == RentalStatus.Active).ToString();
            lblLichTra.Text = data.Contracts.Count(x => x.TrangThai == RentalStatus.Active && x.HenTra.Date <= today).ToString();
            lblCardTitle4.Text = "Đến hạn / quá hạn";
            lblTrangPhuc.Text = data.Products.AsEnumerable().Sum(x => data.Available((string)x[0], today, today)) + " bộ";

            chartDoanhThu.Series.Clear();
            var revenue = new Series("Thực thu") { ChartType = SeriesChartType.Column, Color = Color.FromArgb(194, 94, 114) };
            for (int i = -6; i <= 0; i++)
            {
                DateTime day = today.AddDays(i);
                revenue.Points.AddXY(day.ToString("dd/MM"), (double)(data.Transactions.Where(x => x.Ngay.Date == day).Sum(x => x.Thu - x.Hoan) / 1000000m));
            }
            chartDoanhThu.Series.Add(revenue);
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Triệu đồng";
            chartTyLe.Series.Clear();
            var composition = new Series("Đang thuê") { ChartType = SeriesChartType.Doughnut, LegendText = "#VALX", Label = "#PERCENT{P0}" };
            foreach (var group in data.Contracts.Where(x => x.TrangThai == RentalStatus.Active).SelectMany(x => x.Lines).GroupBy(x => x.Category))
                composition.Points.AddXY(group.Key, group.Sum(x => x.Quantity));
            chartTyLe.Series.Add(composition);
            chartTyLe.Titles.Clear();
            if (composition.Points.Count == 0) chartTyLe.Titles.Add("Chưa có hợp đồng đang thuê");

            var table = new DataTable();
            foreach (string column in new[] { "Mã HĐ", "Khách hàng", "Trang phục", "Hẹn trả", "Trạng thái" }) table.Columns.Add(column);
            foreach (var rental in data.Contracts.Where(x => x.TrangThai == RentalStatus.Active && x.HenTra.Date <= today).OrderBy(x => x.HenTra))
                table.Rows.Add(rental.Ma, rental.KhachHang, rental.SanPham, rental.HenTra.ToString("dd/MM/yyyy"), rental.HenTra.Date < today ? "Quá hạn" : "Đến hạn");
            var previous = dgvLichTra.DataSource as DataTable;
            dgvLichTra.DataSource = table;
            if (previous != null) previous.Dispose();
        }
    }
}
