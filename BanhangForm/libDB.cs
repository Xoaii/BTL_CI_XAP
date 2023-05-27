using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm
{
    internal class libDB
    {
        private string ConnectionString;

        /// <summary>
        /// Hàm tạo 1 tham số
        /// </summary>
        /// <param name="ConnectionString">Đây là chuỗi kết nối tới SQL Server</param>
        public libDB(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public SqlCommand GetCmd(string sp_name)
        {
            SqlCommand cmd = new SqlCommand(sp_name);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
           
        }
        /// <summary>
        /// hàm thực thi sql đặt trong OleDbCommand
        /// </summary>
        /// <param name="cm">đây là đối tượng chứa sql hoặc sp_</param>
        /// <returns>trả về bảng dữ liệu</returns>
        public DataTable Query(SqlCommand cm)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                cm.Connection = cn;
                SqlDataReader dr = cm.ExecuteReader();//thực thi SQL -> trả về dr
                DataTable dt = new DataTable();// tạo 1 bảng trống để lưu dữ liệu đọc được
                dt.Load(dr);    //Lấy hết dữ liệu trong dr vào dt
                dr.Close();
                return dt;  //trả về kq
            }
        }
        /// <summary>
        /// thực thi câu lệnh sql
        /// </summary>
        /// <param name="sql">SQL dạng select</param>
        /// <returns>Trả về bảng dữ liệu</returns>
        public DataTable Query(string sql)
        {
            //sử dụng cấu trúc điều khiển USING để giải phóng đối tượng cm sau khi kết thúc khối USING
            using (SqlCommand cm = new SqlCommand(sql))
            {
                return Query(cm);
            }
        }
        /// <summary>
        /// thực thi 1 sql không trả về dữ liệu: INSERT, UPDATE, DELETE, SP_làm các việc đó
        /// </summary>
        /// <param name="cm">OleDbCommand</param>
        /// <returns>Trả về số lượng bản ghi bị tác động</returns>
        public int RunSQL(SqlCommand cm)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                cm.Connection = cn;
                return cm.ExecuteNonQuery();//thực thi SQL -> trả về số dòng bị tác động
            }
        }
        /// <summary>
        /// thực thi 1 sql loại không trả về dữ liệu INSERT, UPDATE, DELETE
        /// </summary>
        /// <param name="sql">SQL trực tiếp, không chứa tham số</param>
        /// <returns>Trả về số lượng bản ghi bị tác động</returns>
        public int RunSQL(string sql)
        {
            using (SqlCommand cm = new SqlCommand(sql))
            {
                return RunSQL(cm);
            }
        }

        /// <summary>
        /// thực thi cm
        /// </summary>
        /// <param name="cm">cm chứa sql hoặc sp_, có thể có tham số</param>
        /// <returns>Trả về giá trị trong dòng 1 cột 1</returns>
        public object Scalar(SqlCommand cm)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                cm.Connection = cn;
                return cm.ExecuteScalar();
            }
        }
        /// <summary>
        /// thực thi SQL
        /// </summary>
        /// <param name="sql">SQL trực tiếp, không chứa tham số</param>
        /// <returns>Trả về giá trị trong dòng 1 cột 1</returns>
        public object Scalar(string sql)
        {
            using (SqlCommand cm = new SqlCommand(sql))
            {
                return Scalar(cm);
            }

        }
    }
}
