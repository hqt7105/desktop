using DataAccess; // Cần 'using DTO'

namespace BusinessLogic
{
    /// <summary>
    /// Lớp lưu phiên đăng nhập (Đã sửa lỗi 'public')
    /// </summary>
    public static class PhienDangNhap
    {
        public static TaiKhoanDTO NguoiDungHienTai { get; private set; }

        public static void DangNhap(TaiKhoanDTO taiKhoan)
        {
            NguoiDungHienTai = taiKhoan;
        }

        public static void DangXuat()
        {
            NguoiDungHienTai = null;
        }
    }
}