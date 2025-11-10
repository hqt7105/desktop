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
	public partial class OrderForm : Form
	{

        string connectionString = "server = DESKTOP-TLEVS6G\\SQLEXPRESS01; database = Restauranmanagement; Integrated Security = true;";
        public OrderForm()
		{
			InitializeComponent();
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void OrderForm_Load(object sender, EventArgs e)
		{
			dtpTuNgay.Format = DateTimePickerFormat.Custom;
			dtpTuNgay.CustomFormat = "dd/MM/yyyy";
			dtpDenNgay.Format = DateTimePickerFormat.Custom;
			dtpDenNgay.CustomFormat = "dd/MM/yyyy";

			// ✅ Đặt khoảng ngày hợp lý (trùng với dữ liệu bạn có)
			dtpTuNgay.Value = new DateTime(2012, 1, 1);
			dtpDenNgay.Value = DateTime.Now.Date;

			LoadOrders();
		}
		private void LoadOrders()
		{
			try
			{
				lvHD.Items.Clear();

				double tongChuaGiam = 0;
				double tongDaGiam = 0;

				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					// ✅ Câu truy vấn mới: Tính tổng tiền theo chi tiết hóa đơn
					string query = @"
	SELECT 
		hd.MaHD,
		hd.NgayLapHD,
		hd.MaBan,
		ISNULL(hd.GiamGia, 0) AS GiamGia,
		hd.TrangThai,
		ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongTien
	FROM HoaDon hd
	LEFT JOIN CT_HoaDon ct ON hd.MaHD = ct.MaHD
	WHERE CAST(hd.NgayLapHD AS DATE) BETWEEN @TuNgay AND @DenNgay
	GROUP BY hd.MaHD, hd.NgayLapHD, hd.MaBan, hd.GiamGia, hd.TrangThai
	ORDER BY hd.NgayLapHD DESC";

					SqlCommand cmd = new SqlCommand(query, conn);
					cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = dtpTuNgay.Value.Date;
					cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = dtpDenNgay.Value.Date;

					conn.Open();
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						double tongTien = reader["TongTien"] != DBNull.Value ? Convert.ToDouble(reader["TongTien"]) : 0;
						double giamGia = reader["GiamGia"] != DBNull.Value ? Convert.ToDouble(reader["GiamGia"]) : 0;

						// Cộng dồn tổng
						tongChuaGiam += tongTien;
						tongDaGiam += giamGia;

						// Thêm dòng vào ListView
						ListViewItem item = new ListViewItem(reader["MaHD"].ToString());
						item.SubItems.Add(Convert.ToDateTime(reader["NgayLapHD"]).ToString("dd/MM/yyyy HH:mm"));
						item.SubItems.Add(reader["MaBan"].ToString());
						item.SubItems.Add(string.Format("{0:N0}", tongTien));
						item.SubItems.Add(string.Format("{0:N0}", giamGia));
						item.SubItems.Add(reader["TrangThai"].ToString());

						lvHD.Items.Add(item);
					}

					reader.Close();
				}

				// ✅ Tính thực thu
				double thucThu = tongChuaGiam - tongDaGiam;

				// ✅ Hiển thị vào các textbox
				txtTienChuaGiam.Text = string.Format("{0:N0}", tongChuaGiam);
				txtTienDaGiam.Text = string.Format("{0:N0}", tongDaGiam);
				txtThucThu.Text = string.Format("{0:N0}", thucThu);

				// ✅ Cập nhật tiêu đề form
				this.Text = $"Quản lý hóa đơn - Tìm thấy {lvHD.Items.Count} hóa đơn";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void lvHD_DoubleClick(object sender, EventArgs e)
		{
			if (lvHD.SelectedItems.Count > 0)
			{
				// Lấy mã hóa đơn (cột đầu tiên trong ListView)
				int maHD = int.Parse(lvHD.SelectedItems[0].SubItems[0].Text);

				// Truyền mã hóa đơn vào form chi tiết
				FormCT_HD formcthd = new FormCT_HD(maHD);

				// Mở form chi tiết
				formcthd.ShowDialog();
			}
		}

		private void lvHD_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
