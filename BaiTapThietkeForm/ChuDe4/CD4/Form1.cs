using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace CD4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true; ";

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

       
            string query = "SELECT ID, Name, Type FROM Category";
            sqlCommand.CommandText = query;

            sqlConnection.Open();

         
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

       
            this.DisplayCategory(sqlDataReader);

            // Đóng kết nối
            sqlConnection.Close();
        }
        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                lvCategory.Items.Add(item);
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng kết nối
            string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" +
                                     "VALUES (N'" + txtName.Text + "', " + txtType.Text + ")";

            // Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            // Thực thi lệnh bằng phương thức ExecuteNonQuery
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            // Đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm nhóm món ăn thành công");

                // Tải lại dữ liệu
                btnLoad.PerformClick();

                // Xóa các ô nhập
                txtName.Text = "";
                txtType.Text = "";
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }
        }

        private void lvCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                txtID.Text = item.Text;
                txtName.Text = item.SubItems[1].Text;
                txtType.Text = item.SubItems[1].Text == "0" ? "Thức uống" : "Dồ ăn";

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database=Restauranmanagement; Integrated Security=true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "UPDATE Category " +
                                     "SET Name = @Name, [Type] = @Type " +
                                     "WHERE ID = @ID";

            // Thêm tham số để tránh SQL Injection
            sqlCommand.Parameters.AddWithValue("@Name", txtName.Text);
            sqlCommand.Parameters.AddWithValue("@Type", txtType.Text);
            sqlCommand.Parameters.AddWithValue("@ID", txtID.Text);

            // Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            // Thực thi lệnh bằng phương thức ExecuteNonQuery
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            // Đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                // Cập nhật lại dữ liệu trên ListView
                ListViewItem item = lvCategory.SelectedItems[0];
                item.SubItems[1].Text = txtName.Text;
                item.SubItems[2].Text = txtType.Text;

                // Xóa các ô nhập
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                // Disable các nút xóa và cập nhật
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                MessageBox.Show("Cập nhật nhóm món ăn thành công");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng kết nối
            string connectionString = "server=DESKTOP-TLEVS6G\\SQLEXPRESS01; database=Restauranmanagement; Integrated Security=true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "DELETE FROM Category WHERE ID = @ID";
            sqlCommand.Parameters.AddWithValue("@ID", txtID.Text);

            // Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            // Thực thi lệnh bằng phương thức ExecuteNonQuery
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            // Đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                // Cập nhật lại dữ liệu trên ListView
                ListViewItem item = lvCategory.SelectedItems[0];
                lvCategory.Items.Remove(item);

                // Xóa các ô nhập
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                // Disable các nút xóa và cập nhật
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                MessageBox.Show("Xóa nhóm món ăn thành công");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại");
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if(lvCategory.SelectedItems.Count > 0)
            {
                btnDelete.PerformClick();
            }
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                FoodForm foodForm = new FoodForm();
                foodForm.Show(this);
                foodForm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            BillForm billForm = new BillForm();
            billForm.ShowDialog();
        }
    }
}
