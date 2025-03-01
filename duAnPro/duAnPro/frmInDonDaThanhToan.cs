using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    public partial class frmInDonDaThanhToan : Form
    {
        private string tenKhachHang;
        private string tongTien;
        private DataTable dtChiTietHoaDon;
        private DateTime ngayLapHoaDon; // Thuộc tính mới
        private PrintDocument printDocument; // Khai báo PrintDocument

        public frmInDonDaThanhToan(string tenKhachHang, string tongTien, DataTable dtChiTietHoaDon, DateTime ngayLapHoaDon)
        {
            InitializeComponent();
            this.tenKhachHang = tenKhachHang;
            this.tongTien = tongTien;
            this.dtChiTietHoaDon = dtChiTietHoaDon;
            this.ngayLapHoaDon = ngayLapHoaDon;
            this.printDocument = new PrintDocument();
          

            // Thêm cột TongTien vào DataTable nếu chưa tồn tại
            if (!dtChiTietHoaDon.Columns.Contains("TongTien"))
            {
                dtChiTietHoaDon.Columns.Add("TongTien", typeof(decimal));
            }

            // Tính toán giá trị cho cột TongTien
            foreach (DataRow row in dtChiTietHoaDon.Rows)
            {
                if (row["SoLuong"] != DBNull.Value && row["DonGia"] != DBNull.Value)
                {
                    decimal soLuong = Convert.ToDecimal(row["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(row["DonGia"]);
                    row["TongTien"] = soLuong * donGia;
                }
            }
        }
        
        private void frmInDonDaThanhToan_Load(object sender, EventArgs e)
        {
            // Bind the DataTable to the DataGridView
            dgvDanhSach.DataSource = dtChiTietHoaDon;

            // Cập nhật tiêu đề các cột
            if (dgvDanhSach.Columns["DonGia"] != null)
            {
                dgvDanhSach.Columns["DonGia"].HeaderText = "Đơn giá (VND)";
                dgvDanhSach.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDanhSach.Columns["SoLuong"] != null)
            {
                dgvDanhSach.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvDanhSach.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDanhSach.Columns["TenSanPham"] != null)
            {
                dgvDanhSach.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            }
            if (dgvDanhSach.Columns["TenKhachHang"] != null)
            {
                dgvDanhSach.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                dgvDanhSach.Columns["TenKhachHang"].Visible = false; // Ẩn cột
            }
            if (dgvDanhSach.Columns["MaChiTietHoaDon"] != null)
            {
                dgvDanhSach.Columns["MaChiTietHoaDon"].HeaderText = "Mã chi tiết hóa đơn";
                dgvDanhSach.Columns["MaChiTietHoaDon"].Visible = false; // Ẩn cột
            }
            if (dgvDanhSach.Columns["MaHoaDon"] != null)
            {
                dgvDanhSach.Columns["MaHoaDon"].HeaderText = "Mã hóa đơn";
                dgvDanhSach.Columns["MaHoaDon"].Visible = false; // Ẩn cột
            }

            // Cập nhật tiêu đề cột "TongTien" nếu nó đã tồn tại
            if (dgvDanhSach.Columns["TongTien"] != null)
            {
                dgvDanhSach.Columns["TongTien"].HeaderText = "Tổng tiền (VND)";
                dgvDanhSach.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhSach.Columns["TongTien"].DefaultCellStyle.Format = "#,##0"; // Định dạng với dấu phân cách hàng nghìn
            }
            else
            {
                // Thêm cột "TongTien" nếu chưa có
                DataGridViewTextBoxColumn colTongTien = new DataGridViewTextBoxColumn();
                colTongTien.Name = "TongTien";
                colTongTien.HeaderText = "Tổng tiền (VND)";
                colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colTongTien.DefaultCellStyle.Format = "#,##0"; // Định dạng với dấu phân cách hàng nghìn
                dgvDanhSach.Columns.Add(colTongTien);
            }

            // Hiển thị thông tin ngày lập hóa đơn và tổng tiền
            label6.Text = "Tên khách hàng: " + tenKhachHang;
            label7.Text = "Ngày xuất hóa đơn: " + ngayLapHoaDon.ToString("dd/MM/yyyy HH:mm:ss"); // Sử dụng ngày giờ lập hóa đơn
            label8.Text = "Tổng tiền: " + tongTien + " VND";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
 

        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal value))
                {
                    e.Value = value.ToString("#,##0"); // Định dạng với dấu phân cách hàng nghìn
                    e.FormattingApplied = true;
                }
            }

            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "TongTien")
            {
                var donGiaCell = dgvDanhSach.Rows[e.RowIndex].Cells["DonGia"].Value;
                var soLuongCell = dgvDanhSach.Rows[e.RowIndex].Cells["SoLuong"].Value;

                if (donGiaCell != null && soLuongCell != null)
                {
                    if (decimal.TryParse(donGiaCell.ToString(), out decimal donGia) &&
                        int.TryParse(soLuongCell.ToString(), out int soLuong))
                    {
                        decimal tongTien = donGia * soLuong;
                        e.Value = tongTien.ToString("#,##0"); // Định dạng với dấu phân cách hàng nghìn
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

   
   
    }
}
