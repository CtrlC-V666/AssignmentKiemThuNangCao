using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro
{
    [TestFixture]
    public class frmThongKeTests
    {
        private frmThongKe _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmThongKe();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void ExecuteQuery_Should_Return_Data_When_Valid_Query()
        {
            string query = "SELECT TOP 1 * FROM HoaDon"; // Kiểm tra xem bảng có dữ liệu không
            DataTable result = InvokePrivateMethod<DataTable>(_form, "ExecuteQuery", new object[] { query, null });

            Assert.IsNotNull(result, "Kết quả không được null.");
            Assert.IsTrue(result.Rows.Count > 0, "Bảng HoaDon phải có ít nhất 1 bản ghi.");
        }

        [Test]
        public void ThongKeDoanhThu_Should_Return_Valid_Data()
        {
            InvokePrivateMethod(_form, "btnThongKeDoanhThu_Click", new object[] { null, null });

            DataGridView dgv = (DataGridView)_form.Controls["dgvThongKe"];
            Assert.IsTrue(dgv.Rows.Count > 0, "Thống kê doanh thu phải trả về dữ liệu.");
        }

        [Test]
        public void ThongKeMonAn_Should_Return_Valid_Data()
        {
            InvokePrivateMethod(_form, "btnThongKeMonAn_Click", new object[] { null, null });

            DataGridView dgv = (DataGridView)_form.Controls["dgvThongKe"];
            Assert.IsTrue(dgv.Rows.Count > 0, "Thống kê món ăn phải trả về dữ liệu.");
        }

        [Test]
        public void ThongKeKhachHang_Should_Return_Valid_Data()
        {
            InvokePrivateMethod(_form, "btnThongKeKhachHang_Click", new object[] { null, null });

            DataGridView dgv = (DataGridView)_form.Controls["dgvThongKe"];
            Assert.IsTrue(dgv.Rows.Count > 0, "Thống kê khách hàng phải trả về dữ liệu.");
        }

        [Test]
        public void ThongKeHoaDon_Should_Return_Valid_Data()
        {
            InvokePrivateMethod(_form, "btnThongKeHoaDon_Click", new object[] { null, null });

            DataGridView dgv = (DataGridView)_form.Controls["dgvThongKe"];
            Assert.IsTrue(dgv.Rows.Count > 0, "Thống kê hóa đơn phải trả về dữ liệu.");
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
