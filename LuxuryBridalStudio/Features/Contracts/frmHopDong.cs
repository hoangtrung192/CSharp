using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LuxuryBridalStudio
{
    public partial class frmHopDong : Form
    {
        private readonly ContractService service;
        public frmHopDong() : this(new List<RentalContract>()) { }
        public frmHopDong(List<RentalContract> data)
        {


            InitializeComponent();
            service = new ContractService(data);
            Wire(btnAction1, CreateRental);
            Wire(btnAction2, () => { customer.Clear(); product.Clear(); price.Value = paid.Value = 0; start.Value = end.Value = DateTime.Today; });
            Wire(btnAction3, CancelRental);
            Wire(btnAction4, RefreshRows);
            search.TextChanged += (s, e) => RefreshRows(); RefreshRows();

        }
        private static void Wire(Button button, Action action)
        {
            button.Click += (sender, args) =>
            {
                try { action(); }
                catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "Kiểm tra dữ liệu"); }
            };
        }
        private void CreateRental()
        {
            BridalFeatureUi.Require(customer.Text.Trim().Length > 0 && product.Text.Trim().Length > 0, "Nhập khách hàng và sản phẩm.");
            BridalFeatureUi.Require(end.Value.Date >= start.Value.Date, "Ngày hẹn trả phải từ ngày thuê trở đi.");
            BridalFeatureUi.Require(price.Value > 0 && paid.Value <= price.Value, "Tiền thuê phải lớn hơn 0; thu trước không vượt tiền thuê.");
            service.Create(customer.Text, product.Text, start.Value, end.Value, price.Value, paid.Value);
            RefreshRows(); MessageBox.Show("Đã lập hợp đồng. Mở Trả đồ để xử lý hợp đồng này.");
        }
        private void CancelRental()
        {
            var rental = grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as RentalContract;
            BridalFeatureUi.Require(rental != null, "Chọn một hợp đồng.");
            BridalFeatureUi.Require(rental.TrangThai == "Đang thuê", "Chỉ hủy hợp đồng đang thuê.");
            BridalFeatureUi.Require(rental.DaThu == 0, "Hợp đồng đã thu tiền: cần nghiệp vụ hoàn tiền, chưa hỗ trợ hủy.");
            if (!BridalFeatureUi.Confirm("Hủy hợp đồng " + rental.Ma + "?")) return;
            service.Cancel(rental.Ma); RefreshRows();
        }
        private void RefreshRows()
        {
            string q = search.Text.Trim(); grid.DataSource = new BindingList<RentalContract>(service.Search(q));
        }
    }
}
