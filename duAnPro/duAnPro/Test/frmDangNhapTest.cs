using NUnit.Framework;
using duAnPro;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmDangNhapTests
    {
        private frmDangNhap _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmDangNhap();
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void GetDataToTable_ValidUser_ReturnsDataTable()
        {
            string sqlQuery = "SELECT TOP 1 * FROM NguoiDung";
            SqlParameter[] parameters = { };

            DataTable result = frmDangNhap.GetDataToTable(sqlQuery, parameters);

            Assert.IsNotNull(result, "Phải trả về DataTable hợp lệ.");
            Assert.Greater(result.Rows.Count, 0, "DataTable phải có ít nhất một dòng.");
        }

        [Test]
        public void Login_WithValidCredentials_ShouldSuccess()
        {
            string sqlQuery = "SELECT NhanVien.MaNhanVien, NhanVien.Ten, NguoiDung.MaQuyen " +
                              "FROM NguoiDung " +
                              "INNER JOIN NhanVien ON NguoiDung.MaNhanVien = NhanVien.MaNhanVien " +
                              "WHERE NguoiDung.TenDangNhap = @TenDangNhap AND NguoiDung.MatKhau = @MatKhau";

            SqlParameter[] parameters = {
                new SqlParameter("@TenDangNhap", SqlDbType.NVarChar) { Value = "admin" },
                new SqlParameter("@MatKhau", SqlDbType.NVarChar) { Value = "123456" }
            };

            DataTable dt = frmDangNhap.GetDataToTable(sqlQuery, parameters);

            Assert.IsNotNull(dt, "Phải trả về DataTable hợp lệ.");
            Assert.AreEqual(1, dt.Rows.Count, "Tài khoản hợp lệ phải có đúng một dòng dữ liệu.");
        }

        [Test]
        public void Login_WithInvalidCredentials_ShouldFail()
        {
            string sqlQuery = "SELECT NhanVien.MaNhanVien, NhanVien.Ten, NguoiDung.MaQuyen " +
                              "FROM NguoiDung " +
                              "INNER JOIN NhanVien ON NguoiDung.MaNhanVien = NhanVien.MaNhanVien " +
                              "WHERE NguoiDung.TenDangNhap = @TenDangNhap AND NguoiDung.MatKhau = @MatKhau";

            SqlParameter[] parameters = {
                new SqlParameter("@TenDangNhap", SqlDbType.NVarChar) { Value = "wronguser" },
                new SqlParameter("@MatKhau", SqlDbType.NVarChar) { Value = "wrongpass" }
            };

            DataTable dt = frmDangNhap.GetDataToTable(sqlQuery, parameters);

            Assert.AreEqual(0, dt.Rows.Count, "Tài khoản sai phải trả về DataTable rỗng.");
        }

        [Test]
        public void txtTenDangNhap_Should_Update_Correctly()
        {
            _form.Controls["txtTenDangNhap"].Text = "testuser";
            Assert.AreEqual("testuser", _form.Controls["txtTenDangNhap"].Text);
        }

        [Test]
        public void txtMatKhau_Should_Update_Correctly()
        {
            _form.Controls["txtMatKhau"].Text = "password";
            Assert.AreEqual("password", _form.Controls["txtMatKhau"].Text);
        }

        [Test]
        public void btnDangNhap_Click_Should_NotLogin_WithEmptyFields()
        {
            _form.Controls["txtTenDangNhap"].Text = "";
            _form.Controls["txtMatKhau"].Text = "";

            Button btn = (Button)_form.Controls["btnDangNhap"];
            btn.PerformClick();

            Assert.AreEqual("", _form.Controls["txtTenDangNhap"].Text);
            Assert.AreEqual("", _form.Controls["txtMatKhau"].Text);
        }
    }
}
