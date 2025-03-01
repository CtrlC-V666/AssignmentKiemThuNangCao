using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    public partial class frmDangNhap : Form
    {
        private string maNhanVien;
        private string tenNhanVien;
        public frmDangNhap()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += new FormClosingEventHandler(ChildFormClosing);
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            frmDangNhap frmDangNhap = new frmDangNhap();    
            frmDangNhap.Show();
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            Function.Connect();  //Mở kết nối
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string sqlQuery;

            if (txtTenDangNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }
            if (txtMatKhau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            sqlQuery = "SELECT NhanVien.MaNhanVien, NhanVien.Ten, NguoiDung.MaQuyen " +
                       "FROM NguoiDung " +
                       "INNER JOIN NhanVien ON NguoiDung.MaNhanVien = NhanVien.MaNhanVien " +
                       "WHERE NguoiDung.TenDangNhap = @TenDangNhap AND NguoiDung.MatKhau = @MatKhau";

            SqlParameter[] parameters = {
        new SqlParameter("@TenDangNhap", SqlDbType.NVarChar) { Value = txtTenDangNhap.Text.Trim() },
        new SqlParameter("@MatKhau", SqlDbType.NVarChar) { Value = txtMatKhau.Text.Trim() }
    };

            DataTable dt = GetDataToTable(sqlQuery, parameters);

            if (dt.Rows.Count == 1)
            {
                maNhanVien = dt.Rows[0]["MaNhanVien"].ToString();
                tenNhanVien = dt.Rows[0]["Ten"].ToString();
                int maQuyen = Convert.ToInt32(dt.Rows[0]["MaQuyen"]);

                if (maQuyen == 1)
                {
                    frmMainQuanLy mainForAdmin = new frmMainQuanLy(this, maNhanVien, tenNhanVien); // Form main cho quản lý
                    mainForAdmin.Show();
                }
                else
                {
                    frmMain main = new frmMain(this, maNhanVien, tenNhanVien); // Form main cho nhân viên
                    main.Show();
                }

                this.Hide();
                txtTenDangNhap.Text = "";
                txtMatKhau.Text = "";
                txtTenDangNhap.Focus();
            }
            else
            {
                MessageBox.Show("Tài khoản không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Text = "";
                txtMatKhau.Text = "";
                txtTenDangNhap.Focus();
            }
            this.Hide();
        }
        public static DataTable GetDataToTable(string sqlQuery, SqlParameter[] parameters)
        {

            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString; // Lấy chuỗi kết nối từ App.config

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddRange(parameters); // Thêm các tham số vào SqlCommand
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Function.Disconnect();       //Đóng kết nối
                Application.Exit();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau dmk = new frmDoiMatKhau();
            dmk.Show();
           
          
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            frmDangKy frmDangKy = new frmDangKy();  
            frmDangKy.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmQuenMatKhau frmQuenMatKhau = new frmQuenMatKhau();
            frmQuenMatKhau.Show();
           
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMatKhau.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true; // Ngăn không cho âm thanh 'ding' khi nhấn Enter
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true; // Ngăn không cho âm thanh 'ding' khi nhấn Enter
            }
        }
    }
}
