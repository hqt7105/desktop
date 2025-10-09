
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace QuanLyNhaCungCap
{
    internal class QuanLyNhaCungCap
    {
        public List<NhaCungCap> dssv = new List<NhaCungCap>();
        public void DocFile(string filename)
        {
         
            dssv.Clear();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        dssv.Add(NhaCungCap.Parse(line));
                    }
                }
            }
        }

        public void GhiFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (var sv in dssv)
                {
                    sw.WriteLine(sv.ToString());
                }
            }
        }

        public void ChenVaoFile(string filePath, NhaCungCap ncc)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true)) // true = ghi tiếp (append)
            {
                sw.WriteLine(ncc.ToString());
            }

            // Đồng thời thêm vào danh sách trong bộ nhớ
            dssv.Add(ncc);
        }
    }
}
