using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    
    public partial class frmDonHang : Form
    {
        

        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataTable dtMenuHaiSan;
        private string maNhanVien;
        private string tenNhanVien;
        public frmDonHang(string maNV, string tenNV)
        {
            InitializeComponent();
            // Khởi tạo chuỗi kết nối
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");

            dtMenuHaiSan = new DataTable();
            this.maNhanVien = maNV;
            this.tenNhanVien = tenNV;

            // Hiển thị thông tin lên giao diện của frmDonHang
            txtMaNhanVien.Text = this.maNhanVien;
            txtTenNhanVien.Text = this.tenNhanVien;
            dgvDanhSach.EditingControlShowing += dgvDanhSach_EditingControlShowing;
            // Hiển thị thông tin lên giao diện của frmDonHang
            txtMaNhanVien.Text = this.maNhanVien;
            txtTenNhanVien.Text = this.tenNhanVien;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmDonHang_Load(object sender, EventArgs e)
        {
            LoadDataFromMenuHaiSan();
            SetDataGridViewColumnWidth();
            DisplayLoggedInUserInfo();
            chkChuaThanhToan.Checked = true;
        }
        private void DisplayLoggedInUserInfo()
        {
            txtMaNhanVien.Text = maNhanVien;
            txtTenNhanVien.Text = tenNhanVien;
        }

        private void SetDataGridViewColumnWidth()
        {
            // Thiết lập AutoSizeMode cho từng cột để đảm bảo hiển thị đầy đủ nội dung
            foreach (DataGridViewColumn column in dgvDanhSach.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void LoadDataFromMenuHaiSan()
        {
            string query = "SELECT MaMon, TenMon, Gia, MoTa, TinhTrang, SoLuong FROM MenuHaiSan";
            adapter = new SqlDataAdapter(query, conn);

            try
            {
                conn.Open();
                dtMenuHaiSan.Clear();
                adapter.Fill(dtMenuHaiSan);

                // Lọc dữ liệu để loại bỏ các dòng có TinhTrang là 'Hết Hàng'
                DataView dv = new DataView(dtMenuHaiSan);
                dv.RowFilter = "TinhTrang <> 'Hết Hàng'";
                dgvDanhSach.DataSource = dv;

                if (dv.Count > 0)
                {
                    dgvDanhSach.Columns["MaMon"].ReadOnly = true;
                    dgvDanhSach.Columns["TenMon"].ReadOnly = true;
                    dgvDanhSach.Columns["Gia"].ReadOnly = true;
                    dgvDanhSach.Columns["MoTa"].ReadOnly = true;
                    dgvDanhSach.Columns["TinhTrang"].ReadOnly = true;
                    dgvDanhSach.Columns["SoLuong"].ReadOnly = false;
                    // Đổi tên tiêu đề cột
                    dgvDanhSach.Columns["MaMon"].HeaderText = "Mã món";
                    dgvDanhSach.Columns["TenMon"].HeaderText = "Tên món";
                    dgvDanhSach.Columns["Gia"].HeaderText = "Giá (VND)";
                    dgvDanhSach.Columns["MoTa"].HeaderText = "Mô tả";
                    dgvDanhSach.Columns["TinhTrang"].HeaderText = "Tình trạng";
                    dgvDanhSach.Columns["SoLuong"].HeaderText = "Số lượng";

                    // Đặt căn lề phải cho cột Giá
                    dgvDanhSach.Columns["Gia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDanhSach.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu từ bảng MenuHaiSan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnThemDonHang_Click(object sender, EventArgs e)
        {
            string maHoaDon = GenerateInvoiceCode();
            string tenKhachHang = txtTenKH.Text;
            string sdt = txtSdt.Text;

            decimal tongTien = 0;
            decimal.TryParse(txtTongTien.Text, out tongTien);
            string trangThai = chkDaThanhToan.Checked ? "Đã thanh toán" : "Chưa thanh toán";
            string phuongThuc = "Tiền mặt"; // hoặc thay đổi theo nhu cầu

            if (string.IsNullOrEmpty(tenKhachHang))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng");
                return;
            }

            if (!chkDaThanhToan.Checked && !chkChuaThanhToan.Checked)
            {
                MessageBox.Show("Vui lòng chọn trạng thái thanh toán.");
                return;
            }

            if (tongTien <= 0)
            {
                MessageBox.Show("Tổng tiền phải lớn hơn 0.");
                return;
            }

            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                // Insert into HoaDon and get the MaHoaDon
                string insertHoaDon = "INSERT INTO HoaDon (NgayLap, TenKhachHang, SDT, TrangThai, PhuongThuc, TongTien) OUTPUT INSERTED.MaHoaDon VALUES (@NgayLap, @TenKhachHang, @SDT, @TrangThai, @PhuongThuc, @TongTien)";
                SqlCommand cmdInsertHoaDon = new SqlCommand(insertHoaDon, conn, transaction);
                cmdInsertHoaDon.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                cmdInsertHoaDon.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                cmdInsertHoaDon.Parameters.AddWithValue("@SDT", sdt);
                cmdInsertHoaDon.Parameters.AddWithValue("@TrangThai", trangThai);
                cmdInsertHoaDon.Parameters.AddWithValue("@PhuongThuc", phuongThuc);
                cmdInsertHoaDon.Parameters.AddWithValue("@TongTien", tongTien);

                int newMaHoaDon = (int)cmdInsertHoaDon.ExecuteScalar();

                // Use newMaHoaDon for ChiTietHoaDon insertion
                foreach (DataGridViewRow row in dgvDanhSach.Rows)
                {
                    if (row.Cells["SoLuong"].Value != null && Convert.ToInt32(row.Cells["SoLuong"].Value) > 0)
                    {
                        string tenSanPham = row.Cells["TenMon"].Value.ToString();
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["Gia"].Value);

                        string insertChiTietHoaDon = "INSERT INTO ChiTietHoaDon (MaHoaDon, TenSanPham, SoLuong, DonGia, TenKhachHang) VALUES (@MaHoaDon, @TenSanPham, @SoLuong, @DonGia, @TenKhachHang)";
                        SqlCommand cmdInsertChiTietHoaDon = new SqlCommand(insertChiTietHoaDon, conn, transaction);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@MaHoaDon", newMaHoaDon);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@DonGia", donGia);
                        cmdInsertChiTietHoaDon.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);

                        cmdInsertChiTietHoaDon.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                MessageBox.Show("Thêm hóa đơn thành công.");
                ResetForm();
                chkChuaThanhToan.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void CalculateTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in dgvDanhSach.Rows)
            {
                if (row.Cells["SoLuong"].Value != null && row.Cells["Gia"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["Gia"].Value);
                    decimal amount = quantity * price;
                    totalAmount += amount;
                }
            }
            txtTongTien.Text = totalAmount.ToString("N0"); // Định dạng số với dấu chấm sau mỗi 3 chữ số
        }

        private void dgvDanhSach_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu cột hiện tại là cột "Số lượng" và ô không rỗng
            if (e.ColumnIndex == dgvDanhSach.Columns["SoLuong"].Index && e.RowIndex >= 0)
            {
                // Lấy giá trị của ô
                var cellValue = dgvDanhSach.Rows[e.RowIndex].Cells["SoLuong"].Value;

                // Nếu giá trị là null hoặc rỗng, gán giá trị mặc định là 0
                if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    dgvDanhSach.Rows[e.RowIndex].Cells["SoLuong"].Value = 0;
                }

                // Cập nhật tổng tiền
                CalculateTotalAmount();
            }
        }
        private void UpdateRowAmount(int rowIndex)
        {
            if (dgvDanhSach.Rows[rowIndex].Cells["SoLuong"].Value != null && dgvDanhSach.Rows[rowIndex].Cells["Gia"].Value != null)
            {
                int quantity = Convert.ToInt32(dgvDanhSach.Rows[rowIndex].Cells["SoLuong"].Value);
                decimal price = Convert.ToDecimal(dgvDanhSach.Rows[rowIndex].Cells["Gia"].Value);
                decimal amount = quantity * price;
                dgvDanhSach.Rows[rowIndex].Cells["ThanhTien"].Value = amount.ToString("N0"); // Định dạng số với dấu chấm sau mỗi 3 chữ số
            }
        }
        private string GenerateInvoiceCode()
        {
            int maxNumber = 0;

            try
            {
                conn.Open();
                string query = "SELECT MAX(CAST(MaHoaDon AS INT)) FROM HoaDon";

                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    maxNumber = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return (maxNumber + 1).ToString();
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkDaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDaThanhToan.Checked)
            {
                chkChuaThanhToan.Checked = false;
            }
        }

        private void chkChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChuaThanhToan.Checked)
            {
                chkDaThanhToan.Checked = false;
            }
        }
        private void ResetForm()
        {
            txtTenKH.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtTongTien.Text = "0";
            chkDaThanhToan.Checked = false;
            chkChuaThanhToan.Checked = false;

            foreach (DataGridViewRow row in dgvDanhSach.Rows)
            {
                row.Cells["SoLuong"].Value = 0;
            }

            dgvDanhSach.ClearSelection();
        }

        private void btnDonDaDat_Click(object sender, EventArgs e)
        {
            frmDonDaDat ddd = new frmDonDaDat();
            ddd.Show();
        }

        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "Gia" && e.Value != null)
            {
                if (e.Value is decimal)
                {
                    e.Value = ((decimal)e.Value).ToString("#,##0"); // Định dạng lại số tiền
                    e.FormattingApplied = true;
                }
            }
        }

        private void lblMaNhanVien_Click(object sender, EventArgs e)
        {

        }
        private void SetReadOnlyColumns()
        {
            foreach (string colName in new[] { "MaMon", "TenMon", "Gia", "MoTa", "TinhTrang" })
            {
                dgvDanhSach.Columns[colName].ReadOnly = true;
            }
            dgvDanhSach.Columns["SoLuong"].ReadOnly = false;
        }

        private void txtTenNhanVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNhanVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvDanhSach_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Xóa sự kiện KeyPress cũ nếu có
            e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);

            // Kiểm tra nếu cột hiện tại là cột Số lượng
            if (dgvDanhSach.CurrentCell.ColumnIndex == dgvDanhSach.Columns["SoLuong"].Index)
            {
                // Thêm sự kiện KeyPress mới
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự nhập vào không phải là số hoặc là ký tự điều khiển (backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Vui lòng nhập số.");
            }
        }
        private void dgvDanhSach_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }
    }
}


