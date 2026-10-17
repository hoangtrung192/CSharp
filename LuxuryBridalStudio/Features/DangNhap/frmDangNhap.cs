using System;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.Add("Tự xác định từ tài khoản");
            cboVaiTro.SelectedIndex = 0;
            cboVaiTro.Enabled = false;
            lblVaiTro.Text = "Vai trò tài khoản";
            AcceptButton = btnDangNhap;
            CancelButton = btnThoat;
            Text = "Luxury Bridal Studio";
            ClientSize = new Size(380, 510);
            Controls.Add(new Label { Location = new Point(25, 425), Size = new Size(335, 80),
                Text = "Đăng nhập bằng tài khoản đã tạo.\nChọn Đăng ký để tạo tài khoản nhân viên.",
                Font = new Font("Segoe UI", 8F), ForeColor = Color.DimGray });
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            BridalFeatureUi.Run(() =>
            {
                if (AppSession.Accounts == null) throw new InvalidOperationException("Chưa cấu hình dịch vụ tài khoản.");
                var user = AppSession.Accounts.Authenticate(txtTaiKhoan.Text, txtMatKhau.Text);
                AppSession.Data.Refresh();
                AppSession.User = user;
                txtMatKhau.Clear();
                Hide();
                try { using (var main = new frmMain()) main.ShowDialog(); }
                finally { AppSession.User = null; Show(); txtMatKhau.Focus(); }
            });
        }

        private void btnChuyenDangKy_Click(object sender, EventArgs e)
        {
            using (var registration = new frmDangKy()) registration.ShowDialog(this);
        }

        private void btnThoat_Click(object sender, EventArgs e) { Close(); }
    }
}
