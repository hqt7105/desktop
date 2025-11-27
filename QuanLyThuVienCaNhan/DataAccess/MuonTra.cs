using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
namespace DataAccess
{
    public class MuonTra
    {
        public string MaGiaoDich { get; set; }  
        public string MaSach { get; set; }
        public string MaDocGia { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayTra { get; set; }
        public string LoaiGiaoDich { get; set; }    
        public int SoLuongGD { get; set; }
        public DateTime? NgayTraThucTe { get; set; }

        // Thuộc tính Join
        public string TenSach   { get; set; }
        public string TenDocGia { get; set; }
        public int SoLuongTonKho { get; set; }  
        public string LienLac { get; set; }
    }
    public class MuonTraDA
    {
        public List<MuonTra> GetByMaSach(string masach)
        {
            var list = new List<MuonTra>();

            using (var sqlConn = new SqlConnection(Ultilitie.ConnectionString))
            using (var command = sqlConn.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = Ultilitie.GiaoDich_GetByMaSach;
                command.Parameters.AddWithValue("@MaSach", masach ?? (object)DBNull.Value);

                sqlConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var muontra = new MuonTra();

                        muontra.MaGiaoDich = reader["MaGiaoDich"] as string;
                        muontra.MaSach = reader["MaSach"] as string;
                        muontra.MaDocGia = reader["MaDocGia"] as string;

                        if (!Convert.IsDBNull(reader["NgayMuon"]))
                            muontra.NgayMuon = Convert.ToDateTime(reader["NgayMuon"]);
                        if (!Convert.IsDBNull(reader["NgayTra"]))
                            muontra.NgayTra = Convert.ToDateTime(reader["NgayTra"]);

                        muontra.LoaiGiaoDich = reader["LoaiGiaoDich"] as string;

                        if (!Convert.IsDBNull(reader["SoLuongGD"]))
                            muontra.SoLuongGD = Convert.ToInt32(reader["SoLuongGD"]);

                        // Thuộc tính Join (kiểm tra DBNull trước khi gán)
                        muontra.TenSach = reader["TenSach"] as string;
                        muontra.TenDocGia = reader["TenDocGia"] as string;
                        if (!Convert.IsDBNull(reader["SoLuongTonKho"]))
                            muontra.SoLuongTonKho = Convert.ToInt32(reader["SoLuongTonKho"]);
                        muontra.LienLac = reader["LienLac"] as string;

                        list.Add(muontra);
                    }
                }
            }

            return list;
        }
        public MuonTra GetLatestActiveMuonTra(string maSach)
        {
            MuonTra activeGD = null;

            using (SqlConnection connection = new SqlConnection(Ultilitie.ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Ultilitie.GiaoDich_GetLatestActiveMuonTra;
                command.Parameters.AddWithValue("@MaSach", maSach);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ánh xạ dữ liệu từ DB sang Model MuonTra
                            activeGD = new MuonTra
                            {
                                MaGiaoDich = reader["MaGiaoDich"].ToString(),
                                MaSach = reader["MaSach"].ToString(),
                                MaDocGia = reader["MaDocGia"].ToString(),
                                NgayMuon = (DateTime)reader["NgayMuon"],
                                NgayTra = (DateTime)reader["NgayTra"], // Ngày hẹn trả
                                SoLuongGD = (int)reader["SoLuongGD"],
                                LoaiGiaoDich = reader["LoaiGD"] as string,

                                // === CẬP NHẬT/THÊM CÁC DÒNG NÀY ===
                                TenDocGia = reader["TenDocGia"].ToString(),
                                LienLac = reader["LienLac"].ToString(),
                                TenSach = reader["TenSach"].ToString()
                            };

                            // Đọc NgayTraThucTe (nếu có)
                            if (reader["NgayTraThucTe"] != DBNull.Value)
                            {
                                activeGD.NgayTraThucTe = (DateTime)reader["NgayTraThucTe"];
                            }
                            else
                            {
                                activeGD.NgayTraThucTe = null; // Quan trọng: set là null
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi
                    throw new Exception($"Lỗi khi lấy giao dịch active: {ex.Message}");
                }
            }
            return activeGD;
        }
        public int Insert_Update_Delete(MuonTra muontra, int action)
        {

            using (SqlConnection sqlConn = new SqlConnection(Ultilitie.ConnectionString))
            using (SqlCommand command = sqlConn.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GiaoDich_InsertUpdateDelete"; // (Tên SP)

                // 1. Tham số @MaGiaoDich (OUTPUT)
                SqlParameter IDPara = new SqlParameter("@MaGiaoDich", SqlDbType.NChar, 10);
                IDPara.Direction = ParameterDirection.InputOutput;
                command.Parameters.Add(IDPara);

                if (action == 0) // INSERT
                {
                    // Khi INSERT, SP sẽ tự tạo mã, ta truyền NULL
                    IDPara.Value = DBNull.Value;
                }
                else // UPDATE (1) or DELETE (2)
                {
                    IDPara.Value = muontra.MaGiaoDich;
                }

                // 2. Các tham số khác (Input)
                // (Giả định MaSach trong SP là nchar(5) để khớp với Bảng Sach)
                command.Parameters.Add("@MaSach", SqlDbType.NChar, 10).Value = muontra.MaSach;
                command.Parameters.Add("@MaDocGia", SqlDbType.NChar, 10).Value = muontra.MaDocGia;

                // Xử lý Nullable Date
                command.Parameters.Add("@NgayMuon", SqlDbType.Date).Value = muontra.NgayMuon;
                command.Parameters.Add("@NgayTra", SqlDbType.Date).Value = muontra.NgayTra; // Ngày hẹn trả

                // Xử lý Ngày Trả Thực Tế (có thể là NULL)
                SqlParameter ngayTraThucTeParam = new SqlParameter("@NgayTraThucTe", SqlDbType.Date);
                if (muontra.NgayTraThucTe.HasValue)
                {
                    ngayTraThucTeParam.Value = muontra.NgayTraThucTe.Value;
                }
                else
                {
                    ngayTraThucTeParam.Value = DBNull.Value;
                }
                command.Parameters.Add(ngayTraThucTeParam);

                // Ánh xạ LoaiGiaoDich (C#) sang @LoaiGiaoDich (SP)
                command.Parameters.Add("@LoaiGiaoDich", SqlDbType.NVarChar, 50).Value = muontra.LoaiGiaoDich;

                command.Parameters.Add("@SoLuongGD", SqlDbType.Int).Value = muontra.SoLuongGD;
                command.Parameters.Add("@Action", SqlDbType.Int).Value = action;

                // 3. Thực thi
                sqlConn.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    if (action == 0) // Nếu là INSERT thành công
                    {
                        
                        // vào lại đối tượng 'muontra'
                        muontra.MaGiaoDich = command.Parameters["@MaGiaoDich"].Value.ToString();
                    }
                    return result; // Trả về số dòng bị ảnh hưởng
                }
            }

            return 0; // Thất bại
        }
    }
}
