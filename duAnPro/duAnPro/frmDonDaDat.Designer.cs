namespace duAnPro
{
    partial class frmDonDaDat
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.dgvDanhSachDon = new System.Windows.Forms.DataGridView();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.dgvDonDaThanhToan = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnXemChiTiet2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonDaThanhToan)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(660, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Danh sách đơn đã đặt";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(1520, 682);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(147, 55);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(33, 690);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(147, 38);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // dgvDanhSachDon
            // 
            this.dgvDanhSachDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDanhSachDon.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvDanhSachDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachDon.Location = new System.Drawing.Point(12, 114);
            this.dgvDanhSachDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDanhSachDon.Name = "dgvDanhSachDon";
            this.dgvDanhSachDon.RowHeadersWidth = 62;
            this.dgvDanhSachDon.RowTemplate.Height = 28;
            this.dgvDanhSachDon.Size = new System.Drawing.Size(831, 553);
            this.dgvDanhSachDon.TabIndex = 6;
            this.dgvDanhSachDon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachDon_CellContentClick);
            this.dgvDanhSachDon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDanhSachDon_CellFormatting);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemChiTiet.Location = new System.Drawing.Point(369, 682);
            this.btnXemChiTiet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(357, 55);
            this.btnXemChiTiet.TabIndex = 11;
            this.btnXemChiTiet.Text = "Xem đơn chưa thanh toán ";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // dgvDonDaThanhToan
            // 
            this.dgvDonDaThanhToan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDonDaThanhToan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvDonDaThanhToan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonDaThanhToan.Location = new System.Drawing.Point(849, 114);
            this.dgvDonDaThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDonDaThanhToan.Name = "dgvDonDaThanhToan";
            this.dgvDonDaThanhToan.RowHeadersWidth = 62;
            this.dgvDonDaThanhToan.RowTemplate.Height = 28;
            this.dgvDonDaThanhToan.Size = new System.Drawing.Size(843, 553);
            this.dgvDonDaThanhToan.TabIndex = 12;
            this.dgvDonDaThanhToan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDonDaThanhToan_CellContentClick);
            this.dgvDonDaThanhToan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDonDaThanhToan_CellFormatting);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(887, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "Đơn đã thanh toán";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Đơn chưa thanh toán";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnXemChiTiet2
            // 
            this.btnXemChiTiet2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemChiTiet2.Location = new System.Drawing.Point(1001, 682);
            this.btnXemChiTiet2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXemChiTiet2.Name = "btnXemChiTiet2";
            this.btnXemChiTiet2.Size = new System.Drawing.Size(354, 55);
            this.btnXemChiTiet2.TabIndex = 15;
            this.btnXemChiTiet2.Text = "Xem đơn đã thanh toán";
            this.btnXemChiTiet2.UseVisualStyleBackColor = true;
            this.btnXemChiTiet2.Click += new System.EventHandler(this.btnXemChiTiet2_Click);
            // 
            // frmDonDaDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightSalmon;
            this.ClientSize = new System.Drawing.Size(1702, 762);
            this.Controls.Add(this.btnXemChiTiet2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvDonDaThanhToan);
            this.Controls.Add(this.btnXemChiTiet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.dgvDanhSachDon);
            this.Name = "frmDonDaDat";
            this.Text = "Rùa Sea Foods";
            this.Load += new System.EventHandler(this.frmDonDaDat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonDaThanhToan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.DataGridView dgvDanhSachDon;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.DataGridView dgvDonDaThanhToan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnXemChiTiet2;
    }
}