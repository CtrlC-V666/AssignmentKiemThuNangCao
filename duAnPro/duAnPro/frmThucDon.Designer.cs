namespace duAnPro
{
    partial class frmThucDon
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvSuaThucDon = new System.Windows.Forms.DataGridView();
            this.chkConHang = new System.Windows.Forms.CheckBox();
            this.chkHetHang = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuaThucDon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLamMoi);
            this.groupBox3.Controls.Add(this.btnXoa);
            this.groupBox3.Controls.Add(this.btnSua);
            this.groupBox3.Controls.Add(this.btnThem);
            this.groupBox3.Location = new System.Drawing.Point(108, 538);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(682, 106);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Công cụ";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnLamMoi.Location = new System.Drawing.Point(567, 26);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(101, 64);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnXoa.Location = new System.Drawing.Point(395, 26);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(101, 64);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSua.Location = new System.Drawing.Point(223, 26);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(101, 64);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnThem.Location = new System.Drawing.Point(56, 26);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(101, 64);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvSuaThucDon
            // 
            this.dgvSuaThucDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuaThucDon.Location = new System.Drawing.Point(-1, -1);
            this.dgvSuaThucDon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSuaThucDon.Name = "dgvSuaThucDon";
            this.dgvSuaThucDon.RowHeadersWidth = 51;
            this.dgvSuaThucDon.RowTemplate.Height = 24;
            this.dgvSuaThucDon.Size = new System.Drawing.Size(1132, 531);
            this.dgvSuaThucDon.TabIndex = 9;
            this.dgvSuaThucDon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSuaThucDon_CellContentClick);
            this.dgvSuaThucDon.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSuaThucDon_CellEndEdit);
            this.dgvSuaThucDon.SelectionChanged += new System.EventHandler(this.dgvSuaThucDon_SelectionChanged);
            // 
            // chkConHang
            // 
            this.chkConHang.AutoSize = true;
            this.chkConHang.Location = new System.Drawing.Point(863, 585);
            this.chkConHang.Name = "chkConHang";
            this.chkConHang.Size = new System.Drawing.Size(104, 24);
            this.chkConHang.TabIndex = 12;
            this.chkConHang.Text = "Còn hàng";
            this.chkConHang.UseVisualStyleBackColor = true;
            this.chkConHang.CheckedChanged += new System.EventHandler(this.chkConHang_CheckedChanged);
            // 
            // chkHetHang
            // 
            this.chkHetHang.AutoSize = true;
            this.chkHetHang.Location = new System.Drawing.Point(982, 585);
            this.chkHetHang.Name = "chkHetHang";
            this.chkHetHang.Size = new System.Drawing.Size(101, 24);
            this.chkHetHang.TabIndex = 13;
            this.chkHetHang.Text = "Hết hàng";
            this.chkHetHang.UseVisualStyleBackColor = true;
            this.chkHetHang.CheckedChanged += new System.EventHandler(this.chkHetHang_CheckedChanged);
            // 
            // frmThucDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(1131, 661);
            this.Controls.Add(this.chkHetHang);
            this.Controls.Add(this.chkConHang);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvSuaThucDon);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmThucDon";
            this.Text = "Quản Lý Thực Đơn";
            this.Load += new System.EventHandler(this.frmThucDon_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuaThucDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvSuaThucDon;
        private System.Windows.Forms.CheckBox chkConHang;
        private System.Windows.Forms.CheckBox chkHetHang;
    }
}