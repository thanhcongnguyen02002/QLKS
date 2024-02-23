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
    public partial class DangXuat : Form
    {
        public DangXuat()
        {
            InitializeComponent();
        }
        private bool check_mail(string mail)
        {//^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(gmail\.com)$
            //string smail = @"^([\\w\\.\\-]+\[gmail.com\s]+$)$";
            if (!Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[gmail.com\s]+$"))
            // if(!Regex.IsMatch(mail, @"[a-zA-Z]+$"))
            {
                return false;
            }
            /* if (!mail.EndsWith("@gmail.com"))
             {
                 return false;
             }*/
            else return true;
        }
        private  bool IsStrongPassword(string password)
        {// check chữ hoa thường, kí tự đặc biệt và có độ dài 9-12
            string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#\\$%\\^&\\*])(?=.{8,12}).*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
        private bool checkUserName(string username)
        {
            string pattern = @"^\S+$";
            return Regex.IsMatch(username, pattern);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            if(txtEmail.Text==""|| txtMatKhau.Text==""||txtNhapLaiMatKhau.Text==""|| txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Thông tin Không được để trống", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           /* if (checkUserName(txtTenDangNhap.Text)== false){
                MessageBox.Show("tên đang nhập không hợp lệ");
                return;
            }

            if (check_mail(txtEmail.Text)==false)
            {
                MessageBox.Show("email không đúng định dạng");
                return;
            }
            if (!IsStrongPassword(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu không đủ mạnh");
                    return;
            }*/
            if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                    MessageBox.Show("Mật Khẩu không Trùng nhau", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
           

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
               
                sqlCommand.CommandText = "insertAccount";
                sqlCommand.Parameters.AddWithValue("@sTenTaiKhoan", txtTenDangNhap.Text);
                sqlCommand.Parameters.AddWithValue("@sMatKhau", txtMatKhau.Text);
                sqlCommand.Parameters.AddWithValue("@sEmail", txtEmail.Text);
                int i = sqlCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    DialogResult result = MessageBox.Show("Bạn đã đăng kí thành công. bạn có muốn đăng nhập không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DangNhap dangNhap = new DangNhap();
                        this.Close();
                        dangNhap.Show();

                    }
                }
                else
                {
                    MessageBox.Show("đăng kí thất bại");
                }
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }

        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangNhap dangNhap =new DangNhap();
            this.Close();
            dangNhap.Show();
        }

        private void DangXuat_Load(object sender, EventArgs e)
        {

        }
    }
}
