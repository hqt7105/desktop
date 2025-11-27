    using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class TacGia
    {
        public string MaTacGia { get; set; }
        public string TenTacGia { get; set; }
    }
    public class TacGiaDA
    {
        public List<TacGia> GetAll()
        {
            List<TacGia> list = new List<TacGia>();
            using (SqlConnection conn = new SqlConnection(Ultilitie.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilitie.TacGia_GetAll;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TacGia tg = new TacGia();
                    tg.MaTacGia = reader["MaTacGia"].ToString();
                    tg.TenTacGia = reader["TenTacGia"].ToString();
                    list.Add(tg);
                }
            }
            return list;
        }
        public int Insert_Update_Delete(TacGia tg, int action)
        {
            using (SqlConnection conn = new SqlConnection(Ultilitie.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilitie.TacGia_InsertUpdateDelete;

                cmd.Parameters.Add("@MaTacGia", SqlDbType.NChar, 10).Value =
                    string.IsNullOrWhiteSpace(tg.MaTacGia) ? (object)DBNull.Value : tg.MaTacGia.Trim();

                cmd.Parameters.Add("@TenTacGia", SqlDbType.NVarChar, 100).Value =
                    tg.TenTacGia ?? (object)DBNull.Value;

                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
