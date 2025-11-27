using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DocGiaBL
    {
        DocGiaDA dgda = new DocGiaDA();

        public List<DocGia> GetAll()
        {
            return dgda.GetAll();
        }
        public int Insert(DocGia dg)
        {
            return dgda.Insert_Update_Delete(dg, 0);
        }

        public int Update(DocGia dg)
        {
            return dgda.Insert_Update_Delete(dg, 1);
        }

        public int Delete(DocGia dg)
        {
            return dgda.Insert_Update_Delete(dg, 2);
        }
    }
}
