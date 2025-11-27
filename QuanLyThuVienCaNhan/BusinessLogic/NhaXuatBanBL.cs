using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class NhaXuatBanBL
    {
        NhaXuatBanDA nhaXuatBanDA = new NhaXuatBanDA();
        public List<NhaXuatBan> GetAll()
        {
            return nhaXuatBanDA.GetAll();
        }
        

        public int Insert(NhaXuatBan nxb)
        {
            return nhaXuatBanDA.Insert_Update_Delete(nxb, 0);
        }

        public int Update(NhaXuatBan nxb)
        {
            return nhaXuatBanDA.Insert_Update_Delete(nxb, 1);
        }

        public int Delete(NhaXuatBan nxb)
        {
            return nhaXuatBanDA.Insert_Update_Delete(nxb, 2);
        }
    }
}
