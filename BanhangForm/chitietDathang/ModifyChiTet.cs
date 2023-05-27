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
        public bool insert(QLchiTietDonHang qLchiTietDonHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "insert into chitietdathang values(@sohoadon,@mahang,@giaban,@soluong,@mucgiamgia)";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@sohoadon", SqlDbType.Int).Value = qLchiTietDonHang.SoHoaDon;
                sqlCommand.Parameters.Add("@mahang", SqlDbType.NVarChar).Value = qLchiTietDonHang.Mahang;
                sqlCommand.Parameters.Add("@giaban", SqlDbType.Money).Value = qLchiTietDonHang.Giaban;
                sqlCommand.Parameters.Add("@soluong", SqlDbType.SmallInt).Value = qLchiTietDonHang.SoLuong;
                sqlCommand.Parameters.Add("@mucgiamgia", SqlDbType.Real).Value = qLchiTietDonHang.Mucgiamgia;
                
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
        public bool Update(QLchiTietDonHang qLchiTietDonHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "update chitietdathang set mahang=@mahang,giaban=@giaban,soluong=@soluong,mucgiamgia=@mucgiamgia where sohoadon=@sohoadon";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@sohoadon", SqlDbType.Int).Value = qLchiTietDonHang.SoHoaDon;
                sqlCommand.Parameters.Add("@mahang", SqlDbType.NVarChar).Value = qLchiTietDonHang.Mahang;
                sqlCommand.Parameters.Add("@giaban", SqlDbType.Money).Value = qLchiTietDonHang.Giaban;
                sqlCommand.Parameters.Add("@soluong", SqlDbType.SmallInt).Value = qLchiTietDonHang.SoLuong;
                sqlCommand.Parameters.Add("@mucgiamgia", SqlDbType.Real).Value = qLchiTietDonHang.Mucgiamgia;

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
        public bool Delete(int sohoadon )
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "delete chitietdathang where sohoadon=@sohoadon";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@sohoadon", SqlDbType.Int).Value = sohoadon;
                

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
