using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD4.DTO;

namespace CD4.DAO
{
    public  class CT_BillDAO
    {
        public  static CT_BillDAO instance;
        public  static CT_BillDAO Instance
        {
            get { if (instance == null) instance = new CT_BillDAO(); return CT_BillDAO.instance; }
            set { CT_BillDAO.instance = value; }
        }
        public  CT_BillDAO() { }
        public List<CT_Bill> LayBill(int id)
        {
            string query = "select * from dbo.CT_HoaDon where MaHD =";
            List<CT_Bill> list = new List<CT_Bill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query + id);
            foreach (DataRow item in data.Rows)
            {
                CT_Bill cthd = new CT_Bill(item);
                list.Add(cthd);
            }
            return list;
        }

    }
}
