using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmTraDo : Form
    {
        private readonly ReturnService service;

        public frmTraDo() : this(AppSession.Data) { }

        public frmTraDo(StudioWorkspace data)
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppSession.Require(Feature.Returns);
            BridalFeatureUi.FormatGrid(grid);
            returned.MaxDate = DateTime.Today;
            returned.Value = DateTime.Today;
            notes.MaxLength = 1000;
            contracts.FormattingEnabled = true;
            contracts.Format += (s, e) =>
            {
                var rental = e.ListItem as RentalContract;
                if (rental != null) e.Value = rental.Ma + " · " + rental.KhachHang;
            };
            service = new ReturnService(data);
            BridalFeatureUi.Wire(btnAction1, CompleteReturn);
            BridalFeatureUi.Wire(btnAction2, RefreshRows);
            contracts.SelectedIndexChanged += (s, e) => UpdateSummary();
            extra.ValueChanged += (s, e) => UpdateSummary();
            returned.ValueChanged += (s, e) => UpdateSummary();
            RefreshRows();
        }

        private void UpdateSummary()
        {
            var rental = contracts.SelectedItem as RentalContract;
            btnAction1.Enabled = rental != null && rental.NgayThue <= DateTime.Today;
            btnAction1.BackColor = btnAction1.Enabled ? System.Drawing.Color.FromArgb(62, 39, 51) : System.Drawing.Color.Gainsboro;
            summary.Text = rental == null ? "Không có hợp đồng đang thuê"
                : "Cần thu: " + InputRules.Money(rental.TienThue + extra.Value - rental.DaThu)
                  + "\nTrễ: " + Math.Max(0, (returned.Value.Date - rental.HenTra).Days) + " ngày";
        }

        private void CompleteReturn()
        {
            var rental = contracts.SelectedItem as RentalContract;
            BridalFeatureUi.Require(rental != null, "Chọn hợp đồng đang thuê.");
            BridalFeatureUi.Require(returned.Value.Date >= rental.NgayThue && returned.Value.Date <= DateTime.Today, "Ngày trả phải từ ngày thuê đến hôm nay.");
            BridalFeatureUi.Require(extra.Value == 0 || notes.Text.Trim().Length > 0, "Nhập lý do khi thu phụ phí.");
            decimal due = rental.TienThue + extra.Value - rental.DaThu;
            if (!BridalFeatureUi.Confirm("Xác nhận đã nhận đủ đồ và thu " + InputRules.Money(due) + " cho " + rental.Ma + "?")) return;
            service.Complete(rental.Ma, returned.Value.Date, extra.Value, notes.Text);
            extra.Value = 0; notes.Clear(); RefreshRows();
        }

        private void RefreshRows()
        {
            contracts.DisplayMember = "Ma";
            contracts.DataSource = service.List(false);
            grid.DataSource = new BindingList<RentalContract>(service.List(true));
            UpdateSummary();
        }
    }
}
