
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaCungCap
{
    public partial class MyForm : Form
    {
        string filename = "NhaCungCap.txt";
        public MyForm()
        {
            InitializeComponent();
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
         

        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtSoDT.Text = "";
            txtMoTa.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
    }
}
