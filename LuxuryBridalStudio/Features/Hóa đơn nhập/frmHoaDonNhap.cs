using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmHoaDonNhap : Form
    {
        private readonly DataTable details = new DataTable();
        private readonly ReceiptService service = new ReceiptService(AppSession.Data);

        public frmHoaDonNhap() { InitializeComponent(); }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            AppSession.Require(Feature.Receipts);
            foreach (string name in new[] { "Mã SP", "Tên sản phẩm", "Loại", "Màu sắc" }) details.Columns.Add(name);
            details.Columns.Add("Số lượng", typeof(int));
            details.Columns.Add("Đơn giá nhập", typeof(decimal));
            details.Columns.Add("Thành tiền", typeof(decimal), "[Số lượng] * [Đơn giá nhập]");
            dgvChiTietNhap.DataSource = details;
            dgvChiTietNhap.ReadOnly = true;
            dgvChiTietNhap.AllowUserToAddRows = false;
            BridalFeatureUi.FormatGrid(dgvChiTietNhap);
            var delete = new DataGridViewButtonColumn { Name = "ThaoTac", HeaderText = "Thao tác", Text = "Xóa", UseColumnTextForButtonValue = true };
            dgvChiTietNhap.Columns.Add(delete);
            cboNhanVien.Items.Clear(); cboNhanVien.Items.Add(AppSession.User.DisplayName); cboNhanVien.SelectedIndex = 0; cboNhanVien.Enabled = false;
            BridalFeatureUi.BindPicker(cboNhaCungCap, AppSession.Data.Suppliers, "Tên nhà cung cấp");
            BridalFeatureUi.BindPicker(cboSanPham, AppSession.Data.Products, "Tên trang phục");
            dtpNgayNhap.MaxDate = DateTime.Today; dtpNgayNhap.Value = DateTime.Today;
            txtDonGiaNhap.Clear(); txtGhiChu.Clear();
            cboSanPham.SelectedIndexChanged += (s, a) =>
            {
                var row = cboSanPham.SelectedItem as DataRowView;
                txtDonGiaNhap.Text = row == null ? "" : InputRules.Money((decimal)row["Giá nhập"]);
            };
            nudSoLuong.Maximum = 1000000;
            nudSoLuong.ValueChanged += (s, a) => CalculateLine();
            txtDonGiaNhap.TextChanged += (s, a) => CalculateLine();
            BridalFeatureUi.Wire(btnThemVaoPhieu, AddLine);
            BridalFeatureUi.Wire(btnLuuNhapKho, Save);
            BridalFeatureUi.Wire(btnHuyPhieu, ResetWithConfirmation);
            BridalFeatureUi.Wire(btnLamMoiHeader, ResetWithConfirmation);
            BridalFeatureUi.Wire(btnThemNCCNhanh, () =>
            {
                using (var form = new frmNhaCungCap()) form.ShowDialog(this);
                BridalFeatureUi.BindPicker(cboNhaCungCap, AppSession.Data.Suppliers, "Tên nhà cung cấp");
            });
            BridalFeatureUi.Wire(btnDanhSachPhieu, ShowHistory);
            dgvChiTietNhap.CellContentClick += (s, a) =>
            {
                if (a.RowIndex < 0 || a.ColumnIndex < 0 || dgvChiTietNhap.Columns[a.ColumnIndex].Name != "ThaoTac") return;
                var row = dgvChiTietNhap.Rows[a.RowIndex].DataBoundItem as DataRowView;
                if (row != null && BridalFeatureUi.Confirm("Xóa " + row["Tên sản phẩm"] + " khỏi phiếu?"))
                {
                    details.Rows.Remove(row.Row); UpdateTotals();
                }
            };
            FormClosing += (s, a) => { if (details.Rows.Count > 0 && !BridalFeatureUi.Confirm("Phiếu chưa nhập kho. Bỏ nội dung đang nhập?")) a.Cancel = true; };
            Disposed += (s, a) => details.Dispose();
            ResetDraft();
        }

        private void CalculateLine()
        {
            try { txtThanhTien.Text = InputRules.Money(InputRules.ParseMoney(txtDonGiaNhap.Text) * nudSoLuong.Value); }
            catch (InvalidOperationException) { txtThanhTien.Text = "Đơn giá chưa hợp lệ"; }
        }

        private void UpdateTotals()
        {
            lblTongSoLuong.Text = "Tổng: " + details.Rows.Count + " dòng (" + details.AsEnumerable().Sum(x => x.Field<int>("Số lượng")) + " sản phẩm)";
            lblTongTienGiaTri.Text = InputRules.Money(details.AsEnumerable().Sum(x => x.Field<decimal>("Thành tiền")));
        }

        private void AddLine()
        {
            AppSession.Require(Feature.Receipts, true);
            var product = cboSanPham.SelectedItem as DataRowView;
            if (product == null) throw new InvalidOperationException("Chọn sản phẩm trong danh mục.");
            decimal cost = InputRules.ParseMoney(txtDonGiaNhap.Text);
            if (cost <= 0) throw new InvalidOperationException("Đơn giá nhập phải lớn hơn 0.");
            int count = (int)nudSoLuong.Value;
            if (cost * count + details.AsEnumerable().Sum(x => x.Field<decimal>("Thành tiền")) > 1000000000m)
                throw new InvalidOperationException("Tổng phiếu nhập tối đa 1 tỷ đồng.");
            var existing = details.AsEnumerable().FirstOrDefault(x => (string)x["Mã SP"] == (string)product[0] && (decimal)x["Đơn giá nhập"] == cost);
            if (existing == null) details.Rows.Add(product[0], product["Tên trang phục"], product["Loại"], product["Màu sắc"], count, cost);
            else existing["Số lượng"] = (int)existing["Số lượng"] + count;
            UpdateTotals();
        }

        private void ResetWithConfirmation()
        {
            if (details.Rows.Count == 0 || BridalFeatureUi.Confirm("Bỏ nội dung phiếu nhập đang soạn?")) ResetDraft();
        }

        private void ResetDraft()
        {
            details.Rows.Clear(); txtGhiChu.Clear(); nudSoLuong.Value = 1;
            txtMaHDN.Text = "HDN" + (AppSession.Data.Receipts.Count + 1).ToString("D4");
            dtpNgayNhap.Value = DateTime.Today; CalculateLine(); UpdateTotals();
        }

        private void Save()
        {
            if (details.Rows.Count == 0) throw new InvalidOperationException("Phiếu nhập chưa có sản phẩm.");
            if (cboNhaCungCap.SelectedValue == null) throw new InvalidOperationException("Chọn nhà cung cấp.");
            if (!BridalFeatureUi.Confirm("Nhập kho phiếu có giá trị " + lblTongTienGiaTri.Text + "?")) return;
            string code = service.Save(Convert.ToString(cboNhaCungCap.SelectedValue), dtpNgayNhap.Value,
                details.AsEnumerable().Select(x => new RentalLine { ProductCode = (string)x["Mã SP"], Quantity = (int)x["Số lượng"], UnitPrice = (decimal)x["Đơn giá nhập"] }), txtGhiChu.Text);
            ResetDraft();
            // Refresh catalog snapshots so the next draft uses the latest unit costs.
            BridalFeatureUi.BindPicker(cboSanPham, AppSession.Data.Products, "Tên trang phục");
            MessageBox.Show("Đã nhập kho " + code + " và cập nhật số lượng sản phẩm.", "Nhập kho");
        }

        private void ShowHistory()
        {
            AppSession.Require(Feature.Receipts);
            using (var form = new Form { Text = "Lịch sử nhập kho", Size = new Size(950, 550), StartPosition = FormStartPosition.CenterParent })
            using (var table = new DataTable())
            {
                foreach (string name in new[] { "Mã phiếu", "Nhà cung cấp", "Nhân viên", "Sản phẩm", "Ghi chú" }) table.Columns.Add(name);
                table.Columns.Add("Ngày", typeof(DateTime)); table.Columns.Add("Tổng tiền", typeof(decimal));
                foreach (var receipt in AppSession.Data.Receipts)
                    table.Rows.Add(receipt.Code, receipt.SupplierCode, receipt.Employee,
                        string.Join("; ", receipt.Lines.Select(x => x.ProductName + " × " + x.Quantity + " @ " + InputRules.Money(x.UnitPrice))), receipt.Notes, receipt.Date, receipt.Total);
                var history = new DataGridView { Dock = DockStyle.Fill, DataSource = table, ReadOnly = true, AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells };
                BridalFeatureUi.FormatGrid(history); form.Controls.Add(history); form.ShowDialog(this);
            }
        }
    }
}
