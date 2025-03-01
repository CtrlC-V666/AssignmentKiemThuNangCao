using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro
{
    [TestFixture]
    public class frmThemThucDonTests
    {
        private frmThemThucDon _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmThemThucDon();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void GenerateMaMon_Should_Return_New_ID()
        {
            int newId = InvokePrivateMethod<int>(_form, "GenerateMaMon");

            Assert.Greater(newId, 0, "Mã món mới phải lớn hơn 0.");
        }

        [Test]
        public void AddFood_Should_Success_When_Valid_Data()
        {
            _form.Controls["txtTenMon"].Text = "Món Test";
            _form.Controls["txtGia"].Text = "50000";
            _form.Controls["txtMoTa"].Text = "Mô tả test";
            ((CheckBox)_form.Controls["chkConHang"]).Checked = true;

            InvokePrivateMethod(_form, "button1_Click", new object[] { null, null });

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM MenuHaiSan WHERE TenMon = 'Món Test'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int count = (int)cmd.ExecuteScalar();
                    Assert.AreEqual(1, count, "Món ăn phải được thêm thành công.");
                }
            }
        }

        [Test]
        public void AddFood_Should_Fail_When_Price_Is_Not_Number()
        {
            _form.Controls["txtTenMon"].Text = "Món Test";
            _form.Controls["txtGia"].Text = "abc"; // Giá không hợp lệ
            _form.Controls["txtMoTa"].Text = "Mô tả test";
            ((CheckBox)_form.Controls["chkConHang"]).Checked = true;

            var ex = Assert.Throws<FormatException>(() =>
            {
                InvokePrivateMethod(_form, "button1_Click", new object[] { null, null });
            });

            Assert.That(ex.Message, Is.Not.Null);
        }

        [Test]
        public void AddFood_Should_Fail_When_No_Status_Selected()
        {
            _form.Controls["txtTenMon"].Text = "Món Test";
            _form.Controls["txtGia"].Text = "50000";
            _form.Controls["txtMoTa"].Text = "Mô tả test";
            ((CheckBox)_form.Controls["chkConHang"]).Checked = false;
            ((CheckBox)_form.Controls["chkHetHang"]).Checked = false;

            var ex = Assert.Throws<Exception>(() =>
            {
                InvokePrivateMethod(_form, "button1_Click", new object[] { null, null });
            });

            Assert.That(ex.Message, Is.EqualTo("Vui lòng chọn tình trạng của món."));
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
