using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    internal static class BridalFeatureUi
    {
        internal static readonly Color Plum = Color.FromArgb(62, 39, 51);
        internal static TableLayoutPanel Page(Form form, string title)
        {
            form.Text = title; form.BackColor = Color.FromArgb(250, 249, 246);
            form.Font = new Font("Segoe UI", 10); form.Size = new Size(1100, 760);
            var page = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(20) };
            page.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 85));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 230));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            page.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            var heading = new Label { Dock = DockStyle.Fill, Text = title + "\nQuản lý hoạt động studio", ForeColor = Plum, Font = new Font("Segoe UI", 14, FontStyle.Bold) };
            page.Controls.Add(heading, 0, 0); form.Controls.Add(page); return page;
        }
        internal static TableLayoutPanel Editor(TableLayoutPanel page)
        {
            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4, BackColor = Color.White, Padding = new Padding(10) };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135)); editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135)); editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 4; i++) editor.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            page.Controls.Add(editor, 0, 1); return editor;
        }
        internal static void Field(TableLayoutPanel editor, int row, int pair, string label, Control input)
        {
            editor.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, pair * 2, row);
            input.Dock = DockStyle.Fill; input.Margin = new Padding(5, 8, 12, 5); editor.Controls.Add(input, pair * 2 + 1, row);
        }
        internal static FlowLayoutPanel Toolbar(TableLayoutPanel page)
        {
            var bar = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true }; page.Controls.Add(bar, 0, 2); return bar;
        }
        internal static void Button(FlowLayoutPanel bar, string text, Action action)
        {
            var button = new Button { Text = text, AutoSize = true, Height = 36, BackColor = Plum, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Padding = new Padding(8, 3, 8, 3) };
            button.Click += (s, e) => { try { action(); } catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "Kiểm tra dữ liệu"); } };
            bar.Controls.Add(button);
        }
        internal static DataGridView Grid(TableLayoutPanel page)
        {
            var grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells, BackgroundColor = Color.White, RowHeadersVisible = false };
            grid.DataBindingComplete += (sender, args) =>
            {
                var headings = new Dictionary<string, string> {
                    { "Ma", "Mã" }, { "KhachHang", "Khách hàng" }, { "SanPham", "Sản phẩm" },
                    { "NgayThue", "Ngày thuê" }, { "HenTra", "Hẹn trả" }, { "TienThue", "Tiền thuê" },
                    { "DaThu", "Đã thu" }, { "TrangThai", "Trạng thái" }, { "NgayTra", "Ngày trả" },
                    { "PhuPhi", "Phụ phí" }, { "ThuKhiTra", "Thu khi trả" }, { "GhiChu", "Ghi chú" },
                    { "HoTen", "Họ tên" }, { "DienThoai", "Điện thoại" }, { "VaiTro", "Vai trò" }, { "DangLam", "Đang làm" }
                };
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    if (headings.ContainsKey(column.DataPropertyName)) column.HeaderText = headings[column.DataPropertyName];
                    if (column.ValueType == typeof(decimal)) column.DefaultCellStyle.Format = "N0";
                    if (column.ValueType == typeof(DateTime) || column.ValueType == typeof(DateTime?)) column.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            };
            page.Controls.Add(grid, 0, 3); return grid;
        }
        internal static NumericUpDown Money() { return new NumericUpDown { Maximum = 1000000000, ThousandsSeparator = true, Increment = 100000 }; }
        internal static DateTimePicker Date() { return new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" }; }
        internal static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
        internal static bool Confirm(string message) { return MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }
    }
}
