using DataAccess;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    public class TaiKhoanBL
    {
        public TaiKhoanBL() { }

        public TaiKhoanDTO Login(string tenTaiKhoan, string matKhau)
        {
            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(matKhau))
                return null;

            TaiKhoanDTO taiKhoan = new TaiKhoan().GetAccount(tenTaiKhoan);
            if (taiKhoan == null) return null;

            return Hashing.VerifyPassword(matKhau, taiKhoan.MatKhau) ? taiKhoan : null;
        }


        private TaiKhoan taiKhoan = new TaiKhoan();
        public bool Register(string tenTaiKhoan, string matKhau, string tenHienThi)
        {
            if (string.IsNullOrWhiteSpace(tenTaiKhoan) || string.IsNullOrWhiteSpace(matKhau))
            {
                throw new Exception("Tên tài khoản và mật khẩu không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(tenHienThi))
            {
                tenHienThi = tenTaiKhoan;
            }

            // Gọi hàm Hashing (bản dự phòng)
            string matKhauDaLuu = Hashing.HashPassword(matKhau);

            // Gọi hàm DA (vừa thêm ở Bước 2)
            int result = taiKhoan.Insert(tenTaiKhoan, matKhauDaLuu, tenHienThi);

            return (result > 0);
        }


    } // <-- SỬA LỖI: Đảm bảo dấu } của CLASS TaiKhoanBL còn
}
