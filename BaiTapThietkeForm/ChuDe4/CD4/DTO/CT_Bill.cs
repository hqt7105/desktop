using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4.DTO
{
    public  class CT_Bill
    {
        public int IDHD {  get; set; }
        public int IDFood {  get; set; }
        public string SL {  get; set; }
        public float DonGia {  get; set; }
        public CT_Bill() { }
        public CT_Bill(int iDHD, int iDFood, string sL, float donGia)
        {
            IDHD = iDHD;
            IDFood = iDFood;
            SL = sL;
            DonGia = donGia;
        }
        public CT_Bill(DataRow data)
        {
            IDHD = Convert.ToInt32(data["MaHD"]);
            IDFood = Convert.ToInt32(data["MaMon"]);
            SL = data["SoLuong"].ToString();
            DonGia = Convert.ToSingle(data["DonGia"]);
        }
    }
}
