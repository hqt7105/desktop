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

namespace CD5
{
    public partial class FoodForm : Form
    {
        private DataTable foodtable;
        public FoodForm()
        {
            InitializeComponent();
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void LoadCategory()
        {
            string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Category";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            // Mở kết nối
            conn.Open();

            // Lấy dữ liệu từ csdl đưa vào DataTable
            adapter.Fill(dt);

            // Đóng kết nối và giải phóng bộ nhớ
            conn.Close();
            conn.Dispose();

            // Đưa dữ liệu vào Combo Box
            cbbCategory.DataSource = dt;

            // Hiển thị tên nhóm sản phẩm
            cbbCategory.DisplayMember = "Name";

            // Nhưng khi lấy giá trị thì lấy ID của nhóm
            cbbCategory.ValueMember = "ID";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCategory.SelectedIndex == -1) return;

            string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Food WHERE ID = @categoryId";

            // Truyền tham số
            cmd.Parameters.Add("@categoryId", SqlDbType.Int);

            if (cbbCategory.SelectedValue is DataRowView)
            {
                DataRowView rowView = cbbCategory.SelectedValue as DataRowView;
                cmd.Parameters["@categoryId"].Value = rowView["ID"];
            }
            else
            {
                cmd.Parameters["@categoryId"].Value = cbbCategory.SelectedValue;
            }

            // Tạo bộ điều phối dữ liệu
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
             foodtable = new DataTable();

            // Mở kết nối
            conn.Open();

            // Lấy dữ liệu từ csdl đưa vào DataTable
            adapter.Fill(foodtable);

            // Đóng kết nối và giải phóng bộ nhớ
            conn.Close();
            conn.Dispose();

            // Đưa dữ liệu vào data gridview
            dgvFoodList.DataSource = foodtable;

            // Hiển thị số lượng món ăn
            lblQuantity.Text = foodtable.Rows.Count.ToString();

            // Hiển thị tên nhóm
            lblCatName.Text = cbbCategory.Text;
        }

        private void ctmTinhSL_Click(object sender, EventArgs e)
        {

            string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT @numSaleFood = SUM(SoLuong) FROM CT_HoaDon WHERE MaMon = @foodId";

            // Lấy thông tin sản phẩm được chọn
            if (dgvFoodList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;

                // Truyền tham số
                cmd.Parameters.Add("@foodId", SqlDbType.Int);
                cmd.Parameters["@foodId"].Value = rowView["MaMon"];

                cmd.Parameters.Add("@numSaleFood", SqlDbType.Int);
                cmd.Parameters["@numSaleFood"].Direction = ParameterDirection.Output;

                // Mở kết nối csdl
                conn.Open();

                // Thực thi truy vấn và lấy dữ liệu từ tham số
                cmd.ExecuteNonQuery();

                string result = cmd.Parameters["@numSaleFood"].Value.ToString();
                MessageBox.Show("Tổng số lượng món " + rowView["Ten"] + " đã bán là: " + result + " " + rowView["GhiChu"]);

                // Đóng kết nối csdl
                conn.Close();
            }

            cmd.Dispose();
            conn.Dispose();
        }

        private void ctmSeperator_Click(object sender, EventArgs e)
        {

        }

        private void FoodForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            int index = cbbCategory.SelectedIndex;
            cbbCategory.SelectedIndex = -1;
            cbbCategory.SelectedIndex = index;
        }
        private void tmsThem_Click(object sender, EventArgs e)
        {
            FoodInfoForm foodForm = new FoodInfoForm();
            foodForm.FormClosed += new FormClosedEventHandler(FoodForm_FormClosed);

            foodForm.Show(this);
        }

        private void ctmCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvFoodList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;

                FoodInfoForm foodForm = new FoodInfoForm();
                foodForm.FormClosed += new FormClosedEventHandler(FoodForm_FormClosed);

                foodForm.Show(this);
                foodForm.DisplayFoodInfo(rowView);
            }
        }

        private void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            // Nếu bảng dữ liệu chưa có thì thoát
            if (foodtable == null) return;

            // Tạo biểu thức lọc (lọc theo tên món ăn)
            string filterExpression = string.Format("Ten LIKE '{0}%'", txtSearchByName.Text);

            // Tạo biểu thức sắp xếp (giá giảm dần)
            string sortExpression = "DonGia DESC";

            // Tạo đối tượng DataView để áp dụng filter và sort
            DataViewRowState rowStateFilter = DataViewRowState.OriginalRows;
            DataView foodView = new DataView(foodtable, filterExpression, sortExpression, rowStateFilter);

            // Gán DataView làm nguồn dữ liệu cho DataGridView
            dgvFoodList.DataSource = foodView;
        }

		private void btnXem_Click(object sender, EventArgs e)
		{
            OrderForm orderForm = new OrderForm();
            orderForm.ShowDialog();
		}
	}
}
