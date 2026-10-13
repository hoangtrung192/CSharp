using System;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            cboVaiTro.SelectedIndex = 0; // Mặc định chọn Quản lý
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đăng nhập thành công và mở Form Main
            this.Hide();
            frmMain mainForm = new frmMain();
            mainForm.ShowDialog();
            this.Close();
        }

        private void btnChuyenDangKy_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangKy fDangKy = new frmDangKy();
            fDangKy.ShowDialog();
            this.Show(); // Khi tắt form đăng ký thì hiện lại form đăng nhập
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}