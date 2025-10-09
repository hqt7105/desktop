using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaCungCap
{
    internal class NhaCungCap
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string SODT { get; set; }
        public string MoTa { get; set; }
        public NhaCungCap(string ma, string ten, string diaChi, string soDT, string moTa)
        {
            this.Ma = ma;
            this.Ten = ten;
            this.DiaChi = diaChi;
            this.SODT = soDT;
            this.MoTa = moTa;
        }
        public NhaCungCap() { }
        public static NhaCungCap Parse(string line)
        {
            var parts = line.Split('|');
            string[] mon = parts.Length > 9 ? parts[9].Split(',') : Array.Empty<string>();
            return new NhaCungCap(
                parts[0],
                parts[1],
                parts[2],
                parts[3],
                parts[4]


            ); ;
        }
    }
}
