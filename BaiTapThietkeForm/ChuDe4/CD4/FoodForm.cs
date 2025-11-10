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

namespace CD4
{
    public partial class FoodForm : Form
    {
        string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database=Restauranmanagement; Integrated Security=true; ";
        public FoodForm()
        {
            InitializeComponent();
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            LoadFood();
        }
        public void LoadFood(int categoryID)
        {
            // Tạo đối tượng kết nối
            string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database=Restauranmanagement; Integrated Security=true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Thiết lập lệnh truy vấn cho đối tượng Command (lấy tên nhóm món ăn)
            sqlCommand.CommandText = "SELECT Name FROM Category WHERE ID = @CategoryID";
            sqlCommand.Parameters.AddWithValue("@CategoryID", categoryID);

            // Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            // Gán tên nhóm sản phẩm cho tiêu đề
            string catname = sqlCommand.ExecuteScalar().ToString();
            this.Text = "Danh sách các món ăn thuộc nhóm: " + catname;

            // Thiết lập lại câu lệnh truy vấn (lấy danh sách món ăn)
            sqlCommand.CommandText = "SELECT * FROM Food WHERE ID = @CategoryID";

            // Tạo đối tượng DataAdapter
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            // Tạo DataTable để chứa dữ liệu
            DataTable dt = new DataTable("Food");
            da.Fill(dt);

            // Hiển thị danh sách món ăn lên Form
            dgvFood.DataSource = dt;

            // Đóng kết nối và giải phóng bộ nhớ
            sqlConnection.Close();
            sqlConnection.Dispose();
            da.Dispose();
        }
        public void LoadFood()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Food";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvFood.DataSource = dt;
            }
        }
        private void dgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // tránh click header
            {
                DataGridViewRow row = dgvFood.Rows[e.RowIndex];

                txtMaMon.Text = row.Cells["ID"].Value.ToString();
                txtTenMon.Text = row.Cells["NameFood"].Value.ToString();
                txtDVT.Text = row.Cells["DVT"].Value.ToString();
                txtMaNhom.Text = row.Cells["IDNhom"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
               string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database=Restauranmanagement; Integrated Security=true;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra mã món đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(*) FROM Food WHERE MaMon = @ID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ID", txtMaMon.Text);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Nếu đã tồn tại → Cập nhật (Update)
                        string updateQuery = @"UPDATE Food 
                                   SET Ten = @NameFood, DVT = @DVT, ID = @IDNhom, 
                                       DonGia = @DonGia, GhiChu = @GhiChu
                                   WHERE MaMon = @ID";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@ID", txtMaMon.Text);
                        updateCmd.Parameters.AddWithValue("@NameFood", txtTenMon.Text);
                        updateCmd.Parameters.AddWithValue("@DVT", txtDVT.Text);
                        updateCmd.Parameters.AddWithValue("@IDNhom", txtMaNhom.Text);
                        updateCmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text);
                        updateCmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Đã cập nhật món ăn thành công!");
                    }
                    else
                    {
                        // Nếu chưa có → Thêm mới (Insert)
                        string insertQuery = @"INSERT INTO Food (Ten, DVT, ID, DonGia, GhiChu) 
                                   VALUES ( @NameFood, @DVT, @IDNhom, @DonGia, @GhiChu)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@NameFood", txtTenMon.Text);
                        insertCmd.Parameters.AddWithValue("@DVT", txtDVT.Text);
                        insertCmd.Parameters.AddWithValue("@IDNhom", txtMaNhom.Text);
                        insertCmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text);
                        insertCmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Đã thêm món ăn mới thành công!");
                    }

                    conn.Close();
                }

                // Load lại dữ liệu vào dgvFood
                LoadFood(Convert.ToInt32(txtMaNhom.Text));
            }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = dgvFood.CurrentRow.Cells["ID"].Value.ToString();

            string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database=Restauranmanagement; Integrated Security=true;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Food WHERE MaMon = @ID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@ID", id);

                deleteCmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Đã xóa món ăn thành công!");
            LoadFood(Convert.ToInt32(txtMaNhom.Text));
        }
    }
    }

