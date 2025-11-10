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
	public partial class FormAddGroup : Form
	{
		public FormAddGroup()
		{
			InitializeComponent();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			try
			{

                string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
                using (SqlConnection conn = new SqlConnection(connectionString))
				{
					SqlCommand cmd = conn.CreateCommand();
					cmd.CommandText = "InsertCategory";
					cmd.CommandType = CommandType.StoredProcedure;

					// Thêm tham số
					cmd.Parameters.Add("@MaNhom", SqlDbType.Int).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("@TenNhom", SqlDbType.NVarChar, 20).Value = txtTenNhom.Text;
					cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 20).Value = txtLoai.Text;

					// Mở kết nối
					conn.Open();

					// Thực thi
					cmd.ExecuteNonQuery();

					// Lấy giá trị ID vừa thêm
					int maNhomMoi = Convert.ToInt32(cmd.Parameters["@MaNhom"].Value);
					txtMaNhom.Text = maNhomMoi.ToString();

					MessageBox.Show("Thêm nhóm món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (SqlException ex)
			{
				MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi");
			}
		}

		private void FormAddGroup_Load(object sender, EventArgs e)
		{

		}
	}
}
