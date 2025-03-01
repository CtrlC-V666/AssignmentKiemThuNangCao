using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    public partial class frmQuenMatKhau : Form
    {
        public frmQuenMatKhau()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
      
        Random random = new Random();
        int otp;

        private void btnOTP_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                if (!IsEmailValid(email))
                {
                    MessageBox.Show("Địa chỉ email không hợp lệ hoặc không tồn tại.");
                    return;
                }

                if (!IsEmailExistsInDatabase(email))
                {
                    MessageBox.Show("Địa chỉ email không tồn tại trong hệ thống.");
                    return;
                }

                otp = random.Next(100000, 1000000); // tạo mã random 6 số 
                var fromAddress = new MailAddress("hungnspd09959@gmail.com"); // mail dùng để gửi mã otp
                var toAddress = new MailAddress(email); // mail dùng để nhận otp
                const string frompass = "upta hshl zudm ebdo";
                const string subject = "OTP code";
                string body = $"Mã xác nhận (mật khẩu mới) của bạn là: {otp}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                })
                {
                    smtp.Send(message);
                }

                // Cập nhật mật khẩu mới là OTP trong cơ sở dữ liệu
                UpdatePasswordInDatabase(email, otp.ToString());

                MessageBox.Show("OTP đã được gửi qua email.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdatePasswordInDatabase(string email, string newPassword)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE NguoiDung SET MatKhau = @MatKhau WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MatKhau", newPassword);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool IsEmailValid(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                string host = addr.Host;

                IPHostEntry entry = Dns.GetHostEntry(host);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (SocketException)
            {
                return false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (otp.ToString().Equals(txtOTP.Text))
            {
                MessageBox.Show("Xác Minh Thành Công");
                
            }
            else
            {
                MessageBox.Show("Xác Minh Không Thành Công");
            }
        }
        private bool IsEmailExistsInDatabase(string email)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void frmQuenMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
