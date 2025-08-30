using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapWindowsForm
{
    public partial class frmBai2 : Form
    {
        public frmBai2()
        {
            InitializeComponent();
        }

        private void cbbTenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var stt = cbbTenHang.SelectedIndex; 
            switch(stt)
            {
                case 0:
                    txtDonGia.Text = "100000";
                    break;
                case 1:
                    txtDonGia.Text = "200000";
                    break;
                case 2:
                    txtDonGia.Text = "150000";
                    break;

            }
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            int gia = int.Parse(txtDonGia.Text);
            int soLuong = int.Parse(txtSoLuong.Text);
            double thanhTien = gia * soLuong;

            if (rdChuyenKhoan.Checked)
                thanhTien = gia * soLuong *0.95;
            lblThongBao.Text = thanhTien.ToString();
        }
    }
}
