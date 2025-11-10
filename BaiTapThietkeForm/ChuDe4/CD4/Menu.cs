using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4
{
    public  class Menu
    {
        public string TenMon {  get; set; }
        public int SL {  get; set; }
        public float DonGia {  get; set; }
        public float TT { get; set; }
        public Menu() { }
        public Menu(string tenMon, int sL,float donGia, float tT = 0)
        {
            this.TenMon = tenMon;
            this.SL = sL;
            this.DonGia = donGia;
            this.TT = tT;
        }
        public Menu(DataRow data)
        {
            TenMon = data["Ten"].ToString();
            SL = Convert.ToInt32(data["SoLuong"]);
            DonGia = Convert.ToSingle(data["DonGia"]);
            TT = Convert.ToSingle(data["TongTien"]);
            
        }


    }
}
