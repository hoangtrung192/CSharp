using System;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    // Normal registration always creates employees. This dialog is only for an empty Accounts table.
    internal sealed class frmInitialSetup : Form
    {
        internal frmInitialSetup(SqlAccountService accounts)
        {
            Text = "Thiết lập Luxury Bridal Studio";
            ClientSize = new Size(440, 340);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = MinimizeBox = false;
            Font = new Font("Segoe UI", 10F);
            Controls.Add(new Label { Text = "Tạo tài khoản quản lý đầu tiên", Location = new Point(20, 20), AutoSize = true, Font = new Font(Font, FontStyle.Bold) });
            Controls.Add(new Label { Text = "Mật khẩu cần 10–128 ký tự, gồm chữ và số.", Location = new Point(20, 55), AutoSize = true });
            var name = Input("Tài khoản", 95, false);
            var password = Input("Mật khẩu", 145, true);
            var confirm = Input("Xác nhận", 195, true);
            name.MaxLength = 50; password.MaxLength = confirm.MaxLength = 128;
            var create = new Button { Text = "Tạo quản lý", Location = new Point(180, 265), Size = new Size(115, 36) };
            var cancel = new Button { Text = "Thoát", Location = new Point(305, 265), Size = new Size(110, 36), DialogResult = DialogResult.Cancel };
            create.Click += (s, e) => BridalFeatureUi.Run(() =>
            {
                if (password.Text != confirm.Text) throw new InvalidOperationException("Mật khẩu xác nhận không khớp.");
                accounts.CreateInitialManager(name.Text, password.Text);
                DialogResult = DialogResult.OK; Close();
            });
            Controls.AddRange(new Control[] { create, cancel });
            AcceptButton = create; CancelButton = cancel;
        }

        private TextBox Input(string label, int top, bool secret)
        {
            Controls.Add(new Label { Text = label, Location = new Point(20, top + 4), AutoSize = true });
            var input = new TextBox { Location = new Point(135, top), Width = 280, UseSystemPasswordChar = secret };
            Controls.Add(input); return input;
        }
    }
}
