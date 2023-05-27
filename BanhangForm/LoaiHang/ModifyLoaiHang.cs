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
        public bool insert(QLloaiHang qLloaiHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "insert into loaihang values(@maloaihang,@tenloaihang)";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@maloaihang", SqlDbType.Int).Value = qLloaiHang.Maloaihang;
                sqlCommand.Parameters.Add("@tenloaihang", SqlDbType.NVarChar).Value = qLloaiHang.Tenloaihang;
                

                sqlCommand.ExecuteNonQuery();//thực thi lệnh truy vấn
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return true;
        }
    }
}
