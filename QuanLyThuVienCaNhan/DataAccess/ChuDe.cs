using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class ChuDe
    {
        public string MaChuDe { get; set; }
        public string TenChuDe { get; set; }
    }
    public class ChuDeDA
    {
        public List<ChuDe> GetAll()
        {
            var list = new List<ChuDe>();
            using (var sqlConn = new Microsoft.Data.SqlClient.SqlConnection(Ultilitie.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = Ultilitie.ChuDe_GetAll;
                sqlConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var chude = new ChuDe
                        {
                            MaChuDe = reader["MaChuDe"] as string,
                            TenChuDe = reader["TenChuDe"] as string
                        };
                        list.Add(chude);
                    }
                }
            }
            return list;
        }
        public int Insert_Update_Delete(ChuDe cd, int action)
        {
            using (SqlConnection conn = new SqlConnection(Ultilitie.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilitie.ChuDe_InsertUpdateDelete;

                // Xử lý MaChuDe NULL khi thêm
                if (cd.MaChuDe == null)
                    cmd.Parameters.Add("@MaChuDe", SqlDbType.NChar, 10).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@MaChuDe", SqlDbType.NChar, 10).Value = cd.MaChuDe.Trim();

                // TenChuDe có thể null
                cmd.Parameters.Add("@TenChuDe", SqlDbType.NVarChar, 100).Value =
                    cd.TenChuDe ?? (object)DBNull.Value;

                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
