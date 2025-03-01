using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    public partial class frmChiTietDaThanhToan : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter da;
        private DataSet ds;
        private DataTable dtChiTietHoaDon; // Khai báo dtChiTietHoaDon ở mức lớp
        private int _maHoaDon;
        public frmChiTietDaThanhToan()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            ds = new DataSet();
            this.StartPosition = FormStartPosition.CenterScreen;
            dtChiTietHoaDon = new DataTable();
            // Gán sự kiện DataBindingComplete
            dtgvChiTietHoaDon.DataBindingComplete += dtgvChiTietHoaDon_DataBindingComplete;

            // Gán sự kiện CellFormatting
            dtgvChiTietHoaDon.CellFormatting += dtgvChiTietHoaDon_CellFormatting;
        }

        private void frmChiTietDaThanhToan_Load(object sender, EventArgs e)
        {
            // Gán sự kiện CellFormatting
            dtgvChiTietHoaDon.CellFormatting += dtgvChiTietHoaDon_CellFormatting;
        }
        public void LoadChiTietDonDaThanhToan(int maHoaDon)
        {
            _maHoaDon = maHoaDon;
            try
            {
                conn.Open();

                // Lấy chi tiết đơn đã thanh toán
                SqlCommand cmd = new SqlCommand("GetChiTietDonDaThanhToan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                da = new SqlDataAdapter(cmd);
                ds.Clear(); // Xóa dữ liệu cũ
                da.Fill(ds, "ChiTietDonDaThanhToan");

                // Kiểm tra dữ liệu có được nạp không
                if (ds.Tables["ChiTietDonDaThanhToan"].Rows.Count > 0)
                {
                    dtChiTietHoaDon = ds.Tables["ChiTietDonDaThanhToan"];
                    dtgvChiTietHoaDon.DataSource = dtChiTietHoaDon; // Đảm bảo DataSource được gán đúng
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu chi tiết cho đơn hàng.");
                }

                // Lấy tên khách hàng và tổng tiền
                SqlCommand cmdKhachHang = new SqlCommand("SELECT TenKhachHang, TongTien FROM HoaDon WHERE MaHoaDon = @MaHoaDon", conn);
                cmdKhachHang.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                SqlDataReader reader = cmdKhachHang.ExecuteReader();
                if (reader.Read())
                {
                    txtTenKhachHang.Text = reader["TenKhachHang"].ToString();

                    // Định dạng tổng tiền
                    decimal tongTien = Convert.ToDecimal(reader["TongTien"]);
                    txtTongTien.Text = tongTien.ToString("N0"); // Định dạng với dấu phẩy
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void txtTenKhachHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgvChiTietHoaDon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgvChiTietHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra tên cột và giá trị không rỗng
            if (dtgvChiTietHoaDon.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
            {
                // Kiểm tra kiểu dữ liệu và chuyển đổi
                if (decimal.TryParse(e.Value.ToString(), out decimal value))
                {
                    e.Value = value.ToString("#,##0"); // Định dạng lại số tiền
                    e.FormattingApplied = true;
                }
            }
            else if (dtgvChiTietHoaDon.Columns[e.ColumnIndex].Name == "TongTien" && e.Value != null)
            {
                // Kiểm tra kiểu dữ liệu và chuyển đổi
                if (decimal.TryParse(e.Value.ToString(), out decimal value))
                {
                    e.Value = value.ToString("#,##0"); // Định dạng lại số tiền
                    e.FormattingApplied = true;
                }
            }
        }

        private void dtgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btninHoaDon_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            string tongTien = txtTongTien.Text;
            DateTime ngayLapHoaDon = DateTime.Now; // Or use the relevant date if applicable

            // Pass the DataTable and DateTime to the frmInHoaDon constructor
            frmInDonDaThanhToan frmIDDTT = new frmInDonDaThanhToan(tenKhachHang, tongTien, dtChiTietHoaDon, ngayLapHoaDon);
            frmIDDTT.Show();
        }

        private void dtgvChiTietHoaDon_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtgvChiTietHoaDon.Columns["MaChiTietHoaDon"] != null)
            {
                dtgvChiTietHoaDon.Columns["MaChiTietHoaDon"].HeaderText = "Nhót";
                dtgvChiTietHoaDon.Columns["MaChiTietHoaDon"].Visible = false; // Ẩn cột
            }
            if (dtgvChiTietHoaDon.Columns["MaHoaDon"] != null)
            {
                dtgvChiTietHoaDon.Columns["MaHoaDon"].HeaderText = "Mã hóa đơn";
                dtgvChiTietHoaDon.Columns["MaHoaDon"].Visible = false; // Ẩn cột
            }

            if (dtgvChiTietHoaDon.Columns["TenKhachHang"] != null)
            {
                dtgvChiTietHoaDon.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                dtgvChiTietHoaDon.Columns["TenKhachHang"].Visible = false; // Ẩn cột
            }

            if (dtgvChiTietHoaDon.Columns["TenSanPham"] != null)
            {
                dtgvChiTietHoaDon.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            }

            if (dtgvChiTietHoaDon.Columns["SoLuong"] != null)
            {
                dtgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số lượng";
                dtgvChiTietHoaDon.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dtgvChiTietHoaDon.Columns["DonGia"] != null)
            {
                dtgvChiTietHoaDon.Columns["DonGia"].HeaderText = "Đơn giá (VND)";
                dtgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dtgvChiTietHoaDon.Columns["TongTien"] != null)
            {
                dtgvChiTietHoaDon.Columns["TongTien"].HeaderText = "Thành tiền (VND)";
                dtgvChiTietHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text;
            string tongTien = txtTongTien.Text;
            DateTime ngayLapHoaDon = DateTime.Now; // Hoặc ngày tháng phù hợp nếu có

            // Sử dụng thuộc tính để lấy mã hóa đơn
            int maHoaDon = _maHoaDon;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save an Invoice";
                saveFileDialog.FileName = $"hoadon_{maHoaDon}_{ngayLapHoaDon.ToString("yyyyMMddHHmm")}_{tenKhachHang}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pdfPath = saveFileDialog.FileName;
                    GeneratePdf(tenKhachHang, tongTien, dtChiTietHoaDon, pdfPath, maHoaDon);
                }
            }
        }


        private void GeneratePdf(string tenKhachHang, string tongTien, DataTable dtChiTietHoaDon, string pdfPath, int maHoaDon)
        {
            // Đổi tên khách hàng để sử dụng trong tên file
            tenKhachHang = tenKhachHang.Replace(" ", "_");
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                tenKhachHang = tenKhachHang.Replace(c.ToString(), string.Empty);
            }

            // Lấy đường dẫn của tệp font từ thư mục đầu ra của dự án
            string fontPath = Path.Combine(Application.StartupPath, "times.ttf");

            using (var doc = new Document())
            {
                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));
                    doc.Open();

                    // Sử dụng font từ thư mục đầu ra của dự án
                    var bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var fontUnicode = new iTextSharp.text.Font(bf, 12);

                    var title = new iTextSharp.text.Paragraph("Hóa Đơn", new iTextSharp.text.Font(bf, 16))
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(title);
                    doc.Add(new iTextSharp.text.Paragraph(" "));

                    doc.Add(new iTextSharp.text.Paragraph("Mã Hóa Đơn: " + maHoaDon.ToString(), fontUnicode));
                    doc.Add(new iTextSharp.text.Paragraph("Tên Khách Hàng: " + tenKhachHang, fontUnicode));
                    doc.Add(new iTextSharp.text.Paragraph("Tổng tiền: " + tongTien + " VND", fontUnicode));
                    doc.Add(new iTextSharp.text.Paragraph("Ngày Xuất Hóa Đơn: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fontUnicode));
                    doc.Add(new iTextSharp.text.Paragraph(" "));

                    PdfPTable table = new PdfPTable(3)
                    {
                        WidthPercentage = 100
                    };

                    var columnHeaders = new string[] { "Tên sản phẩm", "Số lượng", "Giá (VND)" };
                    foreach (var header in columnHeaders)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, fontUnicode))
                        {
                            HorizontalAlignment = header == "Số lượng" || header == "Giá (VND)" ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT
                        };
                        table.AddCell(cell);
                    }

                    foreach (DataRow row in dtChiTietHoaDon.Rows)
                    {
                        table.AddCell(new PdfPCell(new Phrase(row["TenSanPham"].ToString(), fontUnicode)));
                        PdfPCell soLuongCell = new PdfPCell(new Phrase(row["SoLuong"].ToString(), fontUnicode))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT
                        };
                        table.AddCell(soLuongCell);

                        decimal donGia = Convert.ToDecimal(row["DonGia"]);
                        PdfPCell donGiaCell = new PdfPCell(new Phrase(donGia.ToString("#,##0"), fontUnicode))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT
                        };
                        table.AddCell(donGiaCell);
                    }

                    doc.Add(table);
                    doc.Add(new iTextSharp.text.Paragraph(" "));
                    doc.Add(new iTextSharp.text.Paragraph("Thành Tiền: " + tongTien + " VND", fontUnicode) { Alignment = Element.ALIGN_RIGHT });
                    doc.Add(new iTextSharp.text.Paragraph(" "));
                    doc.Add(new iTextSharp.text.Paragraph("Nhà hàng Rùa Seadfoods cảm ơn quý khách!", fontUnicode) { Alignment = Element.ALIGN_LEFT });

                    MessageBox.Show("Hóa đơn đã được in thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tạo PDF: " + ex.Message);
                }
                finally
                {
                    doc.Close();
                }
            }
        }
    }
}
