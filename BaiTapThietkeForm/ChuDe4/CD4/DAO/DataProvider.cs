using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD4.DAO
{
    public  class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        // 🔹 Chuỗi kết nối tới SQL Server
        private string connectionString = @"Data Source=DESKTOP-TLEVS6G\SQLEXPRESS01;Initial Catalog=Restauranmanagement;Integrated Security=True;";

        // 🔹 Constructor private để không tạo trực tiếp từ bên ngoài
        private DataProvider() { }

        // 🔹 Hàm thực thi câu truy vấn SQL và trả về DataTable
        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(data);

                sqlConnection.Close();
            }

            return data;
        }
    }
}
