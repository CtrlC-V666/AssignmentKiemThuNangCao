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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace duAnPro
{
    public partial class frmDoiMatKhau : Form
    {
     
        public frmDoiMatKhau()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                txtMKCu.UseSystemPasswordChar = false;
            }
            else
            {
                txtMKCu.UseSystemPasswordChar = true;
            }
            txtMKCu.UseSystemPasswordChar = !checkBox3.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtMKMoi.UseSystemPasswordChar = false;
            }
            else
            {
                txtMKMoi.UseSystemPasswordChar = true;
            }
            txtMKMoi.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                txtConfirm.UseSystemPasswordChar = false;
            }
            else
            {
                txtConfirm.UseSystemPasswordChar = true;
            }
            txtConfirm.UseSystemPasswordChar = !checkBox2.Checked;
        }
        
           
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
             
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Focus();
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Chưa nhập tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMKCu.Text))
            {
                MessageBox.Show("Chưa nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKCu.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtConfirm.Text))
            {
                MessageBox.Show("Chưa nhập lại mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirm.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMKMoi.Text))
            {
                MessageBox.Show("Chưa nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKMoi.Focus();
                return;
            }
            if (txtMKMoi.Text != txtConfirm.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp, vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirm.Focus();
                return;
            }
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhauCu";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text.Trim());
                    command.Parameters.AddWithValue("@MatKhauCu", txtMKCu.Text.Trim());
                    int count = (int)command.ExecuteScalar();
                    if (count == 1)
                    {
                        sql = "UPDATE NguoiDung SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
                        using (SqlCommand updateCommand = new SqlCommand(sql, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@MatKhauMoi", txtMKMoi.Text.Trim());
                            updateCommand.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text.Trim());
                            updateCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        frmDangNhap loginForm = new frmDangNhap();
                        loginForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu hiện tại không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMKCu.Focus();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDangNhap frmDN = new frmDangNhap();
            frmDN.Show();
            this.Hide();
        }
    }
}
