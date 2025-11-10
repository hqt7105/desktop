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
	public partial class FormTaiKhoan : Form
	{

        string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";

        public FormTaiKhoan()
		{
			InitializeComponent();
		}

		private void FormTaiKhoan_Load(object sender, EventArgs e)
		{
			LoadTaiKhoan();
		}
		private void LoadTaiKhoan()
		{
			try
			{
				lvTaiKhoan.Items.Clear();

				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					string query = "SELECT TenTK FROM TaiKhoan";
					SqlCommand cmd = new SqlCommand(query, conn);
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						ListViewItem item = new ListViewItem(reader["TenTK"].ToString());
						lvTaiKhoan.Items.Add(item);
					}

					reader.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi tải dữ liệu tài khoản: {ex.Message}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FormTaoTaiKhoan formttk = new FormTaoTaiKhoan();
			formttk.ShowDialog();
		}

		private void btnXem_Click(object sender, EventArgs e)
		{
			FoodForm foodForm = new FoodForm();
			foodForm.ShowDialog();
		}
		private void LoadThongTinTaiKhoan(string tenTK)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					string query = @"
                SELECT tk.MaTK, tk.TenTK, tk.MatKhau, tk.Email, vt.TenVaiTro 
                FROM TaiKhoan tk
                LEFT JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                WHERE tk.TenTK = @TenTK";

					SqlCommand cmd = new SqlCommand(query, conn);
					cmd.Parameters.AddWithValue("@TenTK", tenTK);

					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						txtMaTK.Text = reader["MaTK"].ToString();     // ✅ thêm dòng này
						txtTenTK.Text = reader["TenTK"].ToString();
						txtMatKhau.Text = reader["MatKhau"].ToString();
						txtEmail.Text = reader["Email"].ToString();
						txtVaiTro.Text = reader["TenVaiTro"].ToString();
					}
					reader.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi tải thông tin tài khoản: {ex.Message}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void lvTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvTaiKhoan.SelectedItems.Count > 0)
			{
				string tenTK = lvTaiKhoan.SelectedItems[0].Text;
				LoadThongTinTaiKhoan(tenTK);
			}
		}
		

		private void btnCapNhat_Click(object sender, EventArgs e)
		{
			try
			{
				// --- 1️⃣ Kiểm tra dữ liệu đầu vào ---
				if (string.IsNullOrEmpty(txtTenTK.Text))
				{
					MessageBox.Show("Vui lòng nhập tên tài khoản cần cập nhật.",
									"Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (string.IsNullOrEmpty(txtMaTK.Text))
				{
					MessageBox.Show("Không tìm thấy mã tài khoản (MaTK).",
									"Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}


                string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";

                using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					// --- 2️⃣ Lấy MaVaiTro từ tên vai trò nhập trong txtVaiTro ---
					string getRoleQuery = "SELECT MaVaiTro FROM VaiTro WHERE TenVaiTro = @TenVaiTro";
					int maVaiTro;

					using (SqlCommand getRoleCmd = new SqlCommand(getRoleQuery, conn))
					{
						getRoleCmd.Parameters.Add("@TenVaiTro", SqlDbType.NVarChar, 200).Value = txtVaiTro.Text.Trim();

						object result = getRoleCmd.ExecuteScalar();

						if (result == null || result == DBNull.Value)
						{
							MessageBox.Show("Vai trò không tồn tại. Vui lòng nhập đúng tên vai trò.",
											"Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}

						if (!int.TryParse(result.ToString(), out maVaiTro))
						{
							MessageBox.Show("Giá trị MaVaiTro trong cơ sở dữ liệu không hợp lệ.",
											"Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}

					// --- 3️⃣ Gọi stored procedure UpdateTaiKhoan ---
					using (SqlCommand cmd = new SqlCommand("UpdateTaiKhoan", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;

						// Truyền các tham số
						cmd.Parameters.Add("@MaTK", SqlDbType.Int).Value = Convert.ToInt32(txtMaTK.Text);
						cmd.Parameters.Add("@TenTK", SqlDbType.NVarChar, 100).Value = txtTenTK.Text.Trim();
						cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = txtMatKhau.Text.Trim();
						cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = txtEmail.Text.Trim();
						cmd.Parameters.Add("@MaVaiTro", SqlDbType.Int).Value = maVaiTro;

						// --- 4️⃣ Thực thi và kiểm tra kết quả ---
						int numRowAffected = cmd.ExecuteNonQuery();

						if (numRowAffected > 0)
						{
							MessageBox.Show("✅ Cập nhật tài khoản thành công!",
											"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
							LoadTaiKhoan(); // Cập nhật lại danh sách
						}
						else
						{
							MessageBox.Show("Cập nhật thành công",
											"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
