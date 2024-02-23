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
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            loadNhanVien();

        }
        private void loadNhanVien()
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
            sqlCommand.CommandText = "select * from SELECTNHANVIEN";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvNhanVien.DataSource = dataTable;

            // dgvThongTIn.Columns.Add("Mã Sinh Viên", 50);
            dgvNhanVien.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên Nhân Viên";
            dgvNhanVien.Columns[2].HeaderText = "Ngày Sinh";
            dgvNhanVien.Columns[3].HeaderText = "Giới Tính";
            dgvNhanVien.Columns[4].HeaderText = "Số Điện Thoại";
            dgvNhanVien.Columns[5].HeaderText = "Lương";
            dgvNhanVien.Columns[6].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns[7].HeaderText = "Bộ Phận";
            dgvNhanVien.Refresh();
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
            if(txtBoPhan.Text==""||txtDiaChi.Text==""||txtGioiTinh.Text==""||txtHSL.Text==""||txtMaNV.Text==""
                || txtSDT.Text == "" || txtTenNV.Text == "")
            {
                MessageBox.Show("Thông tin không được để trống","thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (float.Parse(txtHSL.Text) < 2.0 || float.Parse(txtHSL.Text) > 10.0)
            {
                MessageBox.Show("hê số lương sai");
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "insertNHANVIEN";
            sqlCommand.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
            sqlCommand.Parameters.AddWithValue("@sTenNV", txtTenNV.Text);
            sqlCommand.Parameters.AddWithValue("@dNgaySinh", dtNgaySinh.Text);
            sqlCommand.Parameters.AddWithValue("@sGioiTinh", txtGioiTinh.Text);
            sqlCommand.Parameters.AddWithValue("@sSoDienThoai", txtSDT.Text);
            sqlCommand.Parameters.AddWithValue("@fHSL", txtHSL.Text);
            sqlCommand.Parameters.AddWithValue("@sDiaChi", txtDiaChi.Text);
            sqlCommand.Parameters.AddWithValue("@sBoPhan", txtDiaChi.Text);
            sqlCommand.ExecuteNonQuery();
            loadNhanVien();
            if (sqlConnection.State == ConnectionState.Open)
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
            sqlCommand.CommandText = "UPDATE_NHANVIEN";
            sqlCommand.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
            sqlCommand.Parameters.AddWithValue("@sTenNV", txtTenNV.Text);
            sqlCommand.Parameters.AddWithValue("@dNgaySinh", dtNgaySinh.Text);
            sqlCommand.Parameters.AddWithValue("@sGioiTinh", txtGioiTinh.Text);
            sqlCommand.Parameters.AddWithValue("@sSoDienThoai", txtSDT.Text);
            sqlCommand.Parameters.AddWithValue("@fHSL", txtHSL.Text);
            sqlCommand.Parameters.AddWithValue("@sDiaChi", txtDiaChi.Text);
            sqlCommand.Parameters.AddWithValue("@sBoPhan", txtDiaChi.Text);
            sqlCommand.ExecuteNonQuery();
            loadNhanVien();
            if (sqlConnection.State == ConnectionState.Open)
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
            sqlCommand.CommandText = "DELETE_NHANVIEN";
            sqlCommand.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
            sqlCommand.ExecuteNonQuery();
            loadNhanVien();
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            //SqlCommand command = new SqlCommand("spTimKiemNhanVien", connection);
            command.CommandText = "spTimKiemNhanVien1";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sMaNV", string.IsNullOrEmpty(txtMaNV.Text) ? (object)DBNull.Value : txtMaNV.Text);
            command.Parameters.AddWithValue("@sTenNV", string.IsNullOrEmpty(txtTenNV.Text) ? (object)DBNull.Value : txtTenNV.Text);
          //  command.Parameters.AddWithValue("@dNgaySinh", string.IsNullOrEmpty(dtNgaySinh.Text) ? (object)DBNull.Value : DateTime.Parse(dtNgaySinh.Text));
            command.Parameters.AddWithValue("@sGioiTinh", string.IsNullOrEmpty(txtGioiTinh.Text) ? (object)DBNull.Value : txtGioiTinh.Text);
            command.Parameters.AddWithValue("@sSoDienThoai", string.IsNullOrEmpty(txtSDT.Text) ? (object)DBNull.Value : txtHSL.Text);
            command.Parameters.AddWithValue("@fLuong", string.IsNullOrEmpty(txtHSL.Text) ? (object)DBNull.Value : float.Parse(txtHSL.Text));
            command.Parameters.AddWithValue("@sDiaChi", string.IsNullOrEmpty(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text);
            command.Parameters.AddWithValue("@sBoPhan", string.IsNullOrEmpty(txtBoPhan.Text) ? (object)DBNull.Value : txtBoPhan.Text);
            DataTable dataTable = new DataTable();
            
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                dataAdapter.Fill(dataTable);
            }

            // Hiển thị kết quả tìm kiếm trên DataGridView
            dgvNhanVien.DataSource = dataTable;

        }
    }
}
