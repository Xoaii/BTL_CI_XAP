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
        public bool insert(QLdonDatHang qLdonDatHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "insert into dondathang values(@sohoadon,@makhachhang,@manhanvien,@ngaydathang,@ngaygiaohang,@noigiaohang)";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@sohoadon", SqlDbType.Int).Value = qLdonDatHang.Sohoadon;
                sqlCommand.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = qLdonDatHang.Makhachhang;
                sqlCommand.Parameters.Add("@manhanvien", SqlDbType.NVarChar).Value = qLdonDatHang.Manhanvien;
                sqlCommand.Parameters.Add("@ngaydathang", SqlDbType.Date).Value = qLdonDatHang.Ngaydathang.ToShortDateString();
                sqlCommand.Parameters.Add("@ngaygiaohang", SqlDbType.Date).Value = qLdonDatHang.Ngaygiaohang.ToLongDateString();
                sqlCommand.Parameters.Add("@noigiaohang", SqlDbType.NVarChar).Value = qLdonDatHang.Noigiaohang;

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
        public bool Update(QLdonDatHang qLdonDatHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "update dondathang set makhachhang=@makhachhang,manhanvien=@manhanvien,ngaydathang=@ngaydathang,ngaygiaohang=@ngaygiaohang,noigiaohang=@noigiaohang where sohoadon=@sohoadon";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@sohoadon", SqlDbType.Int).Value = qLdonDatHang.Sohoadon;
                sqlCommand.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = qLdonDatHang.Makhachhang;
                sqlCommand.Parameters.Add("@manhanvien", SqlDbType.NVarChar).Value = qLdonDatHang.Manhanvien;
                sqlCommand.Parameters.Add("@ngaydathang", SqlDbType.Date).Value = qLdonDatHang.Ngaydathang.ToShortDateString();
                sqlCommand.Parameters.Add("@ngaygiaohang", SqlDbType.Date).Value = qLdonDatHang.Ngaygiaohang.ToLongDateString();
                sqlCommand.Parameters.Add("@noigiaohang", SqlDbType.NVarChar).Value = qLdonDatHang.Noigiaohang;

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
        public bool Delete(int sohoadon)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "delete dondathang where sohoadon=@sohoadon";

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
