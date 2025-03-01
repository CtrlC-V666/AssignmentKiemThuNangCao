namespace duAnPro
{
    partial class frmThemThucDon
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
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.chkConHang = new System.Windows.Forms.CheckBox();
            this.chkHetHang = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên món";
            // 
            // txtTenMon
            // 
            this.txtTenMon.Location = new System.Drawing.Point(178, 41);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(537, 22);
            this.txtTenMon.TabIndex = 1;
            this.txtTenMon.TextChanged += new System.EventHandler(this.txtTenMon_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(178, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(537, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Thêm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Giá";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mô tả";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(60, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tình trạng";
            // 
            // txtGia
            // 
            this.txtGia.Location = new System.Drawing.Point(178, 83);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(537, 22);
            this.txtGia.TabIndex = 9;
            this.txtGia.TextChanged += new System.EventHandler(this.txtGia_TextChanged);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(178, 130);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(537, 22);
            this.txtMoTa.TabIndex = 10;
            this.txtMoTa.TextChanged += new System.EventHandler(this.txtMoTa_TextChanged);
            // 
            // chkConHang
            // 
            this.chkConHang.AutoSize = true;
            this.chkConHang.Checked = true;
            this.chkConHang.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConHang.Location = new System.Drawing.Point(304, 181);
            this.chkConHang.Name = "chkConHang";
            this.chkConHang.Size = new System.Drawing.Size(86, 20);
            this.chkConHang.TabIndex = 12;
            this.chkConHang.Text = "Còn hàng";
            this.chkConHang.UseVisualStyleBackColor = true;
            this.chkConHang.CheckedChanged += new System.EventHandler(this.chkConHang_CheckedChanged);
            // 
            // chkHetHang
            // 
            this.chkHetHang.AutoSize = true;
            this.chkHetHang.Location = new System.Drawing.Point(457, 181);
            this.chkHetHang.Name = "chkHetHang";
            this.chkHetHang.Size = new System.Drawing.Size(83, 20);
            this.chkHetHang.TabIndex = 13;
            this.chkHetHang.Text = "Hết hàng";
            this.chkHetHang.UseVisualStyleBackColor = true;
            this.chkHetHang.CheckedChanged += new System.EventHandler(this.chkHetHang_CheckedChanged);
            // 
            // frmThemThucDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(808, 311);
            this.Controls.Add(this.chkHetHang);
            this.Controls.Add(this.chkConHang);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.txtGia);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTenMon);
            this.Controls.Add(this.label1);
            this.Name = "frmThemThucDon";
            this.Text = "Thêm thực đơn";
            this.Load += new System.EventHandler(this.frmThemThucDon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.CheckBox chkConHang;
        private System.Windows.Forms.CheckBox chkHetHang;
    }
}