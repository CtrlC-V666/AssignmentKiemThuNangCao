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
    public partial class frmDonDaDat : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataTable dtHoaDon;
        private DataTable dtHoaDonChuaThanhToan;
        private DataTable dtHoaDonDaThanhToan;
        public frmDonDaDat()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            dtHoaDon = new DataTable();
            this.StartPosition = FormStartPosition.CenterScreen;


        }
        private void CallStoredProcedure()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateDatabaseData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
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
        private void frmDonDaDat_Load(object sender, EventArgs e)
        {
            CallStoredProcedure();
            LoadDataFromHoaDon();
            SetDataGridViewColumnWidth();
            dgvDanhSachDon.ReadOnly = false;
            foreach (DataGridViewColumn column in dgvDanhSachDon.Columns)
            {
                column.ReadOnly = column.Name == "MaHoaDon"; // Không cho phép chỉnh sửa cột MaHoaDon
            }
            // Đặt tất cả các cột của dgvDanhSachDon thành ReadOnly
            foreach (DataGridViewColumn column in dgvDanhSachDon.Columns)
            {
                column.ReadOnly = true;
            }

            // Đặt tất cả các cột của dgvDonDaThanhToan thành ReadOnly
            foreach (DataGridViewColumn column in dgvDonDaThanhToan.Columns)
            {
                column.ReadOnly = true;
            }
        }
        private void SetDataGridViewColumnWidth()
        {
            // Thiết lập AutoSizeColumnsMode cho từng DataGridView để đảm bảo hiển thị phù hợp
            dgvDanhSachDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonDaThanhToan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Định dạng cột Thành Tiền căn lề phải
            if (dgvDanhSachDon.Columns.Contains("TongTien"))
            {
                dgvDanhSachDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDonDaThanhToan.Columns.Contains("TongTien"))
            {
                dgvDonDaThanhToan.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void LoadDataFromHoaDon()
        {
            string query = "SELECT MaHoaDon, Ngaylap, TenKhachHang, SDT, TrangThai, PhuongThuc, TongTien FROM HoaDon WHERE CONVERT(DATE, Ngaylap) = @Today ORDER BY MaHoaDon DESC";
            adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@Today", DateTime.Now.Date);

            try
            {
                conn.Open();
                DataTable dtHoaDon = new DataTable();
                dtHoaDon.Clear(); // Xóa dữ liệu cũ trước khi tải dữ liệu mới
                adapter.Fill(dtHoaDon);

                if (dtHoaDon.Rows.Count > 0)
                {
                    // Phân loại đơn hàng chưa thanh toán và đã thanh toán
                    dtHoaDonChuaThanhToan = dtHoaDon.Clone(); // Tạo bảng có cấu trúc tương tự
                    dtHoaDonDaThanhToan = dtHoaDon.Clone();

                    foreach (DataRow row in dtHoaDon.Rows)
                    {
                        if (row["TrangThai"].ToString() == "Chưa thanh toán")
                        {
                            dtHoaDonChuaThanhToan.ImportRow(row);
                        }
                        else
                        {
                            dtHoaDonDaThanhToan.ImportRow(row);
                        }
                    }

                    dgvDanhSachDon.DataSource = dtHoaDonChuaThanhToan;
                    dgvDonDaThanhToan.DataSource = dtHoaDonDaThanhToan;
                    dgvDanhSachDon.Columns["PhuongThuc"].Visible = false;
                    dgvDonDaThanhToan.Columns["PhuongThuc"].Visible = false;

                    // Đổi tên tiêu đề cột

                    dgvDanhSachDon.Columns["MaHoaDon"].HeaderText = "Mã hóa đơn";
                    dgvDanhSachDon.Columns["Ngaylap"].HeaderText = "Ngày lập";
                    dgvDanhSachDon.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                    dgvDanhSachDon.Columns["SDT"].HeaderText = "SĐT";
                    dgvDanhSachDon.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvDanhSachDon.Columns["PhuongThuc"].HeaderText = "Phương thức";
                    dgvDanhSachDon.Columns["TongTien"].HeaderText = "Thành tiền (VND)";

                    dgvDonDaThanhToan.Columns["MaHoaDon"].HeaderText = "Mã hóa hơn";
                    dgvDonDaThanhToan.Columns["Ngaylap"].HeaderText = "Ngày lập";
                    dgvDonDaThanhToan.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                    dgvDonDaThanhToan.Columns["SDT"].HeaderText = "SĐT";
                    dgvDonDaThanhToan.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvDonDaThanhToan.Columns["PhuongThuc"].HeaderText = "Phương thức";
                    dgvDonDaThanhToan.Columns["TongTien"].HeaderText = "Thành tiền (VND)";

                    // Định dạng cột Thành Tiền căn lề phải
                    dgvDanhSachDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDonDaThanhToan.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu từ bảng HoaDon.");
                
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDon.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvDanhSachDon.SelectedRows[0].Index;
                int maHoaDon = Convert.ToInt32(dgvDanhSachDon.Rows[selectedRowIndex].Cells["MaHoaDon"].Value);

                string queryDeleteChiTietHoaDon = "DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
                string queryDeleteHoaDon = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";

                // Sử dụng `using` để đảm bảo rằng các đối tượng được giải phóng đúng cách
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Xóa các chi tiết hóa đơn trước
                            using (SqlCommand cmdDeleteChiTietHoaDon = new SqlCommand(queryDeleteChiTietHoaDon, conn, transaction))
                            {
                                cmdDeleteChiTietHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                cmdDeleteChiTietHoaDon.ExecuteNonQuery();
                            }

                            // Xóa hóa đơn sau
                            using (SqlCommand cmdDeleteHoaDon = new SqlCommand(queryDeleteHoaDon, conn, transaction))
                            {
                                cmdDeleteHoaDon.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                cmdDeleteHoaDon.ExecuteNonQuery();
                            }

                            // Xác nhận giao dịch
                            transaction.Commit();
                            MessageBox.Show("Xóa đơn hàng thành công.");
                        }
                        catch (Exception ex)
                        {
                            // Hủy giao dịch nếu xảy ra lỗi
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi xóa đơn hàng: " + ex.Message);
                        }
                    }
                }

                // Cập nhật lại dữ liệu trên DataGridView
                LoadDataFromHoaDon();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xóa.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDon.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvDanhSachDon.SelectedRows[0].Index;
                int maHoaDon = Convert.ToInt32(dgvDanhSachDon.Rows[selectedRowIndex].Cells["MaHoaDon"].Value);

                // Lấy các giá trị mới từ DataGridView
                string tenKhachHang = dgvDanhSachDon.Rows[selectedRowIndex].Cells["TenKhachHang"].Value.ToString();
                string sdt = dgvDanhSachDon.Rows[selectedRowIndex].Cells["SDT"].Value.ToString();
                string trangThai = dgvDanhSachDon.Rows[selectedRowIndex].Cells["TrangThai"].Value.ToString();
                string phuongThuc = dgvDanhSachDon.Rows[selectedRowIndex].Cells["PhuongThuc"].Value.ToString();
                decimal tongTien = Convert.ToDecimal(dgvDanhSachDon.Rows[selectedRowIndex].Cells["TongTien"].Value);

                // Cập nhật thông tin đơn hàng vào cơ sở dữ liệu
                string query = "UPDATE HoaDon SET TenKhachHang = @TenKhachHang, SDT = @SDT, TrangThai = @TrangThai, PhuongThuc = @PhuongThuc, TongTien = @TongTien WHERE MaHoaDon = @MaHoaDon";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.Parameters.AddWithValue("@PhuongThuc", phuongThuc);
                    cmd.Parameters.AddWithValue("@TongTien", tongTien);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật đơn hàng thành công.");

                    // Cập nhật lại dữ liệu trên DataGridView
                    LoadDataFromHoaDon();
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
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để sửa.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDon.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvDanhSachDon.SelectedRows[0].Index;
                int maHoaDon = Convert.ToInt32(dgvDanhSachDon.Rows[selectedRowIndex].Cells["MaHoaDon"].Value);

                frmChiTietHoaDon frmChiTiet = new frmChiTietHoaDon(maHoaDon);

                // Đăng ký sự kiện với frmChiTietHoaDon
                frmChiTiet.PaymentConfirmed += FrmChiTiet_PaymentConfirmed;

                frmChiTiet.LoadChiTietHoaDon(maHoaDon);
                frmChiTiet.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xem chi tiết.");
            }
        }
        private void FrmChiTiet_PaymentConfirmed()
        {
            // Làm mới dữ liệu trên DataGridView
            LoadDataFromHoaDon();
        }
        private void dgvDanhSachDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDanhSachDon.Columns[e.ColumnIndex].Name == "TongTien" && e.Value != null)
            {
                if (e.Value is decimal)
                {
                    e.Value = ((decimal)e.Value).ToString("#,##0"); // Định dạng lại số tiền
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvDanhSachDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dgvDonDaThanhToan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDonDaThanhToan.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvDonDaThanhToan.SelectedRows[0].Index;
                int maHoaDon = Convert.ToInt32(dgvDonDaThanhToan.Rows[selectedRowIndex].Cells["MaHoaDon"].Value);

                frmChiTietDaThanhToan frmChiTiet = new frmChiTietDaThanhToan();
                frmChiTiet.LoadChiTietDonDaThanhToan(maHoaDon);
                frmChiTiet.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xem chi tiết.");
            }
        }

        private void btnXemChiTiet2_Click(object sender, EventArgs e)
        {
            if (dgvDonDaThanhToan.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvDonDaThanhToan.SelectedRows[0].Index;
                int maHoaDon = Convert.ToInt32(dgvDonDaThanhToan.Rows[selectedRowIndex].Cells["MaHoaDon"].Value);

                frmChiTietDaThanhToan frmChiTiet = new frmChiTietDaThanhToan();
                frmChiTiet.LoadChiTietDonDaThanhToan(maHoaDon);
                frmChiTiet.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xem chi tiết.");
            }
        }

        private void dgvDonDaThanhToan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDonDaThanhToan.Columns[e.ColumnIndex].Name == "TongTien" && e.Value != null)
            {
                if (e.Value is decimal)
                {
                    e.Value = ((decimal)e.Value).ToString("#,##0"); // Định dạng lại số tiền
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
