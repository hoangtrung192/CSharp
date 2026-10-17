using System;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.Add(Roles.Employee);
            cboVaiTro.SelectedIndex = 0;
            cboVaiTro.Enabled = false;
            lblVaiTro.Text = "Quyền mặc định";
            AcceptButton = btnDangKy;
            CancelButton = btnQuayLai;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            BridalFeatureUi.Run(() =>
            {
                if (txtMatKhau.Text != txtXacNhanMK.Text) throw new InvalidOperationException("Mật khẩu xác nhận không khớp.");
                if (AppSession.Accounts == null) throw new InvalidOperationException("Chưa cấu hình dịch vụ tài khoản.");
                AppSession.Accounts.Register(txtTaiKhoan.Text, txtMatKhau.Text);
                MessageBox.Show("Đã tạo tài khoản nhân viên. Bạn có thể đăng nhập ngay.", "Đăng ký");
                Close();
            });
        }

        private void btnQuayLai_Click(object sender, EventArgs e) { Close(); }
    }
}
