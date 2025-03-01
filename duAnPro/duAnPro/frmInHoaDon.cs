using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    public partial class frmInHoaDon : Form
    {
        private string tenKhachHang;
        private string tongTien;
        private DataTable dtChiTietHoaDon;
        private DateTime ngayLapHoaDon;
        private string maHoaDon;

        private void frmInHoaDon_Load(object sender, EventArgs e)
        {
            ngayLapHoaDon = DateTime.Now;
            label6.Text = "Tên khách hàng: " + tenKhachHang;
            label7.Text = "Ngày xuất hóa đơn: " + ngayLapHoaDon.ToString("dd/MM/yyyy HH:mm:ss"); // Sử dụng ngày giờ lập hóa đơn
            label8.Text = "Tổng tiền: " + tongTien + " VND";
            label5.Text = "Mã hóa đơn: " + maHoaDon; // Gán mã hóa đơn cho label5
            // Bind the DataTable to the DataGridView
            dgvDanhSach.DataSource = dtChiTietHoaDon;
         
            // Rename columns with Vietnamese headers
            if (dgvDanhSach.Columns["DonGia"] != null)
            {
                dgvDanhSach.Columns["DonGia"].HeaderText = "Đơn giá (VND)";
                dgvDanhSach.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDanhSach.Columns["TenKhachHang"] != null)
            {
                dgvDanhSach.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                dgvDanhSach.Columns["TenKhachHang"].Visible = false;
            }
            if (dgvDanhSach.Columns["TenSanPham"] != null)
            {
                dgvDanhSach.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            }

            if (dgvDanhSach.Columns["SoLuong"] != null)
            {
                dgvDanhSach.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvDanhSach.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            
            if (dgvDanhSach.Columns["TongTien"] != null)
            {
                dgvDanhSach.Columns["TongTien"].HeaderText = "Tổng tiền";
                
            }
            if (dgvDanhSach.Columns["TongTien"] != null)
            {
                dgvDanhSach.Columns["TongTien"].HeaderText = "Thành tiền (VND)";
                dgvDanhSach.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDanhSach.Columns["TongTien"].DefaultCellStyle.Format = "#,##0"; // Định dạng với dấu phân cách hàng nghìn
            }
        }
        public frmInHoaDon(string tenKhachHang, string tongTien, DataTable dtChiTietHoaDon, DateTime ngayLapHoaDon, string maHoaDon)
        {
            InitializeComponent();
            this.tenKhachHang = tenKhachHang;
            this.tongTien = tongTien;
            this.dtChiTietHoaDon = dtChiTietHoaDon;
            this.ngayLapHoaDon = ngayLapHoaDon;
            this.maHoaDon = maHoaDon;
            this.StartPosition = FormStartPosition.CenterScreen;
            if (dgvDanhSach.Columns["DonGia"] != null)
            {
                dgvDanhSach.Columns["DonGia"].HeaderText = "Đơn giá (VND)";
                dgvDanhSach.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
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

            // Gán DataTable đã cập nhật cho DataGridView
            dgvDanhSach.DataSource = dtChiTietHoaDon;
            this.maHoaDon = maHoaDon;
        }

        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra cột có tên là "DonGia"
            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
            {
                // Định dạng giá trị của cột "DonGia"
                if (e.Value is decimal)
                {
                    dgvDanhSach.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    e.Value = ((decimal)e.Value).ToString("#,##0"); // Định dạng với dấu phân cách hàng nghìn
                    e.FormattingApplied = true;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
