using CD4.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4
{
  public    class TableDAO
    {
        private static TableDAO instance;
        public static int TableWidth = 50;
        public static int TableHeight = 50;
        public static TableDAO Instance
        {
            get { if (instance == null) { instance = new TableDAO(); } return TableDAO.instance; }
                private set { TableDAO.instance = value; }
        }
        private TableDAO() { }
        public List<Table> LoadTableList()
        {
            List<Table> tableLisst = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows) 
            {
                Table table = new Table(item);
                tableLisst.Add(table);
            }
            return tableLisst;
        }
    }
}
