using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4.DAO
{
    public  class MenuDAO
    {
        public static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            set { MenuDAO.instance = value; }
        }
        public MenuDAO() { }
        public List<Menu> LayMenu(int iD)
        {
            List<Menu > list = new List<Menu>();
            string query = string.Format(@"select a.Ten, b.SoLuong, a.DonGia, b.SoLuong*a.DonGia as TongTien
                            from dbo.Food a, dbo.CT_HoaDon b, dbo.Ban c, dbo.HoaDon d
                            where a.MaMon = b.MaMon and c.MaBan = d.MaBan and b.MaHD = d.MaHD and c.MaBan = {0}", iD);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                list.Add(menu);
            }
            return list;
        }
    }
}
