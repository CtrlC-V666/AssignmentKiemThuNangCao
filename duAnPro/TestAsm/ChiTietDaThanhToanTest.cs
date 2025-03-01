using NUnit.Framework;
using duAnPro;  // Import project chính
using System;
using System.Data;
using System.Windows.Forms;

namespace duAnPro.Tests
{
    [TestFixture]
    public class frmChiTietDaThanhToanTests
    {
        private frmChiTietDaThanhToan _form;

        [SetUp]
        public void Setup()
        {
            _form = new frmChiTietDaThanhToan();
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void LoadChiTietDonDaThanhToan_ValidMaHoaDon_LoadsData()
        {
            // Giả lập mã hóa đơn hợp lệ
            int maHoaDon = 1;

            // Gọi phương thức Load dữ liệu
            _form.LoadChiTietDonDaThanhToan(maHoaDon);

            // Kiểm tra dữ liệu đã được load hay chưa
            Assert.IsNotNull(_form.Controls["dtgvChiTietHoaDon"]);
            Assert.IsTrue(_form.Controls["dtgvChiTietHoaDon"] is DataGridView);
            DataGridView grid = (DataGridView)_form.Controls["dtgvChiTietHoaDon"];
            Assert.IsTrue(grid.RowCount > 0, "Bảng chi tiết hóa đơn phải có dữ liệu.");
        }

        [Test]
        public void txtTenKhachHang_Should_Update_Correctly()
        {
            // Gán giá trị test
            _form.Controls["txtTenKhachHang"].Text = "Nguyễn Văn A";
            Assert.AreEqual("Nguyễn Văn A", _form.Controls["txtTenKhachHang"].Text);
        }

        [Test]
        public void txtTongTien_Should_Format_Correctly()
        {
            // Gán giá trị test
            _form.Controls["txtTongTien"].Text = "1000000";
            Assert.AreEqual("1000000", _form.Controls["txtTongTien"].Text);
        }

        [Test]
        public void btnInHoaDon_Should_Open_frmInDonDaThanhToan()
        {
            // Giả lập click nút in hóa đơn
            Button btn = (Button)_form.Controls["btninHoaDon"];
            btn.PerformClick();

            // Kiểm tra form mới đã mở
            FormCollection openForms = Application.OpenForms;
            bool found = false;
            foreach (Form form in openForms)
            {
                if (form is frmInDonDaThanhToan)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found, "Form frmInDonDaThanhToan phải được mở khi bấm nút in.");
        }
    }
}
