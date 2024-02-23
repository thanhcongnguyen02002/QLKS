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
    public partial class HoaDonDichVu : Form
    {
        public HoaDonDichVu()
        {
            InitializeComponent();
        }

        private void HoaDonDichVu_Load(object sender, EventArgs e)
        {
            cbMaDV.Text = "";
            loadComboBox();
            loadHoaDonDichVu();
        }
        private void loadComboBox()
        {

            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection conn2 = new SqlConnection(conn);
            // Tạo câu truy vấn SQL để lấy danh sách mã sách
            string query = "SELECT sTenDV FROM tblDichVu";

            // Tạo đối tượng SqlDataAdapter để thực thi câu truy vấn và lấy dữ liệu
            SqlDataAdapter da = new SqlDataAdapter(query, conn2);

            // Tạo đối tượng DataTable để chứa dữ liệu
            DataTable dt = new DataTable();

            // Đổ dữ liệu từ SqlDataAdapter vào DataTable
            da.Fill(dt);


            // Gán danh sách mã sách cho thuộc tính DataSource của combobox
            cbMaDV.DataSource = dt;
            cbMaDV.DisplayMember = "sTenDV";
            cbMaDV.Text = "";
            txtMaDP.Text = "";
            txtMaHD.Text = "";
            txtMaNV.Text = "";
            txtSoLanSD.Text = "";


        }
        private void loadHoaDonDichVu()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";;
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand;
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "select * from tblHoaDonDV";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvHoaDonDV.DataSource = dataTable;


            dgvHoaDonDV.Columns[0].HeaderText = "Mã Hóa Đơn";
            dgvHoaDonDV.Columns[1].HeaderText = "Mã Phòng Đặt";
            dgvHoaDonDV.Columns[2].HeaderText = "Mã Nhân Viên";
            dgvHoaDonDV.Columns[3].HeaderText = "Mã Dịch Vụ";
            dgvHoaDonDV.Columns[4].HeaderText = "Số Lần Sử Dụng";
            cbMaDV.Text = "";
            txtMaDP.Text = "";
            txtMaHD.Text = "";
            txtMaNV.Text = "";
            txtSoLanSD.Text = "";

            dgvHoaDonDV.Refresh();
            // đóng kết nối
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlDataAdapter.Dispose();
            }
        }
        public bool check_MaDP()
        {
            string constr = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM tblDatPhong WHERE sMaDatPhong = @sMaDatPhong", conn))
                {
                    checkCommand.Parameters.AddWithValue("@sMaDatPhong", txtMaDP.Text);
                    int count = (int)checkCommand.ExecuteScalar();
                    return count > 0;
                }
            }

        }
        public bool check_MaNV()
        {
            string constr = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM tblNhanVien WHERE SMaNV = @sMaNV", conn))
                {
                    checkCommand.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
                    int count = (int)checkCommand.ExecuteScalar();
                    return count > 0;
                }
            }

        }
        public bool check_MaHD()
        {
            string constr = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM tblHoaDonDV WHERE sMaHD = @sMaHD", conn))
                {
                    checkCommand.Parameters.AddWithValue("@sMaHD", txtMaHD.Text);
                    int count = (int)checkCommand.ExecuteScalar();
                    return count > 0;
                }
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";;
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            if (txtSoLanSD.Text == "" || txtMaNV.Text == "" || txtMaHD.Text == "" || txtMaDP.Text == "" || cbMaDV.Text == "")
            {
                MessageBox.Show("Vui Lòng Điền Đầy Đủ Thông Tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(txtSoLanSD.Text, "^[1-9]*$"))
            {
                MessageBox.Show("Số lần sử dụng chỉ chứa số là lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!check_MaDP())
            {
                MessageBox.Show("Mã Đặt Phòng Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!check_MaNV())
            {
                MessageBox.Show("Mã Nhân Viên Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (check_MaHD())
            {
                MessageBox.Show("Mã Hóa Đơn Đã Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "INSERT_HOADONDICHVU";
            sqlCommand.Parameters.AddWithValue("@sMaHD", txtMaHD.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDV", cbMaDV.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", txtMaDP.Text);
            sqlCommand.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
            sqlCommand.Parameters.AddWithValue("@iSoLanSuDungDV", txtSoLanSD.Text);
            int i = sqlCommand.ExecuteNonQuery();
            loadHoaDonDichVu();
            if (i > 0)
            {
                MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";;
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            if (txtSoLanSD.Text == "" || txtMaNV.Text == "" || txtMaHD.Text == "" || txtMaDP.Text == "" || cbMaDV.Text == "")
            {
                MessageBox.Show("Vui Lòng Điền Đầy Đủ Thông Tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(txtSoLanSD.Text, "^[1-9]*$"))
            {
                MessageBox.Show("Số lần sử dụng chỉ chứa số là lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!check_MaDP())
            {
                MessageBox.Show("Mã Đặt Phòng Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!check_MaNV())
            {
                MessageBox.Show("Mã Nhân Viên Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!check_MaHD())
            {
                MessageBox.Show("Mã Hóa Đơn Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "UPDATE_HOADONDICHVU";
            sqlCommand.Parameters.AddWithValue("@sMaHD", txtMaHD.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDV", cbMaDV.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", txtMaDP.Text);
            sqlCommand.Parameters.AddWithValue("@sMaNV", txtMaNV.Text);
            sqlCommand.Parameters.AddWithValue("@iSoLanSuDungDV", txtSoLanSD.Text);
            int i = sqlCommand.ExecuteNonQuery();
            loadHoaDonDichVu();
            if (i > 0)
            {
                MessageBox.Show("Sửa Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Vui Lòng Điền Mã Hóa Đơn Cần Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (!check_MaHD())
            {
                MessageBox.Show("Mã Hóa Đơn Không Tồn Tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "DELETE_HOADONDICHVU";
            sqlCommand.Parameters.AddWithValue("@sMaHD", txtMaHD.Text);

            int i = sqlCommand.ExecuteNonQuery();
            loadHoaDonDichVu();
            if (i > 0)
            {
                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

       /* private void btnTim_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "SEARCH_HOADONDV";
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", string.IsNullOrEmpty(txtMaDP.Text) ? (object)DBNull.Value : txtMaDP.Text);
            sqlCommand.Parameters.AddWithValue("@sMaHD", string.IsNullOrEmpty(txtMaHD.Text) ? (object)DBNull.Value : txtMaHD.Text);
            sqlCommand.Parameters.AddWithValue("@sMaNV", string.IsNullOrEmpty(txtMaNV.Text) ? (object)DBNull.Value : txtMaNV.Text);
            sqlCommand.Parameters.AddWithValue("@iSoLanSuDungDv", string.IsNullOrEmpty(txtSoLanSD.Text) ? (object)DBNull.Value : txtSoLanSD.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDV", string.IsNullOrEmpty(cbMaDV.Text) ? (object)DBNull.Value : cbMaDV.Text);


            DataTable dataTable = new DataTable();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
            {
                dataAdapter.Fill(dataTable);
            }

            // Hiển thị kết quả tìm kiếm trên DataGridVie

            //dgvThongTin.AutoGenerateColumns = false;
            dgvHoaDonDV.DataSource = dataTable;
            DataView dataView = new DataView(dataTable);
            int count = dataView.Count;
            dgvHoaDonDV.Text = "số bản ghi tìm được là " + count;
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }*/
    }
}
