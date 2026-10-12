using System;
using System.Collections.Generic;
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

        private bool daKhoiTao = false;

        // Giữ các control Tổng quan để hiển thị lại khi cần.
        private readonly List<Control> dashboardControls =
            new List<Control>();

        // Hợp đồng và Trả đồ dùng chung danh sách này.
        private readonly List<RentalContract> rentalContracts =
            new List<RentalContract>();

        // Dữ liệu Nhân sự.
        private readonly List<StaffMember> staffMembers =
            new List<StaffMember>();

        public frmMain()
        {
            InitializeComponent();

            // Đảm bảo Load được gắn đúng một lần với handler này.
            this.Load -= frmMain_Load;
            this.Load += frmMain_Load;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (daKhoiTao)
                return;

            daKhoiTao = true;

            // Lưu lại các control Tổng quan đã được Designer tạo.
            foreach (Control control in panelContent.Controls)
            {
                dashboardControls.Add(control);
            }

            GanSuKienDieuHuong();
            PhanQuyenMenu();

            NapDuLieuBieuDoCot();
            NapDuLieuBieuDoTron();
            NapDuLieuBangLichTra();

            HighlightButton(btnTongQuan);
        }

        #region ĐIỀU HƯỚNG

        private void GanSuKienDieuHuong()
        {
            // Gỡ rồi gắn lại các handler có tên để tránh trùng.
            btnTongQuan.Click -= btnTongQuan_Click;
            btnTongQuan.Click += btnTongQuan_Click;

            btnKhachHang.Click -= btnKhachHang_Click;
            btnKhachHang.Click += btnKhachHang_Click;

            btnSanPham.Click -= btnSanPham_Click;
            btnSanPham.Click += btnSanPham_Click;

            btnNhaCungCap.Click -= btnNhaCungCap_Click;
            btnNhaCungCap.Click += btnNhaCungCap_Click;

            btnNhapKho.Click -= btnNhapKho_Click;
            btnNhapKho.Click += btnNhapKho_Click;

            btnHopDong.Click -= btnHopDong_Click;
            btnHopDong.Click += btnHopDong_Click;

            btnTraDo.Click -= btnTraDo_Click;
            btnTraDo.Click += btnTraDo_Click;

            btnNhanSu.Click -= btnNhanSu_Click;
            btnNhanSu.Click += btnNhanSu_Click;

            btnBaoCao.Click -= btnBaoCao_Click;
            btnBaoCao.Click += btnBaoCao_Click;
        }

        private void OpenChildForm(Form childForm)
        {
            if (childForm == null)
                return;

            panelContent.SuspendLayout();

            try
            {
                DongFormConHienTai();

                // Clear chỉ gỡ control khỏi panel.
                // Các control Tổng quan vẫn được giữ trong danh sách.
                panelContent.Controls.Clear();

                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.MinimumSize = Size.Empty;
                childForm.Dock = DockStyle.Fill;

                activeChildForm = childForm;

                panelContent.Controls.Add(childForm);
                panelContent.Tag = childForm;

                childForm.Show();
                childForm.BringToFront();
            }
            finally
            {
                panelContent.ResumeLayout(true);
            }
        }

        private void DongFormConHienTai()
        {
            if (activeChildForm == null)
                return;

            Form oldForm = activeChildForm;
            activeChildForm = null;

            panelContent.Controls.Remove(oldForm);

            oldForm.Close();
            oldForm.Dispose();

            panelContent.Tag = null;
        }

        private void HienThiTongQuan()
        {
            panelContent.SuspendLayout();

            try
            {
                DongFormConHienTai();
                panelContent.Controls.Clear();

                // Gắn lại các control Tổng quan ban đầu.
                foreach (Control control in dashboardControls)
                {
                    panelContent.Controls.Add(control);
                }

                // Khôi phục thứ tự control để giữ cách Dock ban đầu.
                for (int i = 0; i < dashboardControls.Count; i++)
                {
                    panelContent.Controls.SetChildIndex(
                        dashboardControls[i], i);
                }

                panelContent.Tag = null;
            }
            finally
            {
                panelContent.ResumeLayout(true);
            }

            HighlightButton(btnTongQuan);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            HienThiTongQuan();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            HighlightButton(btnKhachHang);
            OpenChildForm(new frmKhachHang());
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            HighlightButton(btnSanPham);
            OpenChildForm(new frmSanPham());
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            HighlightButton(btnNhaCungCap);
            OpenChildForm(new frmNhaCungCap());
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            HighlightButton(btnNhapKho);
            OpenChildForm(new frmHoaDonNhap());
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            HighlightButton(btnHopDong);
            OpenChildForm(new frmHopDong(rentalContracts));
        }

        private void btnTraDo_Click(object sender, EventArgs e)
        {
            HighlightButton(btnTraDo);
            OpenChildForm(new frmTraDo(rentalContracts));
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            HighlightButton(btnNhanSu);
            OpenChildForm(new frmNhanSu(staffMembers));
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            HighlightButton(btnBaoCao);
            OpenChildForm(new frmThongKe());
        }

        private void HighlightButton(Button btnSelected)
        {
            Button[] menuButtons =
            {
                btnTongQuan,
                btnKhachHang,
                btnSanPham,
                btnNhaCungCap,
                btnNhapKho,
                btnHopDong,
                btnTraDo,
                btnNhanSu,
                btnBaoCao
            };

            foreach (Button button in menuButtons)
            {
                button.BackColor = Color.FromArgb(74, 21, 37);
            }

            if (btnSelected != null)
            {
                btnSelected.BackColor =
                    Color.FromArgb(194, 94, 114);
            }
        }

        // Khi đang mở form con, các control Tổng quan không nằm
        // trong panel. Giải phóng chúng khi đóng ứng dụng.
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DongFormConHienTai();

            foreach (Control control in dashboardControls)
            {
                if (!control.IsDisposed && control.Parent == null)
                {
                    control.Dispose();
                }
            }

            dashboardControls.Clear();

            base.OnFormClosed(e);
        }

        #endregion

        #region PHÂN QUYỀN MENU HIỆN CÓ

        private void PhanQuyenMenu()
        {
            // Đưa menu về trạng thái mặc định.
            btnTongQuan.Visible = true;
            btnKhachHang.Visible = true;
            btnSanPham.Visible = true;
            btnNhaCungCap.Visible = true;
            btnNhapKho.Visible = true;
            btnHopDong.Visible = true;
            btnTraDo.Visible = true;
            btnNhanSu.Visible = true;
            btnBaoCao.Visible = true;

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

        #endregion

        #region DỮ LIỆU MẪU TỔNG QUAN

        private void NapDuLieuBieuDoCot()
        {
            chartDoanhThu.Series.Clear();

            if (chartDoanhThu.ChartAreas.Count == 0)
            {
                chartDoanhThu.ChartAreas.Add(
                    new ChartArea("DoanhThuArea"));
            }

            Series series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(194, 94, 114),
                IsXValueIndexed = true
            };

            string[] days =
            {
                "T2", "T3", "T4", "T5", "T6", "T7", "CN"
            };

            double[] values =
            {
                75, 140, 100, 160, 120, 145, 175
            };

            for (int i = 0; i < days.Length; i++)
            {
                series.Points.AddXY(days[i], values[i]);
            }

            chartDoanhThu.Series.Add(series);

            ChartArea area = chartDoanhThu.ChartAreas[0];
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(240, 240, 240);
        }

        private void NapDuLieuBieuDoTron()
        {
            chartTyLe.Series.Clear();

            if (chartTyLe.ChartAreas.Count == 0)
            {
                chartTyLe.ChartAreas.Add(
                    new ChartArea("TyLeArea"));
            }

            Series series = new Series("TyLe")
            {
                ChartType = SeriesChartType.Doughnut
            };

            series.Points.AddXY("Váy cưới 55%", 55);
            series.Points.AddXY("Áo dài 25%", 25);
            series.Points.AddXY("Vest 20%", 20);

            series.Points[0].Color =
                Color.FromArgb(194, 94, 114);

            series.Points[1].Color =
                Color.FromArgb(220, 140, 155);

            series.Points[2].Color =
                Color.FromArgb(235, 190, 200);

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

            dt.Rows.Add(
                "HĐ 50001",
                "Nguyễn Thu Hà",
                "0901234567",
                "Váy cưới, Áo dài",
                "Đã trả");

            dt.Rows.Add(
                "HĐ 50002",
                "Nguyễn Văn B",
                "0987654321",
                "Váy cưới, Vest",
                "Chưa trả");

            dgvLichTra.DataSource = dt;

            dgvLichTra.CellFormatting -= DgvLichTra_CellFormatting;
            dgvLichTra.CellFormatting += DgvLichTra_CellFormatting;
        }

        private void DgvLichTra_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex < 0 ||
                e.ColumnIndex >= dgvLichTra.Columns.Count ||
                e.Value == null)
            {
                return;
            }

            if (dgvLichTra.Columns[e.ColumnIndex].Name != "Trạng thái")
                return;

            e.CellStyle.ForeColor =
                e.Value.ToString() == "Đã trả"
                ? Color.MediumSeaGreen
                : Color.DarkOrange;
        }

        #endregion
    }
}