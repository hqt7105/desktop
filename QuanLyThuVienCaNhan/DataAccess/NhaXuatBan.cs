using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class NhaXuatBan
    {
        public string MaNXB { get; set; }
        public string TenNXB { get; set; }
    }
    public class NhaXuatBanDA
    {
        public List<NhaXuatBan> GetAll()
        {
            var list = new List<NhaXuatBan>();
            using (var sqlConn = new Microsoft.Data.SqlClient.SqlConnection(Ultilitie.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = Ultilitie.NhaXuatBan_GetAll;
                sqlConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nxb = new NhaXuatBan
                        {
                            MaNXB = reader["MaNXB"] as string,
                            TenNXB = reader["TenNXB"] as string
                        };
                        list.Add(nxb);
                    }
                }
            }
            return list;
        }
        public int Insert_Update_Delete(NhaXuatBan nxb, int action)
        {
            using (SqlConnection conn = new SqlConnection(Ultilitie.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilitie.NhaXuatBan_InsertUpdateDelete;

                // Kiểm tra null trước khi Trim
                if (string.IsNullOrEmpty(nxb.MaNXB))
                {
                    cmd.Parameters.Add("@MaNXB", SqlDbType.NChar, 10).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@MaNXB", SqlDbType.NChar, 10).Value = nxb.MaNXB.Trim();
                }

                cmd.Parameters.Add("@TenNXB", SqlDbType.NVarChar, 100).Value = nxb.TenNXB ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
