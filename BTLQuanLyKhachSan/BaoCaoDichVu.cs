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
    public partial class BaoCaoDichVu : Form
    {
        public BaoCaoDichVu()
        {
            InitializeComponent();
        }

        private void BaoCaoDichVu_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
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
            // sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "select * from tblDichVu";

            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            rptDichVu crystalReport2 = new rptDichVu();
            crystalReport2.SetDataSource(dataTable);
            crystalReportViewer1.ReportSource = crystalReport2;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string checkSL = "^[0-9]*$";
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            try {
                
                if (int.Parse(textBox1.Text) >= int.Parse(textBox2.Text))
                {
                    MessageBox.Show("Khoảng giá trị không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                DataTable dataTable = new DataTable();
                SqlCommand sqlCommand;
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "rptSEARCH_X_Y";
                sqlCommand.Parameters.AddWithValue("@x", textBox1.Text);
                sqlCommand.Parameters.AddWithValue("@y", textBox2.Text);
                sqlDataAdapter.SelectCommand = sqlCommand;
                dataTable.Clear();
                sqlDataAdapter.Fill(dataTable);
                rptDichVu crystalReport2 = new rptDichVu();
                crystalReport2.SetDataSource(dataTable);
                crystalReportViewer1.ReportSource = crystalReport2;
                BaoCaoDichVu_Load(sender, e);

            }
            catch
            {
                if (!Regex.IsMatch(textBox1.Text, checkSL) || !Regex.IsMatch(textBox2.Text, checkSL))
                {
                    MessageBox.Show("giá trị chỉ chứa số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           if(sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        } 
    }
}
