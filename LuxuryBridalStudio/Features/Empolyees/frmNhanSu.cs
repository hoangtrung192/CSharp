using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace LuxuryBridalStudio
{
    public partial class frmNhanSu : Form
    {
        private readonly EmployeeService service;
        private StaffMember selected;
        public frmNhanSu() : this(AppSession.Data.Employees) { }
        public frmNhanSu(List<StaffMember> data)
        {


            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AppSession.Require(Feature.Employees);
            BridalFeatureUi.FormatGrid(grid);
            service = new EmployeeService(data);
            role.Items.AddRange(new object[] { "Quản lý", "Nhân viên", "Thủ kho" }); role.SelectedIndex = 1;
            Wire(btnAction1, () => SaveEmployee(true)); Wire(btnAction2, () => SaveEmployee(false));
            Wire(btnAction3, () => { BridalFeatureUi.Require(selected != null, "Chọn nhân viên."); if (BridalFeatureUi.Confirm("Đánh dấu nhân viên nghỉ việc?")) { service.Deactivate(selected.Ma); RefreshRows(); } });
            Wire(btnAction4, () => { grid.ClearSelection(); selected = null; name.Clear(); phone.Clear(); role.SelectedIndex = 1; active.Checked = true; });
            grid.SelectionChanged += (s, e) => { selected = grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem as StaffMember; if (selected != null) { name.Text = selected.HoTen; phone.Text = selected.DienThoai; role.SelectedItem = selected.VaiTro; active.Checked = selected.DangLam; } };
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
        private void SaveEmployee(bool add)
        {
            BridalFeatureUi.Require(add || selected != null, "Chọn nhân viên cần sửa.");
            service.Save(add ? null : selected.Ma, name.Text, phone.Text, role.Text, active.Checked);
            RefreshRows();
        }
        private void RefreshRows()
        {
            string q = search.Text.Trim(); grid.DataSource = new BindingList<StaffMember>(service.Search(q));
        }
    }
}
