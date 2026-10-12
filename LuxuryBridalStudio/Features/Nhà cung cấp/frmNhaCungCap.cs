using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmNhaCungCap : Form
    {
        private DataTable dtNhaCungCap = new DataTable();
        private bool isThemMoi = false;
        private const string PLACEHOLDER_TEXT = "Nhập tên nhà cung cấp, số điện thoại hoặc địa chỉ...";

        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            KhoiTaoDuLieuBanDau();
            GanSuKien();
            CaiDatPlaceholderTimKiem();

            cboSapXep.SelectedIndex = 0;
            CapNhatTongSoLuong();

            if (dgvNhaCungCap.Rows.Count > 0)
            {
                HienThiChiTiet(0);
            }
        }

        #region PLACEHOLDER VÀ SỰ KIỆN
        private void CaiDatPlaceholderTimKiem()
        {
            txtTimKiem.Text = PLACEHOLDER_TEXT;
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == PLACEHOLDER_TEXT)
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = PLACEHOLDER_TEXT;
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };
        }

        private void GanSuKien()
        {
            dgvNhaCungCap.CellClick += DgvNhaCungCap_CellClick;
            btnThemNCC.Click += BtnThemNCC_Click;
            btnLuu.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            cboSapXep.SelectedIndexChanged += CboSapXep_SelectedIndexChanged;

            btnTraCuu.Click += (s, e) => ThucHienTimKiem();
            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ThucHienTimKiem();
                }
            };
        }
        #endregion

        #region KHỞI TẠO DỮ LIỆU MOCK DATA
        private void KhoiTaoDuLieuBanDau()
        {
            dtNhaCungCap.Columns.Clear();
            dtNhaCungCap.Columns.Add("Mã NCC");
            dtNhaCungCap.Columns.Add("Tên nhà cung cấp");
            dtNhaCungCap.Columns.Add("Số điện thoại");
            dtNhaCungCap.Columns.Add("Địa chỉ");
            dtNhaCungCap.Columns.Add("Ghi chú");

            dtNhaCungCap.Rows.Add("NCC001", "Xưởng May Bella Bridal", "024 3822 5678", "102 Bà Triệu, Hoàn Kiếm, Hà Nội", "Đối tác cung cấp dòng váy cưới ren cao cấp");
            dtNhaCungCap.Rows.Add("NCC002", "Thời Trang HERA", "028 3930 1234", "45 Nam Kỳ Khởi Nghĩa, Q.3, TP. HCM", "Chuyên áo cưới xuất khẩu và satin");
            dtNhaCungCap.Rows.Add("NCC003", "Xưởng Vest Hoàng Gia", "0918 222 333", "88 Nguyễn Đình Chiểu, Q.1, TP. HCM", "Cung cấp vest chú rể cao cấp");
            dtNhaCungCap.Rows.Add("NCC004", "Áo Dài Truyền Thống", "0905 456 789", "12 Lê Lợi, TP. Huế", "Áo dài gấm thêu tay thủ công");
            dtNhaCungCap.Rows.Add("NCC005", "Phụ Kiện Diamond Studio", "0977 888 999", "25 Kim Mã, Ba Đình, Hà Nội", "Vương miện, voan cài đầu, nơ cài áo");

            dgvNhaCungCap.DataSource = dtNhaCungCap;

            // Bật hiển thị cột Ghi chú và căn tỷ lệ chiều rộng các cột
            dgvNhaCungCap.Columns["Ghi chú"].Visible = true;
            dgvNhaCungCap.Columns["Mã NCC"].FillWeight = 50;
            dgvNhaCungCap.Columns["Tên nhà cung cấp"].FillWeight = 110;
            dgvNhaCungCap.Columns["Số điện thoại"].FillWeight = 75;
            dgvNhaCungCap.Columns["Địa chỉ"].FillWeight = 115;
            dgvNhaCungCap.Columns["Ghi chú"].FillWeight = 110;
        }
        #endregion

        #region HIỂN THỊ CHI TIẾT
        private void DgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhaCungCap.Rows.Count)
            {
                isThemMoi = false;
                HienThiChiTiet(e.RowIndex);
            }
        }

        private void HienThiChiTiet(int rowIndex)
        {
            DataGridViewRow row = dgvNhaCungCap.Rows[rowIndex];
            txtMaNCC.Text = row.Cells["Mã NCC"].Value?.ToString();
            txtTenNCC.Text = row.Cells["Tên nhà cung cấp"].Value?.ToString();
            txtDienThoai.Text = row.Cells["Số điện thoại"].Value?.ToString();
            txtDiaChi.Text = row.Cells["Địa chỉ"].Value?.ToString();
            txtGhiChu.Text = row.Cells["Ghi chú"].Value?.ToString();
        }
        #endregion

        #region THAO TÁC THÊM, SỬA, XÓA, LÀM MỚI
        private void BtnThemNCC_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            LamMoiFormNhap();

            int nextId = dtNhaCungCap.Rows.Count + 1;
            txtMaNCC.Text = "NCC" + nextId.ToString("D3");
            txtTenNCC.Focus();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            string maNCC = txtMaNCC.Text.Trim();
            DataRow[] foundRows = dtNhaCungCap.Select($"[Mã NCC] = '{maNCC}'");

            if (isThemMoi || foundRows.Length == 0)
            {
                dtNhaCungCap.Rows.Add(maNCC, txtTenNCC.Text.Trim(), txtDienThoai.Text.Trim(), txtDiaChi.Text.Trim(), txtGhiChu.Text.Trim());
                MessageBox.Show("Thêm mới nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isThemMoi = false;
            }
            else
            {
                foundRows[0]["Tên nhà cung cấp"] = txtTenNCC.Text.Trim();
                foundRows[0]["Số điện thoại"] = txtDienThoai.Text.Trim();
                foundRows[0]["Địa chỉ"] = txtDiaChi.Text.Trim();
                foundRows[0]["Ghi chú"] = txtGhiChu.Text.Trim();
                MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CapNhatTongSoLuong();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult res = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp [{txtTenNCC.Text}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                DataRow[] rows = dtNhaCungCap.Select($"[Mã NCC] = '{txtMaNCC.Text.Trim()}'");
                if (rows.Length > 0)
                {
                    dtNhaCungCap.Rows.Remove(rows[0]);
                    LamMoiFormNhap();
                    CapNhatTongSoLuong();
                    MessageBox.Show("Đã xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            isThemMoi = false;
            LamMoiFormNhap();
            txtTimKiem.Text = PLACEHOLDER_TEXT;
            txtTimKiem.ForeColor = Color.Gray;
            dtNhaCungCap.DefaultView.RowFilter = "";
            CapNhatTongSoLuong();
        }

        private void LamMoiFormNhap()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtGhiChu.Text = "";
        }

        private void CapNhatTongSoLuong()
        {
            lblTongCong.Text = $"Tổng cộng: {dgvNhaCungCap.Rows.Count} nhà cung cấp";
        }
        #endregion

        #region TÌM KIẾM VÀ SẮP XẾP
        private void ThucHienTimKiem()
        {
            string keyword = txtTimKiem.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(keyword) || keyword == PLACEHOLDER_TEXT)
            {
                dtNhaCungCap.DefaultView.RowFilter = "";
            }
            else
            {
                dtNhaCungCap.DefaultView.RowFilter = $"[Tên nhà cung cấp] LIKE '%{keyword}%' OR [Số điện thoại] LIKE '%{keyword}%' OR [Địa chỉ] LIKE '%{keyword}%'";
            }

            CapNhatTongSoLuong();

            if (dgvNhaCungCap.Rows.Count > 0)
            {
                HienThiChiTiet(0);
            }
        }

        private void CboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSapXep.SelectedIndex == 0) // Mới nhất
            {
                dtNhaCungCap.DefaultView.Sort = "[Mã NCC] DESC";
            }
            else if (cboSapXep.SelectedIndex == 1) // Tên (A-Z)
            {
                dtNhaCungCap.DefaultView.Sort = "[Tên nhà cung cấp] ASC";
            }
            else if (cboSapXep.SelectedIndex == 2) // Mã NCC
            {
                dtNhaCungCap.DefaultView.Sort = "[Mã NCC] ASC";
            }
        }
        #endregion
    }
}