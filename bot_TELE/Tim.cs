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
        public string TimKH( string t) {
            string query = "";
            string kq="";
            using(SqlConnection con = new SqlConnection(chuoiketnoi)) {

                con.Open();
                try { 
                    using(SqlCommand cmd = new SqlCommand(query,con)) { 
                        object kqTrave = cmd.ExecuteScalar();
                        cmd.Parameters.Add("@tenkhachhang",System.Data.SqlDbType.NVarChar,50).Value=t;
                        kq = (string) kqTrave;
                    }
                }
                catch(Exception e) { 
                    MessageBox.Show("lỗi tìm kiếm"+e.Message);
                }
                return kq;
            }


        }
    }
}
