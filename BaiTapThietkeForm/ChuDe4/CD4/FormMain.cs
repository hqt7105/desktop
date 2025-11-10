using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CD4.DTO;
using CD4.DAO;

namespace CD4
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            List<Table> tablelisst = TableDAO.Instance.LoadTableList();
            foreach (Table item in tablelisst)
            {
                Button btn = new Button()
                {
                    Width = TableDAO.TableWidth,
                    Height = TableDAO.TableHeight
                };

                string tenBan = item.TenBan?.Trim() ?? "";
                string trangThai = item.TrangThai?.Trim() ?? "Trống"; // nếu NULL thì coi như "Trống"

                btn.Text = tenBan + Environment.NewLine + trangThai;
                btn.Tag = item; // gắn dữ liệu bàn vào nút

                btn.Click += btn_click;

                // So sánh không phân biệt hoa thường, loại bỏ khoảng trắng
                string status = trangThai.ToLower();

                if (status == "trống")
                    btn.BackColor = Color.Aqua;
                else if (status == "có người")
                    btn.BackColor = Color.Yellow;
                else
                    btn.BackColor = Color.LightGray; // nếu dữ liệu khác

                flpBan.Controls.Add(btn);
            }
        }
        void ShowBill(int iD)
        {
            lvBan.Items.Clear();
            List<Menu> list = MenuDAO.Instance.LayMenu(iD);
            foreach(Menu item in list)
            {
                ListViewItem listban = new ListViewItem(item.TenMon.ToString());
                listban.SubItems.Add(item.SL.ToString());
                listban.SubItems.Add(item.DonGia.ToString());
                listban.SubItems.Add(item.TT.ToString());

                lvBan.Items.Add(listban);
            }
        }
        void btn_click(object sender, EventArgs e)
        {
            int tableID = (((sender as Button).Tag  as Table).MaBan);
            ShowBill(tableID);
        }

		private void button1_Click(object sender, EventArgs e)
		{
            Form1 form1 = new Form1();
            form1.ShowDialog();
		}

        private void flpBan_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
