namespace BTLQuanLyKhachSan
{
    partial class BaoCaoKhachHang
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
            this.crvKhachHang = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvKhachHang
            // 
            this.crvKhachHang.ActiveViewIndex = -1;
            this.crvKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvKhachHang.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvKhachHang.Location = new System.Drawing.Point(0, 0);
            this.crvKhachHang.Name = "crvKhachHang";
            this.crvKhachHang.Size = new System.Drawing.Size(800, 450);
            this.crvKhachHang.TabIndex = 0;
            // 
            // BaoCaoKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvKhachHang);
            this.Name = "BaoCaoKhachHang";
            this.Text = "Báo Cáo Khách Hàng";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvKhachHang;
    }
}