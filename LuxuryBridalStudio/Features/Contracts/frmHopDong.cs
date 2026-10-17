using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmHopDong : Form
    {
        private readonly ContractService service;
        private readonly List<RentalLine> draft = new List<RentalLine>();
        private readonly NumericUpDown quantity = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 1 };
        private readonly DataGridView items = new DataGridView { ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false, BackgroundColor = Color.White };

        public frmHopDong()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppSession.Require(Feature.Contracts);
            service = new ContractService(AppSession.Data);
            BridalFeatureUi.BindPicker(customer, AppSession.Data.Customers, "Họ và tên");
            BridalFeatureUi.BindPicker(product, AppSession.Data.Products, "Tên trang phục");
            BridalFeatureUi.FormatGrid(grid); BridalFeatureUi.FormatGrid(items);
            start.Value = end.Value = DateTime.Today;
            start.MinDate = DateTime.Today;
            price.ReadOnly = true; price.Controls[0].Enabled = false; price.TabStop = false;
            lblField4.Text = "Tổng tiền thuê";
            var label = new Label { Text = "Số lượng", Location = new Point(544, 260), Size = new Size(125, 30) };
            quantity.Location = new Point(679, 255); quantity.Size = new Size(65, 32);
            var add = new Button { Text = "Thêm mặt hàng", Location = new Point(754, 253), Size = new Size(140, 36) };
            var remove = new Button { Text = "Bỏ mặt hàng", Location = new Point(904, 253), Size = new Size(125, 36) };
            items.Location = new Point(24, 300); items.Size = new Size(1052, 105);
            items.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.AddRange(new Control[] { label, quantity, add, remove, items });
            foreach (var button in new[] { btnAction1, btnAction2, btnAction3, btnAction4 }) button.Top = 420;
            grid.Top = 475; grid.Height = ClientSize.Height - 495;
            BridalFeatureUi.Wire(add, AddItem);
            BridalFeatureUi.Wire(remove, () =>
            {
                var line = items.CurrentRow == null ? null : items.CurrentRow.DataBoundItem as RentalLine;
                if (line != null) draft.Remove(line);
                RefreshCart();
            });
            BridalFeatureUi.Wire(btnAction1, CreateRental);
            BridalFeatureUi.Wire(btnAction2, () => { if (draft.Count == 0 || BridalFeatureUi.Confirm("Xóa nội dung hợp đồng đang nhập?")) ResetDraft(); });
            BridalFeatureUi.Wire(btnAction3, CancelRental);
            BridalFeatureUi.Wire(btnAction4, RefreshRows);
            search.TextChanged += (s, e) => RefreshRows();
            FormClosing += (s, e) => { if (draft.Count > 0 && !BridalFeatureUi.Confirm("Hợp đồng chưa lập. Bỏ nội dung đang nhập?")) e.Cancel = true; };
            RefreshRows(); RefreshCart();
        }

        private void AddItem()
        {
            var row = product.SelectedItem as DataRowView;
            if (row == null) throw new InvalidOperationException("Chọn sản phẩm trong danh mục.");
            if (end.Value.Date < start.Value.Date) throw new InvalidOperationException("Ngày hẹn trả không được trước ngày thuê.");
            string code = Convert.ToString(row[0]);
            var existing = draft.SingleOrDefault(x => x.ProductCode == code);
            int count = (existing == null ? 0 : existing.Quantity) + (int)quantity.Value;
            if (count > AppSession.Data.Available(code, start.Value, end.Value)) throw new InvalidOperationException("Không đủ số lượng trong khoảng thuê này.");
            decimal nextTotal = draft.Where(x => x != existing).Sum(x => x.Total) + count * (decimal)row["Giá thuê"];
            if (nextTotal > price.Maximum) throw new InvalidOperationException("Tổng tiền hợp đồng tối đa 1 tỷ đồng.");
            if (existing == null) draft.Add(new RentalLine { ProductCode = code, ProductName = (string)row["Tên trang phục"], Category = (string)row["Loại"], Quantity = count, UnitPrice = (decimal)row["Giá thuê"] });
            else existing.Quantity = count;
            RefreshCart();
        }

        private void RefreshCart()
        {
            items.DataSource = new BindingList<RentalLine>(draft);
            price.Value = draft.Sum(x => x.Total);
        }

        private void ResetDraft()
        {
            draft.Clear(); customer.SelectedIndex = product.SelectedIndex = -1;
            paid.Value = 0; start.Value = end.Value = DateTime.Today; quantity.Value = 1;
            RefreshCart();
        }

        private void CreateRental()
        {
            if (customer.SelectedValue == null) throw new InvalidOperationException("Chọn khách hàng trong danh mục.");
            if (draft.Count == 0) throw new InvalidOperationException("Thêm ít nhất một sản phẩm vào hợp đồng.");
            if (!BridalFeatureUi.Confirm("Lập hợp đồng " + InputRules.Money(price.Value) + ", thu trước " + InputRules.Money(paid.Value) + "?")) return;
            string code = service.Create(Convert.ToString(customer.SelectedValue), draft, start.Value, end.Value, paid.Value);
            ResetDraft(); search.Clear(); RefreshRows();
            MessageBox.Show("Đã lập " + code + ". Mở Trả đồ để xử lý hợp đồng này.", "Hợp đồng");
        }

        private void CancelRental()
        {
            var rental = grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as RentalContract;
            if (rental == null) throw new InvalidOperationException("Chọn hợp đồng cần hủy.");
            string reason = "";
            if (rental.DaThu > 0)
            {
                if (AppSession.User.Role != Roles.Manager) throw new InvalidOperationException("Hợp đồng đã thu tiền cần quản lý xác nhận hoàn tiền.");
                reason = BridalFeatureUi.AskReason(this, "Lý do hủy và hoàn tiền");
                if (reason == null) return;
                InputRules.Required(reason, "Lý do hoàn tiền", 1000);
            }
            if (!BridalFeatureUi.Confirm("Hủy hợp đồng " + rental.Ma + (rental.DaThu > 0 ? " và xác nhận đã hoàn " + InputRules.Money(rental.DaThu) + " cho khách?" : "?"))) return;
            service.Cancel(rental.Ma, reason); RefreshRows();
        }

        private void RefreshRows() { grid.DataSource = new BindingList<RentalContract>(service.Search(search.Text.Trim())); }
    }
}
