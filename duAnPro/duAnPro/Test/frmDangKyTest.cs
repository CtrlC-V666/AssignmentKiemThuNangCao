using NUnit.Framework;
using duAnPro;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmDangKyTests
    {
        private frmDangKy _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmDangKy();
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void KiemTraTenDangNhap_TenTonTai_ReturnsTrue()
        {
            string tenDangNhap = "testuser@example.com";

            bool result = _form.GetType()
                               .GetMethod("KiemTraTenDangNhap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                               .Invoke(_form, new object[] { tenDangNhap }) as bool? ?? false;

            Assert.IsTrue(result, "Tên đăng nhập đã tồn tại.");
        }

        [Test]
        public void KiemTraTenDangNhap_TenMoi_ReturnsFalse()
        {
            string tenDangNhap = "newuser@example.com";

            bool result = _form.GetType()
                               .GetMethod("KiemTraTenDangNhap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                               .Invoke(_form, new object[] { tenDangNhap }) as bool? ?? true;

            Assert.IsFalse(result, "Tên đăng nhập chưa tồn tại.");
        }

        [Test]
        public void TaoMaNhanVien_Returns_CorrectFormat()
        {
            string result = _form.GetType()
                                 .GetMethod("TaoMaNhanVien", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                 .Invoke(_form, null) as string;

            Assert.IsTrue(result.StartsWith("NV"), "Mã nhân viên phải bắt đầu bằng 'NV'.");
            Assert.AreEqual(6, result.Length, "Mã nhân viên phải có 6 ký tự.");
        }

        [Test]
        public void DangKyNguoiDung_ValidInput_AddsUser()
        {
            string tenDangNhap = "testuser@example.com";
            string matKhau = "123456";
            string email = tenDangNhap;
            string maNhanVien = "NV9999";
            int maQuyen = 2;
            string hoVaTen = "Test User";

            _form.GetType()
                 .GetMethod("DangKyNguoiDung", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                 .Invoke(_form, new object[] { tenDangNhap, matKhau, email, maNhanVien, maQuyen, hoVaTen });

            bool result = CheckUserExists(tenDangNhap);
            Assert.IsTrue(result, "Người dùng phải được thêm vào database.");
        }

        private bool CheckUserExists(string tenDangNhap)
        {
            bool exists = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @tenDangNhap", conn);
                cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                int count = (int)cmd.ExecuteScalar();
                exists = count > 0;
            }
            return exists;
        }
    }
}
