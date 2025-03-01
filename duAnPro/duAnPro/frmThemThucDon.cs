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
    public partial class frmThemThucDon : Form
    {
        private SqlConnection conn;

        public frmThemThucDon()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void txtTenMon_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMoTa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTinhTrang_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int newMaMon = GenerateMaMon();

      
            string tenMon = txtTenMon.Text.Trim();
            decimal gia = decimal.Parse(txtGia.Text.Trim());
            string moTa = txtMoTa.Text.Trim();

           
            string tinhTrang = chkConHang.Checked ? "Còn hàng" : chkHetHang.Checked ? "Hết hàng" : "";

            if (string.IsNullOrEmpty(tinhTrang))
            {
                MessageBox.Show("Vui lòng chọn tình trạng của món.");
                return;
            }

         
            try
            {
                conn.Open();

                // Bật IDENTITY_INSERT
                SqlCommand enableIdentityInsertCmd = new SqlCommand("SET IDENTITY_INSERT MenuHaiSan ON", conn);
                enableIdentityInsertCmd.ExecuteNonQuery();

                string insertQuery = "INSERT INTO MenuHaiSan (MaMon, TenMon, Gia, MoTa, TinhTrang) " +
                                     "VALUES (@MaMon, @TenMon, @Gia, @MoTa, @TinhTrang)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@MaMon", newMaMon);
                cmd.Parameters.AddWithValue("@TenMon", tenMon);
                cmd.Parameters.AddWithValue("@Gia", gia);
                cmd.Parameters.AddWithValue("@MoTa", moTa);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                cmd.ExecuteNonQuery();

                // Tắt IDENTITY_INSERT
                SqlCommand disableIdentityInsertCmd = new SqlCommand("SET IDENTITY_INSERT MenuHaiSan OFF", conn);
                disableIdentityInsertCmd.ExecuteNonQuery();

                MessageBox.Show("Thêm món ăn thành công.");
                ClearForm();
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

        private void frmThemThucDon_Load(object sender, EventArgs e)
        {

        }
        private int GenerateMaMon()
        {
            int maxMaMon = 0;

            try
            {
                conn.Open();
                string query = "SELECT MAX(MaMon) FROM MenuHaiSan";

                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    maxMaMon = Convert.ToInt32(result);
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

            // Tăng maxMaMon lên 1 để tạo mã món mới
            return maxMaMon + 1;
        }
        private void ClearForm()
        {
            txtTenMon.Text = "";
            txtGia.Text = "";
            txtMoTa.Text = "";
            chkConHang.Checked = false;
            chkHetHang.Checked = false;
            
        }

        private void chkConHang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConHang.Checked)
            {
                chkHetHang.Checked = false;
            }
        }

        private void chkHetHang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHetHang.Checked)
            {
                chkConHang.Checked = false;
            }
        }
    }
}
