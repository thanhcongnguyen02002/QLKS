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
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            loadKhachHang();
        }
        private void loadKhachHang()
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
            sqlCommand.CommandText = "select * from tblKhachHang";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvKhachHang.DataSource = dataTable;

            // dgvThongTIn.Columns.Add("Mã Sinh Viên", 50);
            dgvKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên Khách Hàng";
            dgvKhachHang.Columns[2].HeaderText = "Ngày Sinh";
            dgvKhachHang.Columns[3].HeaderText = "Giới Tính";
            dgvKhachHang.Columns[4].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns[5].HeaderText = "Địa Chỉ";
            dgvKhachHang.Columns[6].HeaderText = "Số CCCD";

            dgvKhachHang.Refresh();
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
            if (cbGioiTinh.Text == ""||txtDiaChi.Text==""|| txtMaKhachHang.Text==""||txtSoCCCD.Text==""||txtSoDienThoai.Text==""
                || txtTenKhachHang.Text=="")
            {
                MessageBox.Show("Thông tin không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "insertKHACHHANG";
            sqlCommand.Parameters.AddWithValue("@sMaKH", txtMaKhachHang.Text);
            sqlCommand.Parameters.AddWithValue("@sTenKH", txtTenKhachHang.Text);
            sqlCommand.Parameters.AddWithValue("@dNgaySinh", dtNgaySinh.Text);
            sqlCommand.Parameters.AddWithValue("@sGioiTinh", cbGioiTinh.Text);
            sqlCommand.Parameters.AddWithValue("@sSoDienThoai", txtSoDienThoai.Text);
            sqlCommand.Parameters.AddWithValue("@sDiaChi", txtMaKhachHang.Text);
            sqlCommand.Parameters.AddWithValue("@sCMND", txtSoCCCD.Text);
            int i = sqlCommand.ExecuteNonQuery();

            KhachHang_Load(sender, e);
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlCommand.Dispose();

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
            if (cbGioiTinh.Text == "" || txtDiaChi.Text == "" || txtMaKhachHang.Text == "" || txtSoCCCD.Text == "" || txtSoDienThoai.Text == ""
               || txtTenKhachHang.Text == "")
            {
                MessageBox.Show("Thông tin không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "UPDATE_KHACHHANG";
            sqlCommand.Parameters.AddWithValue("@sMaKH", txtMaKhachHang.Text);
            sqlCommand.Parameters.AddWithValue("@sTenKH", txtTenKhachHang.Text);
            sqlCommand.Parameters.AddWithValue("@dNgaySinh", dtNgaySinh.Text);
            sqlCommand.Parameters.AddWithValue("@sGioiTinh", cbGioiTinh.Text);
            sqlCommand.Parameters.AddWithValue("@sSoDienThoai", txtSoDienThoai.Text);
            sqlCommand.Parameters.AddWithValue("@sDiaChi", txtMaKhachHang.Text);
            sqlCommand.Parameters.AddWithValue("@sCMND", txtSoCCCD.Text);
            int i = sqlCommand.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Thay đổi thành công ", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            KhachHang_Load(sender, e);
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
            if (txtMaKhachHang.Text == "")
            {
                MessageBox.Show("Mã Khách hàng không để trống", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "DELETE_KHACHHANG";
                sqlCommand.Parameters.AddWithValue("@sMaKH", txtMaKhachHang.Text);
                sqlCommand.ExecuteNonQuery();
                KhachHang_Load(sender, e);

            }

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}
