using System;
using System.Data;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmNhaCungCap : Form
    {
        private readonly CatalogService catalog = new CatalogService(AppSession.Data);
        private DataView view;
        private string selectedCode;
        private bool adding;
        private const string Placeholder = "Nhập tên nhà cung cấp, số điện thoại hoặc địa chỉ...";

        public frmNhaCungCap() { InitializeComponent(); }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            AppSession.Require(Feature.Suppliers);
            view = new DataView(AppSession.Data.Suppliers);
            dgvNhaCungCap.SelectionChanged += (s, a) => SelectRow();
            dgvNhaCungCap.DataSource = view;
            dgvNhaCungCap.MultiSelect = false;
            BridalFeatureUi.Placeholder(txtTimKiem, Placeholder);
            BridalFeatureUi.Wire(btnThemNCC, () => { ResetEditor(); dgvNhaCungCap.ClearSelection(); adding = true; txtMaNCC.Text = InputRules.NextCode(AppSession.Data.Suppliers, "NCC"); txtTenNCC.Focus(); });
            BridalFeatureUi.Wire(btnLuu, Save);
            BridalFeatureUi.Wire(btnXoa, Delete);
            BridalFeatureUi.Wire(btnLamMoi, () => { txtTimKiem.Clear(); view.RowFilter = ""; dgvNhaCungCap.ClearSelection(); ResetEditor(); UpdateCount(); });
            BridalFeatureUi.Wire(btnTraCuu, Filter);
            txtTimKiem.KeyDown += (s, a) => { if (a.KeyCode == Keys.Enter) { a.SuppressKeyPress = true; BridalFeatureUi.Run(Filter); } };
            cboSapXep.SelectedIndexChanged += (s, a) =>
            {
                view.Sort = cboSapXep.SelectedIndex == 1 ? "[Tên nhà cung cấp] ASC" : "[Mã NCC] " + (cboSapXep.SelectedIndex == 2 ? "ASC" : "DESC");
                SelectRow();
            };
            cboSapXep.SelectedIndex = 0;
            Disposed += (s, a) => view.Dispose();
            SelectRow(); UpdateCount();
        }

        private void SelectRow()
        {
            var row = dgvNhaCungCap.SelectedRows.Count == 0 ? null : dgvNhaCungCap.SelectedRows[0].DataBoundItem as DataRowView;
            ResetEditor();
            if (row == null) return;
            selectedCode = Convert.ToString(row[0]);
            txtMaNCC.Text = selectedCode;
            txtTenNCC.Text = Convert.ToString(row["Tên nhà cung cấp"]);
            txtDienThoai.Text = Convert.ToString(row["Số điện thoại"]);
            txtDiaChi.Text = Convert.ToString(row["Địa chỉ"]);
            txtGhiChu.Text = Convert.ToString(row["Ghi chú"]);
        }

        private void ResetEditor()
        {
            selectedCode = null; adding = false;
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtGhiChu.Clear();
        }

        private void Save()
        {
            if (!adding && selectedCode == null) throw new InvalidOperationException("Chọn nhà cung cấp hoặc bấm Thêm mới trước khi lưu.");
            string code = catalog.SaveSupplier(adding ? null : selectedCode, txtTenNCC.Text, txtDienThoai.Text, txtDiaChi.Text, txtGhiChu.Text);
            view.RowFilter = "";
            SelectCode(code); UpdateCount();
        }

        private void SelectCode(string code)
        {
            foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
            {
                var data = row.DataBoundItem as DataRowView;
                if (data != null && Convert.ToString(data[0]) == code) { dgvNhaCungCap.CurrentCell = row.Cells[0]; row.Selected = true; SelectRow(); return; }
            }
            ResetEditor();
        }

        private void Delete()
        {
            if (selectedCode == null) throw new InvalidOperationException("Chọn nhà cung cấp cần xóa.");
            if (!BridalFeatureUi.Confirm("Xóa nhà cung cấp " + selectedCode + "?")) return;
            catalog.Delete(Feature.Suppliers, selectedCode);
            SelectRow(); UpdateCount();
        }

        private void Filter()
        {
            view.RowFilter = InputRules.SearchFilter(txtTimKiem.Text == Placeholder ? "" : txtTimKiem.Text, "Tên nhà cung cấp", "Số điện thoại", "Địa chỉ");
            SelectRow(); UpdateCount();
        }

        private void UpdateCount() { lblTongCong.Text = "Tổng cộng: " + view.Count + " nhà cung cấp"; }
    }
}
