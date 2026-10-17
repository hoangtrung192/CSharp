using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    internal static class BridalFeatureUi
    {
        internal static void Run(Action action)
        {
            try { action(); }
            catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "Kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        internal static void Wire(Button button, Action action) { button.Click += (s, e) => Run(action); }

        internal static string AskReason(IWin32Window owner, string title)
        {
            using (var dialog = new Form { Text = title, ClientSize = new Size(460, 165), FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent, MaximizeBox = false, MinimizeBox = false })
            {
                var input = new TextBox { Multiline = true, Location = new Point(15, 15), Size = new Size(430, 85), MaxLength = 1000 };
                var ok = new Button { Text = "Tiếp tục", Location = new Point(255, 115), DialogResult = DialogResult.OK };
                var cancel = new Button { Text = "Quay lại", Location = new Point(350, 115), DialogResult = DialogResult.Cancel };
                dialog.Controls.AddRange(new Control[] { input, ok, cancel }); dialog.AcceptButton = ok; dialog.CancelButton = cancel;
                return dialog.ShowDialog(owner) == DialogResult.OK ? input.Text.Trim() : null;
            }
        }

        internal static void Placeholder(TextBox input, string text)
        {
            input.Text = text; input.ForeColor = Color.Gray;
            input.Enter += (s, e) => { if (input.Text == text) { input.Clear(); input.ForeColor = Color.Black; } };
            input.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(input.Text)) { input.Text = text; input.ForeColor = Color.Gray; } };
        }

        internal static void BindPicker(ComboBox input, DataTable table, string name)
        {
            var previous = input.DataSource as DataTable;
            var choices = table.Copy();
            choices.Columns.Add("Display", typeof(string));
            foreach (DataRow row in choices.Rows) row["Display"] = row[0] + " · " + row[name];
            input.DropDownStyle = ComboBoxStyle.DropDownList;
            input.DisplayMember = "Display"; input.ValueMember = table.Columns[0].ColumnName;
            input.DataSource = choices; input.SelectedIndex = -1;
            if (previous != null) previous.Dispose();
            input.Disposed += (s, e) => choices.Dispose();
        }

        internal static void FormatGrid(DataGridView grid)
        {
            grid.DataBindingComplete += (s, e) => ApplyGridFormat(grid);
            ApplyGridFormat(grid);
        }

        private static void ApplyGridFormat(DataGridView grid)
        {
            var headings = new Dictionary<string, string> {
                { "Ma", "Mã" }, { "CustomerCode", "Mã khách" }, { "KhachHang", "Khách hàng" }, { "SanPham", "Sản phẩm" },
                { "NgayThue", "Ngày thuê" }, { "HenTra", "Hẹn trả" }, { "TienThue", "Tiền thuê" }, { "DaThu", "Đã thu" },
                { "TrangThai", "Trạng thái" }, { "NgayTra", "Ngày trả" }, { "PhuPhi", "Phụ phí" }, { "ThuKhiTra", "Thu khi trả" }, { "GhiChu", "Ghi chú" },
                { "HoTen", "Họ tên" }, { "DienThoai", "Điện thoại" }, { "VaiTro", "Vai trò" }, { "DangLam", "Đang làm" },
                { "ProductCode", "Mã SP" }, { "ProductName", "Sản phẩm" }, { "Category", "Loại" }, { "Quantity", "Số lượng" }, { "UnitPrice", "Đơn giá" }, { "Total", "Thành tiền" }
            };
            foreach (DataGridViewColumn column in grid.Columns)
            {
                string key = column.DataPropertyName;
                if (headings.ContainsKey(key)) column.HeaderText = headings[key];
                if (column.ValueType == typeof(decimal)) { column.DefaultCellStyle.Format = "N0"; column.DefaultCellStyle.FormatProvider = InputRules.Vietnamese; }
                if (column.ValueType == typeof(DateTime) || column.ValueType == typeof(DateTime?)) column.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        internal static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
        internal static bool Confirm(string message) { return MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }
    }
}
