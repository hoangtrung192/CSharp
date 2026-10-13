using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LuxuryBridalStudio
{
    public partial class frmMain : Form
    {
        // Khai báo biến rỗng, nhận trực tiếp từ CSDL sau khi đăng nhập
        string currentUserName = "";
        string currentUserRole = "";

        // Form con đang mở trong panelContent
        Form activeForm = null;

        // Constructor chính nhận dữ liệu đăng nhập
        public frmMain(string hoTen, string vaiTro)
        {
            InitializeComponent();
            currentUserName = hoTen;
            currentUserRole = vaiTro;
        }

        // Constructor mặc định (để Visual Studio mở được chế độ Design)
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Hiển thị tên và ngày tháng lên góc phải
            lblAdmin.Text = currentUserName + " (" + currentUserRole + ")";
            lblToday.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // Gán sự kiện Click cho tất cả các nút điều hướng
            btnTongQuan.Click += btnTongQuan_Click;
            btnKhachHang.Click += btnKhachHang_Click;
            btnSanPham.Click += btnSanPham_Click;
            btnNhaCungCap.Click += btnNhaCungCap_Click;
            btnNhapKho.Click += btnNhapKho_Click;
            btnHopDong.Click += btnHopDong_Click;
            btnTraDo.Click += btnTraDo_Click;
            btnNhanSu.Click += btnNhanSu_Click;
            btnBaoCao.Click += btnBaoCao_Click;
            btnDangXuat.Click += btnDangXuat_Click;

            // Phân quyền ẩn/hiện nút menu
            PhanQuyenMenu();

            // Nạp dữ liệu mẫu cho Tổng quan
            NapDuLieuTongQuan();
        }

        // Hàm phân quyền đơn giản
        void PhanQuyenMenu()
        {
            if (currentUserRole == "Quản lý")
            {
                // Quản lý hiển thị tất cả các nút
                btnTongQuan.Visible = true;
                btnKhachHang.Visible = true;
                btnSanPham.Visible = true;
                btnNhaCungCap.Visible = true;
                btnNhapKho.Visible = true;
                btnHopDong.Visible = true;
                btnTraDo.Visible = true;
                btnNhanSu.Visible = true;
                btnBaoCao.Visible = true;
            }
            else if (currentUserRole == "Thủ kho")
            {
                // Thủ kho chỉ quản lý sản phẩm, nhà cung cấp và nhập kho
                btnTongQuan.Visible = true;
                btnSanPham.Visible = true;
                btnNhaCungCap.Visible = true;
                btnNhapKho.Visible = true;

                btnKhachHang.Visible = false;
                btnHopDong.Visible = false;
                btnTraDo.Visible = false;
                btnNhanSu.Visible = false;
                btnBaoCao.Visible = false;
            }
            else // "Nhân viên"
            {
                // Nhân viên chỉ làm hợp đồng thuê và trả đồ cho khách
                btnTongQuan.Visible = true;
                btnKhachHang.Visible = true;
                btnSanPham.Visible = true;
                btnHopDong.Visible = true;
                btnTraDo.Visible = true;

                btnNhaCungCap.Visible = false;
                btnNhapKho.Visible = false;
                btnNhanSu.Visible = false;
                btnBaoCao.Visible = false;
            }
        }

        // Hàm mở một form con vào panelContent
        void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Đổi màu nút khi được bấm
        void ChonNutMenu(Button btn)
        {
            // Trả tất cả nút về màu nền cũ
            btnTongQuan.BackColor = Color.FromArgb(74, 21, 37);
            btnKhachHang.BackColor = Color.FromArgb(74, 21, 37);
            btnSanPham.BackColor = Color.FromArgb(74, 21, 37);
            btnNhaCungCap.BackColor = Color.FromArgb(74, 21, 37);
            btnNhapKho.BackColor = Color.FromArgb(74, 21, 37);
            btnHopDong.BackColor = Color.FromArgb(74, 21, 37);
            btnTraDo.BackColor = Color.FromArgb(74, 21, 37);
            btnNhanSu.BackColor = Color.FromArgb(74, 21, 37);
            btnBaoCao.BackColor = Color.FromArgb(74, 21, 37);

            // Đổi màu nút đang chọn
            btn.BackColor = Color.FromArgb(194, 94, 114);
        }

        // Sự kiện Click của các nút menu
        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnTongQuan);
            if (activeForm != null)
            {
                activeForm.Close(); // Đóng form con để lộ giao diện Tổng quan bên dưới
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnKhachHang);
            OpenChildForm(new frmKhachHang());
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnSanPham);
            OpenChildForm(new frmSanPham());
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnNhaCungCap);
            OpenChildForm(new frmNhaCungCap());
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnNhapKho);
            OpenChildForm(new frmHoaDonNhap());
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnHopDong);
            OpenChildForm(new frmHopDong());
        }

        private void btnTraDo_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnTraDo);
            OpenChildForm(new frmTraDo());
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnNhanSu);
            OpenChildForm(new frmNhanSu());
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ChonNutMenu(btnBaoCao);
            OpenChildForm(new frmThongKe());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Hide();
                frmDangNhap loginForm = new frmDangNhap();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        // Nạp dữ liệu các biểu đồ và bảng trên màn hình Tổng quan
        void NapDuLieuTongQuan()
        {
            // 1. Biểu đồ cột Doanh thu
            chartDoanhThu.Series.Clear();
            Series seriesCot = new Series("DoanhThu");
            seriesCot.ChartType = SeriesChartType.Column;
            seriesCot.Color = Color.FromArgb(194, 94, 114);

            seriesCot.Points.AddXY("T2", 75);
            seriesCot.Points.AddXY("T3", 140);
            seriesCot.Points.AddXY("T4", 100);
            seriesCot.Points.AddXY("T5", 160);
            seriesCot.Points.AddXY("T6", 120);
            seriesCot.Points.AddXY("T7", 145);
            seriesCot.Points.AddXY("CN", 175);
            chartDoanhThu.Series.Add(seriesCot);

            // 2. Biểu đồ tròn Tỷ lệ trang phục
            chartTyLe.Series.Clear();
            Series seriesTron = new Series("TyLe");
            seriesTron.ChartType = SeriesChartType.Doughnut;

            seriesTron.Points.AddXY("Váy cưới 55%", 55);
            seriesTron.Points.AddXY("Áo dài 25%", 25);
            seriesTron.Points.AddXY("Vest 20%", 20);

            seriesTron.Points[0].Color = Color.FromArgb(194, 94, 114);
            seriesTron.Points[1].Color = Color.FromArgb(220, 140, 155);
            seriesTron.Points[2].Color = Color.FromArgb(235, 190, 200);
            chartTyLe.Series.Add(seriesTron);

            // 3. Bảng lịch hẹn trả đồ
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã HĐ");
            dt.Columns.Add("Khách hàng");
            dt.Columns.Add("Điện thoại");
            dt.Columns.Add("Trang phục");
            dt.Columns.Add("Trạng thái");

            dt.Rows.Add("HĐ 50001", "Nguyễn Thu Hà", "0901234567", "Váy cưới, Áo dài", "Đã trả");
            dt.Rows.Add("HĐ 50002", "Nguyễn Văn B", "0987654321", "Váy cưới, Vest", "Chưa trả");
            dgvLichTra.DataSource = dt;
        }
    }
}