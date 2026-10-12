using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LuxuryBridalStudio
{
    public partial class frmKhachHang : Form
    {
        private DataTable dtKhachHang = new DataTable();
        private bool isThemMoi = false;
        private const string PLACEHOLDER_TEXT = "Nhập họ tên, số điện thoại hoặc số CCCD...";

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            KhoiTaoDuLieuBanDau();
            GanSuKienCacNut();
            CaiDatPlaceholderTimKiem();

            cboSapXep.SelectedIndex = 0;
            CapNhatTongSoLuong();

            if (dgvKhachHang.Rows.Count > 0)
            {
                HienThiChiTiet(0);
            }
        }

        #region XỬ LÝ PLACEHOLDER TỰ ĐỘNG BIẾN MẤT
        private void CaiDatPlaceholderTimKiem()
        {
            txtTimKiem.Text = PLACEHOLDER_TEXT;
            txtTimKiem.ForeColor = Color.Gray;

            // Khi click chuột vào ô tìm kiếm: Tự động xóa placeholder
            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == PLACEHOLDER_TEXT)
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            // Khi click chuột ra ngoài: Nếu chưa gõ gì thì hiện lại placeholder
            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = PLACEHOLDER_TEXT;
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };
        }
        #endregion

        #region KHỞI TẠO DỮ LIỆU
        private void KhoiTaoDuLieuBanDau()
        {
            dtKhachHang.Columns.Clear();
            dtKhachHang.Columns.Add("Mã KH");
            dtKhachHang.Columns.Add("Họ và tên");
            dtKhachHang.Columns.Add("Số điện thoại");
            dtKhachHang.Columns.Add("Số CCCD");
            dtKhachHang.Columns.Add("Địa chỉ");

            dtKhachHang.Rows.Add("KH001", "Nguyễn Thu Hà", "0901 234 567", "079186001234", "123 Lê Lợi, Q.1, TP. HCM");
            dtKhachHang.Rows.Add("KH002", "Trần Thị Mai Anh", "0987 654 321", "001206012345", "45 Cầu Giấy, Hà Nội");
            dtKhachHang.Rows.Add("KH003", "Hoàng Thùy Linh", "0963 111 222", "038195007890", "78 Trần Phú, Hà Đông, H");
            dtKhachHang.Rows.Add("KH004", "Lê Hoàng Yến", "0912 345 678", "025198009876", "12 Kim Mã, Ba Đình, H");
            dtKhachHang.Rows.Add("KH005", "Phạm Minh Đức", "0935 678 901", "001099003456", "89 Nguyễn Trãi, Thanh");

            dgvKhachHang.DataSource = dtKhachHang;
        }

        private void GanSuKienCacNut()
        {
            dgvKhachHang.CellClick += DgvKhachHang_CellClick;
            btnThemKhach.Click += BtnThemKhach_Click;
            btnLuu.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            cboSapXep.SelectedIndexChanged += CboSapXep_SelectedIndexChanged;

            btnTraCuu.Click += BtnTraCuu_Click;
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;
        }
        #endregion

        #region XỬ LÝ TÌM KIẾM
        private void BtnTraCuu_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void TxtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThucHienTimKiem();
            }
        }

        private void ThucHienTimKiem()
        {
            string keyword = txtTimKiem.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(keyword) || keyword == PLACEHOLDER_TEXT)
            {
                dtKhachHang.DefaultView.RowFilter = "";
            }
            else
            {
                dtKhachHang.DefaultView.RowFilter = $"[Họ và tên] LIKE '%{keyword}%' OR [Số điện thoại] LIKE '%{keyword}%' OR [Số CCCD] LIKE '%{keyword}%'";
            }

            CapNhatTongSoLuong();

            if (dgvKhachHang.Rows.Count > 0)
            {
                HienThiChiTiet(0);
            }
        }
        #endregion

        #region XỬ LÝ CHI TIẾT VÀ CÁC THAO TÁC KHÁC
        private void DgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count)
            {
                isThemMoi = false;
                HienThiChiTiet(e.RowIndex);
            }
        }

        private void HienThiChiTiet(int rowIndex)
        {
            DataGridViewRow row = dgvKhachHang.Rows[rowIndex];
            txtMaKH.Text = row.Cells["Mã KH"].Value?.ToString();
            txtHoTen.Text = row.Cells["Họ và tên"].Value?.ToString();
            txtDienThoai.Text = row.Cells["Số điện thoại"].Value?.ToString();
            txtCCCD.Text = row.Cells["Số CCCD"].Value?.ToString();
            txtDiaChi.Text = row.Cells["Địa chỉ"].Value?.ToString();
        }

        private void BtnThemKhach_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            LamMoiFormNhap();

            int nextId = dtKhachHang.Rows.Count + 1;
            txtMaKH.Text = "KH" + nextId.ToString("D3");
            txtHoTen.Focus();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            string maKH = txtMaKH.Text.Trim();
            DataRow[] foundRows = dtKhachHang.Select($"[Mã KH] = '{maKH}'");

            if (isThemMoi || foundRows.Length == 0)
            {
                dtKhachHang.Rows.Add(maKH, txtHoTen.Text.Trim(), txtDienThoai.Text.Trim(), txtCCCD.Text.Trim(), txtDiaChi.Text.Trim());
                MessageBox.Show("Thêm mới khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isThemMoi = false;
            }
            else
            {
                foundRows[0]["Họ và tên"] = txtHoTen.Text.Trim();
                foundRows[0]["Số điện thoại"] = txtDienThoai.Text.Trim();
                foundRows[0]["Số CCCD"] = txtCCCD.Text.Trim();
                foundRows[0]["Địa chỉ"] = txtDiaChi.Text.Trim();
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CapNhatTongSoLuong();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng [{txtHoTen.Text}] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                string maKH = txtMaKH.Text.Trim();
                DataRow[] rows = dtKhachHang.Select($"[Mã KH] = '{maKH}'");
                if (rows.Length > 0)
                {
                    dtKhachHang.Rows.Remove(rows[0]);
                    LamMoiFormNhap();
                    CapNhatTongSoLuong();
                    MessageBox.Show("Đã xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            isThemMoi = false;
            LamMoiFormNhap();
            txtTimKiem.Text = PLACEHOLDER_TEXT;
            txtTimKiem.ForeColor = Color.Gray;
            dtKhachHang.DefaultView.RowFilter = "";
            CapNhatTongSoLuong();
        }

        private void LamMoiFormNhap()
        {
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtDienThoai.Text = "";
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
        }

        private void CapNhatTongSoLuong()
        {
            int count = dgvKhachHang.Rows.Count;
            lblTongCong.Text = $"Tổng cộng: {count} khách hàng";
        }

        private void CboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSapXep.SelectedIndex == 0)
            {
                dtKhachHang.DefaultView.Sort = "[Mã KH] DESC";
            }
            else if (cboSapXep.SelectedIndex == 1)
            {
                dtKhachHang.DefaultView.Sort = "[Họ và tên] ASC";
            }
            else if (cboSapXep.SelectedIndex == 2)
            {
                dtKhachHang.DefaultView.Sort = "[Mã KH] ASC";
            }
        }
        #endregion
    }
}