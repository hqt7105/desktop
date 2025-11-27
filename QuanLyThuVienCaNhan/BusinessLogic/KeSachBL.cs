using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class KeSachBL
    {
        public KeSachDA ksda = new KeSachDA();
        public List<KeSach> GetAll()
        {
            return ksda.GetAll();
        }
        public int Insert(KeSach ks)
        {
            return ksda.Insert_Update_Delete(ks, 0);
        }

        public int Update(KeSach ks)
        {
            return ksda.Insert_Update_Delete(ks, 1);
        }

        public int Delete(KeSach ks)
        {
            return ksda.Insert_Update_Delete(ks, 2);
        }
    }
}
