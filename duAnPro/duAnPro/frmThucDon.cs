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
    public partial class frmThucDon : Form
    {
        private DataTable dtMenuHaiSan;
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        public frmThucDon()
        {
       
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            dtMenuHaiSan = new DataTable();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmThucDon_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
            SetDataGridViewColumnWidth();
            CallStoredProcedure();

           
        }
        private void CallStoredProcedure()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateDatabaseData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
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
        private void LoadDataToDataGridView()
        {
            string query = "SELECT MaMon, TenMon, Gia, MoTa, TinhTrang FROM MenuHaiSan";
            adapter = new SqlDataAdapter(query, conn);
            dtMenuHaiSan.Clear();
            adapter.Fill(dtMenuHaiSan);

            // Clear existing columns and data in DataGridView
            dgvSuaThucDon.DataSource = null;

            // Assign DataTable as DataSource
            dgvSuaThucDon.DataSource = dtMenuHaiSan;

            // Đặt tên cột thành tiếng Việt
            if (dgvSuaThucDon.Columns.Contains("MaMon"))
                dgvSuaThucDon.Columns["MaMon"].HeaderText = "Mã món";
            if (dgvSuaThucDon.Columns.Contains("TenMon"))
                dgvSuaThucDon.Columns["TenMon"].HeaderText = "Tên món";
            if (dgvSuaThucDon.Columns.Contains("Gia"))
            {
                dgvSuaThucDon.Columns["Gia"].HeaderText = "Giá";
                dgvSuaThucDon.Columns["Gia"].DefaultCellStyle.Format = "N0";
                dgvSuaThucDon.Columns["Gia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSuaThucDon.Columns.Contains("MoTa"))
                dgvSuaThucDon.Columns["MoTa"].HeaderText = "Mô tả";
            if (dgvSuaThucDon.Columns.Contains("TinhTrang"))
            {
                dgvSuaThucDon.Columns["TinhTrang"].HeaderText = "Tình trạng";
                dgvSuaThucDon.Columns["TinhTrang"].ReadOnly = true; // Đặt thuộc tính ReadOnly
            }
               
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSuaThucDon.SelectedRows.Count > 0)
            {
                // Lấy mã món của hàng được chọn
                string maMon = dgvSuaThucDon.SelectedRows[0].Cells["MaMon"].Value.ToString();

                // Xác nhận xóa món ăn
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa món ăn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Xóa món ăn từ cơ sở dữ liệu
                    try
                    {
                        conn.Open();
                        string deleteQuery = "DELETE FROM MenuHaiSan WHERE MaMon = @MaMon";
                        SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa món ăn thành công.");
                        LoadDataToDataGridView(); // Làm mới DataGridView sau khi xóa thành công
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
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa.");
            }
        }
        private void UpdateMonAn(string maMon, string tenMon, string gia, string moTa, string tinhTrang)
        {
            try
            {
                conn.Open();
                string updateQuery = "UPDATE MenuHaiSan SET TenMon = @TenMon, Gia = @Gia, MoTa = @MoTa, TinhTrang = @TinhTrang WHERE MaMon = @MaMon";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@TenMon", tenMon);
                cmd.Parameters.AddWithValue("@Gia", gia);
                cmd.Parameters.AddWithValue("@MoTa", moTa);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                cmd.Parameters.AddWithValue("@MaMon", maMon);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật món ăn thành công.");
                }
                else
                {
                    MessageBox.Show("Món ăn không tồn tại để cập nhật.");
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

        private void dgvSuaThucDon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvSuaThucDon.Rows[e.RowIndex];

                // Lấy giá trị từ các ô của hàng
                string maMon = row.Cells["MaMon"]?.Value?.ToString() ?? string.Empty;
                string tenMon = row.Cells["TenMon"]?.Value?.ToString() ?? string.Empty;
                string giaText = row.Cells["Gia"]?.Value?.ToString() ?? string.Empty;
                string moTa = row.Cells["MoTa"]?.Value?.ToString() ?? string.Empty;
                string tinhTrang = row.Cells["TinhTrang"]?.Value?.ToString() ?? string.Empty;

                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(maMon) || string.IsNullOrWhiteSpace(tenMon) || string.IsNullOrWhiteSpace(giaText))
                {
                    MessageBox.Show("Mã món, tên món, và giá không được để trống.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra giá phải là số
                if (!decimal.TryParse(giaText, out decimal gia))
                {
                    MessageBox.Show("Giá phải là số hợp lệ.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                UpdateMonAn(maMon, tenMon, gia.ToString(), moTa, tinhTrang);
            }
        }
        private void SetDataGridViewColumnWidth()
        {
            dgvSuaThucDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dgvSuaThucDon.Columns["MaMon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSuaThucDon.Columns["Gia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        

        private void dgvSuaThucDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemThucDon themThucDon = new frmThemThucDon();  
            themThucDon.Show();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }

        private void chkConHang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConHang.Checked)
            {
                chkHetHang.Checked = false;
            }
            if (chkConHang.Checked && dgvSuaThucDon.SelectedRows.Count > 0)
            {
                // Lấy mã món của hàng được chọn
                string maMon = dgvSuaThucDon.SelectedRows[0].Cells["MaMon"].Value.ToString();

                // Cập nhật tình trạng thành "Còn Hàng"
                UpdateTinhTrang(maMon, "Còn Hàng");

                // Bỏ chọn chkHetHang
                chkConHang.Checked = false;

                // Làm mới DataGridView
                LoadDataToDataGridView();
            }
        }

        private void chkHetHang_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHetHang.Checked)
            {
                chkConHang.Checked = false;
            }
            if (chkHetHang.Checked && dgvSuaThucDon.SelectedRows.Count > 0)
            {
                // Lấy mã món của hàng được chọn
                string maMon = dgvSuaThucDon.SelectedRows[0].Cells["MaMon"].Value.ToString();

                // Cập nhật tình trạng thành "Hết Hàng"
                UpdateTinhTrang(maMon, "Hết Hàng");

                // Bỏ chọn chkConHang
                chkConHang.Checked = false;

                // Làm mới DataGridView
                LoadDataToDataGridView();
               
                chkHetHang.Checked = false;
               
            }
        }

        private void UpdateTinhTrang(string maMon, string tinhTrang)
        {
            try
            {
                conn.Open();
                string updateQuery = "UPDATE MenuHaiSan SET TinhTrang = @TinhTrang WHERE MaMon = @MaMon";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                cmd.Parameters.AddWithValue("@MaMon", maMon);
                cmd.ExecuteNonQuery();
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

        private void dgvSuaThucDon_SelectionChanged(object sender, EventArgs e)
        {
          
        }
    }
}
