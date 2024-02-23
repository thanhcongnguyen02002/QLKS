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

namespace BTLQuanLyKhachSan
{
    public partial class thi : Form
    {
        public thi()
        {
            InitializeComponent();
        }

        private void thi_Load(object sender, EventArgs e)
        {
            
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand;
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "select * from cau1thi";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvThi.DataSource = dataTable;

            // dgvThongTIn.Columns.Add("Mã Sinh Viên", 50);
            dgvThi.Columns[0].HeaderText = "Mã Khách Hàng";
            dgvThi.Columns[0].Name = "MaPhong";
            dgvThi.Columns[1].HeaderText = "Tên Khách Hàng";
            dgvThi.Columns[2].HeaderText = "Giới Tính";
            dgvThi.Columns[3].HeaderText = "Ngày Sinh";
            dgvThi.Columns[4].HeaderText = "Ngày đặt ";
            dgvThi.Columns[5].HeaderText = "Ngày Trả";
            dgvThi.Columns[6].HeaderText = "số lượng";
            AddButtons();
            dgvThi.Columns[7].HeaderText = "";

            dgvThi.Refresh();
            // đóng kết nối
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlDataAdapter.Dispose();
            }
            
        }
        private void AddButtons()
        {
            // Thêm cột "Đặt phòng"
            DataGridViewButtonColumn bookColumn = new DataGridViewButtonColumn();
            bookColumn.HeaderText = "";
            bookColumn.Text = "xóa";
            bookColumn.UseColumnTextForButtonValue = true;
            bookColumn.Name = "btnxoa";

            dgvThi.Columns.Add(bookColumn);
        }

            private void dgvThi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dgvThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
           // string maPhong = dgvThi.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            string maPhong = dgvThi.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();

            
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand;
            sqlCommand = sqlConnection.CreateCommand();
             sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "thicau3";
            sqlCommand.Parameters.AddWithValue("makh", maPhong);

            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);

            reporthi rpt = new reporthi();
            rpt.SetDataSource(dataTable);
            Form1 baoCaoKhachHang = new Form1();
            baoCaoKhachHang.crystalReportViewer1.ReportSource = rpt;
            baoCaoKhachHang.Show();
        }
    }
}
