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
    public partial class DatPhong : Form
    {
        string maPhong;
        public DatPhong( string maPhong)
        {
            InitializeComponent();
            this.maPhong = maPhong;
        }
        private void Load_comboboxMaKH()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            // b3 câu lệnh thực thi

            // Tạo câu truy vấn SQL để lấy danh sách mã sách
            string query = "SELECT sMaKH FROM tblKhachHang";

            // Tạo đối tượng SqlDataAdapter để thực thi câu truy vấn và lấy dữ liệu
            SqlDataAdapter da = new SqlDataAdapter(query, conn);

            // Tạo đối tượng DataTable để chứa dữ liệu
            DataTable dt = new DataTable();

            // Đổ dữ liệu từ SqlDataAdapter vào DataTable
            da.Fill(dt);


            // Gán danh sách mã sách cho thuộc tính DataSource của combobox
            cbMaKH.DataSource = dt;
            cbMaKH.DisplayMember = "sMaKH";
            cbMaKH.Text = "";
            lbCCCD.Text = "";
            lbDiaChi.Text = "";
            lbGioiTinh.Text = "";
            lbNgaySinh.Text = "";
            lbSDT.Text = "";
            lbTenKH.Text = "";

        }
        private void load_cbNV()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            // b3 câu lệnh thực thi

            // Tạo câu truy vấn SQL để lấy danh sách mã sách
            string query = "SELECT sMaNV FROM tblNhanVien";

            // Tạo đối tượng SqlDataAdapter để thực thi câu truy vấn và lấy dữ liệu
            SqlDataAdapter da = new SqlDataAdapter(query, conn);

            // Tạo đối tượng DataTable để chứa dữ liệu
            DataTable dt = new DataTable();

            // Đổ dữ liệu từ SqlDataAdapter vào DataTable
            da.Fill(dt);


            // Gán danh sách mã sách cho thuộc tính DataSource của combobox
            cbMaNV.DataSource = dt;
            cbMaNV.DisplayMember = "sMaNV";
            cbMaNV.Text = "";
            /*cbMaKH.Text = "";
            lbCCCD.Text = "";
            lbDiaChi.Text = "";
            lbGioiTinh.Text = "";
            lbNgaySinh.Text = "";
            lbSDT.Text = "";
            lbTenKH.Text = "";*/
        }
        private void select_maPhong()
        {
            lbMaP.Text = maPhong;
        }
        private void load_Phong()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            // Tạo câu truy vấn SQL để lấy thông tin đối tượng
            string query = "SELECT * FROM tblPhong where sMaP=@sMaP";
            // Tạo đối tượng SqlConnection và thiết lập kết nối đến CSDL
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                // Tạo đối tượng SqlDataAdapter và thiết lập giá trị tham số
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@sMaP", lbMaP.Text);

                    // Tạo đối tượng DataTable để lưu dữ liệu lấy về
                    DataTable dataTable = new DataTable();

                    // Sử dụng phương thức Fill() của đối tượng SqlDataAdapter để lấy dữ liệu và lưu vào DataTable
                    adapter.Fill(dataTable);

                    // Kiểm tra nếu có ít nhất một dòng dữ liệu trong DataTable, gán giá trị từng cột dữ liệu vào các label
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        lbLoaiPhong.Text = row["sLoaiPhong"].ToString();
                        lbSoGiuong.Text = row["iSoGiuong"].ToString();
                        lbDonGia.Text = row["fGiaP"].ToString();

                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Load_comboboxMaKH();
            load_cbNV();
            select_maPhong();
            load_Phong();
        }

        private void btxXacNhan_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            if (cbMaKH.Text == "" || cbMaNV.Text == "" || cbTreEm.Text == ""||txtMaDatPhong.Text=="")
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "INSERT_DATPHONG";
            // sqlCommand.CommandText = "UpdateTrangThaiPhong";
            sqlCommand.Parameters.AddWithValue("@sMaKH", cbMaKH.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", txtMaDatPhong.Text);
            sqlCommand.Parameters.AddWithValue("@sMaNV", cbMaNV.Text);
            sqlCommand.Parameters.AddWithValue("@dNgayDat", dNgayDat.Text);
            sqlCommand.Parameters.AddWithValue("@dNgayNhan", dNgayNhan.Text);
            sqlCommand.Parameters.AddWithValue("@dNgayTra", dNgayTra.Text);
            sqlCommand.Parameters.AddWithValue("@sNguoiDiCung", cbTreEm.Text);

            sqlCommand.Parameters.AddWithValue("@sMaP", lbMaP.Text);
            //  sqlCommand.Parameters.AddWithValue("@sTrangThai", "đã đặt");
            
            int i = sqlCommand.ExecuteNonQuery();
            if (i > 0)
            {
                updateTrangThaiPhong();
                DialogResult result = MessageBox.Show("Đặt Phòng Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               /* if (result == DialogResult.OK)
                {
                    this.Close();
                    DSPhong dSPhong = new DSPhong();
                    dSPhong.Show();
                }*/
            }
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();

            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
            DSPhong dSPhong =new DSPhong();
            dSPhong.Show();
        }
        private void updateTrangThaiPhong()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "UpdateTrangThaiPhong";
            sqlCommand.Parameters.AddWithValue("@sMaP", lbMaP.Text);
            sqlCommand.Parameters.AddWithValue("@sTrangThai", "đã đặt");
            int i = sqlCommand.ExecuteNonQuery();

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
        }

        private void cbMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            // b3 câu lệnh thực thi

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand;
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "select  * from tblKhachHang ";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);

            DataView dataView = new DataView(dataTable);
            int index = cbMaKH.SelectedIndex;
            lbTenKH.Text = dataView[index]["sTenKH"].ToString();
            lbSDT.Text = dataView[index]["sSoDienThoai"].ToString();
            lbNgaySinh.Text = dataView[index]["dNgaySinh"].ToString();
            lbCCCD.Text = dataView[index]["sCMND"].ToString();
            lbDiaChi.Text = dataView[index]["sDiaChi"].ToString();
            lbGioiTinh.Text = dataView[index]["sGioiTinh"].ToString();
        }

       /* private void btnHoaDon_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand;
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "HoaDonDatPhong";
           // sqlCommand.Parameters.AddWithValue("@maP", lbMaP.Text);
            sqlCommand.Parameters.AddWithValue("@MaDP", txtMaDatPhong.Text);
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            /* rptChiTietoaDon rptChiTietoaDon= new rptChiTietoaDon();
             rptChiTietoaDon.SetDataSource(dataTable);
             ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
             chiTietHoaDon.crytalReportHoaDon.ReportSource = rptChiTietoaDon;
             chiTietHoaDon.Show();
            rptHoaDonDatPhong rptHoaDonDatPhong = new rptHoaDonDatPhong();
            rptHoaDonDatPhong.SetDataSource(dataTable);
            reportHoaDonDatPhong  reportHoaDonDatPhong = new reportHoaDonDatPhong();
            reportHoaDonDatPhong.crvDatPhong.ReportSource = rptHoaDonDatPhong;
            reportHoaDonDatPhong.Show();
        }*/
    }
}
