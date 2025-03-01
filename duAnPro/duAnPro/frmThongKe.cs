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
    public partial class frmThongKe : Form
    {
       private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
        
        
        public frmThongKe()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt các thuộc tính căn chỉnh cho các cột
            dgvThongKe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvThongKe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Attach the event handler for DataGridView row click
            dgvThongKe.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgvThongKe_RowHeaderMouseClick);
         
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(-1);
            dtpEndDate.Value = DateTime.Now;
           
        }
        private DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        adapter.SelectCommand.Parameters.Add(param);
                    }
                }

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
         
        }

        private void dgvThongKe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value is decimal)
            {
                // Định dạng số với dấu phân cách hàng nghìn
                e.Value = string.Format("{0:N0}", e.Value);
                e.FormattingApplied = true;
            }
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            string query = @"
                SELECT NgayLap, SUM(TongTien) AS DoanhThu
                FROM HoaDon
                WHERE NgayLap >= @StartDate AND NgayLap <= @EndDate
                GROUP BY NgayLap
                ORDER BY NgayLap;
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StartDate", dtpStartDate.Value),
                new SqlParameter("@EndDate", dtpEndDate.Value)
            };

            DataTable dataTable = ExecuteQuery(query, parameters);
            dgvThongKe.DataSource = dataTable;
            dgvThongKe.Columns["NgayLap"].HeaderText = "Ngày Lập";
            dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh Thu (VND)";
            dgvThongKe.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnThongKeMonAn_Click(object sender, EventArgs e)
        {
            string query = @"
                SELECT TenSanPham, SUM(SoLuong * DonGia) AS DoanhThu
                FROM ChiTietHoaDon
                JOIN HoaDon ON ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon
                WHERE NgayLap >= @StartDate AND NgayLap <= @EndDate
                GROUP BY TenSanPham
                ORDER BY DoanhThu DESC;
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StartDate", dtpStartDate.Value),
                new SqlParameter("@EndDate", dtpEndDate.Value)
            };

            DataTable dataTable = ExecuteQuery(query, parameters);
            dgvThongKe.DataSource = dataTable;
            dgvThongKe.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh Thu (VND)";
            dgvThongKe.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnThongKeKhachHang_Click(object sender, EventArgs e)
        {
            string query = @"
          SELECT NgayLap, COUNT(DISTINCT TenKhachHang) AS SoLuongKhachHang
          FROM HoaDon
          WHERE NgayLap >= @StartDate AND NgayLap <= @EndDate
          GROUP BY NgayLap
          ORDER BY NgayLap;
      ";

            SqlParameter[] parameters = new SqlParameter[]
            {
          new SqlParameter("@StartDate", dtpStartDate.Value),
          new SqlParameter("@EndDate", dtpEndDate.Value)
            };

            DataTable dataTable = ExecuteQuery(query, parameters);
            dgvThongKe.DataSource = dataTable;

            dgvThongKe.Columns["NgayLap"].HeaderText = "Ngày Lập";
            dgvThongKe.Columns["SoLuongKhachHang"].HeaderText = "Số Lượng Khách Hàng";
            dgvThongKe.Columns["SoLuongKhachHang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnThongKeHoaDon_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT MaHoaDon, NgayLap, TenKhachHang, SDT, TrangThai, PhuongThuc, TongTien
        FROM HoaDon
        WHERE NgayLap >= @StartDate AND NgayLap <= @EndDate
        ORDER BY NgayLap;
    ";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@StartDate", dtpStartDate.Value),
        new SqlParameter("@EndDate", dtpEndDate.Value)
            };

            DataTable dataTable = ExecuteQuery(query, parameters);
            dgvThongKe.DataSource = dataTable;

            dgvThongKe.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
            dgvThongKe.Columns["NgayLap"].HeaderText = "Ngày Lập";
            dgvThongKe.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            dgvThongKe.Columns["SDT"].HeaderText = "Số Điện Thoại";
            dgvThongKe.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvThongKe.Columns["PhuongThuc"].HeaderText = "Phương Thức";
            dgvThongKe.Columns["TongTien"].HeaderText = "Tổng Tiền (VND)";
            dgvThongKe.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvThongKe.Columns["PhuongThuc"].Visible = false;
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvThongKe_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo rằng nhấp vào một hàng hợp lệ
            {
                int maHoaDon = Convert.ToInt32(dgvThongKe.Rows[e.RowIndex].Cells["MaHoaDon"].Value);
                string tenKhachHang = dgvThongKe.Rows[e.RowIndex].Cells["TenKhachHang"].Value.ToString();
                string tongTien = dgvThongKe.Rows[e.RowIndex].Cells["TongTien"].Value.ToString();
                string ngayLap = dgvThongKe.Rows[e.RowIndex].Cells["NgayLap"].Value.ToString(); // Lấy ngày giờ lập hóa đơn

                // Kiểm tra nếu form đã mở, nếu không thì mở nó
                foreach (Form form in Application.OpenForms)
                {
                    if (form is frmThongKeChiTietHoaDon)
                    {
                        form.Activate(); // Nếu form đã mở, đưa nó lên trên
                        return;
                    }
                }

                // Tạo instance của frmThongKeChiTietHoaDon và truyền tất cả các tham số
                frmThongKeChiTietHoaDon chiTietHoaDonForm = new frmThongKeChiTietHoaDon(maHoaDon, tenKhachHang, tongTien, ngayLap);
                chiTietHoaDonForm.Show();
            }
        }
    }
}
