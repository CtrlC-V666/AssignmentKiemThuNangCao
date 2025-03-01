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
    public partial class frmQuanLyNhanVien : Form
    {
        private DataTable dtNhanVien;
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QLNH.mdf;Integrated Security=True");
            dtNhanVien = new DataTable();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void dgvQuanLyNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvQuanLyNhanVien.SelectedRows.Count > 0)
            {
                // Lấy mã nhân viên của hàng được chọn
                string maNhanVien = dgvQuanLyNhanVien.SelectedRows[0].Cells["MaNhanVien"].Value.ToString();

                // Xác nhận xóa nhân viên
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Xóa nhân viên từ cơ sở dữ liệu
                    SqlTransaction transaction = null;
                    try
                    {
                        conn.Open();
                        // Bắt đầu giao dịch
                        transaction = conn.BeginTransaction();

                        // Xóa nhân viên từ bảng NhanVien
                        string deleteNhanVienQuery = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                        SqlCommand cmdNhanVien = new SqlCommand(deleteNhanVienQuery, conn, transaction);
                        cmdNhanVien.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        cmdNhanVien.ExecuteNonQuery();

                        // Xóa người dùng từ bảng NguoiDung
                        string deleteNguoiDungQuery = "DELETE FROM NguoiDung WHERE MaNhanVien = @MaNhanVien";
                        SqlCommand cmdNguoiDung = new SqlCommand(deleteNguoiDungQuery, conn, transaction);
                        cmdNguoiDung.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        cmdNguoiDung.ExecuteNonQuery();

                        // Commit giao dịch
                        transaction.Commit();

                        MessageBox.Show("Xóa nhân viên thành công.");
                        LoadDataToDataGridView(); // Làm mới DataGridView sau khi xóa thành công
                    }
                    catch (Exception ex)
                    {
                        // Rollback giao dịch nếu có lỗi
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }
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
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
            }
        }
        private void SetDataGridViewColumnWidth()
        {

            dgvQuanLyNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dgvQuanLyNhanVien.Columns["MaNhanVien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvQuanLyNhanVien.Columns["ChucVu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
            SetDataGridViewColumnWidth();
        }

        private void LoadDataToDataGridView()
        {
            string query = "SELECT MaNhanVien, Ten, ChucVu FROM NhanVien";
            adapter = new SqlDataAdapter(query, conn);
            dtNhanVien.Clear();
            adapter.Fill(dtNhanVien);

            dgvQuanLyNhanVien.DataSource = null;

            dgvQuanLyNhanVien.DataSource = dtNhanVien;

            dgvQuanLyNhanVien.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvQuanLyNhanVien.Columns["Ten"].HeaderText = "Tên nhân viên";
            dgvQuanLyNhanVien.Columns["ChucVu"].HeaderText = "Chức vụ";
        }
    }
}
