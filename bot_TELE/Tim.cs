using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bot_TELE
{
    public class Tim
    {
       
        string chuoiketnoi = @"Data Source=MSI\XOAII;Initial Catalog=BANHANG_DT;Integrated Security=True";
        /*public string TimKH( string t) {
            string query = "sp_khachhang_Search";
            string kq="";
            using(SqlConnection con = new SqlConnection(chuoiketnoi)) {

                con.Open();
                try { 
                    using(SqlCommand cmd = new SqlCommand(query,con)) {
                        cmd.Parameters.AddWithValue("@TenKhachHang", t);
                        object kqTrave = cmd.ExecuteScalar();
                        
                        kq = (string) kqTrave;
                    }
                }
                catch(Exception e) { 
                    MessageBox.Show("lỗi tìm kiếm"+e.Message);
                }
                return kq;
            }*/



        public string TimKH(string t)
        {
            string ketQua = string.Empty;
           // Thay thế bằng chuỗi kết nối của bạn

            using (SqlConnection con = new SqlConnection(chuoiketnoi))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_khachhang_Search", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenKhachHang", t);

                        SqlParameter ketQuaParameter = new SqlParameter("@KetQua", SqlDbType.NVarChar, 100);
                        ketQuaParameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ketQuaParameter);

                        cmd.ExecuteNonQuery();

                        ketQua = cmd.Parameters["@KetQua"].Value.ToString();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + e.Message);
                }
            }
            return ketQua;
        }

    }
}
