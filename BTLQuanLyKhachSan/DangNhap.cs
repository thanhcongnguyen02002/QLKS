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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
       // public static bool IsLogger = false;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            // chỗ này là gọi đến from đăng kí nhé . viết nhầm đăng xuất ạ -.-'
            DangXuat dangXuat = new DangXuat();
            this.Close();
            dangXuat.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if(txtMatKhau.Text==""|| txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được để trống!","Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "Check_login";
            sqlCommand.Parameters.AddWithValue("@sTaiKhoan",txtTaiKhoan.Text);
            sqlCommand.Parameters.AddWithValue("@sMatKhau", txtMatKhau.Text);
             SqlDataReader i = sqlCommand.ExecuteReader();
            if (i.HasRows)
            {
                MessageBox.Show("Bạn đã đang nhập thành công . Bạn có thể sử dụng các chức năng ngay bây giờ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;

                
                // HeThong heThong = new HeThong(IsLogger);
                this.Close();
            }
            else {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu! Vui lòng kiểm tra lại","Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          /*  DataTable dataTable = new DataTable();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
            {
                dataAdapter.Fill(dataTable);
            }


            DataView dataView = new DataView(dataTable);
            int count = dataView.Count;
            if(count > 0)
            {
                MessageBox.Show("thành công");
            }
            else
            {
                MessageBox.Show("thất bại");
            }*/
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
