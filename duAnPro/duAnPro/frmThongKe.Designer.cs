namespace duAnPro
{
    partial class frmThongKe
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThongKeKhachHang = new System.Windows.Forms.Button();
            this.btnThongKeMonAn = new System.Windows.Forms.Button();
            this.btnThongKeDoanhThu = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dgvThongKe = new System.Windows.Forms.DataGridView();
            this.btnThongKeHoaDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(542, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "Mốc Ngày Kết Thúc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Mốc Ngày Bắt Đầu";
            // 
            // btnThongKeKhachHang
            // 
            this.btnThongKeKhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeKhachHang.Location = new System.Drawing.Point(674, 562);
            this.btnThongKeKhachHang.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThongKeKhachHang.Name = "btnThongKeKhachHang";
            this.btnThongKeKhachHang.Size = new System.Drawing.Size(243, 68);
            this.btnThongKeKhachHang.TabIndex = 34;
            this.btnThongKeKhachHang.Text = "Thống kê khách hàng";
            this.btnThongKeKhachHang.UseVisualStyleBackColor = true;
            this.btnThongKeKhachHang.Click += new System.EventHandler(this.btnThongKeKhachHang_Click);
            // 
            // btnThongKeMonAn
            // 
            this.btnThongKeMonAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeMonAn.Location = new System.Drawing.Point(355, 562);
            this.btnThongKeMonAn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThongKeMonAn.Name = "btnThongKeMonAn";
            this.btnThongKeMonAn.Size = new System.Drawing.Size(282, 68);
            this.btnThongKeMonAn.TabIndex = 33;
            this.btnThongKeMonAn.Text = "Thống kê món ăn";
            this.btnThongKeMonAn.UseVisualStyleBackColor = true;
            this.btnThongKeMonAn.Click += new System.EventHandler(this.btnThongKeMonAn_Click);
            // 
            // btnThongKeDoanhThu
            // 
            this.btnThongKeDoanhThu.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThongKeDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeDoanhThu.Location = new System.Drawing.Point(55, 562);
            this.btnThongKeDoanhThu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThongKeDoanhThu.Name = "btnThongKeDoanhThu";
            this.btnThongKeDoanhThu.Size = new System.Drawing.Size(243, 68);
            this.btnThongKeDoanhThu.TabIndex = 32;
            this.btnThongKeDoanhThu.Text = "Thống kê doanh thu";
            this.btnThongKeDoanhThu.UseVisualStyleBackColor = true;
            this.btnThongKeDoanhThu.Click += new System.EventHandler(this.btnThongKeDoanhThu_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(735, 404);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(224, 26);
            this.dtpEndDate.TabIndex = 31;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(231, 404);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(224, 26);
            this.dtpStartDate.TabIndex = 30;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dgvThongKe
            // 
            this.dgvThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKe.Location = new System.Drawing.Point(55, 18);
            this.dgvThongKe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvThongKe.Name = "dgvThongKe";
            this.dgvThongKe.ReadOnly = true;
            this.dgvThongKe.RowHeadersWidth = 51;
            this.dgvThongKe.RowTemplate.Height = 24;
            this.dgvThongKe.Size = new System.Drawing.Size(1204, 362);
            this.dgvThongKe.TabIndex = 29;
            this.dgvThongKe.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvThongKe_CellFormatting);
            this.dgvThongKe.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvThongKe_RowHeaderMouseClick);
            // 
            // btnThongKeHoaDon
            // 
            this.btnThongKeHoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeHoaDon.Location = new System.Drawing.Point(961, 562);
            this.btnThongKeHoaDon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThongKeHoaDon.Name = "btnThongKeHoaDon";
            this.btnThongKeHoaDon.Size = new System.Drawing.Size(243, 68);
            this.btnThongKeHoaDon.TabIndex = 37;
            this.btnThongKeHoaDon.Text = "Thống kê hóa đơn";
            this.btnThongKeHoaDon.UseVisualStyleBackColor = true;
            this.btnThongKeHoaDon.Click += new System.EventHandler(this.btnThongKeHoaDon_Click);
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.CancelButton = this.btnThongKeDoanhThu;
            this.ClientSize = new System.Drawing.Size(1318, 648);
            this.Controls.Add(this.btnThongKeHoaDon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnThongKeKhachHang);
            this.Controls.Add(this.btnThongKeMonAn);
            this.Controls.Add(this.btnThongKeDoanhThu);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.dgvThongKe);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmThongKe";
            this.Text = "Thống kê";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThongKeKhachHang;
        private System.Windows.Forms.Button btnThongKeMonAn;
        private System.Windows.Forms.Button btnThongKeDoanhThu;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DataGridView dgvThongKe;
        private System.Windows.Forms.Button btnThongKeHoaDon;
    }
}