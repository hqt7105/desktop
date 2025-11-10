using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4.DTO
{
    public  class Bill
    {
        public int MaHD {  get; set; }
        public DateTime NgayLapHD {  get; set; }

        public int MaBan {  get; set; }
        public float TongTien {  get; set; }
        public string TrangThai { get; set; }
        public Bill() { }
        public Bill(int maHD, DateTime ngayLapHD, int maBan, float tongTien, string trangThai)
        {
            MaHD = maHD;
            NgayLapHD = ngayLapHD;
            MaBan = maBan;
            TongTien = tongTien;
            TrangThai = trangThai;
        }
        public Bill (DataRow data)
        {
                MaHD = Convert.ToInt32(data["MaHD"]);
                NgayLapHD = Convert.ToDateTime(data["NgayLapHD"]);
                MaBan = Convert.ToInt32(data["MaBan"]);
                TongTien = Convert.ToSingle(data["TongTien"]);
                TrangThai = data["TrangThai"].ToString();
        }
    }
}
