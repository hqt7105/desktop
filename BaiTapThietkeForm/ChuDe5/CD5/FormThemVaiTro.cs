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
	public partial class FormThemVaiTro : Form
	{
		public FormThemVaiTro()
		{
			InitializeComponent();
		}

		private void FormThemVaiTro_Load(object sender, EventArgs e)
		{

		}
		private void ResetText()
		{
			txtTenVaiTro.ResetText();
		}

		private void Thêm_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTenVaiTro.Text))
			{
				MessageBox.Show("Vui lòng nhập tên vai trò", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtTenVaiTro.Focus();
				return;
			}


            string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";

            try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					// Chỉ thêm tên vai trò — MaVaiTro sẽ tự tăng
					string query = "INSERT INTO VaiTro (TenVaiTro) VALUES (@tenVaiTro)";
					SqlCommand cmd = new SqlCommand(query, conn);
					cmd.Parameters.AddWithValue("@tenVaiTro", txtTenVaiTro.Text.Trim());

					conn.Open();
					int rows = cmd.ExecuteNonQuery();

					if (rows > 0)
					{
						MessageBox.Show("Thêm vai trò thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtTenVaiTro.Clear();
						this.DialogResult = DialogResult.OK;
						this.Close();
					}
					else
					{
						MessageBox.Show("Thêm vai trò thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch (SqlException ex)
			{
				MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
	}
	}
}

