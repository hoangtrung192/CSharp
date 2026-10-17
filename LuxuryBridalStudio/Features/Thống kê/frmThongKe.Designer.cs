namespace LuxuryBridalStudio
{
    partial class frmThongKe
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (bangHienThi != null) bangHienThi.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.header = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblNguon = new System.Windows.Forms.Label();
            this.filters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.cboNhom = new System.Windows.Forms.ComboBox();
            this.cboGom = new System.Windows.Forms.ComboBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnThangNay = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.cards = new System.Windows.Forms.TableLayoutPanel();
            this.card0 = new System.Windows.Forms.Panel();
            this.title0 = new System.Windows.Forms.Label();
            this.lblThu = new System.Windows.Forms.Label();
            this.card1 = new System.Windows.Forms.Panel();
            this.title1 = new System.Windows.Forms.Label();
            this.lblHoan = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.title2 = new System.Windows.Forms.Label();
            this.lblThucThu = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.title3 = new System.Windows.Forms.Label();
            this.lblSoGD = new System.Windows.Forms.Label();
            this.charts = new System.Windows.Forms.TableLayoutPanel();
            this.chartDongTien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCoCau = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.layout.SuspendLayout();
            this.header.SuspendLayout();
            this.filters.SuspendLayout();
            this.cards.SuspendLayout();
            this.card0.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.charts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDongTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCoCau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(246)))));
            this.layout.ColumnCount = 1;
            this.layout.Controls.Add(this.header, 0, 0);
            this.layout.Controls.Add(this.filters, 0, 1);
            this.layout.Controls.Add(this.cards, 0, 2);
            this.layout.Controls.Add(this.charts, 0, 3);
            this.layout.Controls.Add(this.dgvChiTiet, 0, 4);
            this.layout.Controls.Add(this.lblTrangThai, 0, 5);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Padding = new System.Windows.Forms.Padding(20);
            this.layout.RowCount = 6;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.layout.Size = new System.Drawing.Size(1240, 800);
            this.layout.TabIndex = 0;
            // 
            // header
            // 
            this.header.Controls.Add(this.lblTieuDe);
            this.header.Controls.Add(this.lblNguon);
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Location = new System.Drawing.Point(23, 23);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1194, 60);
            this.header.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(39)))), ((int)(((byte)(51)))));
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(362, 46);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BÁO CÁO & THỐNG KÊ";
            // 
            // lblNguon
            // 
            this.lblNguon.AutoSize = true;
            this.lblNguon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(77)))), ((int)(((byte)(89)))));
            this.lblNguon.Location = new System.Drawing.Point(2, 42);
            this.lblNguon.Name = "lblNguon";
            this.lblNguon.Size = new System.Drawing.Size(304, 23);
            this.lblNguon.TabIndex = 1;
            this.lblNguon.Text = "DỮ LIỆU MẪU • Chưa kết nối database";
            // 
            // filters
            // 
            this.filters.AutoScroll = true;
            this.filters.Controls.Add(this.lblTu);
            this.filters.Controls.Add(this.dtpTuNgay);
            this.filters.Controls.Add(this.lblDen);
            this.filters.Controls.Add(this.dtpDenNgay);
            this.filters.Controls.Add(this.cboNhom);
            this.filters.Controls.Add(this.cboGom);
            this.filters.Controls.Add(this.btnLoc);
            this.filters.Controls.Add(this.btnThangNay);
            this.filters.Controls.Add(this.btnLamMoi);
            this.filters.Controls.Add(this.btnXuat);
            this.filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filters.Location = new System.Drawing.Point(23, 89);
            this.filters.Name = "filters";
            this.filters.Size = new System.Drawing.Size(1194, 68);
            this.filters.TabIndex = 1;
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Location = new System.Drawing.Point(3, 9);
            this.lblTu.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(71, 23);
            this.lblTu.TabIndex = 0;
            this.lblTu.Text = "Từ ngày";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(80, 3);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(125, 30);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(211, 9);
            this.lblDen.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(83, 23);
            this.lblDen.TabIndex = 2;
            this.lblDen.Text = "Đến ngày";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(300, 3);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(125, 30);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // cboNhom
            // 
            this.cboNhom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhom.Items.AddRange(new object[] {
            "Tất cả nhóm",
            "Váy cưới",
            "Vest",
            "Áo dài",
            "Phụ kiện"});
            this.cboNhom.Location = new System.Drawing.Point(431, 3);
            this.cboNhom.Name = "cboNhom";
            this.cboNhom.Size = new System.Drawing.Size(140, 31);
            this.cboNhom.TabIndex = 4;
            // 
            // cboGom
            // 
            this.cboGom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGom.Items.AddRange(new object[] {
            "Theo ngày",
            "Theo tháng"});
            this.cboGom.Location = new System.Drawing.Point(577, 3);
            this.cboGom.Name = "cboGom";
            this.cboGom.Size = new System.Drawing.Size(140, 31);
            this.cboGom.TabIndex = 5;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(112)))), ((int)(((byte)(126)))));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(723, 3);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(106, 32);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "&Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnThangNay
            // 
            this.btnThangNay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(112)))), ((int)(((byte)(126)))));
            this.btnThangNay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThangNay.ForeColor = System.Drawing.Color.White;
            this.btnThangNay.Location = new System.Drawing.Point(835, 3);
            this.btnThangNay.Name = "btnThangNay";
            this.btnThangNay.Size = new System.Drawing.Size(106, 32);
            this.btnThangNay.TabIndex = 7;
            this.btnThangNay.Text = "Tháng &này";
            this.btnThangNay.UseVisualStyleBackColor = false;
            this.btnThangNay.Click += new System.EventHandler(this.btnThangNay_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(112)))), ((int)(((byte)(126)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(947, 3);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(106, 32);
            this.btnLamMoi.TabIndex = 8;
            this.btnLamMoi.Text = "Làm &mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(112)))), ((int)(((byte)(126)))));
            this.btnXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuat.ForeColor = System.Drawing.Color.White;
            this.btnXuat.Location = new System.Drawing.Point(1059, 3);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(106, 32);
            this.btnXuat.TabIndex = 9;
            this.btnXuat.Text = "&Xuất CSV";
            this.btnXuat.UseVisualStyleBackColor = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // cards
            // 
            this.cards.ColumnCount = 4;
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cards.Controls.Add(this.card0, 0, 0);
            this.cards.Controls.Add(this.card1, 1, 0);
            this.cards.Controls.Add(this.card2, 2, 0);
            this.cards.Controls.Add(this.card3, 3, 0);
            this.cards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cards.Location = new System.Drawing.Point(23, 163);
            this.cards.Name = "cards";
            this.cards.RowCount = 1;
            this.cards.Size = new System.Drawing.Size(1194, 94);
            this.cards.TabIndex = 2;
            // 
            // card0
            // 
            this.card0.BackColor = System.Drawing.Color.White;
            this.card0.Controls.Add(this.title0);
            this.card0.Controls.Add(this.lblThu);
            this.card0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card0.Location = new System.Drawing.Point(3, 3);
            this.card0.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.card0.Name = "card0";
            this.card0.Size = new System.Drawing.Size(285, 100);
            this.card0.TabIndex = 0;
            // 
            // title0
            // 
            this.title0.AutoSize = true;
            this.title0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(95)))), ((int)(((byte)(102)))));
            this.title0.Location = new System.Drawing.Point(12, 10);
            this.title0.Name = "title0";
            this.title0.Size = new System.Drawing.Size(136, 23);
            this.title0.TabIndex = 0;
            this.title0.Text = "TỔNG TIỀN THU";
            // 
            // lblThu
            // 
            this.lblThu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThu.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(39)))), ((int)(((byte)(51)))));
            this.lblThu.Location = new System.Drawing.Point(10, 32);
            this.lblThu.Name = "lblThu";
            this.lblThu.Size = new System.Drawing.Size(335, 40);
            this.lblThu.TabIndex = 1;
            this.lblThu.Text = "0";
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.Controls.Add(this.title1);
            this.card1.Controls.Add(this.lblHoan);
            this.card1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card1.Location = new System.Drawing.Point(301, 3);
            this.card1.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(285, 100);
            this.card1.TabIndex = 1;
            // 
            // title1
            // 
            this.title1.AutoSize = true;
            this.title1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(95)))), ((int)(((byte)(102)))));
            this.title1.Location = new System.Drawing.Point(12, 10);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(100, 23);
            this.title1.TabIndex = 0;
            this.title1.Text = "HOÀN TIỀN";
            // 
            // lblHoan
            // 
            this.lblHoan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHoan.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblHoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(39)))), ((int)(((byte)(51)))));
            this.lblHoan.Location = new System.Drawing.Point(10, 32);
            this.lblHoan.Name = "lblHoan";
            this.lblHoan.Size = new System.Drawing.Size(335, 40);
            this.lblHoan.TabIndex = 1;
            this.lblHoan.Text = "0";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.Controls.Add(this.title2);
            this.card2.Controls.Add(this.lblThucThu);
            this.card2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card2.Location = new System.Drawing.Point(599, 3);
            this.card2.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(285, 100);
            this.card2.TabIndex = 2;
            // 
            // title2
            // 
            this.title2.AutoSize = true;
            this.title2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(95)))), ((int)(((byte)(102)))));
            this.title2.Location = new System.Drawing.Point(12, 10);
            this.title2.Name = "title2";
            this.title2.Size = new System.Drawing.Size(92, 23);
            this.title2.TabIndex = 0;
            this.title2.Text = "THỰC THU";
            // 
            // lblThucThu
            // 
            this.lblThucThu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThucThu.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblThucThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(39)))), ((int)(((byte)(51)))));
            this.lblThucThu.Location = new System.Drawing.Point(10, 32);
            this.lblThucThu.Name = "lblThucThu";
            this.lblThucThu.Size = new System.Drawing.Size(335, 40);
            this.lblThucThu.TabIndex = 1;
            this.lblThucThu.Text = "0";
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.Controls.Add(this.title3);
            this.card3.Controls.Add(this.lblSoGD);
            this.card3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card3.Location = new System.Drawing.Point(897, 3);
            this.card3.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(287, 100);
            this.card3.TabIndex = 3;
            // 
            // title3
            // 
            this.title3.AutoSize = true;
            this.title3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(95)))), ((int)(((byte)(102)))));
            this.title3.Location = new System.Drawing.Point(12, 10);
            this.title3.Name = "title3";
            this.title3.Size = new System.Drawing.Size(123, 23);
            this.title3.TabIndex = 0;
            this.title3.Text = "SỐ GIAO DỊCH";
            // 
            // lblSoGD
            // 
            this.lblSoGD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoGD.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblSoGD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(39)))), ((int)(((byte)(51)))));
            this.lblSoGD.Location = new System.Drawing.Point(10, 32);
            this.lblSoGD.Name = "lblSoGD";
            this.lblSoGD.Size = new System.Drawing.Size(337, 40);
            this.lblSoGD.TabIndex = 1;
            this.lblSoGD.Text = "0";
            // 
            // charts
            // 
            this.charts.ColumnCount = 2;
            this.charts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.charts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.charts.Controls.Add(this.chartDongTien, 0, 0);
            this.charts.Controls.Add(this.chartCoCau, 1, 0);
            this.charts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.charts.Location = new System.Drawing.Point(23, 263);
            this.charts.Name = "charts";
            this.charts.RowCount = 1;
            this.charts.Size = new System.Drawing.Size(1194, 224);
            this.charts.TabIndex = 3;
            // 
            // chartDongTien
            // 
            chartArea1.Name = "Main";
            this.chartDongTien.ChartAreas.Add(chartArea1);
            this.chartDongTien.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend";
            this.chartDongTien.Legends.Add(legend1);
            this.chartDongTien.Location = new System.Drawing.Point(3, 3);
            this.chartDongTien.Name = "chartDongTien";
            this.chartDongTien.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chartDongTien.Size = new System.Drawing.Size(770, 300);
            this.chartDongTien.TabIndex = 0;
            // 
            // chartCoCau
            // 
            chartArea2.Name = "Main";
            this.chartCoCau.ChartAreas.Add(chartArea2);
            this.chartCoCau.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend";
            this.chartCoCau.Legends.Add(legend2);
            this.chartCoCau.Location = new System.Drawing.Point(779, 3);
            this.chartCoCau.Name = "chartCoCau";
            this.chartCoCau.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chartCoCau.Size = new System.Drawing.Size(412, 300);
            this.chartCoCau.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.dgvChiTiet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(39)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTiet.ColumnHeadersHeight = 36;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(215)))), ((int)(((byte)(221)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.EnableHeadersVisualStyles = false;
            this.dgvChiTiet.Location = new System.Drawing.Point(23, 493);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 62;
            this.dgvChiTiet.RowTemplate.Height = 30;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1194, 243);
            this.dgvChiTiet.TabIndex = 4;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoEllipsis = true;
            this.lblTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrangThai.Location = new System.Drawing.Point(23, 739);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(1194, 41);
            this.lblTrangThai.TabIndex = 5;
            this.lblTrangThai.Text = "Đơn vị: VNĐ";
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 800);
            this.Controls.Add(this.layout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 720);
            this.Name = "frmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo & thống kê - Luxury Bridal Studio";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            this.layout.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.filters.ResumeLayout(false);
            this.filters.PerformLayout();
            this.cards.ResumeLayout(false);
            this.card0.ResumeLayout(false);
            this.card0.PerformLayout();
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            this.charts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDongTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCoCau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblNguon;
        private System.Windows.Forms.FlowLayoutPanel filters;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.ComboBox cboNhom;
        private System.Windows.Forms.ComboBox cboGom;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnThangNay;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.TableLayoutPanel cards;
        private System.Windows.Forms.Panel card0;
        private System.Windows.Forms.Label title0;
        private System.Windows.Forms.Label lblThu;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Label title1;
        private System.Windows.Forms.Label lblHoan;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Label title2;
        private System.Windows.Forms.Label lblThucThu;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Label title3;
        private System.Windows.Forms.Label lblSoGD;
        private System.Windows.Forms.TableLayoutPanel charts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDongTien;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCoCau;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblTrangThai;
    }
}
