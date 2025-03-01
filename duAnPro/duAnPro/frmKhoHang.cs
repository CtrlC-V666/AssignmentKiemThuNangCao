using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duAnPro
{
    public partial class frmKhoHang : Form
    {
        private DataTable dtKho;
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        public frmKhoHang()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            dtKho = new DataTable();
            // Thêm sự kiện KeyPress cho các TextBox chỉ cho phép nhập số
            txtSoluong.KeyPress += new KeyPressEventHandler(txtSoLuong_KeyPress);
            txtGianhap.KeyPress += new KeyPressEventHandler(txtGiaNhap_KeyPress);
            txtGiaban.KeyPress += new KeyPressEventHandler(txtGiaBan_KeyPress);
            // Đặt kích thước form ở đây hoặc trong sự kiện Load
            this.Width = 1000; 
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmKhoHang_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
            ResizeDataGridViewColumns();
            this.StartPosition = FormStartPosition.CenterScreen;
            // Thêm vào constructor hoặc sự kiện Load của form
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
        }
        private void LoadDataToDataGridView()
        {

            string query = "SELECT MaHang, TenHang, SoLuong, GiaNhap, GiaBan, NgayNhap, MoTa FROM KhoHang";
            adapter = new SqlDataAdapter(query, conn);
            dtKho.Clear();
            adapter.Fill(dtKho);
            dataGridView1.DataSource = dtKho;

            dataGridView1.Columns["MaHang"].HeaderText = "Mã hàng";
            dataGridView1.Columns["TenHang"].HeaderText = "Tên hàng";
            dataGridView1.Columns["SoLuong"].HeaderText = "Số lượng";
            dataGridView1.Columns["GiaNhap"].HeaderText = "Giá nhập";
            dataGridView1.Columns["GiaBan"].HeaderText = "Giá bán";
            dataGridView1.Columns["NgayNhap"].HeaderText = "Ngày nhập";
            dataGridView1.Columns["MoTa"].HeaderText = "Mô tả";
            // Đặt cột Mã hàng và Ngày nhập thành ReadOnly
            dataGridView1.Columns["MaHang"].ReadOnly = true;
            dataGridView1.Columns["NgayNhap"].ReadOnly = true;

            // Căn lề phải và định dạng số cho các cột
            dataGridView1.Columns["GiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dataGridView1.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["SoLuong"].DefaultCellStyle.Format = "N0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string Tenhang = txtTenhang.Text;
                int Soluong = int.Parse(txtSoluong.Text);
                float Gianhap = float.Parse(txtGianhap.Text);
                float Giaban = float.Parse(txtGiaban.Text);
                DateTime Ngaynhap = dateTimePicker1.Value;
                string MoTa = txtMoTa.Text; // Giả sử bạn đã thêm một TextBox txtMoTa cho mô tả

                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Thêm vào bảng KhoHang
                        string insertKhoHangQuery = "INSERT INTO KhoHang (TenHang, SoLuong, GiaNhap, GiaBan, NgayNhap, MoTa) VALUES (@TenHang, @SoLuong, @GiaNhap, @GiaBan, @NgayNhap, @MoTa)";
                        SqlCommand cmdInsertKhoHang = new SqlCommand(insertKhoHangQuery, conn, transaction);
                        cmdInsertKhoHang.Parameters.AddWithValue("@TenHang", Tenhang);
                        cmdInsertKhoHang.Parameters.AddWithValue("@SoLuong", Soluong);
                        cmdInsertKhoHang.Parameters.AddWithValue("@GiaNhap", Gianhap);
                        cmdInsertKhoHang.Parameters.AddWithValue("@GiaBan", Giaban);
                        cmdInsertKhoHang.Parameters.AddWithValue("@NgayNhap", Ngaynhap);
                        cmdInsertKhoHang.Parameters.AddWithValue("@MoTa", MoTa);
                        cmdInsertKhoHang.ExecuteNonQuery();

                        // Cập nhật hoặc thêm vào bảng MenuHaiSan
                        string updateMenuHaiSanQuery = "IF EXISTS (SELECT 1 FROM MenuHaiSan WHERE TenMon = @TenHang) " +
                                                        "UPDATE MenuHaiSan SET Gia = @GiaBan, MoTa = @MoTa WHERE TenMon = @TenHang " +
                                                        "ELSE " +
                                                        "INSERT INTO MenuHaiSan (TenMon, Gia, MoTa, TinhTrang, SoLuong) VALUES (@TenHang, @GiaBan, @MoTa, 'Còn hàng', 0)";
                        SqlCommand cmdUpdateMenuHaiSan = new SqlCommand(updateMenuHaiSanQuery, conn, transaction);
                        cmdUpdateMenuHaiSan.Parameters.AddWithValue("@TenHang", Tenhang);
                        cmdUpdateMenuHaiSan.Parameters.AddWithValue("@GiaBan", Giaban);
                        cmdUpdateMenuHaiSan.Parameters.AddWithValue("@MoTa", MoTa);
                        cmdUpdateMenuHaiSan.ExecuteNonQuery();

                        // Commit transaction nếu không có lỗi
                        transaction.Commit();
                        MessageBox.Show("Thêm hàng hóa thành công.");
                        LoadDataToDataGridView();
                        ResetForm();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction nếu có lỗi
                        transaction.Rollback();
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        private void ResetForm()
        {
            txtTenhang.Text = "";
            txtSoluong.Text = "";
            txtGianhap.Text = "";
            txtGiaban.Text = "";
            txtMoTa.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            object newValue = cell.Value;
            int MaHang = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["MaHang"].Value.ToString());

            // Kiểm tra các cột "TenHang", "SoLuong", "GiaNhap", "GiaBan"
            if (string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["TenHang"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["GiaNhap"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells["GiaBan"].Value?.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Cập nhật dữ liệu vào bảng KhoHang
                    string updateKhoHangQuery = $"UPDATE KhoHang SET {columnName} = @Value WHERE MaHang = @MaHang";
                    SqlCommand cmdUpdateKhoHang = new SqlCommand(updateKhoHangQuery, conn, transaction);
                    cmdUpdateKhoHang.Parameters.AddWithValue("@Value", newValue);
                    cmdUpdateKhoHang.Parameters.AddWithValue("@MaHang", MaHang);
                    cmdUpdateKhoHang.ExecuteNonQuery();

                    // Nếu cột được chỉnh sửa là MoTa, cập nhật bảng MenuHaiSan
                    if (columnName == "MoTa")
                    {
                        // Lấy tên hàng hóa từ bảng KhoHang để cập nhật bảng MenuHaiSan
                        string tenHang = dataGridView1.Rows[e.RowIndex].Cells["TenHang"].Value.ToString();

                        string updateMenuHaiSanQuery = "UPDATE MenuHaiSan SET MoTa = @MoTa WHERE TenMon = @TenHang";
                        SqlCommand cmdUpdateMenuHaiSan = new SqlCommand(updateMenuHaiSanQuery, conn, transaction);
                        cmdUpdateMenuHaiSan.Parameters.AddWithValue("@MoTa", newValue);
                        cmdUpdateMenuHaiSan.Parameters.AddWithValue("@TenHang", tenHang);
                        cmdUpdateMenuHaiSan.ExecuteNonQuery();
                    }

                    // Commit transaction nếu không có lỗi
                    transaction.Commit();
                    MessageBox.Show("Cập nhật thành công.");
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void ResizeDataGridViewColumns()
        {
            // Đặt tự động điều chỉnh kích thước cột để hiển thị full màn hình
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["GiaNhap"].Index || e.ColumnIndex == dataGridView1.Columns["GiaBan"].Index)
            {
                if (e.Value != null)
                {
                    e.Value = string.Format("{0:N0}", e.Value);
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hàng nào được chọn không
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Lấy mã hàng của hàng được chọn
                    int MaHang = int.Parse(dataGridView1.SelectedRows[0].Cells["MaHang"].Value.ToString());

                    // Xác nhận trước khi xóa
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        conn.Open();
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // Xóa dữ liệu khỏi bảng KhoHang
                                string deleteKhoHangQuery = "DELETE FROM KhoHang WHERE MaHang = @MaHang";
                                SqlCommand cmdDeleteKhoHang = new SqlCommand(deleteKhoHangQuery, conn, transaction);
                                cmdDeleteKhoHang.Parameters.AddWithValue("@MaHang", MaHang);
                                cmdDeleteKhoHang.ExecuteNonQuery();

                                // Xóa dữ liệu khỏi bảng MenuHaiSan nếu cần
                                string deleteMenuHaiSanQuery = @"
                        DELETE FROM MenuHaiSan
                        WHERE TenMon IN (
                            SELECT TenHang
                            FROM KhoHang
                            WHERE MaHang = @MaHang
                        ) AND NOT EXISTS (
                            SELECT 1
                            FROM KhoHang
                            WHERE TenHang = MenuHaiSan.TenMon
                        )";
                                SqlCommand cmdDeleteMenuHaiSan = new SqlCommand(deleteMenuHaiSanQuery, conn, transaction);
                                cmdDeleteMenuHaiSan.Parameters.AddWithValue("@MaHang", MaHang);
                                cmdDeleteMenuHaiSan.ExecuteNonQuery();

                                // Commit transaction nếu không có lỗi
                                transaction.Commit();
                                MessageBox.Show("Xóa hàng hóa thành công.");
                                LoadDataToDataGridView();
                            }
                            catch (Exception ex)
                            {
                                // Rollback transaction nếu có lỗi
                                transaction.Rollback();
                                MessageBox.Show("Lỗi: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn hàng hóa cần xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Xóa sự kiện KeyPress cũ nếu có
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);

            // Kiểm tra xem cột nào đang được chỉnh sửa
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["SoLuong"].Index ||
                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["GiaNhap"].Index ||
                dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["GiaBan"].Index)
            {
                // Chỉ cho phép nhập số
                e.Control.KeyPress += new KeyPressEventHandler(Column_KeyPress);
            }
        }

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự nhập vào có phải là số hoặc ký tự điều khiển (như backspace) hay không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Hiển thị thông báo
                MessageBox.Show("Vui lòng nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "TenHang" || columnName == "SoLuong" || columnName == "GiaNhap" || columnName == "GiaBan")
            {
                if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Vui lòng nhập đầy đủ thông tin.";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty; // Clear error text if validation passes
                }
            }
        }
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
