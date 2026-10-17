using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmSanPham : Form
    {
        private readonly CatalogService catalog = new CatalogService(AppSession.Data);
        private DataView view;
        private string selectedCode;
        private bool adding;
        private const string Placeholder = "Nhập tên váy cưới, vest...";

        public frmSanPham() { InitializeComponent(); }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            AppSession.Require(Feature.Products);
            nudSoLuongTon.Maximum = 1000000;
            lblTonKhoDetail.Text = "Tổng số bộ";
            foreach (var combo in new[] { cboLoaiSP, cboLoaiSPDetail })
                if (!combo.Items.Contains("Phụ kiện")) combo.Items.Add("Phụ kiện");
            foreach (var combo in new[] { cboLoaiSP, cboMauSac, cboNoiSX, cboLoaiSPDetail, cboMauSacDetail, cboNoiSXDetail })
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
            view = new DataView(AppSession.Data.Products);
            dgvSanPham.SelectionChanged += (s, a) => SelectRow();
            dgvSanPham.DataSource = view;
            dgvSanPham.MultiSelect = false;
            dgvSanPham.Columns["Giá nhập"].Visible = AppSession.User.Can(Feature.Products, true);
            dgvSanPham.Columns["HinhAnh"].Visible = false;
            dgvSanPham.Columns["Tồn kho"].HeaderText = "Tổng số bộ";
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvSanPham.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            foreach (DataGridViewColumn column in dgvSanPham.Columns) column.Width = 110;
            dgvSanPham.Columns["Mã SP"].Width = 75;
            dgvSanPham.Columns["Tên trang phục"].Width = 220;
            BridalFeatureUi.FormatGrid(dgvSanPham);
            BridalFeatureUi.Placeholder(txtTuKhoa, Placeholder);
            BridalFeatureUi.Wire(btnThemSP, () =>
            {
                ResetEditor(); dgvSanPham.ClearSelection(); adding = true;
                txtMaSP.Text = InputRules.NextCode(AppSession.Data.Products, "SP"); txtTenSP.Focus();
            });
            BridalFeatureUi.Wire(btnLuu, Save);
            BridalFeatureUi.Wire(btnXoa, Delete);
            BridalFeatureUi.Wire(btnLamMoi, () =>
            {
                txtTuKhoa.Clear(); cboLoaiSP.SelectedIndex = cboMauSac.SelectedIndex = cboNoiSX.SelectedIndex = 0;
                view.RowFilter = ""; dgvSanPham.ClearSelection(); ResetEditor();
            });
            BridalFeatureUi.Wire(btnTraCuu, Filter);
            txtTuKhoa.KeyDown += (s, a) => { if (a.KeyCode == Keys.Enter) { a.SuppressKeyPress = true; BridalFeatureUi.Run(Filter); } };
            btnChonAnh.LinkClicked += (s, a) => BridalFeatureUi.Run(ChooseImage);
            bool editable = AppSession.User.Can(Feature.Products, true);
            txtGiaNhap.Visible = lblGiaNhap.Visible = editable;
            btnThemSP.Enabled = btnLuu.Enabled = btnXoa.Enabled = btnChonAnh.Enabled = editable;
            foreach (Control input in new Control[] { txtTenSP, txtGiaThue, txtGiaNhap, nudSoLuongTon, cboLoaiSPDetail, cboMauSacDetail, cboNoiSXDetail }) input.Enabled = editable;
            cboLoaiSP.SelectedIndex = cboMauSac.SelectedIndex = cboNoiSX.SelectedIndex = 0;
            Disposed += (s, a) => { if (picAnhSP.Image != null) picAnhSP.Image.Dispose(); view.Dispose(); };
            SelectRow();
        }

        private void ResetEditor()
        {
            selectedCode = null; adding = false;
            txtMaSP.Clear(); txtTenSP.Clear(); txtGiaNhap.Clear(); txtGiaThue.Clear(); nudSoLuongTon.Value = 0;
            cboLoaiSPDetail.SelectedIndex = cboMauSacDetail.SelectedIndex = cboNoiSXDetail.SelectedIndex = 0;
            SetImage(null, "");
        }

        private void SelectRow()
        {
            var row = dgvSanPham.SelectedRows.Count == 0 ? null : dgvSanPham.SelectedRows[0].DataBoundItem as DataRowView;
            ResetEditor();
            if (row == null) return;
            selectedCode = Convert.ToString(row[0]); txtMaSP.Text = selectedCode;
            txtTenSP.Text = Convert.ToString(row["Tên trang phục"]);
            cboLoaiSPDetail.SelectedItem = row["Loại"]; cboMauSacDetail.SelectedItem = row["Màu sắc"]; cboNoiSXDetail.SelectedItem = row["Nơi SX"];
            nudSoLuongTon.Value = (int)row["Tồn kho"];
            txtGiaNhap.Text = InputRules.Money((decimal)row["Giá nhập"]);
            txtGiaThue.Text = InputRules.Money((decimal)row["Giá thuê"]);
            string path = Convert.ToString(row["HinhAnh"]);
            picAnhSP.Tag = path;
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                try { LoadImage(path); }
                catch (InvalidOperationException) { SetImage(null, path); }
            }
        }

        private void SetImage(Image image, string path)
        {
            var previous = picAnhSP.Image; picAnhSP.Image = image; picAnhSP.Tag = path;
            if (previous != null) previous.Dispose();
        }

        private void LoadImage(string path)
        {
            try
            {
                using (var source = Image.FromFile(path)) SetImage(new Bitmap(source), path);
            }
            catch (Exception ex) when (ex is IOException || ex is ArgumentException || ex is UnauthorizedAccessException || ex is OutOfMemoryException)
            {
                throw new InvalidOperationException("Không đọc được ảnh. Hãy chọn file JPG hoặc PNG hợp lệ.", ex);
            }
        }

        private void ChooseImage()
        {
            AppSession.Require(Feature.Products, true);
            using (var dialog = new OpenFileDialog { Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png" })
                if (dialog.ShowDialog(this) == DialogResult.OK) LoadImage(dialog.FileName);
        }

        private void Save()
        {
            if (!adding && selectedCode == null) throw new InvalidOperationException("Chọn sản phẩm hoặc bấm Thêm mới trước khi lưu.");
            string code = catalog.SaveProduct(adding ? null : selectedCode, txtTenSP.Text, cboLoaiSPDetail.Text, cboMauSacDetail.Text,
                cboNoiSXDetail.Text, (int)nudSoLuongTon.Value, InputRules.ParseMoney(txtGiaThue.Text), InputRules.ParseMoney(txtGiaNhap.Text), Convert.ToString(picAnhSP.Tag));
            view.RowFilter = "";
            foreach (DataGridViewRow row in dgvSanPham.Rows)
                if (Convert.ToString(((DataRowView)row.DataBoundItem)[0]) == code) { dgvSanPham.CurrentCell = row.Cells[0]; row.Selected = true; SelectRow(); break; }
        }

        private void Delete()
        {
            if (selectedCode == null) throw new InvalidOperationException("Chọn sản phẩm cần xóa.");
            if (!BridalFeatureUi.Confirm("Xóa sản phẩm " + selectedCode + "?")) return;
            catalog.Delete(Feature.Products, selectedCode); SelectRow();
        }

        private void Filter()
        {
            var filters = new List<string>();
            if (cboLoaiSP.SelectedIndex > 0) filters.Add("[Loại] = '" + cboLoaiSP.Text.Replace("'", "''") + "'");
            if (cboMauSac.SelectedIndex > 0) filters.Add("[Màu sắc] = '" + cboMauSac.Text.Replace("'", "''") + "'");
            if (cboNoiSX.SelectedIndex > 0) filters.Add("[Nơi SX] = '" + cboNoiSX.Text.Replace("'", "''") + "'");
            string search = InputRules.SearchFilter(txtTuKhoa.Text == Placeholder ? "" : txtTuKhoa.Text, "Mã SP", "Tên trang phục");
            if (search.Length > 0) filters.Add("(" + search + ")");
            view.RowFilter = string.Join(" AND ", filters);
            SelectRow();
        }
    }
}
