using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLQuanLyKhachSan
{
    public partial class DichVu : Form
    {
        public DichVu()
        {
            InitializeComponent();
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
            loadDichVu();
        }
        private void loadDichVu()
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
            sqlCommand.CommandText = "select * from tblDichVu";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvDichVu.DataSource = dataTable;
            dgvDichVu.Columns[0].HeaderText = "Mã Dịch Vụ";
            dgvDichVu.Columns[1].HeaderText = "Tên Dịch Vụ";
            dgvDichVu.Columns[2].HeaderText = "Đơn Giá";
            dgvDichVu.Refresh();
          //  txtBanGhi.Text = "";
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        private bool checkGia(string Gia)
        {
            string checkGia = "[^0-9]";
            return Regex.IsMatch(Gia, checkGia);
        }
        private bool checkTen(string Ten)
        {
            string cT = @"^[a-zA-ZÀ-ỹ\s]+$";
            return Regex.IsMatch(Ten, cT);
        }
        public bool check_MaHD()
        {
            string constr = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM tblDichVu WHERE sMaDV = @sMaDV", conn))
                {
                    checkCommand.Parameters.AddWithValue("@sMaDV", txtMaDV.Text);
                    int count = (int)checkCommand.ExecuteScalar();
                    return count > 0;
                }
            }

        }
        public bool check_Ten()
        {
            string constr = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM tblDichVu WHERE sTenDV = @sTenDV", conn))
                {
                    checkCommand.Parameters.AddWithValue("@sTenDV", txtTenDV.Text);
                    int count = (int)checkCommand.ExecuteScalar();
                    return count > 0;
                }
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
            if (txtMaDV.Text == "" || txtTenDV.Text == "" || txtDonGia.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (check_Ten())
            //if(!Regex.IsMatch(txtTenDV.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("Tên dịch vụ đã có ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (check_MaHD())
            {
                MessageBox.Show("Mã Dịch Vụ Đã Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if(Regex.IsMatch(txtDonGia.Text, @"^\d+(\.\d+)?$"))
            if (Regex.IsMatch(txtDonGia.Text, "[^0-9]") || int.Parse(txtDonGia.Text) >= 300000)
            // if (!checkGia(txtDonGia.Text) ) 
            {
                MessageBox.Show("Định dạng giá sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!checkTen(txtTenDV.Text))

            {
                MessageBox.Show("Định dạng tên sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "INSERT_DICHVU";
            sqlCommand.Parameters.AddWithValue("@MaDV", txtMaDV.Text);
            sqlCommand.Parameters.AddWithValue("@TenDV", txtTenDV.Text);
            sqlCommand.Parameters.AddWithValue("@fGiaDV", txtDonGia.Text);
            sqlCommand.ExecuteNonQuery();
            loadDichVu();
            txtTenDV.Text = "";
            txtMaDV.Text = "";
            txtDonGia.Text = "";
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
            if (txtMaDV.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!check_MaHD())
            {
                MessageBox.Show("Mã Dịch Vụ Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  if(Regex.IsMatch(txtDonGia.Text, @"^\d+(\.\d+)?$"))
            DialogResult result = MessageBox.Show("Bạn có muốn xóa dịch vụ này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "DELETE_DICHVU";
                sqlCommand.Parameters.AddWithValue("@MaDV", txtMaDV.Text);
                sqlCommand.ExecuteNonQuery();
                loadDichVu();
                MessageBox.Show("Xóa Dịch Vụ Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenDV.Text = "";
                txtMaDV.Text = "";
                txtDonGia.Text = "";


            }

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
            if (txtMaDV.Text == "" || txtTenDV.Text == "" || txtDonGia.Text == "")
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!check_MaHD())
            {
                MessageBox.Show("Mã Dịch không Vụ Đã Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if(Regex.IsMatch(txtDonGia.Text, @"^\d+(\.\d+)?$"))
            if (Regex.IsMatch(txtDonGia.Text, "[^0-9]") || int.Parse(txtDonGia.Text) >= 300000)
            // if (!checkGia(txtDonGia.Text) ) 
            {
                MessageBox.Show("Định dạng giá sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!checkTen(txtTenDV.Text))

            {
                MessageBox.Show("Định dạng tên sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "UPDATE_DICHVU";
            sqlCommand.Parameters.AddWithValue("@MaDV", txtMaDV.Text);
            sqlCommand.Parameters.AddWithValue("@TenDV", txtTenDV.Text);
            sqlCommand.Parameters.AddWithValue("@fGiaDV", txtDonGia.Text);
            sqlCommand.ExecuteNonQuery();
            txtTenDV.Text = "";
            txtMaDV.Text = "";
            txtDonGia.Text = "";
            loadDichVu();
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }

      /*  private void btnTim_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "SEARCH_DICHVU";
            sqlCommand.Parameters.AddWithValue("@sMaDV", string.IsNullOrEmpty(txtMaDV.Text) ? (object)DBNull.Value : txtMaDV.Text);
            sqlCommand.Parameters.AddWithValue("@TenDV", string.IsNullOrEmpty(txtTenDV.Text) ? (object)DBNull.Value : txtTenDV.Text);
            sqlCommand.Parameters.AddWithValue("@fGiaDV", string.IsNullOrEmpty(txtDonGia.Text) ? (object)DBNull.Value : txtDonGia.Text);


            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
            {
                dataAdapter.Fill(dataTable);
            }

            // Hiển thị kết quả tìm kiếm trên DataGridVie

            //dgvThongTin.AutoGenerateColumns = false;
            dgvDichVu.DataSource = dataTable;
           // DataView dataView = new DataView(dataTable);
           // int count = dataView.Count;
            //txtBanGhi.Text = "Số Bản Ghi Được Tìm Thấy là " + count.ToString();

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }*/

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvDichVu.CurrentRow.Index;
            txtMaDV.Text = dgvDichVu.Rows[i].Cells[0].Value.ToString().Trim();
            txtTenDV.Text = dgvDichVu.Rows[i].Cells[1].Value.ToString().Trim();
            txtDonGia.Text = dgvDichVu.Rows[i].Cells[2].Value.ToString().Trim();
            loadDichVu();
        }
    }
}
