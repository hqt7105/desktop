using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4
{
    public  class Table
    {
        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public string TrangThai {  get; set; }
        public string GhiChu {  get; set; }
        public Table(int maBan, string tenBan, string trangThai, string ghiChu)
        {
            this.MaBan = maBan;
            this.TenBan = tenBan;
            this.TrangThai = trangThai;
            this.GhiChu = ghiChu;
        }

        public Table (DataRow row)
        {
            this.MaBan = (int)row["MaBan"];
            this.TenBan = row["TenBan"].ToString();
            this.TrangThai = row["TrangThai"].ToString();
            this.GhiChu = row["GhiChu"].ToString();

        }
    }
}
