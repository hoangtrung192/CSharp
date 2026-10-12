using System;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    partial class frmNhanSu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.heading = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.TextBox();
            this.phone = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.TextBox();
            this.role = new System.Windows.Forms.ComboBox();
            this.active = new System.Windows.Forms.CheckBox();

            this.lblField0 = new System.Windows.Forms.Label();
            this.lblField1 = new System.Windows.Forms.Label();
            this.lblField2 = new System.Windows.Forms.Label();
            this.lblField3 = new System.Windows.Forms.Label();
            this.lblField4 = new System.Windows.Forms.Label();

            this.btnAction1 = new System.Windows.Forms.Button();
            this.btnAction2 = new System.Windows.Forms.Button();
            this.btnAction3 = new System.Windows.Forms.Button();
            this.btnAction4 = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();

            // heading
            this.heading.Name = "heading";
            this.heading.Location = new Point(24, 20);
            this.heading.Size = new Size(1000, 48);
            this.heading.Text = "QUẢN LÝ NHÂN SỰ";
            this.heading.Font =
                new Font("Segoe UI", 20F, FontStyle.Bold);
            this.heading.ForeColor = Color.FromArgb(62, 39, 51);

            // Họ tên
            this.lblField0.Name = "lblField0";
            this.lblField0.Text = "Họ tên";
            this.lblField0.Location = new Point(24, 95);
            this.lblField0.Size = new Size(130, 40);

            this.name.Name = "name";
            this.name.Location = new Point(159, 90);
            this.name.Size = new Size(350, 40);
            this.name.TabIndex = 0;

            // Điện thoại
            this.lblField1.Name = "lblField1";
            this.lblField1.Text = "Điện thoại";
            this.lblField1.Location = new Point(544, 95);
            this.lblField1.Size = new Size(130, 40);

            this.phone.Name = "phone";
            this.phone.Location = new Point(679, 90);
            this.phone.Size = new Size(350, 40);
            this.phone.TabIndex = 1;

            // Vai trò
            this.lblField2.Name = "lblField2";
            this.lblField2.Text = "Vai trò";
            this.lblField2.Location = new Point(24, 150);
            this.lblField2.Size = new Size(130, 40);

            this.role.Name = "role";
            this.role.Location = new Point(159, 145);
            this.role.Size = new Size(350, 40);
            this.role.DropDownStyle = ComboBoxStyle.DropDownList;
            this.role.TabIndex = 2;

            // Trạng thái
            this.lblField3.Name = "lblField3";
            this.lblField3.Text = "Trạng thái";
            this.lblField3.Location = new Point(544, 150);
            this.lblField3.Size = new Size(130, 40);

            this.active.Name = "active";
            this.active.Location = new Point(679, 145);
            this.active.Size = new Size(350, 40);
            this.active.Text = "Đang làm việc";
            this.active.Checked = true;
            this.active.TabIndex = 3;

            // Tìm kiếm
            this.lblField4.Name = "lblField4";
            this.lblField4.Text = "Tìm mã / tên";
            this.lblField4.Location = new Point(24, 205);
            this.lblField4.Size = new Size(130, 40);

            this.search.Name = "search";
            this.search.Location = new Point(159, 200);
            this.search.Size = new Size(350, 40);
            this.search.TabIndex = 4;

            // Thêm mới
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Text = "Thêm mới";
            this.btnAction1.Location = new Point(24, 320);
            this.btnAction1.Size = new Size(220, 40);
            this.btnAction1.BackColor = Color.FromArgb(62, 39, 51);
            this.btnAction1.ForeColor = Color.White;
            this.btnAction1.FlatStyle = FlatStyle.Flat;
            this.btnAction1.TabIndex = 5;
            this.btnAction1.UseVisualStyleBackColor = false;

            // Lưu thay đổi
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Text = "Lưu thay đổi";
            this.btnAction2.Location = new Point(254, 320);
            this.btnAction2.Size = new Size(220, 40);
            this.btnAction2.BackColor = Color.FromArgb(62, 39, 51);
            this.btnAction2.ForeColor = Color.White;
            this.btnAction2.FlatStyle = FlatStyle.Flat;
            this.btnAction2.TabIndex = 6;
            this.btnAction2.UseVisualStyleBackColor = false;

            // Ngừng làm việc
            this.btnAction3.Name = "btnAction3";
            this.btnAction3.Text = "Ngừng làm việc";
            this.btnAction3.Location = new Point(484, 320);
            this.btnAction3.Size = new Size(220, 40);
            this.btnAction3.BackColor = Color.FromArgb(62, 39, 51);
            this.btnAction3.ForeColor = Color.White;
            this.btnAction3.FlatStyle = FlatStyle.Flat;
            this.btnAction3.TabIndex = 7;
            this.btnAction3.UseVisualStyleBackColor = false;

            // Xóa ô nhập
            this.btnAction4.Name = "btnAction4";
            this.btnAction4.Text = "Xóa ô nhập";
            this.btnAction4.Location = new Point(714, 320);
            this.btnAction4.Size = new Size(220, 40);
            this.btnAction4.BackColor = Color.FromArgb(62, 39, 51);
            this.btnAction4.ForeColor = Color.White;
            this.btnAction4.FlatStyle = FlatStyle.Flat;
            this.btnAction4.TabIndex = 8;
            this.btnAction4.UseVisualStyleBackColor = false;

            // Bảng nhân sự
            this.grid.Name = "grid";
            this.grid.Location = new Point(24, 380);
            this.grid.Size = new Size(1052, 340);
            this.grid.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.MultiSelect = false;
            this.grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            this.grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grid.RowHeadersVisible = false;
            this.grid.BackgroundColor = Color.White;
            this.grid.TabIndex = 9;

            // Form
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(1100, 744);
            this.AutoScroll = true;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(250, 249, 246);
            this.Name = "frmNhanSu";
            this.Text = "QUẢN LÝ NHÂN SỰ";

            this.Controls.Add(this.heading);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.name);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.search);
            this.Controls.Add(this.role);
            this.Controls.Add(this.active);
            this.Controls.Add(this.lblField0);
            this.Controls.Add(this.lblField1);
            this.Controls.Add(this.lblField2);
            this.Controls.Add(this.lblField3);
            this.Controls.Add(this.lblField4);
            this.Controls.Add(this.btnAction1);
            this.Controls.Add(this.btnAction2);
            this.Controls.Add(this.btnAction3);
            this.Controls.Add(this.btnAction4);

            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label heading;
        private System.Windows.Forms.DataGridView grid;

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.TextBox search;

        private System.Windows.Forms.ComboBox role;
        private System.Windows.Forms.CheckBox active;

        private System.Windows.Forms.Label lblField0;
        private System.Windows.Forms.Label lblField1;
        private System.Windows.Forms.Label lblField2;
        private System.Windows.Forms.Label lblField3;
        private System.Windows.Forms.Label lblField4;

        private System.Windows.Forms.Button btnAction1;
        private System.Windows.Forms.Button btnAction2;
        private System.Windows.Forms.Button btnAction3;
        private System.Windows.Forms.Button btnAction4;
    }
}