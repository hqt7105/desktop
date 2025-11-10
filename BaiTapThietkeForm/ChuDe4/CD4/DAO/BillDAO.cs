using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD4.DTO;

namespace CD4.DAO
{
    public  class BillDAO
    {
        public static BillDAO instance;
        public  static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            set { BillDAO.instance = value; }
        }
        public  BillDAO() { }

        public int Lay(int iD)
        {
            string query = "select* from dbo.HoaDon where MaBan = \" + iD + \" and TrangThai = N'Trống'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.MaBan;
            }
            return -1;
        }

    }
}
