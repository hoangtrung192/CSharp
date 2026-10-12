using System;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    partial class frmTraDo
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
            this.contracts = new System.Windows.Forms.ComboBox();
            this.returned = new System.Windows.Forms.DateTimePicker();
            this.extra = new System.Windows.Forms.NumericUpDown();
            this.notes = new System.Windows.Forms.TextBox();
            this.summary = new System.Windows.Forms.Label();

            this.lblField0 = new System.Windows.Forms.Label();
            this.lblField1 = new System.Windows.Forms.Label();
            this.lblField2 = new System.Windows.Forms.Label();
            this.lblField3 = new System.Windows.Forms.Label();
            this.lblField4 = new System.Windows.Forms.Label();

            this.btnAction1 = new System.Windows.Forms.Button();
            this.btnAction2 = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.extra)).BeginInit();
            this.SuspendLayout();

            // Tiêu đề
            this.heading.Name = "heading";
            this.heading.Location = new Point(24, 20);
            this.heading.Size = new Size(1000, 48);
            this.heading.Text = "TRẢ ĐỒ & THANH TOÁN";
            this.heading.Font =
                new Font("Segoe UI", 20F, FontStyle.Bold);
            this.heading.ForeColor = Color.FromArgb(62, 39, 51);

            // Hợp đồng
            this.lblField0.Name = "lblField0";
            this.lblField0.Text = "Hợp đồng";
            this.lblField0.Location = new Point(24, 95);
            this.lblField0.Size = new Size(130, 40);

            this.contracts.Name = "contracts";
            this.contracts.Location = new Point(159, 90);
            this.contracts.Size = new Size(350, 40);
            this.contracts.DropDownStyle = ComboBoxStyle.DropDownList;
            this.contracts.TabIndex = 0;

            // Ngày trả thực tế
            this.lblField1.Name = "lblField1";
            this.lblField1.Text = "Ngày trả thực tế";
            this.lblField1.Location = new Point(544, 95);
            this.lblField1.Size = new Size(130, 40);

            this.returned.Name = "returned";
            this.returned.Location = new Point(679, 90);
            this.returned.Size = new Size(350, 40);
            this.returned.Format = DateTimePickerFormat.Custom;
            this.returned.CustomFormat = "dd/MM/yyyy";
            this.returned.TabIndex = 1;

            // Phụ phí
            this.lblField2.Name = "lblField2";
            this.lblField2.Text = "Phụ phí";
            this.lblField2.Location = new Point(24, 150);
            this.lblField2.Size = new Size(130, 40);

            this.extra.Name = "extra";
            this.extra.Location = new Point(159, 145);
            this.extra.Size = new Size(350, 40);
            this.extra.Minimum = 0M;
            this.extra.Maximum = 1000000000M;
            this.extra.Increment = 100000M;
            this.extra.ThousandsSeparator = true;
            this.extra.TabIndex = 2;

            // Ghi chú
            this.lblField3.Name = "lblField3";
            this.lblField3.Text = "Ghi chú / lý do";
            this.lblField3.Location = new Point(544, 150);
            this.lblField3.Size = new Size(130, 40);

            this.notes.Name = "notes";
            this.notes.Location = new Point(679, 145);
            this.notes.Size = new Size(350, 40);
            this.notes.TabIndex = 3;

            // Quyết toán
            this.lblField4.Name = "lblField4";
            this.lblField4.Text = "Quyết toán";
            this.lblField4.Location = new Point(24, 205);
            this.lblField4.Size = new Size(130, 40);

            this.summary.Name = "summary";
            this.summary.Location = new Point(159, 200);
            this.summary.Size = new Size(870, 70);
            this.summary.AutoSize = false;
            this.summary.ForeColor = Color.FromArgb(62, 39, 51);
            this.summary.Text = "Chọn hợp đồng để xem quyết toán.";

            // Xác nhận trả
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Text = "Xác nhận trả & thu đủ";
            this.btnAction1.Location = new Point(24, 320);
            this.btnAction1.Size = new Size(220, 40);
            this.btnAction1.BackColor = Color.FromArgb(62, 39, 51);
            this.btnAction1.ForeColor = Color.White;
            this.btnAction1.FlatStyle = FlatStyle.Flat;
            this.btnAction1.UseVisualStyleBackColor = false;
            this.btnAction1.TabIndex = 4;

            // Làm mới
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Text = "Làm mới";
            this.btnAction2.Location = new Point(254, 320);
            this.btnAction2.Size = new Size(220, 40);
            this.btnAction2.BackColor = Color.FromArgb(62, 39, 51);
            this.btnAction2.ForeColor = Color.White;
            this.btnAction2.FlatStyle = FlatStyle.Flat;
            this.btnAction2.UseVisualStyleBackColor = false;
            this.btnAction2.TabIndex = 5;

            // Bảng lịch sử trả đồ
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
            this.grid.TabIndex = 6;

            // Form
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(1100, 744);
            this.AutoScroll = true;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(250, 249, 246);
            this.Name = "frmTraDo";
            this.Text = "TRẢ ĐỒ & THANH TOÁN";

            this.Controls.Add(this.heading);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.contracts);
            this.Controls.Add(this.returned);
            this.Controls.Add(this.extra);
            this.Controls.Add(this.notes);
            this.Controls.Add(this.summary);
            this.Controls.Add(this.lblField0);
            this.Controls.Add(this.lblField1);
            this.Controls.Add(this.lblField2);
            this.Controls.Add(this.lblField3);
            this.Controls.Add(this.lblField4);
            this.Controls.Add(this.btnAction1);
            this.Controls.Add(this.btnAction2);

            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.extra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label heading;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ComboBox contracts;
        private System.Windows.Forms.DateTimePicker returned;
        private System.Windows.Forms.NumericUpDown extra;
        private System.Windows.Forms.TextBox notes;
        private System.Windows.Forms.Label summary;

        private System.Windows.Forms.Label lblField0;
        private System.Windows.Forms.Label lblField1;
        private System.Windows.Forms.Label lblField2;
        private System.Windows.Forms.Label lblField3;
        private System.Windows.Forms.Label lblField4;

        private System.Windows.Forms.Button btnAction1;
        private System.Windows.Forms.Button btnAction2;
    }
}