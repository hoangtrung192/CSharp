using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LuxuryBridalStudio
{
    public partial class frmTraDo : Form
    {
        private readonly ReturnService service;
        public frmTraDo() : this(new List<RentalContract>()) { }
        public frmTraDo(List<RentalContract> data)
        {


            InitializeComponent();
            service = new ReturnService(data);
            Wire(btnAction1, CompleteReturn); Wire(btnAction2, RefreshRows);
            contracts.SelectedIndexChanged += (s, e) => UpdateSummary(); extra.ValueChanged += (s, e) => UpdateSummary(); returned.ValueChanged += (s, e) => UpdateSummary();
            RefreshRows();

        }
        private static void Wire(Button button, Action action)
        {
            button.Click += (sender, args) =>
            {
                try { action(); }
                catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "Kiểm tra dữ liệu"); }
            };
        }
        private void UpdateSummary()
        {
            var r = contracts.SelectedItem as RentalContract;
            summary.Text = r == null ? "Không có hợp đồng đang thuê" : "Cần thu: " + (r.TienThue + extra.Value - r.DaThu).ToString("N0") + " đ\nTrễ: " + Math.Max(0, (returned.Value.Date - r.HenTra).Days) + " ngày";
        }
        private void CompleteReturn()
        {
            var r = contracts.SelectedItem as RentalContract;
            BridalFeatureUi.Require(r != null && r.TrangThai == "Đang thuê", "Chọn hợp đồng đang thuê.");
            BridalFeatureUi.Require(returned.Value.Date >= r.NgayThue && returned.Value.Date <= DateTime.Today, "Ngày trả phải từ ngày thuê đến hôm nay.");
            BridalFeatureUi.Require(extra.Value == 0 || notes.Text.Trim().Length > 0, "Nhập lý do khi thu phụ phí.");
            decimal due = r.TienThue + extra.Value - r.DaThu;
            if (!BridalFeatureUi.Confirm("Xác nhận đã nhận đủ đồ và thu " + due.ToString("N0") + " đ cho " + r.Ma + "?")) return;
            service.Complete(r.Ma, returned.Value.Date, extra.Value, notes.Text);
            extra.Value = 0; notes.Clear(); RefreshRows();
        }
        private void RefreshRows()
        {
            contracts.DataSource = service.List(false); contracts.DisplayMember = "Ma";
            grid.DataSource = new BindingList<RentalContract>(service.List(true)); UpdateSummary();
        }
    }
}
