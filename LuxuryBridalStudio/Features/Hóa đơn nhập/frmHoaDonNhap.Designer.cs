namespace LuxuryBridalStudio
{
    partial class frmHoaDonNhap
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
            this.btnLamMoiHeader = new System.Windows.Forms.Button();
            this.btnDanhSachPhieu = new System.Windows.Forms.Button();
            this.pnlThongTinPhieu = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.btnThemNCCNhanh = new System.Windows.Forms.Button();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.txtMaHDN = new System.Windows.Forms.TextBox();
            this.lblMaHDN = new System.Windows.Forms.Label();
            this.pnlChonSanPham = new System.Windows.Forms.Panel();
            this.btnThemVaoPhieu = new System.Windows.Forms.Button();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.lblDonGiaNhap = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.pnlDanhSachSP = new System.Windows.Forms.Panel();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.btnLuuNhapKho = new System.Windows.Forms.Button();
            this.lblTongTienGiaTri = new System.Windows.Forms.Label();
            this.lblTongTienTitle = new System.Windows.Forms.Label();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.dgvChiTietNhap = new System.Windows.Forms.DataGridView();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.pnlThongTinPhieu.SuspendLayout();
            this.pnlChonSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.pnlDanhSachSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).BeginInit();
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
            this.lblTitle.Text = "Lập hóa đơn nhập kho";
            // 
            // btnLamMoiHeader
            // 
            this.btnLamMoiHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiHeader.BackColor = System.Drawing.Color.White;
            this.btnLamMoiHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoiHeader.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.btnLamMoiHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiHeader.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoiHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnLamMoiHeader.Location = new System.Drawing.Point(882, 18);
            this.btnLamMoiHeader.Name = "btnLamMoiHeader";
            this.btnLamMoiHeader.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoiHeader.TabIndex = 2;
            this.btnLamMoiHeader.Text = "+ Làm mới";
            this.btnLamMoiHeader.UseVisualStyleBackColor = false;
            // 
            // btnDanhSachPhieu
            // 
            this.btnDanhSachPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDanhSachPhieu.BackColor = System.Drawing.Color.White;
            this.btnDanhSachPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDanhSachPhieu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.btnDanhSachPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhSachPhieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDanhSachPhieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnDanhSachPhieu.Location = new System.Drawing.Point(718, 18);
            this.btnDanhSachPhieu.Name = "btnDanhSachPhieu";
            this.btnDanhSachPhieu.Size = new System.Drawing.Size(155, 32);
            this.btnDanhSachPhieu.TabIndex = 1;
            this.btnDanhSachPhieu.Text = "📑 Danh sách phiếu nhập";
            this.btnDanhSachPhieu.UseVisualStyleBackColor = false;
            // 
            // pnlThongTinPhieu
            // 
            this.pnlThongTinPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlThongTinPhieu.BackColor = System.Drawing.Color.White;
            this.pnlThongTinPhieu.Controls.Add(this.txtGhiChu);
            this.pnlThongTinPhieu.Controls.Add(this.lblGhiChu);
            this.pnlThongTinPhieu.Controls.Add(this.btnThemNCCNhanh);
            this.pnlThongTinPhieu.Controls.Add(this.cboNhaCungCap);
            this.pnlThongTinPhieu.Controls.Add(this.lblNhaCungCap);
            this.pnlThongTinPhieu.Controls.Add(this.cboNhanVien);
            this.pnlThongTinPhieu.Controls.Add(this.lblNhanVien);
            this.pnlThongTinPhieu.Controls.Add(this.dtpNgayNhap);
            this.pnlThongTinPhieu.Controls.Add(this.lblNgayNhap);
            this.pnlThongTinPhieu.Controls.Add(this.txtMaHDN);
            this.pnlThongTinPhieu.Controls.Add(this.lblMaHDN);
            this.pnlThongTinPhieu.Location = new System.Drawing.Point(36, 68);
            this.pnlThongTinPhieu.Name = "pnlThongTinPhieu";
            this.pnlThongTinPhieu.Size = new System.Drawing.Size(952, 76);
            this.pnlThongTinPhieu.TabIndex = 3;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChu.Location = new System.Drawing.Point(750, 24);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(186, 42);
            this.txtGhiChu.TabIndex = 10;
            this.txtGhiChu.Text = "Nhập bổ sung bộ sưu tập cưới Thu - Đông";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGhiChu.ForeColor = System.Drawing.Color.DimGray;
            this.lblGhiChu.Location = new System.Drawing.Point(747, 7);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(48, 15);
            this.lblGhiChu.TabIndex = 9;
            this.lblGhiChu.Text = "Ghi chú";
            // 
            // btnThemNCCNhanh
            // 
            this.btnThemNCCNhanh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnThemNCCNhanh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemNCCNhanh.FlatAppearance.BorderSize = 0;
            this.btnThemNCCNhanh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNCCNhanh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemNCCNhanh.ForeColor = System.Drawing.Color.White;
            this.btnThemNCCNhanh.Location = new System.Drawing.Point(707, 24);
            this.btnThemNCCNhanh.Name = "btnThemNCCNhanh";
            this.btnThemNCCNhanh.Size = new System.Drawing.Size(30, 25);
            this.btnThemNCCNhanh.TabIndex = 8;
            this.btnThemNCCNhanh.Text = "+";
            this.btnThemNCCNhanh.UseVisualStyleBackColor = false;
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(540, 25);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(162, 23);
            this.cboNhaCungCap.TabIndex = 7;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNhaCungCap.ForeColor = System.Drawing.Color.DimGray;
            this.lblNhaCungCap.Location = new System.Drawing.Point(537, 7);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(81, 15);
            this.lblNhaCungCap.TabIndex = 6;
            this.lblNhaCungCap.Text = "Nhà cung cấp";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(355, 25);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(165, 23);
            this.cboNhanVien.TabIndex = 5;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNhanVien.ForeColor = System.Drawing.Color.DimGray;
            this.lblNhanVien.Location = new System.Drawing.Point(352, 7);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(89, 15);
            this.lblNhanVien.TabIndex = 4;
            this.lblNhanVien.Text = "Nhân viên nhập";
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayNhap.Location = new System.Drawing.Point(195, 25);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(140, 23);
            this.dtpNgayNhap.TabIndex = 3;
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNgayNhap.ForeColor = System.Drawing.Color.DimGray;
            this.lblNgayNhap.Location = new System.Drawing.Point(192, 7);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(65, 15);
            this.lblNgayNhap.TabIndex = 2;
            this.lblNgayNhap.Text = "Ngày nhập";
            // 
            // txtMaHDN
            // 
            this.txtMaHDN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtMaHDN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaHDN.Location = new System.Drawing.Point(15, 25);
            this.txtMaHDN.Name = "txtMaHDN";
            this.txtMaHDN.ReadOnly = true;
            this.txtMaHDN.Size = new System.Drawing.Size(160, 23);
            this.txtMaHDN.TabIndex = 1;
            this.txtMaHDN.Text = "HDN001";
            // 
            // lblMaHDN
            // 
            this.lblMaHDN.AutoSize = true;
            this.lblMaHDN.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMaHDN.ForeColor = System.Drawing.Color.DimGray;
            this.lblMaHDN.Location = new System.Drawing.Point(12, 7);
            this.lblMaHDN.Name = "lblMaHDN";
            this.lblMaHDN.Size = new System.Drawing.Size(103, 15);
            this.lblMaHDN.TabIndex = 0;
            this.lblMaHDN.Text = "Mã hóa đơn nhập";
            // 
            // pnlChonSanPham
            // 
            this.pnlChonSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChonSanPham.BackColor = System.Drawing.Color.White;
            this.pnlChonSanPham.Controls.Add(this.btnThemVaoPhieu);
            this.pnlChonSanPham.Controls.Add(this.txtThanhTien);
            this.pnlChonSanPham.Controls.Add(this.lblThanhTien);
            this.pnlChonSanPham.Controls.Add(this.txtDonGiaNhap);
            this.pnlChonSanPham.Controls.Add(this.lblDonGiaNhap);
            this.pnlChonSanPham.Controls.Add(this.nudSoLuong);
            this.pnlChonSanPham.Controls.Add(this.lblSoLuong);
            this.pnlChonSanPham.Controls.Add(this.cboSanPham);
            this.pnlChonSanPham.Controls.Add(this.lblSanPham);
            this.pnlChonSanPham.Location = new System.Drawing.Point(36, 152);
            this.pnlChonSanPham.Name = "pnlChonSanPham";
            this.pnlChonSanPham.Size = new System.Drawing.Size(952, 68);
            this.pnlChonSanPham.TabIndex = 4;
            // 
            // btnThemVaoPhieu
            // 
            this.btnThemVaoPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemVaoPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnThemVaoPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemVaoPhieu.FlatAppearance.BorderSize = 0;
            this.btnThemVaoPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoPhieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoPhieu.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoPhieu.Location = new System.Drawing.Point(796, 23);
            this.btnThemVaoPhieu.Name = "btnThemVaoPhieu";
            this.btnThemVaoPhieu.Size = new System.Drawing.Size(140, 28);
            this.btnThemVaoPhieu.TabIndex = 8;
            this.btnThemVaoPhieu.Text = "+ Thêm vào phiếu";
            this.btnThemVaoPhieu.UseVisualStyleBackColor = false;
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtThanhTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtThanhTien.Location = new System.Drawing.Point(630, 25);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(145, 23);
            this.txtThanhTien.TabIndex = 7;
            this.txtThanhTien.Text = "40.000.000 đ";
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblThanhTien.ForeColor = System.Drawing.Color.DimGray;
            this.lblThanhTien.Location = new System.Drawing.Point(627, 7);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(63, 15);
            this.lblThanhTien.TabIndex = 6;
            this.lblThanhTien.Text = "Thành tiền";
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDonGiaNhap.Location = new System.Drawing.Point(460, 25);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(150, 23);
            this.txtDonGiaNhap.TabIndex = 5;
            this.txtDonGiaNhap.Text = "8.000.000";
            // 
            // lblDonGiaNhap
            // 
            this.lblDonGiaNhap.AutoSize = true;
            this.lblDonGiaNhap.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDonGiaNhap.ForeColor = System.Drawing.Color.DimGray;
            this.lblDonGiaNhap.Location = new System.Drawing.Point(457, 7);
            this.lblDonGiaNhap.Name = "lblDonGiaNhap";
            this.lblDonGiaNhap.Size = new System.Drawing.Size(100, 15);
            this.lblDonGiaNhap.TabIndex = 4;
            this.lblDonGiaNhap.Text = "Đơn giá nhập (đ)";
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoLuong.Location = new System.Drawing.Point(340, 25);
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(95, 23);
            this.nudSoLuong.TabIndex = 3;
            this.nudSoLuong.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.DimGray;
            this.lblSoLuong.Location = new System.Drawing.Point(337, 7);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(85, 15);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số lượng nhập";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(15, 25);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(305, 23);
            this.cboSanPham.TabIndex = 1;
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSanPham.ForeColor = System.Drawing.Color.DimGray;
            this.lblSanPham.Location = new System.Drawing.Point(12, 7);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(60, 15);
            this.lblSanPham.TabIndex = 0;
            this.lblSanPham.Text = "Sản phẩm";
            // 
            // pnlDanhSachSP
            // 
            this.pnlDanhSachSP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDanhSachSP.BackColor = System.Drawing.Color.White;
            this.pnlDanhSachSP.Controls.Add(this.btnHuyPhieu);
            this.pnlDanhSachSP.Controls.Add(this.btnLuuNhapKho);
            this.pnlDanhSachSP.Controls.Add(this.lblTongTienGiaTri);
            this.pnlDanhSachSP.Controls.Add(this.lblTongTienTitle);
            this.pnlDanhSachSP.Controls.Add(this.lblTongSoLuong);
            this.pnlDanhSachSP.Controls.Add(this.dgvChiTietNhap);
            this.pnlDanhSachSP.Controls.Add(this.lblListTitle);
            this.pnlDanhSachSP.Location = new System.Drawing.Point(36, 230);
            this.pnlDanhSachSP.Name = "pnlDanhSachSP";
            this.pnlDanhSachSP.Size = new System.Drawing.Size(952, 445);
            this.pnlDanhSachSP.TabIndex = 5;
            // 
            // btnHuyPhieu
            // 
            this.btnHuyPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuyPhieu.BackColor = System.Drawing.Color.White;
            this.btnHuyPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuyPhieu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.btnHuyPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyPhieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuyPhieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnHuyPhieu.Location = new System.Drawing.Point(836, 395);
            this.btnHuyPhieu.Name = "btnHuyPhieu";
            this.btnHuyPhieu.Size = new System.Drawing.Size(100, 35);
            this.btnHuyPhieu.TabIndex = 6;
            this.btnHuyPhieu.Text = "Hủy phiếu";
            this.btnHuyPhieu.UseVisualStyleBackColor = false;
            // 
            // btnLuuNhapKho
            // 
            this.btnLuuNhapKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuNhapKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(94)))), ((int)(((byte)(114)))));
            this.btnLuuNhapKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuNhapKho.FlatAppearance.BorderSize = 0;
            this.btnLuuNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuNhapKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuuNhapKho.ForeColor = System.Drawing.Color.White;
            this.btnLuuNhapKho.Location = new System.Drawing.Point(680, 395);
            this.btnLuuNhapKho.Name = "btnLuuNhapKho";
            this.btnLuuNhapKho.Size = new System.Drawing.Size(145, 35);
            this.btnLuuNhapKho.TabIndex = 5;
            this.btnLuuNhapKho.Text = "💾 Lưu && Nhập kho";
            this.btnLuuNhapKho.UseVisualStyleBackColor = false;
            // 
            // lblTongTienGiaTri
            // 
            this.lblTongTienGiaTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienGiaTri.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongTienGiaTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTongTienGiaTri.Location = new System.Drawing.Point(670, 350);
            this.lblTongTienGiaTri.Name = "lblTongTienGiaTri";
            this.lblTongTienGiaTri.Size = new System.Drawing.Size(266, 32);
            this.lblTongTienGiaTri.TabIndex = 4;
            this.lblTongTienGiaTri.Text = "66.700.000 đ";
            this.lblTongTienGiaTri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongTienTitle
            // 
            this.lblTongTienTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienTitle.AutoSize = true;
            this.lblTongTienTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongTienTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTongTienTitle.Location = new System.Drawing.Point(700, 328);
            this.lblTongTienTitle.Name = "lblTongTienTitle";
            this.lblTongTienTitle.Size = new System.Drawing.Size(147, 19);
            this.lblTongTienTitle.TabIndex = 3;
            this.lblTongTienTitle.Text = "Tổng tiền thanh toán:";
            // 
            // lblTongSoLuong
            // 
            this.lblTongSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongSoLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.lblTongSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTongSoLuong.Location = new System.Drawing.Point(15, 395);
            this.lblTongSoLuong.Name = "lblTongSoLuong";
            this.lblTongSoLuong.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblTongSoLuong.Size = new System.Drawing.Size(320, 35);
            this.lblTongSoLuong.TabIndex = 2;
            this.lblTongSoLuong.Text = "Tổng số lượng mặt hàng: 3 loại (12 sản phẩm)";
            this.lblTongSoLuong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvChiTietNhap
            // 
            this.dgvChiTietNhap.AllowUserToAddRows = false;
            this.dgvChiTietNhap.AllowUserToDeleteRows = false;
            this.dgvChiTietNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTietNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietNhap.Location = new System.Drawing.Point(15, 42);
            this.dgvChiTietNhap.Name = "dgvChiTietNhap";
            this.dgvChiTietNhap.ReadOnly = true;
            this.dgvChiTietNhap.RowHeadersVisible = false;
            this.dgvChiTietNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietNhap.Size = new System.Drawing.Size(921, 275);
            this.dgvChiTietNhap.TabIndex = 1;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.Location = new System.Drawing.Point(12, 12);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(201, 19);
            this.lblListTitle.TabIndex = 0;
            this.lblListTitle.Text = "Danh sách sản phẩm nhập kho";
            // 
            // frmHoaDonNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1024, 700);
            this.Controls.Add(this.pnlDanhSachSP);
            this.Controls.Add(this.pnlChonSanPham);
            this.Controls.Add(this.pnlThongTinPhieu);
            this.Controls.Add(this.btnDanhSachPhieu);
            this.Controls.Add(this.btnLamMoiHeader);
            this.lblTitle.BringToFront();
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmHoaDonNhap";
            this.Text = "Lập hóa đơn nhập kho";
            this.Load += new System.EventHandler(this.frmHoaDonNhap_Load);
            this.pnlThongTinPhieu.ResumeLayout(false);
            this.pnlThongTinPhieu.PerformLayout();
            this.pnlChonSanPham.ResumeLayout(false);
            this.pnlChonSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.pnlDanhSachSP.ResumeLayout(false);
            this.pnlDanhSachSP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnLamMoiHeader;
        private System.Windows.Forms.Button btnDanhSachPhieu;
        private System.Windows.Forms.Panel pnlThongTinPhieu;
        private System.Windows.Forms.Label lblMaHDN;
        private System.Windows.Forms.TextBox txtMaHDN;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Button btnThemNCCNhanh;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlChonSanPham;
        private System.Windows.Forms.Label lblSanPham;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblDonGiaNhap;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.Button btnThemVaoPhieu;
        private System.Windows.Forms.Panel pnlDanhSachSP;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.DataGridView dgvChiTietNhap;
        private System.Windows.Forms.Label lblTongSoLuong;
        private System.Windows.Forms.Label lblTongTienTitle;
        private System.Windows.Forms.Label lblTongTienGiaTri;
        private System.Windows.Forms.Button btnLuuNhapKho;
        private System.Windows.Forms.Button btnHuyPhieu;
    }
}