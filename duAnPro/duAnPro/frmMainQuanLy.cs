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
    public partial class frmMainQuanLy : Form
    {
        private string maNhanVien;
        private string tenNhanVien;
        private Form loginForm;
 
        frmDangNhap dangNhap = new frmDangNhap();

        public frmMainQuanLy(Form loginForm, string maNhanVien, string tenNhanVien)
        {
            InitializeComponent();
          
            this.maNhanVien = maNhanVien;
            this.tenNhanVien = tenNhanVien;
            this.loginForm = loginForm;
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void frmMainQuanLy_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMainQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                dangNhap.Show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmQuanLyNhanVien quanLyNhanVien = new frmQuanLyNhanVien();
            quanLyNhanVien.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmThongKe frmThongKe = new frmThongKe();
            frmThongKe.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmKhoHang frmKhoHang = new frmKhoHang();
            frmKhoHang.Show();
        }
    }
}
