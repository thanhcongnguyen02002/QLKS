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
    public partial class Phong : Form
    {
        public Phong()
        {
            InitializeComponent();
        }

        private void Phong_Load(object sender, EventArgs e)
        {
            loadPhong();
        }
        private void loadPhong()
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
            sqlCommand.CommandText = "select * from tblPhong";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvPhong.DataSource = dataTable;


            dgvPhong.Columns[0].HeaderText = "Mã Phòng";
            dgvPhong.Columns[1].HeaderText = "Loại Phòng";
            dgvPhong.Columns[2].HeaderText = "Giá Phòng";
            dgvPhong.Columns[3].HeaderText = "Số Giường";
            dgvPhong.Columns[4].HeaderText = "Trạng Thái";

            dgvPhong.Refresh();
            // đóng kết nối
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlDataAdapter.Dispose();
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "INSERT_PHONG";
            sqlCommand.Parameters.AddWithValue("@sMaP", txtMaP.Text);
            sqlCommand.Parameters.AddWithValue("@sLoaiPhong", txtLoaiP.Text);
            sqlCommand.Parameters.AddWithValue("@fGiaP", txtGiaP.Text);
            sqlCommand.Parameters.AddWithValue("@iSoGiuong", txtSoGiuong.Text);
            sqlCommand.Parameters.AddWithValue("@sTrangThai", txtTrangThai.Text);
            sqlCommand.ExecuteNonQuery();
            loadPhong();
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "UPDATE_PHONG";
            sqlCommand.Parameters.AddWithValue("@sMaP", txtMaP.Text);
            sqlCommand.Parameters.AddWithValue("@sLoaiPhong", txtLoaiP.Text);
            sqlCommand.Parameters.AddWithValue("@fGiaP", txtGiaP.Text);
            sqlCommand.Parameters.AddWithValue("@iSoGiuong", txtSoGiuong.Text);
            sqlCommand.Parameters.AddWithValue("@sTrangThai", txtTrangThai.Text);
            sqlCommand.ExecuteNonQuery();
            loadPhong();
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "DELETE_PHONG";
            sqlCommand.Parameters.AddWithValue("@sMaP", txtMaP.Text);
            sqlCommand.ExecuteNonQuery();
            loadPhong();
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
           

           // string connString = "Data Source=your_server_name;Initial Catalog=your_database_name;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("spTimKiemPhong", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Set parameter values
            cmd.Parameters.AddWithValue("@sMaP", string.IsNullOrEmpty(txtMaP.Text) ? (object)DBNull.Value : txtMaP.Text);
            cmd.Parameters.AddWithValue("@sLoaiPhong", string.IsNullOrEmpty(txtLoaiP.Text) ? (object)DBNull.Value : txtLoaiP.Text);
            cmd.Parameters.AddWithValue("@fGiaP", string.IsNullOrEmpty(txtGiaP.Text) ? (object)DBNull.Value : float.Parse(txtGiaP.Text));
            cmd.Parameters.AddWithValue("@iSoGiuong", string.IsNullOrEmpty(txtSoGiuong.Text) ? (object)DBNull.Value : int.Parse(txtSoGiuong.Text));
           // cmd.Parameters.AddWithValue("@sTrangThai", string.IsNullOrEmpty(txtTrangThai.Text) ? (object)DBNull.Value : txtTrangThai.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvPhong.DataSource = table;

        }
    }
}
