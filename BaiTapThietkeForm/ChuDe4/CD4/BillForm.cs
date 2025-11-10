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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CD4
{
    public partial class BillForm : Form
    {
        string connectionString = @"Data Source=DESKTOP-TLEVS6G\SQLEXPRESS01;Initial Catalog=Restauranmanagement;Integrated Security=True;";
        public BillForm()
        {
            InitializeComponent();
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            HienThiDanhSachHoaDon();
        }
        private void HienThiDanhSachHoaDon()
        {
			lvBill.Items.Clear(); // Xóa dữ liệu cũ

			DateTime tuNgay = TuNgay.Value.Date;
			DateTime denNgay = DenNgay.Value.Date;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				string query = @"
            SELECT 
                hd.MaHD, 
                hd.NgayLapHD, 
                hd.MaBan, 
                ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongTien,
                hd.TrangThai
            FROM HoaDon hd
            LEFT JOIN CT_HoaDon ct ON hd.MaHD = ct.MaHD
            WHERE hd.NgayLapHD BETWEEN @TuNgay AND @DenNgay
            GROUP BY hd.MaHD, hd.NgayLapHD, hd.MaBan, hd.TrangThai";

				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
				cmd.Parameters.AddWithValue("@DenNgay", denNgay);

				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					ListViewItem item = new ListViewItem(reader["MaHD"].ToString());
					item.SubItems.Add(Convert.ToDateTime(reader["NgayLapHD"]).ToString("dd/MM/yyyy"));
					item.SubItems.Add(reader["MaBan"].ToString());
					item.SubItems.Add(reader["TongTien"].ToString());
					item.SubItems.Add(reader["TrangThai"].ToString());

					lvBill.Items.Add(item);
				}

				reader.Close();
			}
		}

        private void TuNgay_ValueChanged(object sender, EventArgs e)
        {
            HienThiDanhSachHoaDon();
        }

        private void DenNgay_ValueChanged(object sender, EventArgs e)
        {
            HienThiDanhSachHoaDon();
        }

        private void lvBill_DoubleClick(object sender, EventArgs e)
        {
            if (lvBill.SelectedItems.Count > 0)
            {
                // Lấy mã hóa đơn từ cột đầu tiên
                int maHD = int.Parse(lvBill.SelectedItems[0].SubItems[0].Text);

                // Mở form chi tiết và truyền mã hóa đơn
                CT_HoaDon frm = new CT_HoaDon(maHD);
                frm.ShowDialog();
            }
        }
    }
}
