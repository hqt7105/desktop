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
	public partial class FormCT_HD : Form
	{
		private int maHD; // mã hóa đơn cần xem

        string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
        public FormCT_HD(int maHD)
		{
			InitializeComponent();
			this.maHD = maHD;
		}

		private void FormCT_HD_Load(object sender, EventArgs e)
		{
			LoadOrderDetails();
		}

		private void LoadOrderDetails()
		{
			lvCTHD.Items.Clear();

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string query = @"SELECT a.MaHD, b.Ten AS TenMon, a.SoLuong, a.DonGia, (a.SoLuong * a.DonGia) AS ThanhTien
                             FROM CT_HoaDon a, Food b
                             WHERE a.MaMon = b.MaMon AND a.MaHD = @MaHD";

				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = maHD;

				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					ListViewItem item = new ListViewItem(reader["MaHD"].ToString());
					item.SubItems.Add(reader["TenMon"].ToString());
					item.SubItems.Add(reader["SoLuong"].ToString());
					item.SubItems.Add(reader["DonGia"].ToString());
					item.SubItems.Add(reader["ThanhTien"].ToString());
					lvCTHD.Items.Add(item);
				}

				reader.Close();
			}

			// 👉 Cập nhật tổng tiền
			double tong = 0;
			foreach (ListViewItem item in lvCTHD.Items)
			{
				tong += Convert.ToDouble(item.SubItems[4].Text);
			}

			lblTongTien.Text = $"Tổng tiền: {tong:N0} VND";
		}
	}
}
