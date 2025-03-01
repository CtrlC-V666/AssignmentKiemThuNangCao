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
    public partial class frmThongKeChiTietHoaDon : Form
    {
        private int MaHoaDon;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
        public string TenKhachHang { get; set; } // Thuộc tính để nhận tên khách hàng
        public string TongTien { get; set; } // Thuộc tính để nhận tổng tiền
        public string NgayLap { get; set; } // Thuộc tính để nhận ngày giờ lập hóa đơn

        public frmThongKeChiTietHoaDon(int maHoaDon, string tenKhachHang, string tongTien, string ngayLap)
        {
            InitializeComponent();
            this.MaHoaDon = maHoaDon;
            this.TenKhachHang = tenKhachHang;
            this.TongTien = tongTien;
            this.NgayLap = ngayLap;
            LoadChiTietHoaDon();

        }
        private void LoadChiTietHoaDon()
        {
            // Truy vấn SQL để lấy chi tiết hóa đơn theo MaHoaDon
            string query = @"
              SELECT TenSanPham, SoLuong, DonGia, TongTien
             FROM ChiTietHoaDon
              WHERE MaHoaDon = @MaHoaDon
              ";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@MaHoaDon", this.MaHoaDon)
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            // Gán DataSource cho DataGridView
            dgvDanhSach.DataSource = dataTable;

            // Đặt tên tiêu đề cột
            dgvDanhSach.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dgvDanhSach.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvDanhSach.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDanhSach.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvDanhSach.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhSach.Columns["DonGia"].DefaultCellStyle.Format = "N0"; // Định dạng số với dấu phân cách hàng nghìn

            dgvDanhSach.Columns["TongTien"].HeaderText = "Tổng Tiền (VND)";
            dgvDanhSach.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhSach.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Định dạng số với dấu phân cách hàng nghìn
        }

        private void frmThongKeChiTietHoaDon_Load(object sender, EventArgs e)
        {
            // Cập nhật thông tin khách hàng và tổng tiền lên các label
            label6.Text = "Tên khách hàng: " + this.TenKhachHang;
            if (decimal.TryParse(this.TongTien, out decimal tongTienDecimal))
            {
                label8.Text = "Tổng tiền: " + tongTienDecimal.ToString("N0") + " VND";
            }
            else
            {
                label8.Text = "Tổng tiền: " + this.TongTien + " VND"; // Nếu không thể chuyển đổi, hiển thị giá trị gốc
            }
            // Chuyển đổi chuỗi ngày giờ thành DateTime và định dạng ngày tháng năm
            if (DateTime.TryParse(this.NgayLap, out DateTime ngayLapDateTime))
            {
                label3.Text = "Ngày lập: " + ngayLapDateTime.ToString("dd/MM/yyyy"); // Hoặc "yyyy-MM-dd" tùy vào yêu cầu
            }
            else
            {
                label3.Text = "Ngày lập: " + this.NgayLap; // Nếu không thể chuyển đổi, hiển thị giá trị gốc
            }
        }
        private DataTable ExecuteQuery(string query, SqlParameter[] parameters)
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
