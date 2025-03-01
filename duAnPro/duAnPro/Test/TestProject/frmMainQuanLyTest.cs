using NUnit.Framework;
using System;
using System.Windows.Forms;

namespace duAnPro
{
    [TestFixture]
    public class frmMainQuanLyTests
    {
        private frmMainQuanLy _form;
        private frmDangNhap _dangNhap;

        [SetUp]
        public void Setup()
        {
            _dangNhap = new frmDangNhap();
            _form = new frmMainQuanLy(_dangNhap, "NVQL001", "Trần Quản Lý");
            _form.Show();
        }

        [TearDown]
        public void Cleanup()
        {
            _form.Close();
        }

        [Test]
        public void frmMainQuanLy_Should_Display_Correct_Manager_Info()
        {
            Assert.AreEqual("NVQL001", _form.Controls["txtMaNhanVien"].Text);
            Assert.AreEqual("Trần Quản Lý", _form.Controls["txtTenNhanVien"].Text);
        }

        [Test]
        public void button2_Click_Should_Open_frmQuanLyNhanVien()
        {
            Button btn = (Button)_form.Controls["button2"];
            btn.PerformClick();

            Form openedForm = Application.OpenForms["frmQuanLyNhanVien"];
            Assert.IsNotNull(openedForm, "Form Quản lý nhân viên phải được mở.");
        }

        [Test]
        public void button1_Click_Should_Open_frmKhoHang()
        {
            Button btn = (Button)_form.Controls["button1"];
            btn.PerformClick();

            Form openedForm = Application.OpenForms["frmKhoHang"];
            Assert.IsNotNull(openedForm, "Form Kho hàng phải được mở.");
        }

        [Test]
        public void button3_Click_Should_Open_frmThongKe()
        {
            Button btn = (Button)_form.Controls["button3"];
            btn.PerformClick();

            Form openedForm = Application.OpenForms["frmThongKe"];
            Assert.IsNotNull(openedForm, "Form Thống kê phải được mở.");
        }

        [Test]
        public void button4_Click_Should_Close_frmMainQuanLy()
        {
            Button btn = (Button)_form.Controls["button4"];
            btn.PerformClick();

            Assert.IsFalse(_form.Visible, "Form chính phải đóng sau khi nhấn nút Đăng xuất.");
        }

        [Test]
        public void button5_Click_Should_Close_frmMainQuanLy()
        {
            Button btn = (Button)_form.Controls["button5"];
            btn.PerformClick();

            Assert.IsFalse(_form.Visible, "Form chính phải đóng sau khi nhấn nút Thoát.");
        }
    }
}
