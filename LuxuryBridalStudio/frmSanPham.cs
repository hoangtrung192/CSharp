using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmSanPham : Form
    {
        private DataTable dtSanPham = new DataTable();
        private bool isThemMoi = false;
        private const string PLACEHOLDER_TEXT = "Nhập tên váy cưới, vest...";

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            KhoiTaoDuLieuBanDau();
            GanSuKien();
            CaiDatPlaceholder();

            cboLoaiSP.SelectedIndex = 0;
            cboMauSac.SelectedIndex = 0;
            cboNoiSX.SelectedIndex = 0;

            if (dgvSanPham.Rows.Count > 0)
            {
                HienThiChiTiet(0);
            }
        }

        #region PLACEHOLDER VÀ SỰ KIỆN TÌM KIẾM
        private void CaiDatPlaceholder()
        {
            txtTuKhoa.Text = PLACEHOLDER_TEXT;
            txtTuKhoa.ForeColor = Color.Gray;

            txtTuKhoa.Enter += (s, e) =>
            {
                if (txtTuKhoa.Text == PLACEHOLDER_TEXT)
                {
                    txtTuKhoa.Text = "";
                    txtTuKhoa.ForeColor = Color.Black;
                }
            };

            txtTuKhoa.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTuKhoa.Text))
                {
                    txtTuKhoa.Text = PLACEHOLDER_TEXT;
                    txtTuKhoa.ForeColor = Color.Gray;
                }
            };
        }

        private void GanSuKien()
        {
            dgvSanPham.CellClick += DgvSanPham_CellClick;
            dgvSanPham.CellFormatting += DgvSanPham_CellFormatting;

            btnThemSP.Click += BtnThemSP_Click;
            btnLuu.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnChonAnh.LinkClicked += BtnChonAnh_LinkClicked;

            // Chỉ lọc khi nhấn nút Tra cứu hoặc gõ xong ấn Enter
            btnTraCuu.Click += (s, e) => ThucHienLocDuLieu();
            txtTuKhoa.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ThucHienLocDuLieu();
                }
            };
        }
        #endregion

        #region KHỞI TẠO DỮ LIỆU MOCK DATA
        private void KhoiTaoDuLieuBanDau()
        {
            dtSanPham.Columns.Clear();
            dtSanPham.Columns.Add("Mã SP");
            dtSanPham.Columns.Add("Tên trang phục");
            dtSanPham.Columns.Add("Loại");
            dtSanPham.Columns.Add("Màu sắc");
            dtSanPham.Columns.Add("Nơi SX");
            dtSanPham.Columns.Add("Tồn kho", typeof(int));
            dtSanPham.Columns.Add("Giá thuê");
            dtSanPham.Columns.Add("Giá nhập");
            dtSanPham.Columns.Add("HinhAnh"); // Lưu đường dẫn ảnh nếu có

            dtSanPham.Rows.Add("SP001", "Váy cưới công chúa đính đá", "Váy cưới", "Trắng", "Việt Nam", 2, "3.500.000 đ", "10.000.000 đ", "");
            dtSanPham.Rows.Add("SP002", "Váy cưới cúp ngực Satin", "Váy cưới", "Trắng kem", "Hàn Quốc", 1, "2.800.000 đ", "8.000.000 đ", "");
            dtSanPham.Rows.Add("SP003", "Vest chú rể classic đen", "Vest", "Đen", "Việt Nam", 4, "1.500.000 đ", "3.000.000 đ", "");
            dtSanPham.Rows.Add("SP004", "Áo dài cưới Long Phụng", "Áo dài", "Đỏ thẫm", "Việt Nam", 0, "Hết hàng", "4.000.000 đ", "");
            dtSanPham.Rows.Add("SP005", "Váy cưới đuôi cá phối ren", "Váy cưới", "Trắng", "Pháp", 3, "4.200.000 đ", "12.000.000 đ", "");

            dgvSanPham.DataSource = dtSanPham;
            dgvSanPham.Columns["Giá nhập"].Visible = false;
            dgvSanPham.Columns["HinhAnh"].Visible = false;
        }

        // Định dạng màu chữ "Hết hàng" thành màu đỏ nhạt như trong ảnh mockup
        private void DgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSanPham.Columns[e.ColumnIndex].Name == "Giá thuê" && e.Value != null)
            {
                if (e.Value.ToString() == "Hết hàng")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(215, 80, 95);
                    e.CellStyle.SelectionForeColor = Color.FromArgb(215, 80, 95);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }
        #endregion

        #region XỬ LÝ CHI TIẾT VÀ CHỌN ẢNH
        private void DgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSanPham.Rows.Count)
            {
                isThemMoi = false;
                HienThiChiTiet(e.RowIndex);
            }
        }

        private void HienThiChiTiet(int rowIndex)
        {
            DataGridViewRow row = dgvSanPham.Rows[rowIndex];
            txtMaSP.Text = row.Cells["Mã SP"].Value?.ToString();
            txtTenSP.Text = row.Cells["Tên trang phục"].Value?.ToString();
            cboLoaiSPDetail.SelectedItem = row.Cells["Loại"].Value?.ToString();
            cboMauSacDetail.SelectedItem = row.Cells["Màu sắc"].Value?.ToString();
            cboNoiSXDetail.SelectedItem = row.Cells["Nơi SX"].Value?.ToString();

            int tonKho = 0;
            int.TryParse(row.Cells["Tồn kho"].Value?.ToString(), out tonKho);
            nudSoLuongTon.Value = tonKho;

            txtGiaNhap.Text = row.Cells["Giá nhập"].Value?.ToString();
            txtGiaThue.Text = row.Cells["Giá thuê"].Value?.ToString();

            string duongDanAnh = row.Cells["HinhAnh"].Value?.ToString();
            if (!string.IsNullOrEmpty(duongDanAnh) && File.Exists(duongDanAnh))
            {
                picAnhSP.Image = Image.FromFile(duongDanAnh);
            }
            else
            {
                picAnhSP.Image = null; // Trống nếu chưa có ảnh
            }
        }

        private void BtnChonAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.jfif)|*.jpg;*.jpeg;*.png;*.jfif";
                ofd.Title = "Chọn ảnh sản phẩm";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picAnhSP.Image = Image.FromFile(ofd.FileName);
                    picAnhSP.Tag = ofd.FileName; // Lưu tạm đường dẫn ảnh
                }
            }
        }
        #endregion

        #region THAO TÁC THÊM, SỬA, XÓA, LỌC
        private void BtnThemSP_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            LamMoiFormNhap();

            int nextId = dtSanPham.Rows.Count + 1;
            txtMaSP.Text = "SP" + nextId.ToString("D3");
            txtTenSP.Focus();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return;
            }

            string maSP = txtMaSP.Text.Trim();
            string anhPath = picAnhSP.Tag != null ? picAnhSP.Tag.ToString() : "";
            DataRow[] foundRows = dtSanPham.Select($"[Mã SP] = '{maSP}'");

            if (isThemMoi || foundRows.Length == 0)
            {
                dtSanPham.Rows.Add(maSP, txtTenSP.Text.Trim(), cboLoaiSPDetail.Text, cboMauSacDetail.Text,
                                   cboNoiSXDetail.Text, (int)nudSoLuongTon.Value, txtGiaThue.Text.Trim(),
                                   txtGiaNhap.Text.Trim(), anhPath);
                MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isThemMoi = false;
            }
            else
            {
                foundRows[0]["Tên trang phục"] = txtTenSP.Text.Trim();
                foundRows[0]["Loại"] = cboLoaiSPDetail.Text;
                foundRows[0]["Màu sắc"] = cboMauSacDetail.Text;
                foundRows[0]["Nơi SX"] = cboNoiSXDetail.Text;
                foundRows[0]["Tồn kho"] = (int)nudSoLuongTon.Value;
                foundRows[0]["Giá thuê"] = txtGiaThue.Text.Trim();
                foundRows[0]["Giá nhập"] = txtGiaNhap.Text.Trim();
                if (!string.IsNullOrEmpty(anhPath))
                {
                    foundRows[0]["HinhAnh"] = anhPath;
                }
                MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm [{txtTenSP.Text}] không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                DataRow[] rows = dtSanPham.Select($"[Mã SP] = '{txtMaSP.Text.Trim()}'");
                if (rows.Length > 0)
                {
                    dtSanPham.Rows.Remove(rows[0]);
                    LamMoiFormNhap();
                    MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            isThemMoi = false;
            LamMoiFormNhap();
            txtTuKhoa.Text = PLACEHOLDER_TEXT;
            txtTuKhoa.ForeColor = Color.Gray;
            cboLoaiSP.SelectedIndex = 0;
            cboMauSac.SelectedIndex = 0;
            cboNoiSX.SelectedIndex = 0;
            dtSanPham.DefaultView.RowFilter = "";
        }

        private void LamMoiFormNhap()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            nudSoLuongTon.Value = 0;
            txtGiaNhap.Text = "";
            txtGiaThue.Text = "";
            picAnhSP.Image = null;
            picAnhSP.Tag = null;
        }

        private void ThucHienLocDuLieu()
        {
            string filter = "1=1";

            if (cboLoaiSP.SelectedIndex > 0)
                filter += $" AND [Loại] = '{cboLoaiSP.Text}'";

            if (cboMauSac.SelectedIndex > 0)
                filter += $" AND [Màu sắc] = '{cboMauSac.Text}'";

            if (cboNoiSX.SelectedIndex > 0)
                filter += $" AND [Nơi SX] = '{cboNoiSX.Text}'";

            string keyword = txtTuKhoa.Text.Trim().Replace("'", "''");
            if (!string.IsNullOrEmpty(keyword) && keyword != PLACEHOLDER_TEXT)
            {
                filter += $" AND [Tên trang phục] LIKE '%{keyword}%'";
            }

            dtSanPham.DefaultView.RowFilter = filter;

            if (dgvSanPham.Rows.Count > 0)
            {
                HienThiChiTiet(0);
            }
        }
        #endregion
    }
}