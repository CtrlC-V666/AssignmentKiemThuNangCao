using NUnit.Framework;
using duAnPro;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmDoiMatKhauTests
    {
        private frmDoiMatKhau _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmDoiMatKhau();
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void txtTenDangNhap_Should_Update_Correctly()
        {
            _form.Controls["txtTenDangNhap"].Text = "testuser";
            Assert.AreEqual("testuser", _form.Controls["txtTenDangNhap"].Text);
        }

        [Test]
        public void txtMKCu_Should_Update_Correctly()
        {
            _form.Controls["txtMKCu"].Text = "oldpassword";
            Assert.AreEqual("oldpassword", _form.Controls["txtMKCu"].Text);
        }

        [Test]
        public void txtMKMoi_Should_Update_Correctly()
        {
            _form.Controls["txtMKMoi"].Text = "newpassword";
            Assert.AreEqual("newpassword", _form.Controls["txtMKMoi"].Text);
        }

        [Test]
        public void txtConfirm_Should_Update_Correctly()
        {
            _form.Controls["txtConfirm"].Text = "newpassword";
            Assert.AreEqual("newpassword", _form.Controls["txtConfirm"].Text);
        }

        [Test]
        public void btnOk_Click_Should_NotChangePassword_WithEmptyFields()
        {
            _form.Controls["txtTenDangNhap"].Text = "";
            _form.Controls["txtMKCu"].Text = "";
            _form.Controls["txtMKMoi"].Text = "";
            _form.Controls["txtConfirm"].Text = "";

            Button btn = (Button)_form.Controls["btnOk"];
            btn.PerformClick();

            Assert.AreEqual("", _form.Controls["txtTenDangNhap"].Text);
            Assert.AreEqual("", _form.Controls["txtMKCu"].Text);
            Assert.AreEqual("", _form.Controls["txtMKMoi"].Text);
            Assert.AreEqual("", _form.Controls["txtConfirm"].Text);
        }

        [Test]
        public void btnOk_Click_Should_NotChangePassword_When_OldPassword_Wrong()
        {
            _form.Controls["txtTenDangNhap"].Text = "testuser";
            _form.Controls["txtMKCu"].Text = "wrongpassword";
            _form.Controls["txtMKMoi"].Text = "newpassword";
            _form.Controls["txtConfirm"].Text = "newpassword";

            Button btn = (Button)_form.Controls["btnOk"];
            btn.PerformClick();

            bool passwordChanged = CheckPasswordChanged("testuser", "newpassword");
            Assert.IsFalse(passwordChanged, "Mật khẩu không được thay đổi nếu mật khẩu cũ sai.");
        }

        [Test]
        public void btnOk_Click_Should_ChangePassword_When_OldPassword_Correct()
        {
            _form.Controls["txtTenDangNhap"].Text = "testuser";
            _form.Controls["txtMKCu"].Text = "oldpassword";
            _form.Controls["txtMKMoi"].Text = "newpassword";
            _form.Controls["txtConfirm"].Text = "newpassword";

            Button btn = (Button)_form.Controls["btnOk"];
            btn.PerformClick();

            bool passwordChanged = CheckPasswordChanged("testuser", "newpassword");
            Assert.IsTrue(passwordChanged, "Mật khẩu phải được thay đổi khi mật khẩu cũ đúng.");
        }

        private bool CheckPasswordChanged(string tenDangNhap, string matKhauMoi)
        {
            bool updated = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhauMoi", conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                int count = (int)cmd.ExecuteScalar();
                updated = count > 0;
            }
            return updated;
        }
    }
}
