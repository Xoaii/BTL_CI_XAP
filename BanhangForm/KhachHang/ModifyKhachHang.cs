using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BanhangForm.KhachHang
{
    internal class ModifyKhachHang
    {
        SqlDataAdapter dataAdapter;//truy xuat du lieu vao bang
        SqlCommand sqlCommand;//truy vấn cập nhật tới csdl

        public ModifyKhachHang()
        {
        }

       public DataTable getAllKhachhang()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from khachhang";
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
        public bool insert(QLkhachHang qLkhachHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "insert into khachhang values(@makhachhang,@Tencongty,@tengiaodich,@diachi,@email,@dienthoai,@fax,@TenKhachHang)";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = qLkhachHang.MaKhachHang;
                sqlCommand.Parameters.Add("@tencongty", SqlDbType.NVarChar).Value = qLkhachHang.TenCongTy1;
                sqlCommand.Parameters.Add("@tengiaodich", SqlDbType.NVarChar).Value = qLkhachHang.TenGiaoDich;
                sqlCommand.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = qLkhachHang.DiaChi;
                sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = qLkhachHang.Email;
                sqlCommand.Parameters.Add("@dienthoai", SqlDbType.NVarChar).Value = qLkhachHang.DienThoai;
                sqlCommand.Parameters.Add("@fax", SqlDbType.NVarChar).Value = qLkhachHang.Fax;
                sqlCommand.Parameters.Add("@TenKhachHang", SqlDbType.NVarChar).Value = qLkhachHang.TenKhachHang;
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
        public bool Update(QLkhachHang qLkhachHang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "update khachhang set Tencongty = @Tencongty,tengiaodich = @tengiaodich,diachi = @diachi,email = @email," +
                "dienthoai = @dienthoai,fax = @fax,TenKhachhang=@TenKhachHang WHERE makhachhang = @makhachhang;";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = qLkhachHang.MaKhachHang;
                sqlCommand.Parameters.Add("@tencongty", SqlDbType.NVarChar).Value = qLkhachHang.TenCongTy1;
                sqlCommand.Parameters.Add("@tengiaodich", SqlDbType.NVarChar).Value = qLkhachHang.TenGiaoDich;
                sqlCommand.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = qLkhachHang.DiaChi;
                sqlCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = qLkhachHang.Email;
                sqlCommand.Parameters.Add("@dienthoai", SqlDbType.NVarChar).Value = qLkhachHang.DienThoai;
                sqlCommand.Parameters.Add("@fax", SqlDbType.NVarChar).Value = qLkhachHang.Fax;
                sqlCommand.Parameters.Add("@TenKhachHang", SqlDbType.NVarChar).Value = qLkhachHang.TenKhachHang;
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
        public bool Delete(string makhachhang)
        {
            SqlConnection sqlConnection = connect.GetConnection();

            string query = "delete khachhang where makhachhang=@makhachhang";

            //khi thực thi dù ảnh hưởng lỗi như nào thì luôn luôn đóng(ở finally)
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = makhachhang;

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

