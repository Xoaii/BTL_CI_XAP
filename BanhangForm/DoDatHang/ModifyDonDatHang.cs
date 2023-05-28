using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BanhangForm.DoDatHang
{
    internal class ModifyDonDatHang
    {
        SqlDataAdapter dataAdapter;//truy xuat du lieu vao bang
        SqlCommand sqlCommand;//truy vấn cập nhật tới csdl
        public DataTable getAllDonDatHang()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from dondathang";
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
