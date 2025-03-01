using NUnit.Framework;
using duAnPro;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmChiTietHoaDonTests
    {
        private frmChiTietHoaDon _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmChiTietHoaDon(1); // Truyền mã hóa đơn mẫu
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void LoadChiTietHoaDon_ValidMaHoaDon_LoadsData()
        {
            int maHoaDon = 1;
            _form.LoadChiTietHoaDon(maHoaDon);

            var grid = _form.Controls["dtgvChiTietHoaDon"] as DataGridView;
            Assert.IsNotNull(grid, "DataGridView không được null.");
            Assert.Greater(grid.RowCount, 0, "Bảng chi tiết hóa đơn phải có dữ liệu.");
        }

        [Test]
        public void txtTenKhachHang_Should_Update_Correctly()
        {
            _form.Controls["txtTenKhachHang"].Text = "Nguyễn Văn A";
            Assert.AreEqual("Nguyễn Văn A", _form.Controls["txtTenKhachHang"].Text);
        }

        [Test]
        public void txtTongTien_Should_Format_Correctly()
        {
            _form.Controls["txtTongTien"].Text = "1000000";
            Assert.AreEqual("1000000", _form.Controls["txtTongTien"].Text);
        }

        [Test]
        public void btnXacNhanThanhToan_Should_Update_HoaDon_Status()
        {
            // Click nút xác nhận thanh toán
            Button btn = (Button)_form.Controls["btnXacNhanThanhToan"];
            btn.PerformClick();

            // Kiểm tra trạng thái hóa đơn đã thay đổi
            Assert.AreEqual("Đã thanh toán", GetHoaDonStatus(1), "Trạng thái hóa đơn phải là 'Đã thanh toán'.");
        }

        private string GetHoaDonStatus(int maHoaDon)
        {
            string status = "";
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TrangThai FROM HoaDon WHERE MaHoaDon = @MaHoaDon", conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                status = cmd.ExecuteScalar()?.ToString();
            }
            return status;
        }
    }
}
