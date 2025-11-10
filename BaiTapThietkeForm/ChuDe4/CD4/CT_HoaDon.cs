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
    public partial class CT_HoaDon : Form
    {
        string connectionString = @"Data Source=DESKTOP-TLEVS6G\SQLEXPRESS01;Initial Catalog=Restauranmanagement;Integrated Security=True;";
        public CT_HoaDon()
        {
            InitializeComponent();
        }
        private int maHD;


        public CT_HoaDon(int maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }
        private void CT_HoaDon_Load(object sender, EventArgs e)
        {
            txtMaHD.Text = maHD.ToString();
            HienThiChiTietHoaDon(maHD);
        }
        private void HienThiChiTietHoaDon(int maHD)
        {
            lvCT_HoaDon.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT f.Ten, c.SoLuong, c.DonGia, (c.SoLuong * c.DonGia) AS ThanhTien
                    FROM CT_HoaDon c
                    JOIN Food f ON c.MaMon = f.MaMon
                    WHERE c.MaHD = @MaHD";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", maHD);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Ten"].ToString());
                    item.SubItems.Add(reader["SoLuong"].ToString());
                    item.SubItems.Add(reader["DonGia"].ToString());
                    item.SubItems.Add(reader["ThanhTien"].ToString());
                    lvCT_HoaDon.Items.Add(item);
                }

                reader.Close();
            }
        }
    }
}
