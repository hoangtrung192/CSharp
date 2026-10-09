using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LuxuryBridalStudio
{
    public partial class frmMain : Form
    {
        private string currentUserRole = "QuanLy";
        private Form activeChildForm = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // 1. Gán sự kiện Click mở Form cho các nút menu
            GanSuKienDieuHuong();

            // 2. Phân quyền menu
            PhanQuyenMenu();

            // 3. Khởi tạo dữ liệu mẫu cho Dashboard ban đầu
            NapDuLieuBieuDoCot();
            NapDuLieuBieuDoTron();
            NapDuLieuBangLichTra();
        }

        #region HÀM NHÚNG FORM CON VÀO PANELCONTENT
        private void OpenChildForm(Form childForm)
        {
            // Đóng form con hiện tại nếu có
            if (activeChildForm != null)
            {
                activeChildForm.Close();
            }

            activeChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Xóa sạch các control cũ đang nằm trong panelContent và đưa form con vào
            panelContent.Controls.Clear();
            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void GanSuKienDieuHuong()
        {
            // Nút Tổng quan: Reset lại trang Dashboard ban đầu
            btnTongQuan.Click += (s, e) =>
            {
                HighlightButton(btnTongQuan);
                if (activeChildForm != null)
                {
                    activeChildForm.Close();
                    activeChildForm = null;
                }
                panelContent.Controls.Clear();
                // Khởi tạo lại các control của Dashboard
                this.Controls.Clear();
                this.InitializeComponent();
                frmMain_Load(s, e);
            };

            // Nút Khách hàng: Mở frmKhachHang
            btnKhachHang.Click += (s, e) =>
            {
                HighlightButton(btnKhachHang);
                OpenChildForm(new frmKhachHang());
            };

            // Nút Sản phẩm: Mở frmSanPham
            btnSanPham.Click += (s, e) =>
            {
                HighlightButton(btnSanPham);
                OpenChildForm(new frmSanPham());
            };

            // Nút Nhà cung cấp: Mở frmNhaCungCap
            btnNhaCungCap.Click += (s, e) =>
            {
                HighlightButton(btnNhaCungCap);
                OpenChildForm(new frmNhaCungCap());
            };


            // Nút Nhập kho: Mở frmHoaDonNhap
            btnNhapKho.Click += (s, e) =>
            {
                HighlightButton(btnNhapKho);
                OpenChildForm(new frmHoaDonNhap());
            };

            // Gán hiệu ứng chọn màu cho các nút còn lại
            btnSanPham.Click += (s, e) => HighlightButton(btnSanPham);
            btnNhaCungCap.Click += (s, e) => HighlightButton(btnNhaCungCap);
            btnNhapKho.Click += (s, e) => HighlightButton(btnNhapKho);
            btnHopDong.Click += (s, e) => HighlightButton(btnHopDong);
            btnTraDo.Click += (s, e) => HighlightButton(btnTraDo);
            btnNhanSu.Click += (s, e) => HighlightButton(btnNhanSu);
            btnBaoCao.Click += (s, e) => HighlightButton(btnBaoCao);
        }

        private void HighlightButton(Button btnSelected)
        {
            // Reset các nút về màu nền gốc đỏ mận #4A1525
            foreach (Control c in panelSidebar.Controls)
            {
                if (c is Button)
                {
                    c.BackColor = Color.FromArgb(74, 21, 37);
                }
            }
            // Đổi nút đang bấm sang màu hồng đỗ #C25E72
            if (btnSelected != null)
            {
                btnSelected.BackColor = Color.FromArgb(194, 94, 114);
            }
        }
        #endregion

        #region PHÂN QUYỀN VÀ MOCK DATA
        private void PhanQuyenMenu()
        {
            if (currentUserRole == "BanHang")
            {
                btnSanPham.Visible = false;
                btnNhaCungCap.Visible = false;
                btnNhapKho.Visible = false;
                btnNhanSu.Visible = false;
                btnBaoCao.Visible = false;
            }
            else if (currentUserRole == "Kho")
            {
                btnKhachHang.Visible = false;
                btnHopDong.Visible = false;
                btnTraDo.Visible = false;
                btnNhanSu.Visible = false;
                btnBaoCao.Visible = false;
            }
        }

        private void NapDuLieuBieuDoCot()
        {
            chartDoanhThu.Series.Clear();
            Series series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(194, 94, 114)
            };

            string[] days = { "Tu", "Do", "Tu", "We", "Th", "Fr", "Sa", "Su" };
            double[] values = { 75, 140, 100, 160, 120, 145, 175, 80 };

            for (int i = 0; i < days.Length; i++)
            {
                series.Points.AddXY(days[i], values[i]);
            }

            chartDoanhThu.Series.Add(series);
            chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
        }

        private void NapDuLieuBieuDoTron()
        {
            chartTyLe.Series.Clear();
            Series series = new Series("TyLe")
            {
                ChartType = SeriesChartType.Doughnut
            };

            series.Points.AddXY("Váy cưới 55%", 55);
            series.Points.AddXY("Áo dài 25%", 25);
            series.Points.AddXY("Vest 20%", 20);

            series.Points[0].Color = Color.FromArgb(194, 94, 114);
            series.Points[1].Color = Color.FromArgb(220, 140, 155);
            series.Points[2].Color = Color.FromArgb(235, 190, 200);

            chartTyLe.Series.Add(series);
        }

        private void NapDuLieuBangLichTra()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã HĐ");
            dt.Columns.Add("Khách hàng");
            dt.Columns.Add("Điện thoại");
            dt.Columns.Add("Trang phục");
            dt.Columns.Add("Trạng thái");

            dt.Rows.Add("HĐ 50001", "Nguyễn Thu Hà", "0901234567", "Váy cưới, Áo dài", "Đã trả");
            dt.Rows.Add("HĐ 50002", "Nguyễn Văn B", "0987654321", "Váy cưới, Vest", "Chưa trả");

            dgvLichTra.DataSource = dt;
            dgvLichTra.CellFormatting += DgvLichTra_CellFormatting;
        }

        private void DgvLichTra_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLichTra.Columns[e.ColumnIndex].Name == "Trạng thái" && e.Value != null)
            {
                if (e.Value.ToString() == "Đã trả")
                    e.CellStyle.ForeColor = Color.MediumSeaGreen;
                else
                    e.CellStyle.ForeColor = Color.DarkOrange;
            }
        }
        #endregion
    }
}