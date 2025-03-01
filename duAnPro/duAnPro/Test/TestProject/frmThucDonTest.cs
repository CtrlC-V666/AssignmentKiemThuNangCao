using NUnit.Framework;
using System;
using System.Data;
using System.Windows.Forms;

namespace duAnPro
{
    [TestFixture]
    public class frmThucDonTests
    {
        private frmThucDon _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmThucDon();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void LoadDataToDataGridView_Should_Return_Data()
        {
            InvokePrivateMethod(_form, "LoadDataToDataGridView", null);

            DataGridView dgv = (DataGridView)_form.Controls["dgvSuaThucDon"];
            Assert.IsTrue(dgv.Rows.Count > 0, "Dữ liệu món ăn phải hiển thị trong DataGridView.");
        }

        [Test]
        public void CallStoredProcedure_Should_Not_Throw_Exception()
        {
            Assert.DoesNotThrow(() => InvokePrivateMethod(_form, "CallStoredProcedure", null));
        }

        [Test]
        public void UpdateMonAn_Should_Update_Successfully()
        {
            string maMon = "1";
            string tenMon = "Món Test";
            string gia = "50000";
            string moTa = "Món test thử nghiệm";
            string tinhTrang = "Còn hàng";

            InvokePrivateMethod(_form, "UpdateMonAn", new object[] { maMon, tenMon, gia, moTa, tinhTrang });

            DataTable result = InvokePrivateMethod<DataTable>(_form, "ExecuteQuery",
                new object[] { "SELECT * FROM MenuHaiSan WHERE MaMon = 1", null });

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("Món Test", result.Rows[0]["TenMon"]);
            Assert.AreEqual(50000, result.Rows[0]["Gia"]);
        }

        [Test]
        public void UpdateTinhTrang_Should_Update_Successfully()
        {
            string maMon = "1";
            string tinhTrang = "Hết Hàng";

            InvokePrivateMethod(_form, "UpdateTinhTrang", new object[] { maMon, tinhTrang });

            DataTable result = InvokePrivateMethod<DataTable>(_form, "ExecuteQuery",
                new object[] { "SELECT TinhTrang FROM MenuHaiSan WHERE MaMon = 1", null });

            Assert.AreEqual(1, result.Rows.Count);
            Assert.AreEqual("Hết Hàng", result.Rows[0]["TinhTrang"]);
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
