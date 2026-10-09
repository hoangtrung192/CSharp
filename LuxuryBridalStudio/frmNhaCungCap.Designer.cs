namespace LuxuryBridalStudio
{
    partial class frmNhaCungCap
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnThemNCC = new System.Windows.Forms.Button();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.cboSapXep = new System.Windows.Forms.ComboBox();
            this.lblSapXep = new System.Windows.Forms.Label();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.panelList = new System.Windows.Forms.Panel();
            this.dgvNhaCungCap = new System.Windows.Forms.DataGridView();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.lblTenNCC = new System.Windows.Forms.Label();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.lblMaNCC = new System.Windows.Forms.Label();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.panelSearch.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý nhà cung cấp";
            // 
            // btnThemNCC
            // 
            this.btnThemNCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnThemNCC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemNCC.FlatAppearance.BorderSize = 0;
            this.btnThemNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNCC.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemNCC.ForeColor = System.Drawing.Color.White;
            this.btnThemNCC.Location = new System.Drawing.Point(814, 18);
            this.btnThemNCC.Name = "btnThemNCC";
            this.btnThemNCC.Size = new System.Drawing.Size(180, 38);
            this.btnThemNCC.TabIndex = 1;
            this.btnThemNCC.Text = "+ Thêm mới nhà cung cấp";
            this.btnThemNCC.UseVisualStyleBackColor = false;
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.Controls.Add(this.cboSapXep);
            this.panelSearch.Controls.Add(this.lblSapXep);
            this.panelSearch.Controls.Add(this.btnTraCuu);
            this.panelSearch.Controls.Add(this.txtTimKiem);
            this.panelSearch.Controls.Add(this.lblTimKiem);
            this.panelSearch.Location = new System.Drawing.Point(36, 70);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(958, 65);
            this.panelSearch.TabIndex = 2;
            // 
            // cboSapXep
            // 
            this.cboSapXep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSapXep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSapXep.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboSapXep.FormattingEnabled = true;
            this.cboSapXep.Items.AddRange(new object[] {
            "Mới nhất",
            "Tên (A-Z)",
            "Mã NCC"});
            this.cboSapXep.Location = new System.Drawing.Point(828, 23);
            this.cboSapXep.Name = "cboSapXep";
            this.cboSapXep.Size = new System.Drawing.Size(115, 25);
            this.cboSapXep.TabIndex = 4;
            // 
            // lblSapXep
            // 
            this.lblSapXep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSapXep.AutoSize = true;
            this.lblSapXep.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSapXep.ForeColor = System.Drawing.Color.Gray;
            this.lblSapXep.Location = new System.Drawing.Point(825, 6);
            this.lblSapXep.Name = "lblSapXep";
            this.lblSapXep.Size = new System.Drawing.Size(73, 15);
            this.lblSapXep.TabIndex = 3;
            this.lblSapXep.Text = "Sắp xếp theo";
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnTraCuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraCuu.FlatAppearance.BorderSize = 0;
            this.btnTraCuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraCuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTraCuu.ForeColor = System.Drawing.Color.White;
            this.btnTraCuu.Location = new System.Drawing.Point(400, 23);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(85, 25);
            this.btnTraCuu.TabIndex = 2;
            this.btnTraCuu.Text = "🔍 Tra cứu";
            this.btnTraCuu.UseVisualStyleBackColor = false;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.DimGray;
            this.txtTimKiem.Location = new System.Drawing.Point(15, 23);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(375, 24);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTimKiem.Location = new System.Drawing.Point(12, 6);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(130, 15);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm nhà cung cấp";
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelList.BackColor = System.Drawing.Color.White;
            this.panelList.Controls.Add(this.dgvNhaCungCap);
            this.panelList.Controls.Add(this.lblTongCong);
            this.panelList.Controls.Add(this.lblListTitle);
            this.panelList.Location = new System.Drawing.Point(36, 145);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(635, 530);
            this.panelList.TabIndex = 3;
            // 
            // dgvNhaCungCap
            // 
            this.dgvNhaCungCap.AllowUserToAddRows = false;
            this.dgvNhaCungCap.AllowUserToDeleteRows = false;
            this.dgvNhaCungCap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNhaCungCap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhaCungCap.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhaCungCap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhaCungCap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhaCungCap.Location = new System.Drawing.Point(15, 45);
            this.dgvNhaCungCap.Name = "dgvNhaCungCap";
            this.dgvNhaCungCap.ReadOnly = true;
            this.dgvNhaCungCap.RowHeadersVisible = false;
            this.dgvNhaCungCap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhaCungCap.Size = new System.Drawing.Size(605, 445);
            this.dgvNhaCungCap.TabIndex = 2;
            // 
            // lblTongCong
            // 
            this.lblTongCong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongCong.ForeColor = System.Drawing.Color.Gray;
            this.lblTongCong.Location = new System.Drawing.Point(15, 502);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(139, 15);
            this.lblTongCong.TabIndex = 1;
            this.lblTongCong.Text = "Tổng cộng: 5 nhà cung cấp";
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.Location = new System.Drawing.Point(12, 12);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(164, 19);
            this.lblListTitle.TabIndex = 0;
            this.lblListTitle.Text = "Danh sách nhà cung cấp";
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.BackColor = System.Drawing.Color.White;
            this.panelDetail.Controls.Add(this.txtGhiChu);
            this.panelDetail.Controls.Add(this.lblGhiChu);
            this.panelDetail.Controls.Add(this.btnLamMoi);
            this.panelDetail.Controls.Add(this.btnXoa);
            this.panelDetail.Controls.Add(this.btnLuu);
            this.panelDetail.Controls.Add(this.txtDiaChi);
            this.panelDetail.Controls.Add(this.lblDiaChi);
            this.panelDetail.Controls.Add(this.txtDienThoai);
            this.panelDetail.Controls.Add(this.lblDienThoai);
            this.panelDetail.Controls.Add(this.txtTenNCC);
            this.panelDetail.Controls.Add(this.lblTenNCC);
            this.panelDetail.Controls.Add(this.txtMaNCC);
            this.panelDetail.Controls.Add(this.lblMaNCC);
            this.panelDetail.Controls.Add(this.lblDetailTitle);
            this.panelDetail.Location = new System.Drawing.Point(685, 145);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(309, 530);
            this.panelDetail.TabIndex = 4;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Location = new System.Drawing.Point(12, 12);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(170, 19);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "Thông tin chi tiết nhà cung cấp";
            // 
            // lblMaNCC
            // 
            this.lblMaNCC.AutoSize = true;
            this.lblMaNCC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaNCC.ForeColor = System.Drawing.Color.DimGray;
            this.lblMaNCC.Location = new System.Drawing.Point(12, 40);
            this.lblMaNCC.Name = "lblMaNCC";
            this.lblMaNCC.Size = new System.Drawing.Size(98, 15);
            this.lblMaNCC.TabIndex = 1;
            this.lblMaNCC.Text = "Mã nhà cung cấp";
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMaNCC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaNCC.Location = new System.Drawing.Point(15, 58);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.ReadOnly = true;
            this.txtMaNCC.Size = new System.Drawing.Size(278, 24);
            this.txtMaNCC.TabIndex = 2;
            this.txtMaNCC.Text = "NCC001";
            // 
            // lblTenNCC
            // 
            this.lblTenNCC.AutoSize = true;
            this.lblTenNCC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenNCC.ForeColor = System.Drawing.Color.DimGray;
            this.lblTenNCC.Location = new System.Drawing.Point(12, 94);
            this.lblTenNCC.Name = "lblTenNCC";
            this.lblTenNCC.Size = new System.Drawing.Size(108, 15);
            this.lblTenNCC.TabIndex = 3;
            this.lblTenNCC.Text = "Tên nhà cung cấp *";
            // 
            // txtTenNCC
            // 
            this.txtTenNCC.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTenNCC.Location = new System.Drawing.Point(15, 112);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(278, 24);
            this.txtTenNCC.TabIndex = 4;
            this.txtTenNCC.Text = "Xưởng May Bella Bridal";
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDienThoai.ForeColor = System.Drawing.Color.DimGray;
            this.lblDienThoai.Location = new System.Drawing.Point(12, 148);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(84, 15);
            this.lblDienThoai.TabIndex = 5;
            this.lblDienThoai.Text = "Số điện thoại *";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDienThoai.Location = new System.Drawing.Point(15, 166);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(278, 24);
            this.txtDienThoai.TabIndex = 6;
            this.txtDienThoai.Text = "024 3822 5678";
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.DimGray;
            this.lblDiaChi.Location = new System.Drawing.Point(12, 202);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(51, 15);
            this.lblDiaChi.TabIndex = 7;
            this.lblDiaChi.Text = "Địa chỉ *";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDiaChi.Location = new System.Drawing.Point(15, 220);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(278, 24);
            this.txtDiaChi.TabIndex = 8;
            this.txtDiaChi.Text = "102 Bà Triệu, Hoàn Kiếm, Hà Nội";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            this.lblGhiChu.Location = new System.Drawing.Point(12, 256);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(48, 15);
            this.lblGhiChu.TabIndex = 14;
            this.lblGhiChu.Text = "Ghi chú";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGhiChu.Location = new System.Drawing.Point(15, 274);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(278, 80);
            this.txtGhiChu.TabIndex = 15;
            this.txtGhiChu.Text = "Đối tác cung cấp dòng váy cưới ren cao cấp";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(15, 372);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 32);
            this.btnLuu.TabIndex = 11;
            this.btnLuu.Text = "Lưu thay đổi";
            this.btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnXoa.Location = new System.Drawing.Point(106, 372);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(105, 32);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "Xóa nhà cung cấp";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLamMoi.Location = new System.Drawing.Point(217, 372);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(76, 32);
            this.btnLamMoi.TabIndex = 13;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // frmNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1024, 700);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.btnThemNCC);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmNhaCungCap";
            this.Text = "Quản lý nhà cung cấp";
            this.Load += new System.EventHandler(this.frmNhaCungCap_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnThemNCC;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.ComboBox cboSapXep;
        private System.Windows.Forms.Label lblSapXep;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.DataGridView dgvNhaCungCap;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.Label lblMaNCC;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.Label lblTenNCC;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}