using Microsoft.Data.SqlClient;
using System.Data;
using System;


namespace DataAccess
{
    public class TaiKhoanDTO
    {
        public int UserID { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string TenHienThi { get; set; }
    }
    public class TaiKhoan
    {

        // Hàm này phải là 'public'
        public TaiKhoanDTO GetAccount(string tenTaiKhoan)
        {
            TaiKhoanDTO taiKhoan = null;

            // === NÂNG CẤP: Thêm UserID vào câu SELECT ===
            string query = "SELECT UserID, TenTaiKhoan, MatKhau, TenHienThi FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan";

            using (SqlConnection conn = Ultilitie.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar).Value = tenTaiKhoan;
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                taiKhoan = new TaiKhoanDTO();

                                // === NÂNG CẤP: Đọc UserID từ CSDL ===
                                taiKhoan.UserID = (int)reader["UserID"];

                                taiKhoan.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                                taiKhoan.MatKhau = reader["MatKhau"].ToString();
                                taiKhoan.TenHienThi = reader["TenHienThi"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi DA: " + ex.Message);
                    }
                }
            }
            return taiKhoan; // Trả về null nếu không tìm thấy
        }
        public int Insert(string tenTaiKhoan, string matKhauDaLuu, string tenHienThi)
        {
            using (SqlConnection conn = Ultilitie.GetConnection())
            {
                // Gọi SP TaiKhoan_Insert (mới)
                using (SqlCommand cmd = new SqlCommand("TaiKhoan_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 100).Value = tenTaiKhoan;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = matKhauDaLuu;
                    cmd.Parameters.Add("@TenHienThi", SqlDbType.NVarChar, 100).Value = tenHienThi;

                    try
                    {
                        conn.Open();
                        // SP này trả về 0 (trùng) hoặc 1 (thành công)
                        object result = cmd.ExecuteScalar();
                        return (result != null) ? Convert.ToInt32(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi DA khi Insert: " + ex.Message);
                    }
                }
            }
        }
    }
}