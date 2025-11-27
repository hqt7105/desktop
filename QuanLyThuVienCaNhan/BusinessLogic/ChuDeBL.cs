using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ChuDeBL
    {
        ChuDeDA chuDeDA = new ChuDeDA();
        public List<ChuDe> GetAll()
        {
            return chuDeDA.GetAll();
        }
        public int Insert(ChuDe cd)
        {
            return chuDeDA.Insert_Update_Delete(cd, 0);
        }

        public int Update(ChuDe cd)
        {
            return chuDeDA.Insert_Update_Delete(cd, 1);
        }

        public int Delete(ChuDe cd)
        {
            return chuDeDA.Insert_Update_Delete(cd, 2);
        }
    }
}
