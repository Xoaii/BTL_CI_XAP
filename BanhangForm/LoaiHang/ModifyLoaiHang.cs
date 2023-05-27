using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BanhangForm.DoDatHang;

namespace BanhangForm.LoaiHang
{
    internal class ModifyLoaiHang
    {
        SqlDataAdapter dataAdapter;//truy xuat du lieu vao bang
        SqlCommand sqlCommand;//truy vấn cập nhật tới csdl
        public DataTable getAllLoaiHang()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from loaihang";
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
