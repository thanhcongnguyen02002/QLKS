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
    public partial class DSPhong : Form
    {
        public DSPhong()
        {
            InitializeComponent();
        }

        private void DSPhong_Load(object sender, EventArgs e)
        {
            loadform();
            AddButtons();
        }
        private void loadform()
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
            sqlCommand.CommandText = "select * from tblPhong";
            sqlDataAdapter.SelectCommand = sqlCommand;
            dataTable.Clear();
            sqlDataAdapter.Fill(dataTable);
            dgvDSPhong.DataSource = dataTable;

            // dgvThongTIn.Columns.Add("Mã Sinh Viên", 50);
            dgvDSPhong.Columns[0].HeaderText = "Mã Phòng";
            dgvDSPhong.Columns[0].Name = "MaPhong";
            dgvDSPhong.Columns[1].HeaderText = "Loại Phòng";
            dgvDSPhong.Columns[2].HeaderText = "Giá Tiền";
            dgvDSPhong.Columns[3].HeaderText = "Số Giường";
            dgvDSPhong.Columns[4].HeaderText = "Trạng Thái";


            dgvDSPhong.Refresh();
            // đóng kết nối
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlDataAdapter.Dispose();
            }
        }
        private void AddButtons()
        {
            // Thêm cột "Đặt phòng"
            DataGridViewButtonColumn bookColumn = new DataGridViewButtonColumn();
            bookColumn.HeaderText = "";
            bookColumn.Text = "Đặt phòng";
            bookColumn.UseColumnTextForButtonValue = true;
            bookColumn.Name = "btnDat";

            dgvDSPhong.Columns.Add(bookColumn);


            DataGridViewButtonColumn bookColumn1 = new DataGridViewButtonColumn();
            bookColumn1.HeaderText = "";
            bookColumn1.Text = "trả Phòng";
            bookColumn1.UseColumnTextForButtonValue = true;
            bookColumn1.Name = "btnTra";
            dgvDSPhong.Columns.Add(bookColumn1);
        }

        private void dgvDSPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string conn = "Data Source=PC-OF-THANHCONG\\SQLEXPRESS01;Initial Catalog=QuanLyKhachSanC#;Integrated Security=True";

            SqlConnection sqlConnection = new SqlConnection(conn);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            if (e.ColumnIndex == dgvDSPhong.Columns["btnDat"].Index && e.RowIndex >= 0)
            {
                // string maPhong = dgvKhachHang.SelectedRows[0].Cells["MaPhong"].Value.ToString();
                string maPhong = dgvDSPhong.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();
                // SqlCommand cmd = new SqlCommand("HoaDon_TraPhong", sqlConnection);
                // cmd.CommandType = CommandType.StoredProcedure;
                //  cmd.Parameters.AddWithValue("@sMaP", maPhong);
                // string maDatPhong = cmd.ExecuteScalar()?.ToString();

                string query = "SELECT sTrangThai FROM tblPhong WHERE sMaP = @maPhong";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@maPhong", maPhong);
                        string trangThai = (string)command.ExecuteScalar();

                        // Kiểm tra trạng thái của phòng
                        if (trangThai.ToLower() == "Trống".ToLower())
                        {
                            // Nếu phòng đang trống, chuyển đến form đặt phòng
                            //  FormDatPhong formDatPhong = new FormDatPhong(maPhong);
                            // formDatPhong.ShowDialog();
                            DatPhong datPhong = new DatPhong(maPhong);
                            datPhong.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            // Nếu phòng không trống, hiển thị thông báo lỗi
                            MessageBox.Show("Phòng đang không trống, không thể đặt phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    connection.Close();
                }

            }
            else if (e.ColumnIndex == dgvDSPhong.Columns["btnTra"].Index && e.RowIndex >= 0)
            {
                string maPhong = dgvDSPhong.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();
                string query = "SELECT sTrangThai FROM tblPhong WHERE sMaP = @maPhong";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@maPhong", maPhong);
                        string trangThai = (string)command.ExecuteScalar();

                        // Kiểm tra trạng thái của phòng
                        if (trangThai.ToLower() == "Đã Đặt".ToLower())
                        {
                            string query1 = "SELECT TOP 1 sMaDatPhong FROM tblDatPhong WHERE sMaP = @MaPhong ORDER BY dNgayDat DESC";
                            SqlDataAdapter adapter = new SqlDataAdapter(query1, connection);
                            adapter.SelectCommand.Parameters.AddWithValue("@MaPhong", maPhong);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                string maDatPhong = dt.Rows[0]["sMaDatPhong"].ToString();
                                // Thực hiện các thao tác cần thiết với mã đặt phòng
                                TraPhong traPhong = new TraPhong(maPhong,maDatPhong);
                                traPhong.Show();
                                this.Close();

                            }


                        }

                        else
                        {
                            // Nếu phòng  trống, hiển thị thông báo lỗi
                            MessageBox.Show("phòng chưa được đặt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    connection.Close();

                }
            }
            }
    }
}
