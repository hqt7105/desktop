using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class Ultilitie
    {
        private static string Strname = "ConnectionStringName";
        public static string ConnectionString = ConfigurationManager.ConnectionStrings[Strname].ConnectionString;
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static string Sach_GetAll = "Sach_GetAll";
        public static string Sach_InsertUpdateDelete = "Sach_InsertUpdateDelete";
        public static string Sach_GetByTacGia = "Sach_GetByTacGia";
        public static string Sach_GetByMaSach = "Sach_GetByMaSach";
        public static string MuonTra_GetByDocGia = "MuonTra_GetByDocGia";
        public static string GiaoDich_GetByMaSach = "GiaoDich_GetByMaSach";
        public static string GiaoDich_GetLatestActiveMuonTra = "GiaoDich_GetLatestActiveMuonTra";
        public static string DocGia_GetAll = "DocGia_GetAll";
        public static string GiaoDich_InsertUpdateDelete = "GiaoDich_InsertUpdateDelete";
        public static string NhaXuatBan_GetAll = "NhaXuatBan_GetAll";
        public static string NhaXuatBan_InsertUpdateDelete = "NhaXuatBan_InsertUpdateDelete";
        public static string ChuDe_GetAll = "ChuDe_GetAll";
        public static string KeSach_GetAll = "KeSach_GetAll";   
        public static string KeSach_InsertUpdateDelete = "KeSach_InsertUpdateDelete";
        public static string TacGia_GetAll = "TacGia_GetAll";
        public static string TacGia_InsertUpdateDelete = "TacGia_InsertUpdateDelete";

        public static string ChuDe_InsertUpdateDelete = "ChuDe_InsertUpdateDelete";
        public static string DocGia_InsertUpdateDelete = "DocGia_InsertUpdateDelete";
    }
}
