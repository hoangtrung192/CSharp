using System;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            cboVaiTro.SelectedIndex = 0; // Mặc định là Quản lý[cite: 17]

            // Xóa trắng ô tài khoản và mật khẩu khi mở form
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy đúng những gì người dùng nhập vào
            string hoTen = txtTaiKhoan.Text.Trim();
            string vaiTro = cboVaiTro.SelectedItem != null ? cboVaiTro.SelectedItem.ToString() : "Quản lý";

            // Khởi tạo frmMain với đúng tên và vai trò được chọn
            frmMain mainForm = new frmMain(hoTen, vaiTro);
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}