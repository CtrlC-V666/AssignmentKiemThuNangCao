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
    using iTextSharp.text;
    using iTextSharp.text.pdf;


    namespace duAnPro
    {

        public partial class frmChiTietHoaDon : Form
        {
            private SqlConnection conn;
            private SqlDataAdapter adapter;
            private DataTable dtChiTietHoaDon;
            private DataTable dtMenuHaiSan;
            private List<MonAn> danhSachMonAn = new List<MonAn>();
            private int maHoaDon; // Biến lưu trữ mã hóa đơn
            public delegate void PaymentConfirmedHandler();
            public event PaymentConfirmedHandler PaymentConfirmed;


        private void CreateFolder()
        {
            try
            {
                string folderPath = @"C:\HoaDon";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine("Đã tạo thư mục thành công: " + folderPath);
                }
                else
                {
                    Console.WriteLine("Thư mục đã tồn tại: " + folderPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo thư mục: " + ex.Message);
            }
        }
        private class MonAn
            {
                public int MaMon { get; set; }
                public string TenMon { get; set; }
                public decimal Gia { get; set; }
                public int SoLuong { get; set; }
            }
            public frmChiTietHoaDon(int maHoaDon)
            {
                InitializeComponent();

            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            dtChiTietHoaDon = new DataTable();
                dtMenuHaiSan = new DataTable();
                this.StartPosition = FormStartPosition.CenterScreen;
                this.maHoaDon = maHoaDon;
                dgvDSMon.CellValidating += dgvDSMon_CellValidating;
                dgvDSMon.CellEndEdit += dgvDSMon_CellEndEdit;
            }
        public void LoadChiTietHoaDon(int maHoaDon)
        {
            string query = "SELECT cthd.TenSanPham, cthd.SoLuong, cthd.DonGia, hd.TenKhachHang, hd.TongTien " +
                           "FROM ChiTietHoaDon cthd " +
                           "INNER JOIN HoaDon hd ON cthd.MaHoaDon = hd.MaHoaDon " +
                           "WHERE cthd.MaHoaDon = @MaHoaDon";
            adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

            try
            {
                conn.Open();
                dtChiTietHoaDon.Clear();
                adapter.Fill(dtChiTietHoaDon);

                if (dtChiTietHoaDon.Rows.Count > 0)
                {
                    dtgvChiTietHoaDon.DataSource = dtChiTietHoaDon;
                    dtgvChiTietHoaDon.Columns["TenKhachHang"].Visible = false;
                    dtgvChiTietHoaDon.Columns["TongTien"].Visible = false;
                    dtgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    if (dtgvChiTietHoaDon.Columns.Contains("DonGia"))
                    {
                        dtgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    dtgvChiTietHoaDon.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
                    dtgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số lượng";
                    dtgvChiTietHoaDon.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dtgvChiTietHoaDon.Columns["DonGia"].HeaderText = "Giá (VND)";
                    txtTenKhachHang.Text = dtChiTietHoaDon.Rows[0]["TenKhachHang"].ToString();
                    decimal tongTien = Convert.ToDecimal(dtChiTietHoaDon.Rows[0]["TongTien"]);
                    txtTongTien.Text = tongTien.ToString("#,##0");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu chi tiết cho hóa đơn này.");
                }
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

        private void dtgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void dtgvChiTietHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (dtgvChiTietHoaDon.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
                {
                    if (e.Value is decimal)
                    {
                        e.Value = ((decimal)e.Value).ToString("#,##0"); // Định dạng lại số tiền
                        e.FormattingApplied = true;
                    }
                }
            }

            private void frmChiTietHoaDon_Load(object sender, EventArgs e)
            {
                LoadMenuHaiSan();
                // Cài đặt phông chữ cho DataGridView
                foreach (DataGridViewColumn column in dtgvChiTietHoaDon.Columns)
                {
                    column.ReadOnly = true;
                }
                foreach (DataGridViewColumn column in dgvDSMon.Columns)
                {
                    column.ReadOnly = true;
                }
                // Đặt cột SoLuong có thể chỉnh sửa
                dgvDSMon.Columns["SoLuong"].ReadOnly = false;

            dgvDSMon.Columns["SoLuong"].ValueType = typeof(int);

            CreateFolder();
        }
        private void LoadMenuHaiSan()
        {
            string query = "SELECT MaMon, TenMon, Gia, MoTa, TinhTrang, SoLuong FROM MenuHaiSan";
            adapter = new SqlDataAdapter(query, conn);

            try
            {
                conn.Open();
                dtMenuHaiSan.Clear();
                adapter.Fill(dtMenuHaiSan);

                if (dtMenuHaiSan.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtMenuHaiSan);
                    dv.RowFilter = "TinhTrang <> 'Hết Hàng'";
                    dv.Sort = "MaMon DESC";
                    dgvDSMon.DataSource = dv.ToTable();
                    dgvDSMon.Columns["MaMon"].HeaderText = "Mã món";
                    dgvDSMon.Columns["TenMon"].HeaderText = "Tên món";
                    dgvDSMon.Columns["Gia"].HeaderText = "Giá (VND/Món ăn)";
                    dgvDSMon.Columns["MoTa"].HeaderText = "Mô tả";
                    dgvDSMon.Columns["TinhTrang"].HeaderText = "Tình trạng";
                    dgvDSMon.Columns["SoLuong"].HeaderText = "Số lượng";
                    dgvDSMon.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    foreach (DataGridViewColumn column in dgvDSMon.Columns)
                    {
                        if (column.Name == "Gia")
                        {
                            column.DefaultCellStyle.Format = "#,##0";
                        }
                        else
                        {
                            column.ReadOnly = true;
                        }
                    }
                    dgvDSMon.Columns["SoLuong"].ReadOnly = false;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu từ bảng MenuHaiSan.");
                }
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

            private void txtTongTien_TextChanged(object sender, EventArgs e)
            {

            }

            private void button1_Click(object sender, EventArgs e)
            {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO ChiTietHoaDon (MaHoaDon, TenSanPham, SoLuong, DonGia, TenKhachHang) VALUES (@MaHoaDon, @TenSanPham, @SoLuong, @DonGia, @TenKhachHang)", conn);
                    SqlCommand updateCommand = new SqlCommand("UPDATE ChiTietHoaDon SET SoLuong = SoLuong + @SoLuong WHERE MaHoaDon = @MaHoaDon AND TenSanPham = @TenSanPham", conn);
                    SqlCommand updateHoaDonCommand = new SqlCommand("UPDATE HoaDon SET TongTien = @TongTien WHERE MaHoaDon = @MaHoaDon", conn);

                    try
                    {
                        conn.Open();

                        // Xóa danh sách món ăn trước khi thêm mới
                        danhSachMonAn.Clear();

                        // Biến để tính tổng tiền mới
                        decimal tongTienMoi = 0;

                        foreach (DataGridViewRow row in dgvDSMon.Rows)
                        {
                            if (row.Cells["SoLuong"].Value != null && Convert.ToInt32(row.Cells["SoLuong"].Value) > 0)
                            {
                                // Lấy thông tin món ăn từ dgvDSMon
                                int maMon = Convert.ToInt32(row.Cells["MaMon"].Value);
                                string tenMon = row.Cells["TenMon"].Value.ToString();
                                decimal gia = Convert.ToDecimal(row.Cells["Gia"].Value);
                                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);

                                // Cập nhật danh sách món ăn
                                MonAn monAn = danhSachMonAn.FirstOrDefault(m => m.MaMon == maMon);
                                if (monAn != null)
                                {
                                    // Cập nhật số lượng nếu món ăn đã có trong danh sách
                                    monAn.SoLuong += soLuong;
                                }
                                else
                                {
                                    danhSachMonAn.Add(new MonAn
                                    {
                                        MaMon = maMon,
                                        TenMon = tenMon,
                                        Gia = gia,
                                        SoLuong = soLuong
                                    });
                                }

                                // Kiểm tra xem món ăn đã tồn tại trong hóa đơn chưa
                                SqlCommand checkExistCommand = new SqlCommand("SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon AND TenSanPham = @TenSanPham", conn);
                                checkExistCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                checkExistCommand.Parameters.AddWithValue("@TenSanPham", tenMon);
                                int count = (int)checkExistCommand.ExecuteScalar();

                                if (count > 0)
                                {
                                    // Nếu món ăn đã tồn tại, cập nhật số lượng
                                    updateCommand.Parameters.Clear();
                                    updateCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    updateCommand.Parameters.AddWithValue("@TenSanPham", tenMon);
                                    updateCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                                    updateCommand.ExecuteNonQuery();
                                }
                                else
                                {
                                    // Nếu món ăn chưa tồn tại, thêm mới
                                    insertCommand.Parameters.Clear();
                                    insertCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                                    insertCommand.Parameters.AddWithValue("@TenSanPham", tenMon);
                                    insertCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                                    insertCommand.Parameters.AddWithValue("@DonGia", gia);
                                    insertCommand.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHang.Text);
                                    insertCommand.ExecuteNonQuery();
                                }

                                // Cập nhật tổng tiền mới
                                tongTienMoi += gia * soLuong;
                            }
                        }

                        // Cập nhật tổng tiền hiện tại trong hóa đơn
                        decimal tongTienHienTai = Convert.ToDecimal(txtTongTien.Text.Replace(",", ""));
                        decimal tongTienCapNhat = tongTienHienTai + tongTienMoi;
                        txtTongTien.Text = tongTienCapNhat.ToString("#,##0");

                        // Cập nhật tổng tiền trong bảng HoaDon
                        updateHoaDonCommand.Parameters.Clear();
                        updateHoaDonCommand.Parameters.AddWithValue("@TongTien", tongTienCapNhat);
                        updateHoaDonCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                        updateHoaDonCommand.ExecuteNonQuery();

                        // Cập nhật DataGridView chi tiết hóa đơn
                        LoadChiTietHoaDon(maHoaDon);

                        foreach (DataGridViewRow row in dgvDSMon.Rows)
                        {
                            if (row.Cells["SoLuong"].Value != null)
                            {
                                row.Cells["SoLuong"].Value = 0;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }


            private void dgvDSMon_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void dgvDSMon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
            if (e.ColumnIndex == dgvDSMon.Columns["SoLuong"].Index)
            {
                // Lấy giá trị nhập vào
                if (dgvDSMon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    int soLuong;
                    if (!int.TryParse(dgvDSMon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out soLuong) || soLuong < 0)
                    {
                        // Nếu giá trị không hợp lệ, đặt lại giá trị về 0
                        dgvDSMon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        MessageBox.Show("Số lượng phải là một số nguyên dương.");
                    }
                }
            }
        }

            private void chkDaThanhToan_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void chkChuaThanhToan_CheckedChanged(object sender, EventArgs e)
            {

            }

            private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
            {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True"))
            {
                SqlCommand updateCommand = new SqlCommand("UPDATE HoaDon SET TrangThai = 'Đã thanh toán' WHERE MaHoaDon = @MaHoaDon", conn);

                try
                {
                    conn.Open();

                    // Cập nhật trạng thái thanh toán
                    updateCommand.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    updateCommand.ExecuteNonQuery();

                    // Thông báo thành công
                    MessageBox.Show("Đơn hàng đã được xác nhận thanh toán.");

                    // Kích hoạt event để thông báo việc thanh toán thành công
                    PaymentConfirmed?.Invoke();

                   

                    // Đóng form sau khi thanh toán thành công
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

      

        private void label4_Click(object sender, EventArgs e)
            {

            }

            private void button1_Click_1(object sender, EventArgs e)
            {
                string tenKhachHang = txtTenKhachHang.Text;
                string tongTien = txtTongTien.Text;
                DateTime ngayLapHoaDon = DateTime.Now; // Hoặc sử dụng ngày phù hợp nếu có
                string themThongTin = "Thông tin khác"; // Thay bằng giá trị phù hợp

                // Truyền DataTable và DateTime vào constructor của frmInHoaDon
                frmInHoaDon frmInHoaDon = new frmInHoaDon(tenKhachHang, tongTien, dtChiTietHoaDon, ngayLapHoaDon, themThongTin);
                frmInHoaDon.Show();
            }

            private void dgvDSMon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (dgvDSMon.Columns[e.ColumnIndex].Name == "Gia" && e.Value != null)
                {
                    if (e.Value is decimal)
                    {
                        e.Value = ((decimal)e.Value).ToString("#,##0"); // Định dạng lại số tiền
                        e.FormattingApplied = true;
                    }
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // Đặt căn lề phải cho cột
                }
            }

            private void dgvDSMon_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            {
            if (dgvDSMon.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                // Kiểm tra nếu ô đang kiểm tra là cột "SoLuong"
                if (!int.TryParse(e.FormattedValue.ToString(), out int soLuong) || soLuong < 0)
                {
                    // Nếu giá trị không phải số hợp lệ hoặc nhỏ hơn 0, hiển thị thông báo
                    e.Cancel = true; // Ngăn không cho thay đổi giá trị của ô
                    dgvDSMon.Rows[e.RowIndex].ErrorText = "Vui lòng nhập số hợp lệ và không âm";
                }
                else
                {
                    dgvDSMon.Rows[e.RowIndex].ErrorText = string.Empty; // Xóa thông báo lỗi nếu giá trị hợp lệ
                }
            }
        }

            private void dgvDSMon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            {
           
            }

        private void dgvDSMon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvDSMon_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDSMon.CurrentCell.ColumnIndex == dgvDSMon.Columns["SoLuong"].Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(dgvDSMon_SoLuong_KeyPress);
                    tb.KeyPress += new KeyPressEventHandler(dgvDSMon_SoLuong_KeyPress);
                }
            }
        }
        private void dgvDSMon_SoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự không hợp lệ được nhập vào
            }
        }

    }
}

    

