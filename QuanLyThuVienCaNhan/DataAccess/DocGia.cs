using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class DocGia
    {
        public string MaDocGia { get; set; }
        public string TenDocGia { get; set; }
        public string LienLac { get; set; }
    }
    public class DocGiaDA
    {
        public List<DocGia> GetAll()
        {
            var list = new List<DocGia>();
            using (var sqlConn = new SqlConnection(Ultilitie.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Ultilitie.DocGia_GetAll;

                sqlConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dg = new DocGia();
                        dg.MaDocGia = reader["MaDocGia"] as string;
                        dg.TenDocGia = reader["TenDocGia"] as string;
                        dg.LienLac = reader["LienLac"] as string;
                        list.Add(dg);
                    }
                }
            }
            return list;
        }
        public int Insert_Update_Delete(DocGia dg, int action)
        {
            using (SqlConnection conn = new SqlConnection(Ultilitie.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilitie.DocGia_InsertUpdateDelete;

                // Kiểm tra null trước khi Trim
                if (string.IsNullOrEmpty(dg.MaDocGia))
                {
                    cmd.Parameters.Add("@MaDocGia", SqlDbType.NChar, 10).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@MaDocGia", SqlDbType.NChar, 10).Value = dg.MaDocGia.Trim();
                }

                cmd.Parameters.Add("@TenDocGia", SqlDbType.NVarChar, 100).Value = dg.TenDocGia ?? (object)DBNull.Value;
                cmd.Parameters.Add("@LienLac", SqlDbType.NVarChar, 100).Value = dg.LienLac ?? (object)DBNull.Value;
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
