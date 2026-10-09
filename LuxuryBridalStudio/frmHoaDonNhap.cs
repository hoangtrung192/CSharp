using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmHoaDonNhap : Form
    {
        private DataTable dtChiTietNhap = new DataTable();

        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            KhoiTaoComboBox();
            KhoiTaoDuLieuBanDau();
            GanSuKien();
            TinhTongKetHoaDon();
        }

        #region KHỞI TẠO DỮ LIỆU BAN ĐẦU
        private void KhoiTaoComboBox()
        {
            // Danh sách Nhân viên nhập
            cboNhanVien.Items.AddRange(new object[] {
                "Lê Văn C (Thủ kho)",
                "Nguyễn Văn A (Quản lý)",
                "Trần Thị B (Kế toán)"
            });
            cboNhanVien.SelectedIndex = 0;

            // Danh sách Nhà cung cấp
            cboNhaCungCap.Items.AddRange(new object[] {
                "Xưởng May Váy Cưới Bella",
                "Thời Trang HERA",
                "Xưởng Vest Hoàng Gia",
                "Áo Dài Truyền Thống",
                "Phụ Kiện Diamond Studio"
            });
            cboNhaCungCap.SelectedIndex = 0;

            // Danh sách Sản phẩm
            cboSanPham.Items.AddRange(new object[] {
                "SP001 - Váy cưới công chúa đính đá",
                "SP002 - Váy cưới cúp ngực Satin",
                "SP003 - Vest chú rể Classic đen",
                "SP004 - Áo dài cưới Long Phụng",
                "SP005 - Váy cưới đuôi cá phối ren"
            });
            cboSanPham.SelectedIndex = 0;
        }

        private void KhoiTaoDuLieuBanDau()
        {
            dtChiTietNhap.Columns.Clear();
            dtChiTietNhap.Columns.Add("Mã SP");
            dtChiTietNhap.Columns.Add("Tên sản phẩm");
            dtChiTietNhap.Columns.Add("Loại");
            dtChiTietNhap.Columns.Add("Màu sắc");
            dtChiTietNhap.Columns.Add("Số lượng", typeof(int));
            dtChiTietNhap.Columns.Add("Đơn giá nhập");
            dtChiTietNhap.Columns.Add("Thành tiền");

            // Mock data giống 100% bản mẫu thiết kế
            dtChiTietNhap.Rows.Add("SP001", "Váy cưới công chúa đính đá", "Váy cưới", "Trắng", 5, "8.000.000 đ", "40.000.000 đ");
            dtChiTietNhap.Rows.Add("SP002", "Váy cưới cúp ngực Satin", "Váy cưới", "Trắng kem", 3, "6.500.000 đ", "19.500.000 đ");
            dtChiTietNhap.Rows.Add("SP003", "Vest chú rể Classic đen", "Vest", "Đen", 4, "1.800.000 đ", "7.200.000 đ");

            dgvChiTietNhap.DataSource = dtChiTietNhap;

            // Thêm cột nút Thao tác (Xóa) nếu chưa có
            if (!dgvChiTietNhap.Columns.Contains("ThaoTac"))
            {
                DataGridViewButtonColumn colXoa = new DataGridViewButtonColumn();
                colXoa.Name = "ThaoTac";
                colXoa.HeaderText = "Thao tác";
                colXoa.Text = "🗑";
                colXoa.UseColumnTextForButtonValue = true;
                colXoa.FlatStyle = FlatStyle.Flat;
                dgvChiTietNhap.Columns.Add(colXoa);
            }

            // Tỷ lệ độ rộng cột
            dgvChiTietNhap.Columns["Mã SP"].FillWeight = 65;
            dgvChiTietNhap.Columns["Tên sản phẩm"].FillWeight = 160;
            dgvChiTietNhap.Columns["Loại"].FillWeight = 85;
            dgvChiTietNhap.Columns["Màu sắc"].FillWeight = 80;
            dgvChiTietNhap.Columns["Số lượng"].FillWeight = 65;
            dgvChiTietNhap.Columns["Đơn giá nhập"].FillWeight = 100;
            dgvChiTietNhap.Columns["Thành tiền"].FillWeight = 110;
            dgvChiTietNhap.Columns["ThaoTac"].FillWeight = 60;
        }

        private void GanSuKien()
        {
            // Tự động tính Thành tiền trên hàng chọn khi đổi Số lượng hoặc Đơn giá
            nudSoLuong.ValueChanged += (s, e) => TinhThanhTienHangChon();
            txtDonGiaNhap.TextChanged += (s, e) => TinhThanhTienHangChon();

            // Tự cập nhật giá nhập mặc định khi đổi sản phẩm
            cboSanPham.SelectedIndexChanged += CboSanPham_SelectedIndexChanged;

            // Thêm sản phẩm vào bảng
            btnThemVaoPhieu.Click += BtnThemVaoPhieu_Click;

            // Xóa sản phẩm khỏi bảng qua nút ở cột Thao tác
            dgvChiTietNhap.CellContentClick += DgvChiTietNhap_CellContentClick;

            // Các nút thao tác phiếu
            btnLamMoiHeader.Click += (s, e) => LamMoiToanBoPhieu();
            btnHuyPhieu.Click += (s, e) => LamMoiToanBoPhieu();
            btnLuuNhapKho.Click += BtnLuuNhapKho_Click;
            btnThemNCCNhanh.Click += (s, e) => MessageBox.Show("Chức năng mở nhanh form Thêm Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDanhSachPhieu.Click += (s, e) => MessageBox.Show("Chức năng xem Lịch sử danh sách các phiếu nhập kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region TÍNH TOÁN VÀ CẬP NHẬT TIỀN
        private void CboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedIndex == 0) txtDonGiaNhap.Text = "8.000.000";
            else if (cboSanPham.SelectedIndex == 1) txtDonGiaNhap.Text = "6.500.000";
            else if (cboSanPham.SelectedIndex == 2) txtDonGiaNhap.Text = "1.800.000";
            else if (cboSanPham.SelectedIndex == 3) txtDonGiaNhap.Text = "2.500.000";
            else if (cboSanPham.SelectedIndex == 4) txtDonGiaNhap.Text = "9.000.000";
            TinhThanhTienHangChon();
        }

        private void TinhThanhTienHangChon()
        {
            string rawGia = txtDonGiaNhap.Text.Replace(".", "").Replace(",", "").Replace("đ", "").Trim();
            decimal donGia = 0;
            decimal.TryParse(rawGia, out donGia);

            decimal thanhTien = donGia * nudSoLuong.Value;
            txtThanhTien.Text = thanhTien.ToString("N0") + " đ";
        }

        private void TinhTongKetHoaDon()
        {
            int tongMatHang = dtChiTietNhap.Rows.Count;
            int tongSoSanPham = 0;
            decimal tongTien = 0;

            foreach (DataRow row in dtChiTietNhap.Rows)
            {
                int sl = 0;
                int.TryParse(row["Số lượng"]?.ToString(), out sl);
                tongSoSanPham += sl;

                string rawTien = row["Thành tiền"]?.ToString().Replace(".", "").Replace(",", "").Replace("đ", "").Trim();
                decimal tien = 0;
                decimal.TryParse(rawTien, out tien);
                tongTien += tien;
            }

            lblTongSoLuong.Text = $"Tổng số lượng mặt hàng: {tongMatHang} loại ({tongSoSanPham} sản phẩm)";
            lblTongTienGiaTri.Text = tongTien.ToString("N0") + " đ";
        }
        #endregion

        #region THAO TÁC TRÊN BẢNG
        private void BtnThemVaoPhieu_Click(object sender, EventArgs e)
        {
            string spText = cboSanPham.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(spText)) return;

            string maSP = spText.Substring(0, 5);
            string tenSP = spText.Substring(8);
            string loai = "Váy cưới";
            string mauSac = "Trắng";

            if (maSP == "SP003") { loai = "Vest"; mauSac = "Đen"; }
            else if (maSP == "SP004") { loai = "Áo dài"; mauSac = "Đỏ thẫm"; }

            int soLuong = (int)nudSoLuong.Value;
            string donGiaStr = txtDonGiaNhap.Text.Trim();
            string thanhTienStr = txtThanhTien.Text.Trim();

            // Nếu sản phẩm đã tồn tại thì cộng dồn số lượng
            DataRow[] found = dtChiTietNhap.Select($"[Mã SP] = '{maSP}'");
            if (found.Length > 0)
            {
                int slCu = (int)found[0]["Số lượng"];
                int slMoi = slCu + soLuong;
                found[0]["Số lượng"] = slMoi;

                string rawDonGia = donGiaStr.Replace(".", "").Replace(",", "").Replace("đ", "").Trim();
                decimal dg = 0;
                decimal.TryParse(rawDonGia, out dg);
                found[0]["Thành tiền"] = (dg * slMoi).ToString("N0") + " đ";
            }
            else
            {
                dtChiTietNhap.Rows.Add(maSP, tenSP, loai, mauSac, soLuong, donGiaStr + " đ", thanhTienStr);
            }

            TinhTongKetHoaDon();
        }

        private void DgvChiTietNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChiTietNhap.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                string tenSP = dtChiTietNhap.Rows[e.RowIndex]["Tên sản phẩm"].ToString();
                DialogResult res = MessageBox.Show($"Bạn có muốn xóa [{tenSP}] khỏi phiếu nhập không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    dtChiTietNhap.Rows.RemoveAt(e.RowIndex);
                    TinhTongKetHoaDon();
                }
            }
        }

        private void LamMoiToanBoPhieu()
        {
            dtChiTietNhap.Rows.Clear();
            nudSoLuong.Value = 1;
            TinhThanhTienHangChon();
            TinhTongKetHoaDon();
            txtGhiChu.Text = "";
        }

        private void BtnLuuNhapKho_Click(object sender, EventArgs e)
        {
            if (dtChiTietNhap.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập chưa có sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Đã lưu và nhập kho thành công hóa đơn [{txtMaHDN.Text}]!\nTổng giá trị: {lblTongTienGiaTri.Text}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}