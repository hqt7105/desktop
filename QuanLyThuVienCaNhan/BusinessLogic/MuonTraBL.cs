using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class MuonTraBL
    {
        
        MuonTraDA muonTraDA = new MuonTraDA();
        public List<MuonTra> GetByMaSach(string maSach)
        {
            return muonTraDA.GetByMaSach(maSach);
        }
        public MuonTra GetLatestActiveMuonTra(string maSach)
        {
            return muonTraDA.GetLatestActiveMuonTra(maSach);
        }
        public int Insert(MuonTra muonTra)
        {
            return muonTraDA.Insert_Update_Delete(muonTra, 0);
        }
        public int Update(MuonTra muonTra)
        {
            return muonTraDA.Insert_Update_Delete(muonTra, 1);
        }
        public int Delete(MuonTra muonTra)
        {
            return muonTraDA.Insert_Update_Delete(muonTra, 2);
        }
    }
    
}
