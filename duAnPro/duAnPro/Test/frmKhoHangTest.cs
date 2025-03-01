using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro
{
    [TestFixture]
    public class frmKhoHangTests
    {
        private frmKhoHang _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmKhoHang();
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void txtTenHang_Should_Update_Correctly()
        {
            _form.Controls["txtTenhang"].Text = "Mực ống";
            Assert.AreEqual("Mực ống", _form.Controls["txtTenhang"].Text);
        }

        [Test]
        public void txtSoluong_Should_Update_Correctly()
        {
            _form.Controls["txtSoluong"].Text = "50";
            Assert.AreEqual("50", _form.Controls["txtSoluong"].Text);
        }

        [Test]
        public void txtGianhap_Should_Update_Correctly()
        {
            _form.Controls["txtGianhap"].Text = "100000";
            Assert.AreEqual("100000", _form.Controls["txtGianhap"].Text);
        }

        [Test]
        public void btnThem_Click_Should_Add_Product()
        {
            _form.Controls["txtTenhang"].Text = "Mực ống";
            _form.Controls["txtSoluong"].Text = "50";
            _form.Controls["txtGianhap"].Text = "100000";
            _form.Controls["txtGiaban"].Text = "150000";
            _form.Controls["txtMoTa"].Text = "Mực tươi sống";

            Button btn = (Button)_form.Controls["btnThem"];
            btn.PerformClick();

            bool productAdded = CheckProductExists("Mực ống");
            Assert.IsTrue(productAdded, "Hàng hóa phải được thêm vào thành công.");
        }

        private bool CheckProductExists(string tenHang)
        {
            bool exists = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM KhoHang WHERE TenHang = @TenHang", conn);
                cmd.Parameters.AddWithValue("@TenHang", tenHang);
                int count = (int)cmd.ExecuteScalar();
                exists = count > 0;
            }
            return exists;
        }

        [Test]
        public void btnXoa_Click_Should_Remove_Product()
        {
            AddTestProduct("Tôm hùm", 20, 200000, 300000, "Tôm sống");

            var grid = _form.Controls["dataGridView1"] as DataGridView;
            grid.ClearSelection();

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["TenHang"].Value.ToString() == "Tôm hùm")
                {
                    row.Selected = true;
                    break;
                }
            }

            Button btn = (Button)_form.Controls["btnXoa"];
            btn.PerformClick();

            bool productDeleted = !CheckProductExists("Tôm hùm");
            Assert.IsTrue(productDeleted, "Hàng hóa phải được xóa thành công.");
        }

        private void AddTestProduct(string tenHang, int soLuong, decimal giaNhap, decimal giaBan, string moTa)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                string query = "INSERT INTO KhoHang (TenHang, SoLuong, GiaNhap, GiaBan, MoTa) VALUES (@TenHang, @SoLuong, @GiaNhap, @GiaBan, @MoTa)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenHang", tenHang);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
                cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                cmd.Parameters.AddWithValue("@MoTa", moTa);
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void UpdateProduct_Should_Modify_Database()
        {
            AddTestProduct("Cua biển", 10, 150000, 200000, "Cua tươi sống");

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                string query = "UPDATE KhoHang SET SoLuong = 30 WHERE TenHang = 'Cua biển'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            int newQuantity = GetProductQuantity("Cua biển");
            Assert.AreEqual(30, newQuantity, "Số lượng phải được cập nhật thành công.");
        }

        private int GetProductQuantity(string tenHang)
        {
            int quantity = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                string query = "SELECT SoLuong FROM KhoHang WHERE TenHang = @TenHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenHang", tenHang);
                quantity = (int)cmd.ExecuteScalar();
            }
            return quantity;
        }
    }
}
