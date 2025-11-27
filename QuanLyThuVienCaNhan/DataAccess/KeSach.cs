using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
namespace DataAccess
{
    public class KeSach
    {
        public string MaKeSach { get; set; }
        public string TenKeSach { get; set; }

    }
    public class KeSachDA
    {
        public List<KeSach> GetAll()
        {
            var list = new List<KeSach>();
            using (var sqlConn = new Microsoft.Data.SqlClient.SqlConnection(Ultilitie.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = Ultilitie.KeSach_GetAll;
                sqlConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var kesach = new KeSach
                        {
                            MaKeSach = reader["MaKeSach"] as string,
                            TenKeSach = reader["TenKeSach"] as string
                        };
                        list.Add(kesach);
                    }
                }
            }
            return list;
        }
        public int Insert_Update_Delete(KeSach ks, int action)
        {
            using (SqlConnection conn = new SqlConnection(Ultilitie.ConnectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Ultilitie.KeSach_InsertUpdateDelete;

                // 1. Tham số @MaKeSach (NChar(10) và cần là InputOutput để nhận mã mới khi INSERT)
                SqlParameter maKeSachParam = new SqlParameter("@MaKeSach", SqlDbType.NChar, 10);
                maKeSachParam.Direction = ParameterDirection.InputOutput;

                // Gán giá trị: NULL nếu Insert (action == 0) hoặc mã mới là NULL
                maKeSachParam.Value = string.IsNullOrEmpty(ks.MaKeSach) || action == 0
                    ? (object)DBNull.Value
                    : ks.MaKeSach.Trim();

                cmd.Parameters.Add(maKeSachParam);

                // 2. Tham số @TenKeSach (NVarChar(100) - giả định)
                cmd.Parameters.Add("@TenKeSach", SqlDbType.NVarChar, 100).Value =
                    ks.TenKeSach ?? (object)DBNull.Value; // Xử lý NULL

                // 3. Tham số @Action
                cmd.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                conn.Open();
                int result = cmd.ExecuteNonQuery();

                // Đọc lại giá trị mã Kệ Sách mới được tạo nếu là thao tác INSERT (action == 0)
                if (result > 0 && action == 0)
                {
                    ks.MaKeSach = maKeSachParam.Value.ToString().Trim();
                }

                return result;
            }
        }
    }
}
