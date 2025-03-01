namespace duAnPro
{
    partial class frmDonHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDonDaDat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtSdt = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnThemDonHang = new System.Windows.Forms.Button();
            this.dgvDanhSach = new System.Windows.Forms.DataGridView();
            this.chkDaThanhToan = new System.Windows.Forms.CheckBox();
            this.chkChuaThanhToan = new System.Windows.Forms.CheckBox();
            this.lblMaNhanVien = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenNhanVien = new System.Windows.Forms.TextBox();
            this.txtMaNhanVien = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDonDaDat
            // 
            this.btnDonDaDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonDaDat.Location = new System.Drawing.Point(268, 613);
            this.btnDonDaDat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDonDaDat.Name = "btnDonDaDat";
            this.btnDonDaDat.Size = new System.Drawing.Size(185, 50);
            this.btnDonDaDat.TabIndex = 1;
            this.btnDonDaDat.Text = "Đơn đã đặt";
            this.btnDonDaDat.UseVisualStyleBackColor = true;
            this.btnDonDaDat.Click += new System.EventHandler(this.btnDonDaDat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(153, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Họ tên khách hàng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số điện thoại:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(352, 103);
            this.txtTenKH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(346, 22);
            this.txtTenKH.TabIndex = 4;
            this.txtTenKH.TextChanged += new System.EventHandler(this.txtTenKH_TextChanged);
            // 
            // txtSdt
            // 
            this.txtSdt.Location = new System.Drawing.Point(352, 151);
            this.txtSdt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.Size = new System.Drawing.Size(346, 22);
            this.txtSdt.TabIndex = 5;
            this.txtSdt.TextChanged += new System.EventHandler(this.txtSdt_TextChanged);
            // 
            // txtTongTien
            // 
            this.txtTongTien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongTien.Location = new System.Drawing.Point(600, 617);
            this.txtTongTien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(243, 22);
            this.txtTongTien.TabIndex = 28;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTongTien.TextChanged += new System.EventHandler(this.txtTongTien_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(501, 613);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 23);
            this.label18.TabIndex = 27;
            this.label18.Text = "Tổng tiền";
            // 
            // btnThemDonHang
            // 
            this.btnThemDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDonHang.Location = new System.Drawing.Point(31, 613);
            this.btnThemDonHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThemDonHang.Name = "btnThemDonHang";
            this.btnThemDonHang.Size = new System.Drawing.Size(185, 50);
            this.btnThemDonHang.TabIndex = 29;
            this.btnThemDonHang.Text = "Thêm đơn hàng";
            this.btnThemDonHang.UseVisualStyleBackColor = true;
            this.btnThemDonHang.Click += new System.EventHandler(this.btnThemDonHang_Click);
            // 
            // dgvDanhSach
            // 
            this.dgvDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSach.Location = new System.Drawing.Point(12, 187);
            this.dgvDanhSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDanhSach.Name = "dgvDanhSach";
            this.dgvDanhSach.RowHeadersWidth = 62;
            this.dgvDanhSach.RowTemplate.Height = 28;
            this.dgvDanhSach.Size = new System.Drawing.Size(946, 411);
            this.dgvDanhSach.TabIndex = 30;
            this.dgvDanhSach.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgvDanhSach.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDanhSach_CellFormatting);
            this.dgvDanhSach.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSach_CellValueChanged);
            this.dgvDanhSach.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDanhSach_EditingControlShowing);
            this.dgvDanhSach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvDanhSach_KeyPress);
            // 
            // chkDaThanhToan
            // 
            this.chkDaThanhToan.AutoSize = true;
            this.chkDaThanhToan.Location = new System.Drawing.Point(600, 657);
            this.chkDaThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDaThanhToan.Name = "chkDaThanhToan";
            this.chkDaThanhToan.Size = new System.Drawing.Size(110, 20);
            this.chkDaThanhToan.TabIndex = 31;
            this.chkDaThanhToan.Text = "Đã thanh toán";
            this.chkDaThanhToan.UseVisualStyleBackColor = true;
            this.chkDaThanhToan.CheckedChanged += new System.EventHandler(this.chkDaThanhToan_CheckedChanged);
            // 
            // chkChuaThanhToan
            // 
            this.chkChuaThanhToan.AutoSize = true;
            this.chkChuaThanhToan.Location = new System.Drawing.Point(729, 657);
            this.chkChuaThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkChuaThanhToan.Name = "chkChuaThanhToan";
            this.chkChuaThanhToan.Size = new System.Drawing.Size(124, 20);
            this.chkChuaThanhToan.TabIndex = 32;
            this.chkChuaThanhToan.Text = "Chưa thanh toán";
            this.chkChuaThanhToan.UseVisualStyleBackColor = true;
            this.chkChuaThanhToan.CheckedChanged += new System.EventHandler(this.chkChuaThanhToan_CheckedChanged);
            // 
            // lblMaNhanVien
            // 
            this.lblMaNhanVien.AutoSize = true;
            this.lblMaNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNhanVien.Location = new System.Drawing.Point(696, 49);
            this.lblMaNhanVien.Name = "lblMaNhanVien";
            this.lblMaNhanVien.Size = new System.Drawing.Size(57, 16);
            this.lblMaNhanVien.TabIndex = 33;
            this.lblMaNhanVien.Text = "Mã NV:";
            this.lblMaNhanVien.Click += new System.EventHandler(this.lblMaNhanVien_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(683, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "Xin chào:";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // txtTenNhanVien
            // 
            this.txtTenNhanVien.Enabled = false;
            this.txtTenNhanVien.Location = new System.Drawing.Point(759, 6);
            this.txtTenNhanVien.Name = "txtTenNhanVien";
            this.txtTenNhanVien.ReadOnly = true;
            this.txtTenNhanVien.Size = new System.Drawing.Size(193, 22);
            this.txtTenNhanVien.TabIndex = 35;
            this.txtTenNhanVien.TextChanged += new System.EventHandler(this.txtTenNhanVien_TextChanged);
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.Enabled = false;
            this.txtMaNhanVien.Location = new System.Drawing.Point(759, 49);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.ReadOnly = true;
            this.txtMaNhanVien.Size = new System.Drawing.Size(193, 22);
            this.txtMaNhanVien.TabIndex = 36;
            this.txtMaNhanVien.TextChanged += new System.EventHandler(this.txtMaNhanVien_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(849, 619);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 37;
            this.label4.Text = "VND";
            // 
            // frmDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(964, 688);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaNhanVien);
            this.Controls.Add(this.txtTenNhanVien);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMaNhanVien);
            this.Controls.Add(this.chkChuaThanhToan);
            this.Controls.Add(this.chkDaThanhToan);
            this.Controls.Add(this.dgvDanhSach);
            this.Controls.Add(this.btnThemDonHang);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtSdt);
            this.Controls.Add(this.txtTenKH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDonDaDat);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDonHang";
            this.Text = "Rùa Seafoods";
            this.Load += new System.EventHandler(this.frmDonHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDonDaDat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnThemDonHang;
        private System.Windows.Forms.DataGridView dgvDanhSach;
        private System.Windows.Forms.CheckBox chkDaThanhToan;
        private System.Windows.Forms.CheckBox chkChuaThanhToan;
        private System.Windows.Forms.Label lblMaNhanVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenNhanVien;
        private System.Windows.Forms.TextBox txtMaNhanVien;
        private System.Windows.Forms.Label label4;
    }
}