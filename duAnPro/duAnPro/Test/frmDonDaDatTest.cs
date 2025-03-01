using NUnit.Framework;
using duAnPro;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmDonDaDatTests
    {
        private frmDonDaDat _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmDonDaDat();
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void CallStoredProcedure_Should_UpdateDatabase()
        {
            _form.GetType()
                 .GetMethod("CallStoredProcedure", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                 .Invoke(_form, null);

            bool updated = CheckDatabaseUpdated();
            Assert.IsTrue(updated, "Stored Procedure phải cập nhật dữ liệu trong database.");
        }

        private bool CheckDatabaseUpdated()
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HoaDon", conn);
                int count = (int)cmd.ExecuteScalar();
                result = count > 0;
            }
            return result;
        }

        [Test]
        public void LoadDataFromHoaDon_Should_Return_DataTable()
        {
            _form.GetType()
                 .GetMethod("LoadDataFromHoaDon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                 .Invoke(_form, null);

            var grid = _form.Controls["dgvDanhSachDon"] as DataGridView;
            Assert.IsNotNull(grid, "DataGridView không được null.");
            Assert.Greater(grid.RowCount, 0, "Danh sách đơn hàng phải có dữ liệu.");
        }

        [Test]
        public void btnXoa_Click_Should_Delete_SelectedOrder()
        {
            var grid = _form.Controls["dgvDanhSachDon"] as DataGridView;
            grid.Rows[0].Selected = true;

            Button btn = (Button)_form.Controls["btnXoa"];
            btn.PerformClick();

            bool orderDeleted = CheckOrderDeleted(Convert.ToInt32(grid.Rows[0].Cells["MaHoaDon"].Value));
            Assert.IsTrue(orderDeleted, "Đơn hàng phải được xóa thành công.");
        }

        private bool CheckOrderDeleted(int maHoaDon)
        {
            bool deleted = true;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = @MaHoaDon", conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                int count = (int)cmd.ExecuteScalar();
                deleted = count == 0;
            }
            return deleted;
        }

        [Test]
        public void btnSua_Click_Should_Update_OrderDetails()
        {
            var grid = _form.Controls["dgvDanhSachDon"] as DataGridView;
            grid.Rows[0].Selected = true;
            grid.Rows[0].Cells["TenKhachHang"].Value = "Khách Test";

            Button btn = (Button)_form.Controls["btnSua"];
            btn.PerformClick();

            string updatedCustomer = GetUpdatedCustomerName(Convert.ToInt32(grid.Rows[0].Cells["MaHoaDon"].Value));
            Assert.AreEqual("Khách Test", updatedCustomer, "Tên khách hàng phải được cập nhật.");
        }

        private string GetUpdatedCustomerName(int maHoaDon)
        {
            string customerName = "";
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TenKhachHang FROM HoaDon WHERE MaHoaDon = @MaHoaDon", conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                customerName = cmd.ExecuteScalar()?.ToString();
            }
            return customerName;
        }

        [Test]
        public void btnXemChiTiet_Click_Should_Open_frmChiTietHoaDon()
        {
            var grid = _form.Controls["dgvDanhSachDon"] as DataGridView;
            grid.Rows[0].Selected = true;

            Button btn = (Button)_form.Controls["btnXemChiTiet"];
            btn.PerformClick();

            bool formOpened = false;
            foreach (Form form in Application.OpenForms)
            {
                if (form is frmChiTietHoaDon)
                {
                    formOpened = true;
                    break;
                }
            }
            Assert.IsTrue(formOpened, "Form frmChiTietHoaDon phải được mở khi xem chi tiết.");
        }
    }
}
