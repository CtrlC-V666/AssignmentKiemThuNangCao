using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;

namespace duAnPro
{
    [TestFixture]
    public class frmQuenMatKhauTests
    {
        private frmQuenMatKhau _form;
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _form = new frmQuenMatKhau();
            _random = new Random();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void IsEmailValid_Should_Return_True_For_Valid_Email()
        {
            string email = "test@example.com";
            bool result = InvokePrivateMethod<bool>(_form, "IsEmailValid", email);
            Assert.IsTrue(result, "Email hợp lệ phải trả về True.");
        }

        [Test]
        public void IsEmailValid_Should_Return_False_For_Invalid_Email()
        {
            string email = "invalid-email";
            bool result = InvokePrivateMethod<bool>(_form, "IsEmailValid", email);
            Assert.IsFalse(result, "Email không hợp lệ phải trả về False.");
        }

        [Test]
        public void IsEmailExistsInDatabase_Should_Return_False_For_NonExisting_Email()
        {
            string email = "nonexistent@example.com";
            bool result = InvokePrivateMethod<bool>(_form, "IsEmailExistsInDatabase", email);
            Assert.IsFalse(result, "Email không tồn tại trong DB phải trả về False.");
        }

        [Test]
        public void UpdatePasswordInDatabase_Should_Update_Password_Successfully()
        {
            string email = "test@example.com";
            string newPassword = _random.Next(100000, 999999).ToString();

            InvokePrivateMethod(_form, "UpdatePasswordInDatabase", email, newPassword);

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MatKhau FROM NguoiDung WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    string updatedPassword = (string)command.ExecuteScalar();
                    Assert.AreEqual(newPassword, updatedPassword, "Mật khẩu trong DB phải trùng với mật khẩu mới.");
                }
            }
        }

        private T InvokePrivateMethod<T>(object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (T)method.Invoke(obj, parameters);
        }

        private void InvokePrivateMethod(object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(obj, parameters);
        }
    }
}
