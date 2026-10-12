namespace LuxuryBridalStudio
{
    partial class frmSanPham
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
            this.btnThemSP = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.cboNoiSX = new System.Windows.Forms.ComboBox();
            this.lblNoiSX = new System.Windows.Forms.Label();
            this.cboMauSac = new System.Windows.Forms.ComboBox();
            this.lblMauSac = new System.Windows.Forms.Label();
            this.cboLoaiSP = new System.Windows.Forms.ComboBox();
            this.lblLoaiSP = new System.Windows.Forms.Label();
            this.panelList = new System.Windows.Forms.Panel();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtGiaThue = new System.Windows.Forms.TextBox();
            this.lblGiaThue = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.nudSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.lblTonKhoDetail = new System.Windows.Forms.Label();
            this.cboNoiSXDetail = new System.Windows.Forms.ComboBox();
            this.lblNoiSXDetail = new System.Windows.Forms.Label();
            this.cboMauSacDetail = new System.Windows.Forms.ComboBox();
            this.lblMauSacDetail = new System.Windows.Forms.Label();
            this.cboLoaiSPDetail = new System.Windows.Forms.ComboBox();
            this.lblLoaiSPDetail = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.btnChonAnh = new System.Windows.Forms.LinkLabel();
            this.picAnhSP = new System.Windows.Forms.PictureBox();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.panelFilter.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhSP)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(288, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý sản phẩm kho";
            // 
            // btnThemSP
            // 
            this.btnThemSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnThemSP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemSP.FlatAppearance.BorderSize = 0;
            this.btnThemSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemSP.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemSP.ForeColor = System.Drawing.Color.White;
            this.btnThemSP.Location = new System.Drawing.Point(824, 18);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(170, 38);
            this.btnThemSP.TabIndex = 1;
            this.btnThemSP.Text = "+ Thêm mới sản phẩm";
            this.btnThemSP.UseVisualStyleBackColor = false;
            // 
            // panelFilter
            // 
            this.panelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.btnTraCuu);
            this.panelFilter.Controls.Add(this.txtTuKhoa);
            this.panelFilter.Controls.Add(this.lblTuKhoa);
            this.panelFilter.Controls.Add(this.cboNoiSX);
            this.panelFilter.Controls.Add(this.lblNoiSX);
            this.panelFilter.Controls.Add(this.cboMauSac);
            this.panelFilter.Controls.Add(this.lblMauSac);
            this.panelFilter.Controls.Add(this.cboLoaiSP);
            this.panelFilter.Controls.Add(this.lblLoaiSP);
            this.panelFilter.Location = new System.Drawing.Point(36, 68);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(958, 62);
            this.panelFilter.TabIndex = 2;
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnTraCuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraCuu.FlatAppearance.BorderSize = 0;
            this.btnTraCuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraCuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTraCuu.ForeColor = System.Drawing.Color.White;
            this.btnTraCuu.Location = new System.Drawing.Point(740, 22);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(35, 25);
            this.btnTraCuu.TabIndex = 8;
            this.btnTraCuu.Text = "🔍";
            this.btnTraCuu.UseVisualStyleBackColor = false;
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTuKhoa.ForeColor = System.Drawing.Color.DimGray;
            this.txtTuKhoa.Location = new System.Drawing.Point(460, 22);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(275, 24);
            this.txtTuKhoa.TabIndex = 7;
            this.txtTuKhoa.Text = "Nhập tên váy cưới, vest...";
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTuKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTuKhoa.Location = new System.Drawing.Point(457, 5);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(51, 15);
            this.lblTuKhoa.TabIndex = 6;
            this.lblTuKhoa.Text = "Từ khóa";
            // 
            // cboNoiSX
            // 
            this.cboNoiSX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNoiSX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNoiSX.FormattingEnabled = true;
            this.cboNoiSX.Items.AddRange(new object[] {
            "Tất cả",
            "Việt Nam",
            "Hàn Quốc",
            "Pháp"});
            this.cboNoiSX.Location = new System.Drawing.Point(310, 23);
            this.cboNoiSX.Name = "cboNoiSX";
            this.cboNoiSX.Size = new System.Drawing.Size(125, 23);
            this.cboNoiSX.TabIndex = 5;
            // 
            // lblNoiSX
            // 
            this.lblNoiSX.AutoSize = true;
            this.lblNoiSX.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNoiSX.ForeColor = System.Drawing.Color.Gray;
            this.lblNoiSX.Location = new System.Drawing.Point(307, 5);
            this.lblNoiSX.Name = "lblNoiSX";
            this.lblNoiSX.Size = new System.Drawing.Size(70, 15);
            this.lblNoiSX.TabIndex = 4;
            this.lblNoiSX.Text = "Nơi sản xuất";
            // 
            // cboMauSac
            // 
            this.cboMauSac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMauSac.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboMauSac.FormattingEnabled = true;
            this.cboMauSac.Items.AddRange(new object[] {
            "Tất cả",
            "Trắng",
            "Trắng kem",
            "Đen",
            "Đỏ thẫm"});
            this.cboMauSac.Location = new System.Drawing.Point(165, 23);
            this.cboMauSac.Name = "cboMauSac";
            this.cboMauSac.Size = new System.Drawing.Size(125, 23);
            this.cboMauSac.TabIndex = 3;
            // 
            // lblMauSac
            // 
            this.lblMauSac.AutoSize = true;
            this.lblMauSac.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMauSac.ForeColor = System.Drawing.Color.Gray;
            this.lblMauSac.Location = new System.Drawing.Point(162, 5);
            this.lblMauSac.Name = "lblMauSac";
            this.lblMauSac.Size = new System.Drawing.Size(51, 15);
            this.lblMauSac.TabIndex = 2;
            this.lblMauSac.Text = "Màu sắc";
            // 
            // cboLoaiSP
            // 
            this.cboLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiSP.FormattingEnabled = true;
            this.cboLoaiSP.Items.AddRange(new object[] {
            "Tất cả",
            "Váy cưới",
            "Vest",
            "Áo dài"});
            this.cboLoaiSP.Location = new System.Drawing.Point(15, 23);
            this.cboLoaiSP.Name = "cboLoaiSP";
            this.cboLoaiSP.Size = new System.Drawing.Size(130, 23);
            this.cboLoaiSP.TabIndex = 1;
            // 
            // lblLoaiSP
            // 
            this.lblLoaiSP.AutoSize = true;
            this.lblLoaiSP.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLoaiSP.ForeColor = System.Drawing.Color.Gray;
            this.lblLoaiSP.Location = new System.Drawing.Point(12, 5);
            this.lblLoaiSP.Name = "lblLoaiSP";
            this.lblLoaiSP.Size = new System.Drawing.Size(84, 15);
            this.lblLoaiSP.TabIndex = 0;
            this.lblLoaiSP.Text = "Loại sản phẩm";
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelList.BackColor = System.Drawing.Color.White;
            this.panelList.Controls.Add(this.dgvSanPham);
            this.panelList.Controls.Add(this.lblListTitle);
            this.panelList.Location = new System.Drawing.Point(36, 138);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(575, 545);
            this.panelList.TabIndex = 3;
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AllowUserToDeleteRows = false;
            this.dgvSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.White;
            this.dgvSanPham.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Location = new System.Drawing.Point(15, 38);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(545, 492);
            this.dgvSanPham.TabIndex = 1;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.Location = new System.Drawing.Point(12, 10);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(164, 19);
            this.lblListTitle.TabIndex = 0;
            this.lblListTitle.Text = "Danh sách sản phẩm kho";
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.BackColor = System.Drawing.Color.White;
            this.panelDetail.Controls.Add(this.btnLamMoi);
            this.panelDetail.Controls.Add(this.btnXoa);
            this.panelDetail.Controls.Add(this.btnLuu);
            this.panelDetail.Controls.Add(this.txtGiaThue);
            this.panelDetail.Controls.Add(this.lblGiaThue);
            this.panelDetail.Controls.Add(this.txtGiaNhap);
            this.panelDetail.Controls.Add(this.lblGiaNhap);
            this.panelDetail.Controls.Add(this.nudSoLuongTon);
            this.panelDetail.Controls.Add(this.lblTonKhoDetail);
            this.panelDetail.Controls.Add(this.cboNoiSXDetail);
            this.panelDetail.Controls.Add(this.lblNoiSXDetail);
            this.panelDetail.Controls.Add(this.cboMauSacDetail);
            this.panelDetail.Controls.Add(this.lblMauSacDetail);
            this.panelDetail.Controls.Add(this.cboLoaiSPDetail);
            this.panelDetail.Controls.Add(this.lblLoaiSPDetail);
            this.panelDetail.Controls.Add(this.txtTenSP);
            this.panelDetail.Controls.Add(this.lblTenSP);
            this.panelDetail.Controls.Add(this.txtMaSP);
            this.panelDetail.Controls.Add(this.lblMaSP);
            this.panelDetail.Controls.Add(this.btnChonAnh);
            this.panelDetail.Controls.Add(this.picAnhSP);
            this.panelDetail.Controls.Add(this.lblDetailTitle);
            this.panelDetail.Location = new System.Drawing.Point(625, 138);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(369, 545);
            this.panelDetail.TabIndex = 4;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Location = new System.Drawing.Point(12, 8);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(158, 19);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "Thông tin chi tiết sản phẩm";
            // 
            // picAnhSP
            // 
            this.picAnhSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.picAnhSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnhSP.Location = new System.Drawing.Point(135, 28);
            this.picAnhSP.Name = "picAnhSP";
            this.picAnhSP.Size = new System.Drawing.Size(100, 105);
            this.picAnhSP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnhSP.TabIndex = 1;
            this.picAnhSP.TabStop = false;
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(21)))), ((int)(((byte)(37)))));
            this.btnChonAnh.AutoSize = true;
            this.btnChonAnh.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnChonAnh.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnChonAnh.Location = new System.Drawing.Point(125, 136);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(120, 13);
            this.btnChonAnh.TabIndex = 2;
            this.btnChonAnh.TabStop = true;
            this.btnChonAnh.Text = "Chọn ảnh từ máy tính...";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMaSP.ForeColor = System.Drawing.Color.DimGray;
            this.lblMaSP.Location = new System.Drawing.Point(15, 155);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(79, 15);
            this.lblMaSP.TabIndex = 3;
            this.lblMaSP.Text = "Mã sản phẩm";
            // 
            // txtMaSP
            // 
            this.txtMaSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMaSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaSP.Location = new System.Drawing.Point(18, 172);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.ReadOnly = true;
            this.txtMaSP.Size = new System.Drawing.Size(334, 23);
            this.txtMaSP.TabIndex = 4;
            this.txtMaSP.Text = "SP001";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTenSP.ForeColor = System.Drawing.Color.DimGray;
            this.lblTenSP.Location = new System.Drawing.Point(15, 200);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(80, 15);
            this.lblTenSP.TabIndex = 5;
            this.lblTenSP.Text = "Tên sản phẩm";
            // 
            // txtTenSP
            // 
            this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenSP.Location = new System.Drawing.Point(18, 217);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(334, 23);
            this.txtTenSP.TabIndex = 6;
            this.txtTenSP.Text = "Váy cưới công chúa đính đá";
            // 
            // lblLoaiSPDetail
            // 
            this.lblLoaiSPDetail.AutoSize = true;
            this.lblLoaiSPDetail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLoaiSPDetail.ForeColor = System.Drawing.Color.DimGray;
            this.lblLoaiSPDetail.Location = new System.Drawing.Point(15, 245);
            this.lblLoaiSPDetail.Name = "lblLoaiSPDetail";
            this.lblLoaiSPDetail.Size = new System.Drawing.Size(84, 15);
            this.lblLoaiSPDetail.TabIndex = 7;
            this.lblLoaiSPDetail.Text = "Loại sản phẩm";
            // 
            // cboLoaiSPDetail
            // 
            this.cboLoaiSPDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSPDetail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoaiSPDetail.FormattingEnabled = true;
            this.cboLoaiSPDetail.Items.AddRange(new object[] {
            "Váy cưới",
            "Vest",
            "Áo dài"});
            this.cboLoaiSPDetail.Location = new System.Drawing.Point(18, 263);
            this.cboLoaiSPDetail.Name = "cboLoaiSPDetail";
            this.cboLoaiSPDetail.Size = new System.Drawing.Size(160, 23);
            this.cboLoaiSPDetail.TabIndex = 8;
            // 
            // lblMauSacDetail
            // 
            this.lblMauSacDetail.AutoSize = true;
            this.lblMauSacDetail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMauSacDetail.ForeColor = System.Drawing.Color.DimGray;
            this.lblMauSacDetail.Location = new System.Drawing.Point(189, 245);
            this.lblMauSacDetail.Name = "lblMauSacDetail";
            this.lblMauSacDetail.Size = new System.Drawing.Size(51, 15);
            this.lblMauSacDetail.TabIndex = 9;
            this.lblMauSacDetail.Text = "Màu sắc";
            // 
            // cboMauSacDetail
            // 
            this.cboMauSacDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMauSacDetail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboMauSacDetail.FormattingEnabled = true;
            this.cboMauSacDetail.Items.AddRange(new object[] {
            "Trắng",
            "Trắng kem",
            "Đen",
            "Đỏ thẫm"});
            this.cboMauSacDetail.Location = new System.Drawing.Point(192, 263);
            this.cboMauSacDetail.Name = "cboMauSacDetail";
            this.cboMauSacDetail.Size = new System.Drawing.Size(160, 23);
            this.cboMauSacDetail.TabIndex = 10;
            // 
            // lblNoiSXDetail
            // 
            this.lblNoiSXDetail.AutoSize = true;
            this.lblNoiSXDetail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNoiSXDetail.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoiSXDetail.Location = new System.Drawing.Point(15, 292);
            this.lblNoiSXDetail.Name = "lblNoiSXDetail";
            this.lblNoiSXDetail.Size = new System.Drawing.Size(70, 15);
            this.lblNoiSXDetail.TabIndex = 11;
            this.lblNoiSXDetail.Text = "Nơi sản xuất";
            // 
            // cboNoiSXDetail
            // 
            this.cboNoiSXDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNoiSXDetail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNoiSXDetail.FormattingEnabled = true;
            this.cboNoiSXDetail.Items.AddRange(new object[] {
            "Việt Nam",
            "Hàn Quốc",
            "Pháp"});
            this.cboNoiSXDetail.Location = new System.Drawing.Point(18, 309);
            this.cboNoiSXDetail.Name = "cboNoiSXDetail";
            this.cboNoiSXDetail.Size = new System.Drawing.Size(160, 23);
            this.cboNoiSXDetail.TabIndex = 12;
            // 
            // lblTonKhoDetail
            // 
            this.lblTonKhoDetail.AutoSize = true;
            this.lblTonKhoDetail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTonKhoDetail.ForeColor = System.Drawing.Color.DimGray;
            this.lblTonKhoDetail.Location = new System.Drawing.Point(189, 292);
            this.lblTonKhoDetail.Name = "lblTonKhoDetail";
            this.lblTonKhoDetail.Size = new System.Drawing.Size(73, 15);
            this.lblTonKhoDetail.TabIndex = 13;
            this.lblTonKhoDetail.Text = "Số lượng tồn";
            // 
            // nudSoLuongTon
            // 
            this.nudSoLuongTon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoLuongTon.Location = new System.Drawing.Point(192, 309);
            this.nudSoLuongTon.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSoLuongTon.Name = "nudSoLuongTon";
            this.nudSoLuongTon.Size = new System.Drawing.Size(160, 23);
            this.nudSoLuongTon.TabIndex = 14;
            this.nudSoLuongTon.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblGiaNhap
            // 
            this.lblGiaNhap.AutoSize = true;
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGiaNhap.ForeColor = System.Drawing.Color.DimGray;
            this.lblGiaNhap.Location = new System.Drawing.Point(15, 338);
            this.lblGiaNhap.Name = "lblGiaNhap";
            this.lblGiaNhap.Size = new System.Drawing.Size(74, 15);
            this.lblGiaNhap.TabIndex = 15;
            this.lblGiaNhap.Text = "Đơn giá nhập";
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGiaNhap.Location = new System.Drawing.Point(18, 355);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(160, 23);
            this.txtGiaNhap.TabIndex = 16;
            this.txtGiaNhap.Text = "10.000.000 đ";
            // 
            // lblGiaThue
            // 
            this.lblGiaThue.AutoSize = true;
            this.lblGiaThue.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGiaThue.ForeColor = System.Drawing.Color.DimGray;
            this.lblGiaThue.Location = new System.Drawing.Point(189, 338);
            this.lblGiaThue.Name = "lblGiaThue";
            this.lblGiaThue.Size = new System.Drawing.Size(69, 15);
            this.lblGiaThue.TabIndex = 17;
            this.lblGiaThue.Text = "Đơn giá thuê";
            // 
            // txtGiaThue
            // 
            this.txtGiaThue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGiaThue.Location = new System.Drawing.Point(192, 355);
            this.txtGiaThue.Name = "txtGiaThue";
            this.txtGiaThue.Size = new System.Drawing.Size(160, 23);
            this.txtGiaThue.TabIndex = 18;
            this.txtGiaThue.Text = "3.500.000 đ";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(18, 395);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(105, 32);
            this.btnLuu.TabIndex = 19;
            this.btnLuu.Text = "Lưu thay đổi";
            this.btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnXoa.Location = new System.Drawing.Point(130, 395);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(105, 32);
            this.btnXoa.TabIndex = 20;
            this.btnXoa.Text = "Xóa sản phẩm";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnLamMoi.Location = new System.Drawing.Point(243, 395);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(109, 32);
            this.btnLamMoi.TabIndex = 21;
            this.btnLamMoi.Text = "Làm mới / Hủy";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // frmSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1024, 700);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.btnThemSP);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSanPham";
            this.Text = "Quản lý sản phẩm kho";
            this.Load += new System.EventHandler(this.frmSanPham_Load);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhSP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnThemSP;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.Label lblLoaiSP;
        private System.Windows.Forms.ComboBox cboMauSac;
        private System.Windows.Forms.Label lblMauSac;
        private System.Windows.Forms.ComboBox cboNoiSX;
        private System.Windows.Forms.Label lblNoiSX;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.PictureBox picAnhSP;
        private System.Windows.Forms.LinkLabel btnChonAnh;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.ComboBox cboLoaiSPDetail;
        private System.Windows.Forms.Label lblLoaiSPDetail;
        private System.Windows.Forms.ComboBox cboMauSacDetail;
        private System.Windows.Forms.Label lblMauSacDetail;
        private System.Windows.Forms.ComboBox cboNoiSXDetail;
        private System.Windows.Forms.Label lblNoiSXDetail;
        private System.Windows.Forms.NumericUpDown nudSoLuongTon;
        private System.Windows.Forms.Label lblTonKhoDetail;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.TextBox txtGiaThue;
        private System.Windows.Forms.Label lblGiaThue;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}