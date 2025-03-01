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
    public partial class frmMain : Form
    {
        frmDangNhap dangNhap = new frmDangNhap();
        private string maNhanVien;
        private string tenNhanVien;
        public frmMain(frmDangNhap dangNhap, string maNhanVien, string tenNhanVien)
        {
            InitializeComponent();
            this.dangNhap = dangNhap;
            // Lưu thông tin từ frmDangNhap
            this.maNhanVien = maNhanVien;
            this.tenNhanVien = tenNhanVien;
            txtMaNhanVien.Text = this.maNhanVien;
            txtTenNhanVien.Text = this.tenNhanVien;
            this.StartPosition = FormStartPosition.CenterScreen;


        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Function.Disconnect();   //Đóng kết nối
                Application.Exit();
            }
        }
       

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            dangNhap.Show();
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            frmDonHang donHangForm = new frmDonHang(maNhanVien, tenNhanVien);
            donHangForm.Show();
        }

        private void btnThucDon_Click(object sender, EventArgs e)
        {
            frmThucDon thucDon = new frmThucDon();
            thucDon.Show();
        }

        private void on_Click(object sender, EventArgs e)
        {

        }

        private void txtTenNhanVien_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
