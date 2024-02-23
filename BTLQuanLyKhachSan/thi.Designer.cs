namespace BTLQuanLyKhachSan
{
    partial class thi
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
            this.dgvThi = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThi)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvThi
            // 
            this.dgvThi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThi.Location = new System.Drawing.Point(48, 53);
            this.dgvThi.Name = "dgvThi";
            this.dgvThi.RowHeadersWidth = 51;
            this.dgvThi.RowTemplate.Height = 24;
            this.dgvThi.Size = new System.Drawing.Size(788, 331);
            this.dgvThi.TabIndex = 0;
            this.dgvThi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThi_CellClick);
            this.dgvThi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThi_CellContentClick);
            // 
            // thi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 450);
            this.Controls.Add(this.dgvThi);
            this.Name = "thi";
            this.Text = "thi";
            this.Load += new System.EventHandler(this.thi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvThi;
    }
}