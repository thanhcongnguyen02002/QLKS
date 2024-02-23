namespace BTLQuanLyKhachSan
{
    partial class baocaonhanvien
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
            this.crvNhanVien = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvNhanVien
            // 
            this.crvNhanVien.ActiveViewIndex = -1;
            this.crvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvNhanVien.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvNhanVien.Location = new System.Drawing.Point(0, 0);
            this.crvNhanVien.Name = "crvNhanVien";
            this.crvNhanVien.Size = new System.Drawing.Size(800, 450);
            this.crvNhanVien.TabIndex = 0;
            // 
            // baocaonhanvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvNhanVien);
            this.Name = "baocaonhanvien";
            this.Text = "Báo Cáo Nhân Viên";
            this.Load += new System.EventHandler(this.baocaonhanvien_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvNhanVien;
    }
}