using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class SachBL
    {
        SachDA sachDA = new SachDA();
        public List<Sach> GetAll()
        {
            return sachDA.GetAll();
        }
        public int Insert(Sach sach)
        {
            return sachDA.Insert_Update_Delete(sach, 0);
        }
        public int Update(Sach sach)
        {
            return sachDA.Insert_Update_Delete(sach, 1);
        }   
        public int Delete(Sach sach)
        {
            return sachDA.Insert_Update_Delete(sach, 2);
        }   
        public Sach GetByMaSach(string maSach)
        {
            return sachDA.GetByMaSach(maSach);
        }
    }
}
