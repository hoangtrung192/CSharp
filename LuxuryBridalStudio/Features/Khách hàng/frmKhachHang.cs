using System;
using System.Data;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmKhachHang : Form
    {
        private readonly CatalogService catalog = new CatalogService(AppSession.Data);
        private DataView view;
        private string selectedCode;
        private bool adding;
        private const string Placeholder = "Nhập họ tên, số điện thoại hoặc số CCCD...";

        public frmKhachHang() { InitializeComponent(); }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            AppSession.Require(Feature.Customers);
            view = new DataView(AppSession.Data.Customers);
            dgvKhachHang.SelectionChanged += (s, a) => SelectRow();
            dgvKhachHang.DataSource = view;
            dgvKhachHang.MultiSelect = false;
            BridalFeatureUi.Placeholder(txtTimKiem, Placeholder);
            BridalFeatureUi.Wire(btnThemKhach, () => { ResetEditor(); dgvKhachHang.ClearSelection(); adding = true; txtMaKH.Text = InputRules.NextCode(AppSession.Data.Customers, "KH"); txtHoTen.Focus(); });
            BridalFeatureUi.Wire(btnLuu, Save);
            BridalFeatureUi.Wire(btnXoa, Delete);
            BridalFeatureUi.Wire(btnLamMoi, () => { txtTimKiem.Clear(); view.RowFilter = ""; dgvKhachHang.ClearSelection(); ResetEditor(); UpdateCount(); });
            BridalFeatureUi.Wire(btnTraCuu, Filter);
            txtTimKiem.KeyDown += (s, a) => { if (a.KeyCode == Keys.Enter) { a.SuppressKeyPress = true; BridalFeatureUi.Run(Filter); } };
            cboSapXep.SelectedIndexChanged += (s, a) =>
            {
                view.Sort = cboSapXep.SelectedIndex == 1 ? "[Họ và tên] ASC" : "[Mã KH] " + (cboSapXep.SelectedIndex == 2 ? "ASC" : "DESC");
                SelectRow();
            };
            cboSapXep.SelectedIndex = 0;
            Disposed += (s, a) => view.Dispose();
            SelectRow(); UpdateCount();
        }

        private void SelectRow()
        {
            var row = dgvKhachHang.SelectedRows.Count == 0 ? null : dgvKhachHang.SelectedRows[0].DataBoundItem as DataRowView;
            ResetEditor();
            if (row == null) return;
            selectedCode = Convert.ToString(row[0]);
            txtMaKH.Text = selectedCode;
            txtHoTen.Text = Convert.ToString(row["Họ và tên"]);
            txtDienThoai.Text = Convert.ToString(row["Số điện thoại"]);
            txtCCCD.Text = Convert.ToString(row["Số CCCD"]);
            txtDiaChi.Text = Convert.ToString(row["Địa chỉ"]);
        }

        private void ResetEditor()
        {
            selectedCode = null; adding = false;
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtDienThoai.Clear();
            txtCCCD.Clear();
            txtDiaChi.Clear();
        }

        private void Save()
        {
            if (!adding && selectedCode == null) throw new InvalidOperationException("Chọn khách hàng hoặc bấm Thêm mới trước khi lưu.");
            string code = catalog.SaveCustomer(adding ? null : selectedCode, txtHoTen.Text, txtDienThoai.Text, txtCCCD.Text, txtDiaChi.Text);
            view.RowFilter = "";
            SelectCode(code); UpdateCount();
        }

        private void SelectCode(string code)
        {
            foreach (DataGridViewRow row in dgvKhachHang.Rows)
            {
                var data = row.DataBoundItem as DataRowView;
                if (data != null && Convert.ToString(data[0]) == code) { dgvKhachHang.CurrentCell = row.Cells[0]; row.Selected = true; SelectRow(); return; }
            }
            ResetEditor();
        }

        private void Delete()
        {
            if (selectedCode == null) throw new InvalidOperationException("Chọn khách hàng cần xóa.");
            if (!BridalFeatureUi.Confirm("Xóa khách hàng " + selectedCode + "?")) return;
            catalog.Delete(Feature.Customers, selectedCode);
            SelectRow(); UpdateCount();
        }

        private void Filter()
        {
            view.RowFilter = InputRules.SearchFilter(txtTimKiem.Text == Placeholder ? "" : txtTimKiem.Text, "Họ và tên", "Số điện thoại", "Số CCCD");
            SelectRow(); UpdateCount();
        }

        private void UpdateCount() { lblTongCong.Text = "Tổng cộng: " + view.Count + " khách hàng"; }
    }
}
