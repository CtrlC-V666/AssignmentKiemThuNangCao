using NUnit.Framework;
using System;
using System.Windows.Forms;

namespace duAnPro
{
    [TestFixture]
    public class frmMainTests
    {
        private frmMain _form;
        private frmDangNhap _dangNhap;

        [SetUp]
        public void Setup()
        {
            _dangNhap = new frmDangNhap();
            _form = new frmMain(_dangNhap, "NV001", "Nguyễn Văn A");
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void frmMain_Should_Display_Correct_Employee_Info()
        {
            Assert.AreEqual("NV001", _form.Controls["txtMaNhanVien"].Text);
            Assert.AreEqual("Nguyễn Văn A", _form.Controls["txtTenNhanVien"].Text);
        }

        [Test]
        public void btnDonHang_Click_Should_Open_frmDonHang()
        {
            Button btn = (Button)_form.Controls["btnDonHang"];
            btn.PerformClick();

            Form openedForm = Application.OpenForms["frmDonHang"];
            Assert.IsNotNull(openedForm, "Form đơn hàng phải được mở.");
        }

        [Test]
        public void btnThucDon_Click_Should_Open_frmThucDon()
        {
            Button btn = (Button)_form.Controls["btnThucDon"];
            btn.PerformClick();

            Form openedForm = Application.OpenForms["frmThucDon"];
            Assert.IsNotNull(openedForm, "Form thực đơn phải được mở.");
        }

        [Test]
        public void btnDangXuat_Click_Should_Close_frmMain()
        {
            Button btn = (Button)_form.Controls["btnDangXuat"];
            btn.PerformClick();

            Assert.IsFalse(_form.Visible, "Form chính phải đóng sau khi đăng xuất.");
        }

        [Test]
        public void btnThoat_Click_Should_Exit_Application()
        {
            bool appExited = false;

            try
            {
                Button btn = (Button)_form.Controls["btnThoat"];
                btn.PerformClick();
            }
            catch (Exception)
            {
                appExited = true;
            }

            Assert.IsTrue(appExited, "Ứng dụng phải thoát sau khi nhấn nút Thoát.");
        }
    }
}
