
using BanhangForm.DoDatHang;
using BanhangForm.KhachHang;
using BanhangForm.LoaiHang;
using BanhangForm.NCC;
using BanhangForm.NhanVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace BanhangForm
{
    public partial class Form1 : Form
    {
        string chuoiketnoi = @"Data Source=MSI\XOAII;Initial Catalog=BANHANG_DT;Integrated Security=True";


        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            SqlConnection cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            //mathang
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cm = new SqlCommand("select *from mathang", cn);
            SqlDataReader dr = cm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            //step3: dọn dẹp
            dr.Close();
            //khachhang
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cmkhachhang = new SqlCommand("select *from khachhang", cn);
            SqlDataReader drKhachHang = cmkhachhang.ExecuteReader();
            DataTable dtkhachhang = new DataTable();
            dtkhachhang.Load(drKhachHang);

            //step3: dọn dẹp
            drKhachHang.Close();
            //chi tiết đặt hàng
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cmchiTiet = new SqlCommand("select *from chitietdathang", cn);
            SqlDataReader drchiTiet = cmchiTiet.ExecuteReader();
            DataTable dtchiTiet = new DataTable();
            dtchiTiet.Load(drchiTiet);

            //step3: dọn dẹp
            drchiTiet.Close();
            //đơn đặt hàng
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cmDonDat = new SqlCommand("select *from dondathang", cn);
            SqlDataReader drdonDat = cmDonDat.ExecuteReader();
            DataTable dtDonDat = new DataTable();
            dtDonDat.Load(drdonDat);
            drdonDat.Close();
            // loại hàng
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cmLoaiHang = new SqlCommand("select *from loaihang", cn);
            SqlDataReader drLoaiHang = cmLoaiHang.ExecuteReader();
            DataTable dtLoaiHang = new DataTable();
            dtLoaiHang.Load(drLoaiHang);
            drLoaiHang.Close();
            // nhà cung cấp
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cmNCC = new SqlCommand("select *from nhacungcap", cn);
            SqlDataReader drNCC = cmNCC.ExecuteReader();
            DataTable dtNCC = new DataTable();
            dtNCC.Load(drNCC);
            drNCC.Close();
            // nhân viên
            //step2: thực thi 1 sql: dạng lệnh
            SqlCommand cmNhanVien = new SqlCommand("select *from nhanvien", cn);
            SqlDataReader drNhanVien = cmNhanVien.ExecuteReader();
            DataTable dtNhanVien = new DataTable();
            dtNhanVien.Load(drNhanVien);
            drNhanVien.Close();



            cm.Dispose();
            cmkhachhang.Dispose();
            cmchiTiet.Dispose();
            cmDonDat.Dispose();
            cmLoaiHang.Dispose();
            cmNCC.Dispose();
            cmNhanVien.Dispose();

            cn.Close();
            cn.Dispose();

            //step4: display
            dataGridView1.DataSource = dt;
            dataGridView_khachHang.DataSource = dtkhachhang;
            dataV_chiTietDatHang.DataSource = dtchiTiet;
            data_DonDatHang.DataSource = dtDonDat;
            dataGridView_LoaiHang.DataSource = dtLoaiHang;
            data_NCC.DataSource = dtNCC;
            data_nhanvien.DataSource = dtNhanVien;
            dt.Dispose();
            dtkhachhang.Dispose();
            dtchiTiet.Dispose();
            dtDonDat.Dispose();
            dtLoaiHang.Dispose();
            dtNCC.Dispose();
            dtNhanVien.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // lấy tất cả dữ liệu đã nhập xuống:
            // Nên check lỗi người dùng nhập! => nếu mà lỗi thì return;
            string mahang = this.textBox_maH.Text;
            string tenhang = this.textBox_tenH.Text;
            string maCongTy = this.textBox_MaCTY.Text;
            string maLoaiHang = this.textBox_maLH.Text;
            int soLuong = Convert.ToInt32(this.textBox_soLuong.Text);
            string donViTinh = this.textBox_DVT.Text;
            SqlMoney giaHang = SqlMoney.Parse(this.textBox_gia.Text);
            QLmatHang qLmatHang = new QLmatHang(mahang, tenhang, maCongTy, maLoaiHang, soLuong, donViTinh, giaHang);



            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_mathang_Insert";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterMatHang(ref cmd, qLmatHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }
        private void truyenParameterMatHang(ref SqlCommand cmd, QLmatHang qLmatHang)
        {

            cmd.Parameters.Add("@mahang", SqlDbType.NVarChar).Value = qLmatHang.MaHang;
            cmd.Parameters.Add("@tenhang", SqlDbType.NVarChar).Value = qLmatHang.TenHang;
            cmd.Parameters.Add("@macongty", SqlDbType.NVarChar).Value = qLmatHang.MaCongTy;
            cmd.Parameters.Add("@maloaihang", SqlDbType.NVarChar).Value = qLmatHang.Maloaihang;
            cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = qLmatHang.Soluong;
            cmd.Parameters.Add("@donvitinh", SqlDbType.NVarChar).Value = qLmatHang.DonviTinh;
            cmd.Parameters.Add("@giahang", SqlDbType.Money).Value = qLmatHang.GiaHang;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // lấy tất cả dữ liệu đã nhập xuống:
            // Nên check lỗi người dùng nhập! => nếu mà lỗi thì return;
            string mahang = this.textBox_maH.Text;
            string tenhang = this.textBox_tenH.Text;
            string maCongTy = this.textBox_MaCTY.Text;
            string maLoaiHang = this.textBox_maLH.Text;
            int soLuong = Convert.ToInt32(this.textBox_soLuong.Text);
            string donViTinh = this.textBox_DVT.Text;
            SqlMoney giaHang = SqlMoney.Parse(this.textBox_gia.Text);
            QLmatHang qLmatHang = new QLmatHang(mahang, tenhang, maCongTy, maLoaiHang, soLuong, donViTinh, giaHang);



            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_mathang_Update";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterMatHang(ref cmd, qLmatHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                string mahang = row.Cells["mahang"].Value.ToString();
                /* string tenhang = row.Cells["tenhang"].Value.ToString();
                 string macongty = row.Cells["macongty"].Value.ToString();
                 int maloaihang = Convert.ToInt32(row.Cells["maloaihang"].Value);// Thay "mahang" bằng tên cột chứa mã hàng

                 int soluong = Convert.ToInt32(row.Cells["soluong"].Value);
                 string donvitinh = row.Cells["donvitinh"].Value.ToString();
                 double giahang = Convert.ToDouble(row.Cells["giahang"].Value);*/

                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_mathang_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                /*  // Truyền tham số cho cmd
                  cmd.Parameters.Add("@Tenhang", SqlDbType.NVarChar).Value = tenhang;
                  cmd.Parameters.Add("@Macongty", SqlDbType.NVarChar).Value = macongty;
                  cmd.Parameters.Add("@Maloaihang", SqlDbType.Int).Value = maloaihang;*/
                cmd.Parameters.Add("@Mahang", SqlDbType.NVarChar, 10).Value = mahang;
                /*cmd.Parameters.Add("@Soluong", SqlDbType.Int).Value = soluong;
                cmd.Parameters.Add("@Donvitinh", SqlDbType.NVarChar).Value = donvitinh;
                cmd.Parameters.Add("@Giahang", SqlDbType.Money).Value = giahang;*/

                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string makhachhang = this.textBox_MaKH.Text;
            string Tencongty = this.textBox_TenCTY.Text;
            string tengiaodich = this.textBox_tenGD.Text;
            string diaChi = this.textBox_diaChi.Text;
            string email = this.textBox_email.Text;
            string dienThoai = this.textBox_dienThoai.Text;
            string fax = this.textBox_Fax.Text;
            string TenKhachhang = this.textBox_tenKH.Text;
            QLkhachHang qLkhachHang = new QLkhachHang(makhachhang, Tencongty, tengiaodich, diaChi, email, dienThoai, fax, TenKhachhang);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_khachhang_Insert";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterkhachHang(ref cmd, qLkhachHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            string makhachhang = this.textBox_MaKH.Text;
            string Tencongty = this.textBox_TenCTY.Text;
            string tengiaodich = this.textBox_tenGD.Text;
            string diaChi = this.textBox_diaChi.Text;
            string email = this.textBox_email.Text;
            string dienThoai = this.textBox_dienThoai.Text;
            string fax = this.textBox_Fax.Text;
            string TenKhachhang = this.textBox_tenKH.Text;
            QLkhachHang qLkhachHang = new QLkhachHang(makhachhang, Tencongty, tengiaodich, diaChi, email, dienThoai, fax, TenKhachhang);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_khachhang_Update";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterkhachHang(ref cmd, qLkhachHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView_khachHang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_khachHang.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                string makhachhang = row.Cells["makhachhang"].Value.ToString();
                /* string tenhang = row.Cells["tenhang"].Value.ToString();
                 string macongty = row.Cells["macongty"].Value.ToString();
                 int maloaihang = Convert.ToInt32(row.Cells["maloaihang"].Value);// Thay "mahang" bằng tên cột chứa mã hàng

                 int soluong = Convert.ToInt32(row.Cells["soluong"].Value);
                 string donvitinh = row.Cells["donvitinh"].Value.ToString();
                 double giahang = Convert.ToDouble(row.Cells["giahang"].Value);*/

                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_khachhang_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                /*  // Truyền tham số cho cmd
                  cmd.Parameters.Add("@Tenhang", SqlDbType.NVarChar).Value = tenhang;
                  cmd.Parameters.Add("@Macongty", SqlDbType.NVarChar).Value = macongty;
                  cmd.Parameters.Add("@Maloaihang", SqlDbType.Int).Value = maloaihang;*/
                cmd.Parameters.Add("@makhachhang", SqlDbType.NVarChar, 10).Value = makhachhang;
                /*cmd.Parameters.Add("@Soluong", SqlDbType.Int).Value = soluong;
                cmd.Parameters.Add("@Donvitinh", SqlDbType.NVarChar).Value = donvitinh;
                cmd.Parameters.Add("@Giahang", SqlDbType.Money).Value = giahang;*/

                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void truyenParameterkhachHang(ref SqlCommand cmd, QLkhachHang qLkhachHang)
        {

            cmd.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = qLkhachHang.MaKhachHang;
            cmd.Parameters.Add("@tencongty", SqlDbType.NVarChar).Value = qLkhachHang.TenCongTy1;
            cmd.Parameters.Add("@tengiaodich", SqlDbType.NVarChar).Value = qLkhachHang.TenGiaoDich;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = qLkhachHang.DiaChi;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = qLkhachHang.Email;
            cmd.Parameters.Add("@dienthoai", SqlDbType.NVarChar).Value = qLkhachHang.DienThoai;
            cmd.Parameters.Add("@fax", SqlDbType.NVarChar).Value = qLkhachHang.Fax;
            cmd.Parameters.Add("@TenKhachHang", SqlDbType.NVarChar).Value = qLkhachHang.TenKhachHang;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD.Text);
            string mahang = this.txt_maHang.Text;
            SqlMoney giaban = SqlMoney.Parse(this.txt_giaBan.Text);
            int soluong = Convert.ToInt16(this.txt_soLuong.Text);
            double mucgiamgia = Convert.ToDouble(this.txt_MucGG.Text);
            QLchiTietDonHang qLchiTietDonHang = new QLchiTietDonHang(sohoadon, mahang, giaban, soluong, mucgiamgia);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_chitietdathang_Insert";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterChiTiet(ref cmd, qLchiTietDonHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD.Text);
            string mahang = this.txt_maHang.Text;
            SqlMoney giaban = SqlMoney.Parse(this.txt_giaBan.Text);
            int soluong = Convert.ToInt16(this.txt_soLuong.Text);
            double mucgiamgia = Convert.ToDouble(this.txt_MucGG.Text);
            QLchiTietDonHang qLchiTietDonHang = new QLchiTietDonHang(sohoadon, mahang, giaban, soluong, mucgiamgia);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_chitietdathang_Update";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterChiTiet(ref cmd, qLchiTietDonHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataV_chiTietDatHang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataV_chiTietDatHang.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                int sohoadon = Convert.ToInt32(row.Cells["sohoadon"].Value.ToString());
                string mahang = row.Cells["mahang"].Value.ToString();


                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_chitietdathang_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                // Truyền tham số cho cmd

                cmd.Parameters.Add("@sohoadon", SqlDbType.Int).Value = sohoadon;
                cmd.Parameters.Add("@mahang", SqlDbType.NVarChar, 10).Value = mahang;


                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void truyenParameterChiTiet(ref SqlCommand cmd, QLchiTietDonHang qLchiTietDonHang)
        {

            cmd.Parameters.Add("@sohoadon", SqlDbType.Int).Value = qLchiTietDonHang.SoHoaDon;
            cmd.Parameters.Add("@mahang", SqlDbType.NVarChar).Value = qLchiTietDonHang.Mahang;
            cmd.Parameters.Add("@giaban", SqlDbType.Money).Value = qLchiTietDonHang.Giaban;
            cmd.Parameters.Add("@soluong", SqlDbType.SmallInt).Value = qLchiTietDonHang.SoLuong;
            cmd.Parameters.Add("@mucgiamgia", SqlDbType.Real).Value = qLchiTietDonHang.Mucgiamgia;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD_don.Text);
            string makhachhang = this.txt_maKH.Text;
            string manhanvien = this.txt_MaNV.Text;
            DateTime ngaydathang = this.date_DatHang.Value;
            DateTime ngaygiaohang = this.date_giaohang.Value;
            string noigiao = this.txt_noigiao.Text;
            QLdonDatHang qldonDatHang = new QLdonDatHang(sohoadon, makhachhang, manhanvien, ngaydathang, ngaygiaohang, noigiao);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_dondathang_Insert";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterDonDatHang(ref cmd, qldonDatHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD_don.Text);
            string makhachhang = this.txt_maKH.Text;
            string manhanvien = this.txt_MaNV.Text;
            DateTime ngaydathang = this.date_DatHang.Value;
            DateTime ngaygiaohang = this.date_giaohang.Value;
            string noigiao = this.txt_noigiao.Text;
            QLdonDatHang qldonDatHang = new QLdonDatHang(sohoadon, makhachhang, manhanvien, ngaydathang, ngaygiaohang, noigiao);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_dondathang_Update";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterDonDatHang(ref cmd, qldonDatHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (data_DonDatHang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = data_DonDatHang.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                int sohoadon = Convert.ToInt32(row.Cells["sohoadon"].Value.ToString());



                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_dondathang_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                // Truyền tham số cho cmd

                cmd.Parameters.Add("@sohoadon", SqlDbType.Int).Value = sohoadon;



                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void truyenParameterDonDatHang(ref SqlCommand cmd, QLdonDatHang qLdonDatHang)
        {

            cmd.Parameters.Add("@sohoadon", SqlDbType.Int).Value = qLdonDatHang.Sohoadon;
            cmd.Parameters.Add("@makhachhang", SqlDbType.NVarChar).Value = qLdonDatHang.Makhachhang;
            cmd.Parameters.Add("@manhanvien", SqlDbType.NVarChar).Value = qLdonDatHang.Manhanvien;
            cmd.Parameters.Add("@ngaydathang", SqlDbType.Date).Value = qLdonDatHang.Ngaydathang.ToShortDateString();
            cmd.Parameters.Add("@ngaygiaohang", SqlDbType.Date).Value = qLdonDatHang.Ngaygiaohang.ToShortDateString();
            cmd.Parameters.Add("@noigiaohang", SqlDbType.NVarChar).Value = qLdonDatHang.Noigiaohang;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

            // lấy tất cả dữ liệu đã nhập xuống:
            // Nên check lỗi người dùng nhập! => nếu mà lỗi thì return;
            int maloaihang = Convert.ToInt32(this.txt_maLoaiHang.Text);
            string tenloaihang = this.txt_tenLoaiHang.Text;
            QLloaiHang qLloaiHang = new QLloaiHang(maloaihang, tenloaihang);

            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_loaihang_Insert";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterLoaiHang(ref cmd, qLloaiHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


        }

        private void xoaThongTin()
        {

        }



        private void button11_Click(object sender, EventArgs e)
        {

            int maloaihang = Convert.ToInt32(this.txt_maLoaiHang.Text);
            string tenloaihang = this.txt_tenLoaiHang.Text;
            QLloaiHang qLloaiHang = new QLloaiHang(maloaihang, tenloaihang);

            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_loaihang_Update";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterLoaiHang(ref cmd, qLloaiHang);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView_LoaiHang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_LoaiHang.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                string maloaihang = row.Cells["maloaihang"].Value.ToString();



                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_dondathang_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                // Truyền tham số cho cmd

                cmd.Parameters.Add("@maloaihang", SqlDbType.NVarChar).Value = maloaihang;



                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void truyenParameterLoaiHang(ref SqlCommand cmd, QLloaiHang qLloaiHang)
        {

            cmd.Parameters.Add("@maloaihang", SqlDbType.Int).Value = qLloaiHang.Maloaihang;
            cmd.Parameters.Add("@tenloaihang", SqlDbType.NVarChar, 15).Value = qLloaiHang.Tenloaihang;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // lấy tất cả dữ liệu đã nhập xuống:
            // Nên check lỗi người dùng nhập! => nếu mà lỗi thì return;
            string macongty = this.textBox_MaCTY.Text;
            string tencongty = this.txt_tenCTY.Text;
            string tengiaodich = this.txt_tenGD.Text;
            string diachi = this.txt_diaChi.Text;
            string dienthoai = this.txt_dienthoai.Text;
            string fax = this.txt_fax.Text;
            string email = this.text_mail.Text;
            QLNCC qLNCC = new QLNCC(macongty, tencongty, tengiaodich, diachi, dienthoai, fax, email);




            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_nhacungcap_Insert";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterNCC(ref cmd, qLNCC);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // lấy tất cả dữ liệu đã nhập xuống:
            // Nên check lỗi người dùng nhập! => nếu mà lỗi thì return;
            string macongty = this.txt_MaNCC.Text;
            string tencongty = this.txt_tenCTY.Text;
            string tengiaodich = this.txt_tenGD.Text;
            string diachi = this.txt_diaChi.Text;
            string dienthoai = this.txt_dienthoai.Text;
            string fax = this.txt_fax.Text;
            string email = this.text_mail.Text;
            QLNCC qLNCC = new QLNCC(macongty, tencongty, tengiaodich, diachi, dienthoai, fax, email);




            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_nhacungcap_Update";
            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterNCC(ref cmd, qLNCC);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (data_NCC.SelectedRows.Count > 0)
            {
                DataGridViewRow row = data_NCC.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                string macongty = row.Cells["macongty"].Value.ToString();



                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_nhacungcap_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                // Truyền tham số cho cmd

                cmd.Parameters.Add("@macongty", SqlDbType.NVarChar).Value = macongty;



                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void truyenParameterNCC(ref SqlCommand cmd, QLNCC qLNCC)
        {

            cmd.Parameters.Add("@Macongty", SqlDbType.NVarChar).Value = qLNCC.Macongty;
            cmd.Parameters.Add("@tencongty", SqlDbType.NVarChar, 15).Value = qLNCC.Tencongty;
            cmd.Parameters.Add("@tengiaodich", SqlDbType.NVarChar, 15).Value = qLNCC.Tengiaodich;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 15).Value = qLNCC.Diachi;
            cmd.Parameters.Add("@dienthoai", SqlDbType.NVarChar, 15).Value = qLNCC.Dienthoai;
            cmd.Parameters.Add("@fax", SqlDbType.NVarChar, 15).Value = qLNCC.Fax;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 15).Value = qLNCC.Email;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

            string manhanvien = this.txt_maNV_bangNV.Text;
            string ho = this.txt_ho.Text;
            string ten = this.txt_tenNV.Text;
            DateTime ngaysinh = this.date_Ngaysinh.Value;
            DateTime ngaylamviec = this.dateNgayLamViec.Value;
            string diachi = this.txt_diaChi_NV.Text;
            string dienthoai = this.txt_dienThoaiNV.Text;
            SqlMoney luongcoban = SqlMoney.Parse(this.txt_luongCB.Text);
            SqlMoney phucap = SqlMoney.Parse(this.txt_PhuCap.Text);
            int tuoi = DateTime.Now.Year - ngaysinh.Year;


            nhanVien nhanVien = new nhanVien(manhanvien, ho, ten, ngaysinh, ngaylamviec, diachi, dienthoai, luongcoban, phucap, tuoi);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_nhanvien_Insert";


            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman



            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterNhanVien(ref cmd, nhanVien);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("thêm thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            string manhanvien = this.txt_maNV_bangNV.Text;
            string ho = this.txt_ho.Text;
            string ten = this.txt_tenNV.Text;
            DateTime ngaysinh = this.date_Ngaysinh.Value;
            DateTime ngaylamviec = this.dateNgayLamViec.Value;
            string diachi = this.txt_diaChi_NV.Text;
            string dienthoai = this.txt_dienThoaiNV.Text;
            SqlMoney luongcoban = SqlMoney.Parse(this.txt_luongCB.Text);
            SqlMoney phucap = SqlMoney.Parse(this.txt_PhuCap.Text);
            int tuoi = DateTime.Now.Year - ngaysinh.Year;


            nhanVien nhanVien = new nhanVien(manhanvien, ho, ten, ngaysinh, ngaylamviec, diachi, dienthoai, luongcoban, phucap, tuoi);
            // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
            // chuẩn bị tên proceduce:
            string query = "sp_nhanvien_Update";


            // new đối tượng thư viên để gọi các hàm trong thư viện:
            libDB lib = new libDB(chuoiketnoi);
            SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman



            // Cần phải truyền dũ liệu cho cmd 
            truyenParameterNhanVien(ref cmd, nhanVien);


            // thực hiện proceduce bằng cách là gọi  thư viên
            try
            {
                // đây là câu lệnh thêm nên 
                int kq = lib.RunSQL(cmd);
                if (kq > 0)
                {
                    MessageBox.Show("sửa thành công!");
                    Form1_Load(sender, e);
                    xoaThongTin();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (data_nhanvien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = data_nhanvien.SelectedRows[0];
                // Thay "sohoadon" bằng tên cột chứa số hóa đơn
                string manhanvien = row.Cells["manhanvien"].Value.ToString();



                // Tạo câu truy vấn SQL hoặc gọi procedure để xóa dữ liệu
                string query = "sp_nhanvien_Delete";
                libDB lib = new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query);

                // Truyền tham số cho cmd

                cmd.Parameters.Add("@manhanvien", SqlDbType.NVarChar).Value = manhanvien;



                try
                {
                    int kq = lib.RunSQL(cmd);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa thành công!");
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void truyenParameterNhanVien(ref SqlCommand cmd, nhanVien nhanVien)
        {

            cmd.Parameters.Add("@manhanvien", SqlDbType.NVarChar, 10).Value = nhanVien.Manhanvien;
            cmd.Parameters.Add("@ho", SqlDbType.NVarChar, 15).Value = nhanVien.Ho;
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 15).Value = nhanVien.Ten;
            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = nhanVien.Ngaysinh.ToShortDateString();
            cmd.Parameters.Add("@ngaylamviec", SqlDbType.Date).Value = nhanVien.Ngaylamviec.ToShortDateString();
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 15).Value = nhanVien.Diachi;
            cmd.Parameters.Add("@dienthoai", SqlDbType.NVarChar, 15).Value = nhanVien.Dienthoai;
            cmd.Parameters.Add("@luongcoban", SqlDbType.Money, 15).Value = nhanVien.Luongcoban;
            cmd.Parameters.Add("@phucap", SqlDbType.Money, 15).Value = nhanVien.Phucap;
            cmd.Parameters.Add("@tuoi", SqlDbType.Money, 15).Value = nhanVien.Tuoi;


        }

        private void txt_dienthoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search.Text == "")
            
                Form1_Load(sender, e);
            
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            //mathang
            //step2: thực thi 1 sql: dạng lệnh
            
           
                string query = "select*from mathang where mahang =@mahang";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("mahang", txt_Search.Text);
                cmd.Parameters.AddWithValue("tenhang", txt_Search.Text);
                cmd.Parameters.AddWithValue("macongty", txt_Search.Text);
                cmd.Parameters.AddWithValue("maloaihang", txt_Search.Text);
                cmd.Parameters.AddWithValue("soluong", txt_Search.Text);
                cmd.Parameters.AddWithValue("donvitinh", txt_Search.Text);
                cmd.Parameters.AddWithValue("giahang", txt_Search.Text);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();

                dt.Load(dr);

                //step3: dọn dẹp
                dataGridView1.DataSource = dt;

            }

        private void label46_Click(object sender, EventArgs e)
        {

        }
        public void ExportFile(string part)
        {
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            for(int i=0;i<data_nhanvien.Columns.Count;i++)
            {
                application.Cells[1, i + 1] = data_nhanvien.Columns[i].HeaderText;
            }
            for(int i=0;i<data_nhanvien.Rows.Count;i++)
            {
                for(int j=0;j < data_nhanvien.Columns.Count; j++)
                {
                    application.Cells[i + 2, j + 1] = data_nhanvien.Rows[i].Cells[j].Value;
                }
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(part);
            application.ActiveWorkbook.Saved = true;
                
        }

        private void txt_EXCEL_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Excel";
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx"; 
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try {
                   ExportFile(saveFileDialog.FileName);
                    MessageBox.Show("xuất file thành công");
                }
                catch(Exception ex) { 
                    MessageBox.Show("xuất file không thành công\n"+ex.Message);
                }
            }    
        }
        private DataTable GetInvoiceItems(int sohoadon)
        {
            string connectionString = @"Data Source=MSI\XOAII;Initial Catalog=BANHANG_DT;Integrated Security=True"; ;

            string query = @"select c.mahang,c.soluong,c.giaban,m.tenhang from 
    chitietdathang c inner join dondathang d on c.sohoadon =d.Sohoadon 
          inner join mathang m on c.mahang = m.mahang
            where c.sohoadon=@sohoadon";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sohoadon", sohoadon);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        
        

        private void data_DonDatHang_SelectionChanged(object sender, EventArgs e)
        {
           /* if (data_DonDatHang.SelectedRows.Count > 0)
            {
                int selectedInvoiceNumber = (int)data_DonDatHang.SelectedRows[0].Cells["SoHoaDon"].Value;

                DataTable invoiceItemsTable = GetInvoiceItems(selectedInvoiceNumber);

                dataV_chiTietDatHang.DataSource = invoiceItemsTable;
            }*/
        }

        private void data_DonDatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int selectedInvoiceNumber = (int)data_DonDatHang.Rows[e.RowIndex].Cells["SoHoaDon"].Value;

                DataTable invoiceItemsTable = GetInvoiceItems(selectedInvoiceNumber);

                // Tạo một instance mới của InvoiceItemsForm và truyền invoiceItemsTable vào constructor
                ChiTietDonHang ct = new ChiTietDonHang(invoiceItemsTable);
                ct.Show();
            }
        }

        private void textBox_MaCTY_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

    

    

