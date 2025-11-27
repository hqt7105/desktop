using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class TacGiaBL
    {
        TacGiaDA tgda = new TacGiaDA();

        public List<TacGia> GetAll()
        {
            return tgda.GetAll();
        }

        public int Insert(TacGia tg)
        {
            return tgda.Insert_Update_Delete(tg, 0);
        }

        public int Update(TacGia tg)
        {
            return tgda.Insert_Update_Delete(tg, 1);
        }

        public int Delete(TacGia tg)
        {
            return tgda.Insert_Update_Delete(tg, 2);
        }
    }
}
