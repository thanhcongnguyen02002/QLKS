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
    public partial class HeThong : Form
    {
        public  bool isLoggedIn=true;
        public HeThong( )
        {
            InitializeComponent();
          
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        { if(isLoggedIn== true)
            {
                KhachHang khachHang = new KhachHang();
                khachHang.Show();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này","Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (isLoggedIn == true)
            {
                NhanVien nhanVien = new NhanVien();
                nhanVien.Show();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void phòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (isLoggedIn == true)
            {
                Phong phong = new Phong();
                phong.Show();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void quảnLíDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (isLoggedIn == true)
            {
                DichVu dichVu = new DichVu();
                dichVu.Show();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void sửDụngDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (isLoggedIn == true)
            {
                HoaDonDichVu hoaDonDichVu = new HoaDonDichVu();
                hoaDonDichVu.Show();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void danhSáchPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (isLoggedIn == true)
            {
                DSPhong dSPhong = new DSPhong();
                dSPhong.Show();

            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap dangNhap = new DangNhap();
            //dangNhap.Show();
            if(dangNhap.ShowDialog()== DialogResult.OK)
            {
                isLoggedIn = true;
                
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result= MessageBox.Show("Bạn có muốn thoát không","thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                isLoggedIn = false;
                 
            }
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dịchVụToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            if (isLoggedIn == true)
            {
                BaoCaoDichVu baoCaoDichVu = new BaoCaoDichVu();
                baoCaoDichVu.Show();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void kháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isLoggedIn == true)
            {
                BaoCaoHoaDonKhachHang();
            }
            else
            {
                MessageBox.Show("Vui Lòng Đăng Nhập Để Sử Dụng Chức Năng Này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        private void BaoCaoHoaDonKhachHang()
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
            // sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "select * from tblKhachHang";

            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);

             rptKhachHang rpt= new rptKhachHang();
            rpt.SetDataSource(dataTable);
            BaoCaoKhachHang baoCaoKhachHang = new BaoCaoKhachHang();
            baoCaoKhachHang.crvKhachHang.ReportSource = rpt;
            baoCaoKhachHang.Show();
           // chiTietHoaDon.crytalReportHoaDon.ReportSource = rptChiTietoaDon;
           // chiTietHoaDon.Show();
           
        }

        private void nhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
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
            sqlCommand.CommandText = "SELECT * FROM tblNhanVien";

            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            rptNhanVien rptNhanVien = new rptNhanVien();
            rptNhanVien.SetDataSource(dataTable);
            baocaonhanvien baocaonhanvien = new baocaonhanvien();
            baocaonhanvien.crvNhanVien.ReportSource = rptNhanVien;
            baocaonhanvien.Show();
            
        }

        private void HeThong_Load(object sender, EventArgs e)
        {

        }

        private void thiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thi thi = new thi();
            thi.Show();
        }
    }
}
