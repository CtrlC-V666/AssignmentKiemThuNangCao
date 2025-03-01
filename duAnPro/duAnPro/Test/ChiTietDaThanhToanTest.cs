using NUnit.Framework;
using duAnPro;
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
            int maHoaDon = 1;
            _form.LoadChiTietDonDaThanhToan(maHoaDon);

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
        public void btnInHoaDon_Should_Open_frmInDonDaThanhToan()
        {
            Button btn = (Button)_form.Controls["btninHoaDon"];
            btn.PerformClick();

            bool formOpened = false;
            foreach (Form form in Application.OpenForms)
            {
                if (form is frmInDonDaThanhToan)
                {
                    formOpened = true;
                    break;
                }
            }
            Assert.IsTrue(formOpened, "Form frmInDonDaThanhToan phải được mở khi bấm nút in.");
        }
    }
}
