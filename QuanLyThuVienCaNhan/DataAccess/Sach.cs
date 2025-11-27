using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccess
{
    public class Sach
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string MaNXB { get; set; }
        public string MaChuDe { get; set; }
        public string TenChuDe { get; set; }
        public string TenNXB { get; set; }  
        public string TenTacGia { get; set; }
        public string MaTacGia { get; set; }    
        public int NamXuatBan { get; set; }
        public string MaKeSach { get; set; }
        public string ViTri { get; set; }
        public string TinhTrang { get; set; }   
        public string MoTa { get; set; }
        public int SoLuong { get; set; }
    }
    public class SachDA 
    {

        public List<Sach> GetAll()
        {
            // (Dùng SP Sach_GetByTacGia)
            SqlConnection sqlConn = new SqlConnection(Ultilitie.ConnectionString);
            sqlConn.Open();
            SqlCommand command = sqlConn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Ultilitie.Sach_GetByTacGia;

            SqlDataReader reader = command.ExecuteReader();
            List<Sach> list = new List<Sach>();

            while (reader.Read())
            {
                Sach sach = new Sach();
                sach.MaSach = reader["MaSach"].ToString();
                sach.TenSach = reader["TenSach"].ToString();
                sach.TenNXB = reader["NhaXuatBan"] as string;
                sach.TenChuDe = reader["ChuDe"] as string;
                sach.TenTacGia = reader["TacGia"] as string;
                sach.NamXuatBan = Convert.ToInt32(reader["NamXB"]);
                sach.ViTri = reader["ViTri"].ToString();
                sach.TinhTrang = reader["TinhTrang"].ToString();
                sach.MoTa = reader["MoTa"].ToString();
                sach.SoLuong = Convert.ToInt32(reader["SoLuong"]);

                sach.MaTacGia = reader["MaTacGia"] as string;
                sach.MaNXB = reader["MaNXB"] as string;
                sach.MaChuDe = reader["MaChuDe"] as string;
                sach.MaKeSach = reader["MaKeSach"] as string;

                list.Add(sach);
            }
            sqlConn.Close();
            return list;
        }
        public Sach GetByMaSach(string maSach)
        {
            Sach sach = null;
            SqlConnection sqlconn = new SqlConnection(Ultilitie.ConnectionString);
            sqlconn.Open();
            SqlCommand command = sqlconn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Ultilitie.Sach_GetByMaSach;

            // Thêm tham số @MaSach cho SP
            command.Parameters.AddWithValue("@MaSach", maSach);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read()) 
            {
                sach = new Sach();
                sach.MaSach = reader["MaSach"].ToString();
                sach.TenSach = reader["TenSach"].ToString();
                sach.TenNXB = reader["NhaXuatBan"] as string;
                sach.TenChuDe = reader["ChuDe"] as string;
                sach.TenTacGia = reader["TacGia"] as string;
                sach.NamXuatBan = Convert.ToInt32(reader["NamXB"]);
                sach.ViTri = reader["ViTri"].ToString();
                sach.TinhTrang = reader["TinhTrang"].ToString();
                sach.MoTa = reader["MoTa"].ToString();
                sach.SoLuong = Convert.ToInt32(reader["SoLuong"]);

                sach.MaTacGia = reader["MaTacGia"] as string;
                sach.MaNXB = reader["MaNXB"] as string;
                sach.MaChuDe = reader["MaChuDe"] as string;
                sach.MaKeSach = reader["MaKeSach"] as string;
                
            }
            sqlconn.Close();
            return sach;
        }
        public int Insert_Update_Delete(Sach sach, int action)
        {
            using (SqlConnection sqlConn = new SqlConnection(Ultilitie.ConnectionString))
            using (SqlCommand command = sqlConn.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Ultilitie.Sach_InsertUpdateDelete;

                
                SqlParameter IDPara = new SqlParameter("@MaSach", SqlDbType.NChar, 10);
                IDPara.Direction = ParameterDirection.InputOutput;
                command.Parameters.Add(IDPara);

                if (action == 0) // INSERT
                {
                    IDPara.Value = DBNull.Value;
                }
                else // UPDATE (1) or DELETE (2)
                {
                    IDPara.Value = sach.MaSach;
                }

                // 2. Các tham số khác (Gửi DBNull.Value nếu bị null)
                command.Parameters.Add("@TenSach", SqlDbType.NVarChar, 200).Value = (object)sach.TenSach ?? DBNull.Value;

                
                command.Parameters.Add("@MaNXB", SqlDbType.NChar, 10).Value = (object)sach.MaNXB ?? DBNull.Value;
                command.Parameters.Add("@MaChuDe", SqlDbType.NChar, 10).Value = (object)sach.MaChuDe ?? DBNull.Value;
                command.Parameters.Add("@MaKeSach",SqlDbType.NChar,10).Value = (object)sach.MaKeSach ?? DBNull.Value;
                command.Parameters.Add("@NamXuatBan", SqlDbType.Int).Value = sach.NamXuatBan;
                command.Parameters.Add("@MaTacGia", SqlDbType.NChar, 10).Value = (object)sach.MaTacGia ?? DBNull.Value;
                command.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 50).Value = (object)sach.TinhTrang ?? DBNull.Value;
                command.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value = (object)sach.MoTa ?? DBNull.Value; // -1 = MAX
                command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = sach.SoLuong;
                command.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                // 3. Thực thi
                sqlConn.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    if (action == 0)
                    {
                        sach.MaSach = command.Parameters["@MaSach"].Value.ToString();
                    }
                    return result;
                }
            }

            return 0; // Thất bại
        }
    }
}

