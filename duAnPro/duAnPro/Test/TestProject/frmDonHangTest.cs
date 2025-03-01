using NUnit.Framework;
using duAnPro;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmDonHangTests
    {
        private frmDonHang _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmDonHang("NV001", "Nguyễn Văn A");
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void txtTenKH_Should_Update_Correctly()
        {
            _form.Controls["txtTenKH"].Text = "Khách Test";
            Assert.AreEqual("Khách Test", _form.Controls["txtTenKH"].Text);
        }

        [Test]
        public void txtSdt_Should_Update_Correctly()
        {
            _form.Controls["txtSdt"].Text = "0123456789";
            Assert.AreEqual("0123456789", _form.Controls["txtSdt"].Text);
        }

        [Test]
        public void LoadDataFromMenuHaiSan_Should_Return_DataTable()
        {
            _form.GetType()
                 .GetMethod("LoadDataFromMenuHaiSan", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                 .Invoke(_form, null);

            var grid = _form.Controls["dgvDanhSach"] as DataGridView;
            Assert.IsNotNull(grid, "DataGridView không được null.");
            Assert.Greater(grid.RowCount, 0, "Danh sách món ăn phải có dữ liệu.");
        }

        [Test]
        public void btnThemDonHang_Click_Should_Add_Order()
        {
            _form.Controls["txtTenKH"].Text = "Khách Test";
            _form.Controls["txtSdt"].Text = "0123456789";
            _form.Controls["txtTongTien"].Text = "100000";
            var grid = _form.Controls["dgvDanhSach"] as DataGridView;

            grid.Rows[0].Cells["SoLuong"].Value = 1;
            grid.Rows[0].Selected = true;

            Button btn = (Button)_form.Controls["btnThemDonHang"];
            btn.PerformClick();

            bool orderAdded = CheckOrderExists("Khách Test", "0123456789");
            Assert.IsTrue(orderAdded, "Đơn hàng phải được thêm thành công.");
        }

        private bool CheckOrderExists(string tenKhachHang, string sdt)
        {
            bool exists = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HoaDon WHERE TenKhachHang = @TenKhachHang AND SDT = @SDT", conn);
                cmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                int count = (int)cmd.ExecuteScalar();
                exists = count > 0;
            }
            return exists;
        }

        [Test]
        public void GenerateInvoiceCode_Should_Return_Incremented_Value()
        {
            string invoiceCode1 = _form.GetType()
                                      .GetMethod("GenerateInvoiceCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_form, null) as string;

            string invoiceCode2 = _form.GetType()
                                      .GetMethod("GenerateInvoiceCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_form, null) as string;

            Assert.AreNotEqual(invoiceCode1, invoiceCode2, "Mã hóa đơn mới phải tăng dần.");
        }

        [Test]
        public void btnDonDaDat_Click_Should_Open_frmDonDaDat()
        {
            Button btn = (Button)_form.Controls["btnDonDaDat"];
            btn.PerformClick();

            bool formOpened = false;
            foreach (Form form in Application.OpenForms)
            {
                if (form is frmDonDaDat)
                {
                    formOpened = true;
                    break;
                }
            }
            Assert.IsTrue(formOpened, "Form frmDonDaDat phải được mở khi nhấn nút.");
        }
    }
}
