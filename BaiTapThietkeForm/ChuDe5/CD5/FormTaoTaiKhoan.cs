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
	public partial class FormTaoTaiKhoan : Form
	{
		public FormTaoTaiKhoan()
		{
			InitializeComponent();
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void FormTaoTaiKhoan_Load(object sender, EventArgs e)
		{
			this.InitValues();
		}
		private void InitValues()
		{

            string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT MaVaiTro, TenVaiTro FROM VaiTro";

			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();

			// Mở kết nối
			conn.Open();

			// Lấy dữ liệu từ csdl đưa vào DataTable
			adapter.Fill(ds, "VaiTro");

			// Đóng kết nối và giải phóng bộ nhớ
			cbbVaiTro.DataSource = ds.Tables["VaiTro"];
			cbbVaiTro.DisplayMember = "TenVaiTro";
			cbbVaiTro.ValueMember = "MaVaiTro";
			conn.Close();
			conn.Dispose();
		}

		private void ResetText()
		{
			txtTenTaiKhoan.ResetText();
			txtMatKhau.ResetText();
			txtEmail.ResetText();
			cbbVaiTro.ResetText();
		}


		private void btnThem_Click(object sender, EventArgs e)
		{
			try
			{

                string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);

				SqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = "EXECUTE InsertTaiKhoan @maTK OUTPUT, @tenTK, @matKhau, @email, @nhomTK, @ngayTaoTK, @trangThai, @maVaiTro";

				// Thêm tham số vào đối tượng Command
				cmd.Parameters.Add("@maTK", SqlDbType.Int);
				cmd.Parameters.Add("@tenTK", SqlDbType.NVarChar, 100);
				cmd.Parameters.Add("@matKhau", SqlDbType.NVarChar, 100);
				cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
				cmd.Parameters.Add("@nhomTK", SqlDbType.Int);
				cmd.Parameters.Add("@ngayTaoTK", SqlDbType.Date);
				cmd.Parameters.Add("@trangThai", SqlDbType.Int);
				cmd.Parameters.Add("@maVaiTro", SqlDbType.Int);

				cmd.Parameters["@maTK"].Direction = ParameterDirection.Output;

				// Truyền giá trị vào thủ tục qua tham số
				cmd.Parameters["@tenTK"].Value = txtTenTaiKhoan.Text;
				cmd.Parameters["@matKhau"].Value = txtMatKhau.Text;
				cmd.Parameters["@email"].Value = txtEmail.Text;
				cmd.Parameters["@nhomTK"].Value = DBNull.Value; // Có thể điều chỉnh nếu có control cho nhóm TK
				cmd.Parameters["@ngayTaoTK"].Value = DateTime.Now;
				cmd.Parameters["@trangThai"].Value = 1; // Mặc định trạng thái active
				cmd.Parameters["@maVaiTro"].Value = cbbVaiTro.SelectedValue;

				// Mở kết nối
				conn.Open();

				int numRowAffected = cmd.ExecuteNonQuery();

				// Thông báo kết quả
				if (numRowAffected > 0)
				{
					string accountID = cmd.Parameters["@maTK"].Value.ToString();
					MessageBox.Show("Thêm tài khoản thành công. Mã TK = " + accountID, "Thông báo");
					this.ResetText();
				}
				else
				{
					MessageBox.Show("Thêm tài khoản thất bại");
				}

				// Đóng kết nối
				conn.Close();
				conn.Dispose();
			}
			// Bắt lỗi SQL và các lỗi khác
			catch (SqlException exception)
			{
				MessageBox.Show(exception.Message, "SQL Error");
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Error");
			}
		}

		private void btnThemVaiTro_Click(object sender, EventArgs e)
		{
			FormThemVaiTro formThemVaiTro = new FormThemVaiTro();
			// Nếu form thêm vai trò trả về OK (tức là thêm thành công)
			if (formThemVaiTro.ShowDialog() == DialogResult.OK)
			{
				// Load lại danh sách vai trò mới nhất
				InitValues();

				// Chọn luôn vai trò vừa thêm (nếu muốn)
				cbbVaiTro.SelectedIndex = cbbVaiTro.Items.Count - 1;
			}

		}
	}
}
