using BanhangForm.KhachHang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm.chitietDathang
{
    internal class ModifyChiTet
    {
        SqlDataAdapter dataAdapter;//truy xuat du lieu vao bang
        SqlCommand sqlCommand;//truy vấn cập nhật tới csdl
        public DataTable getAllchiTiet()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from chitietdathang";
            using (SqlConnection sqlConnection = connect.GetConnection())
            {
                sqlConnection.Open();//mở kết nối

                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                //truy xuất 
                dataAdapter.Fill(dataTable);

                sqlConnection.Close();
            }
            return dataTable;
        }
        
        
    }
}
