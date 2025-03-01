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
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void frmDangKy_Load(object sender, EventArgs e)
        {
            Function.Connect();
        }
        public static class Function
        {
            public static SqlConnection conn;

            public static void Connect()
            {
                if (conn == null)
                {
                    conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
                }
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
            }

            public static SqlConnection Connection
            {
                get { return conn; }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDangNhap frmDN = new frmDangNhap();
            frmDN.Show();
            this.Hide();

        }

        private bool KiemTraTenDangNhap(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tenDangNhap";
            using (SqlCommand cmd = new SqlCommand(query, Function.Connection))
            {
                cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Function.Connect();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMK.Text.Trim();
            string matKhau2 = txtMK2.Text.Trim();
            string hoVaTen = txtHoVaTen.Text.Trim(); // Lấy giá trị từ TextBox
            string tenDangNhap = email; // Sử dụng toàn bộ email làm tên đăng nhập
            int maQuyen = 2; // Mặc định là nhân viên (2)

            // Kiểm tra các trường trống
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(matKhau2) || string.IsNullOrEmpty(hoVaTen))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra khớp mật khẩu
            if (matKhau != matKhau2)
            {
                MessageBox.Show("Mật khẩu không khớp. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (KiemTraTenDangNhap(tenDangNhap))
            {
                MessageBox.Show("Email đã tồn tại. Gợi ý tạo Email mới: " + GoiYTenDangNhap(tenDangNhap));
            }
            else
            {
                // Khai báo maNhanVien
                string maNhanVien = TaoMaNhanVien();
                DangKyNguoiDung(tenDangNhap, matKhau, email, maNhanVien, maQuyen, hoVaTen);
                MessageBox.Show("Đăng ký thành công!");

                // Hiển thị form đăng nhập
                frmDangNhap frmDN = new frmDangNhap();
                frmDN.Show();
                this.Hide();
            }
        }
        private string GoiYTenDangNhap(string tenGoc)
        {
            Function.Connect();
            string query = "SELECT TenDangNhap FROM NguoiDung WHERE TenDangNhap LIKE @tenGoc + '%'";
            using (SqlCommand cmd = new SqlCommand(query, Function.Connection))
            {
                cmd.Parameters.AddWithValue("@tenGoc", tenGoc);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        string tenGoiY = tenGoc.Split('@')[0] + i.ToString() + "@" + tenGoc.Split('@')[1];
                        if (!reader.GetString(0).Equals(tenGoiY, StringComparison.OrdinalIgnoreCase))
                        {
                            return tenGoiY;
                        }
                        i++;
                    }
                    return tenGoc.Split('@')[0] + i.ToString() + "@" + tenGoc.Split('@')[1];
                }
            }
        }
        private void DangKyNguoiDung(string tenDangNhap, string matKhau, string email, string maNhanVien, int maQuyen, string hoVaTen)
        {
            Function.Connect();
            string query = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, Email, MaNhanVien, MaQuyen) VALUES (@tenDangNhap, @matKhau, @Email, @MaNhanVien, @MaQuyen)";
            using (SqlCommand cmd = new SqlCommand(query, Function.Connection))
            {
                cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@matKhau", matKhau);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmd.Parameters.AddWithValue("@MaQuyen", maQuyen);
                cmd.ExecuteNonQuery();
            }

            // Thêm thông tin nhân viên vào bảng NhanVien
            ThemThongTinNhanVien(maNhanVien, hoVaTen, "Nhân viên");
        }

        private void ThemThongTinNhanVien(string maNhanVien, string ten, string chucVu)
        {
            Function.Connect();
            string query = "INSERT INTO NhanVien (MaNhanVien, Ten, ChucVu) VALUES (@MaNhanVien, @Ten, @ChucVu)";
            using (SqlCommand cmd = new SqlCommand(query, Function.Connection))
            {
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                cmd.ExecuteNonQuery();
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                txtMK.UseSystemPasswordChar = false;
            }
            else
            {
                txtMK.UseSystemPasswordChar = true;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtMK2.UseSystemPasswordChar = false;
            }
            else
            {
                txtMK2.UseSystemPasswordChar = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
  
        private string TaoMaNhanVien()
        {
            Function.Connect();
            string query = "SELECT MAX(MaNhanVien) FROM NhanVien";
            using (SqlCommand cmd = new SqlCommand(query, Function.Connection))
            {
                object result = cmd.ExecuteScalar();
                string maNhanVienCuoi = result != DBNull.Value ? result.ToString() : "NV0000";

                // Chuyển đổi mã nhân viên hiện tại thành số và tăng lên 1
                int soHienTai = int.Parse(maNhanVienCuoi.Substring(2)); // Bỏ tiền tố "NV"
                int soMoi = soHienTai + 1;

                // Tạo mã nhân viên mới với định dạng "NVXXXX"
                return "NV" + soMoi.ToString("D4");
            }
        }

    }
}
