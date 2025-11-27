namespace BusinessLogic
{
    /// <summary>
    /// Lớp Hashing (Đã sửa lỗi 'public' và 'static')
    /// </summary>
    public class Hashing
    {
        public static string HashPassword(string plainPassword)
        {
            // Chỉ trả về mật khẩu gốc
            return plainPassword;
        }
        public static bool VerifyPassword(string plainPassword, string hashedPasswordFromDB)
        {
            // (Code giả lập, KHÔNG an toàn)
            return plainPassword == hashedPasswordFromDB;
        }
    }
}