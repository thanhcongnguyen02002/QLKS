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
    public partial class TraPhong : Form
    {
        string maPhong, maDatPhong;
        public TraPhong(string maPhong, string maDatPhong)
        {
            InitializeComponent();
            this.maPhong = maPhong;
            this.maDatPhong = maDatPhong;
        }

        private void TraPhong_Load(object sender, EventArgs e)
        {
            lbMaP.Text = maPhong;
            lbMaDatPhong.Text = maDatPhong;
            load_ThongTin();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            xoaHoaDon();
            xoaDatPhong();
            update_trangthaiphong();

            DialogResult result = MessageBox.Show("Trả Phòng Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.Close();
                DSPhong dSPhong = new DSPhong();
                dSPhong.Show();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
            DSPhong dSPhong = new DSPhong();
            dSPhong.Show();
        }
        private void load_ThongTin()
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
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "select_tonghop_HoaDon2";
            sqlCommand.Parameters.AddWithValue("@maP", lbMaP.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", lbMaDatPhong.Text);
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                lbCCCD.Text = row["sCMND"].ToString();
                lbDiaChi.Text = row["sDiaChi"].ToString();
                lbGiaP.Text = row["fGiaP"].ToString();
                lbLoaiP.Text = row["sLoaiPhong"].ToString();
                lbNgayNhan.Text = row["dNgayNhan"].ToString();
                lbNgayTra.Text = row["dNgayTra"].ToString();
                lbSDT.Text = row["sSoDienThoai"].ToString();
                lbSoGiuong.Text = row["iSoGiuong"].ToString();
                lbTenKH.Text = row["sTenKH"].ToString();
                lbTreem.Text = row["sNguoiDiCung"].ToString();


            }
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        private void update_trangthaiphong()
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
            sqlCommand.Parameters.AddWithValue("@sTrangThai", "Trống");
            int i = sqlCommand.ExecuteNonQuery();

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();

            }

        }
        private void xoaHoaDon()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "xoa_HOADONDICHVU";
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", lbMaDatPhong.Text);

            int i = sqlCommand.ExecuteNonQuery();

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
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
            sqlCommand.CommandText = "SP_TT_HOADON2";
            sqlCommand.Parameters.AddWithValue("@maP", lbMaP.Text);
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", lbMaDatPhong.Text);
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
           
           rptHoaDonTraPhong rptChiTietoaDon = new rptHoaDonTraPhong();
            rptChiTietoaDon.SetDataSource(dataTable);
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.crvHoaDon.ReportSource = rptChiTietoaDon;
            chiTietHoaDon.Show();*/
        }

       

        private void xoaDatPhong()
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "xoa_DatPhong";
            sqlCommand.Parameters.AddWithValue("@sMaDatPhong", lbMaDatPhong.Text);

            int i = sqlCommand.ExecuteNonQuery();

            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
        }
    }
}
