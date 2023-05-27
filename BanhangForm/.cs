using BanhangForm.chitietDathang;
using BanhangForm.DoDatHang;
using BanhangForm.KhachHang;
using BanhangForm.LoaiHang;
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

namespace BanhangForm
{
    public partial class Form1 : Form
    {
        //Mạt hàng
        Modify modify;
        QLmatHang qLmatHang;
        //Khách hàng
        ModifyKhachHang modifyKhachHang;
        QLkhachHang qLkhachHang;
        //chi tiết đặt hàng
        ModifyChiTet modifyChiTet;
        QLchiTietDonHang qLchiTietDonHang;
        //đơn đặt hàng
        ModifyDonDatHang modifyDonDatHang;
        QLdonDatHang qldonDatHang;
        libDB lib;
        string chuoiketnoi = @"Data Source=MSI\XOAII;Initial Catalog=BANHANG_DT;Integrated Security=True";
        QLloaiHang qLloaiHang;
        ModifyLoaiHang modifyLoai;

        public Form1()
        {
            InitializeComponent();
        }
       
        private void label5_Click(object sender, EventArgs e)
        {

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            modifyKhachHang = new ModifyKhachHang();
            modifyChiTet =new ModifyChiTet();
            modifyDonDatHang= new ModifyDonDatHang();   
            modifyLoai =new ModifyLoaiHang();
            //đổ dữu liệu
            try
            {
                dataGridView1.DataSource = modify.getAllMatHang();
                dataGridView_khachHang.DataSource = modifyKhachHang.getAllKhachhang();
                dataV_chiTietDatHang.DataSource = modifyChiTet.getAllchiTiet();
                data_DonDatHang.DataSource=modifyDonDatHang.getAllDonDatHang();
                dataGridView_LoaiHang.DataSource = modifyLoai.getAllLoaiHang();
               
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi: " + ex.Message,"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mahang = this.textBox_maH.Text;
            string tenhang=this.textBox_tenH.Text;  
            string maCongTy=this.textBox_MaCTY.Text;    
            string maLoaiHang =this.textBox_maLH.Text;   
            int soLuong =Convert.ToInt32(this.textBox_soLuong.Text);
            string donViTinh = this.textBox_DVT.Text;
           SqlMoney giaHang =SqlMoney.Parse(this.textBox_gia .Text);
            qLmatHang = new QLmatHang(mahang,tenhang,maCongTy,maLoaiHang,soLuong,donViTinh,giaHang);

            if (modify.insert(qLmatHang))
            {
                dataGridView1.DataSource=modify.getAllMatHang();    
            }
            else
            {
                MessageBox.Show("Lỗi: "+ "không thêm được","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mahang = this.textBox_maH.Text;
            string tenhang = this.textBox_tenH.Text;
            string maCongTy = this.textBox_MaCTY.Text;
            string maLoaiHang = this.textBox_maLH.Text;
            int soLuong = Convert.ToInt32(this.textBox_soLuong.Text);
            string donViTinh = this.textBox_DVT.Text;
            SqlMoney giaHang = SqlMoney.Parse(this.textBox_gia.Text);
            qLmatHang = new QLmatHang(mahang, tenhang, maCongTy, maLoaiHang, soLuong, donViTinh, giaHang);

            if (modify.Update(qLmatHang))
            {
                dataGridView1.DataSource = modify.getAllMatHang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mahang = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (modify.Delete(mahang))
            {
                dataGridView1.DataSource = modify.getAllMatHang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không XÓA được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string makhachhang =this.textBox_MaKH.Text;
            string Tencongty = this.textBox_TenCTY.Text;
            string tengiaodich = this.textBox_tenGD.Text;
            string diaChi =this.textBox_diaChi.Text;
            string email = this.textBox_email.Text;
            string dienThoai = this.textBox_dienThoai.Text;
            string fax =this.textBox_Fax.Text;
            string TenKhachhang =this.textBox_tenKH.Text;
            qLkhachHang = new QLkhachHang(makhachhang, Tencongty, tengiaodich, diaChi, email, dienThoai, fax, TenKhachhang);

            if (modifyKhachHang.insert(qLkhachHang))
            {
                dataGridView_khachHang.DataSource = modifyKhachHang.getAllKhachhang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không thêm được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            qLkhachHang = new QLkhachHang(makhachhang, Tencongty, tengiaodich, diaChi, email, dienThoai, fax, TenKhachhang);

            if (modifyKhachHang.Update(qLkhachHang))
            {
                dataGridView_khachHang.DataSource = modifyKhachHang.getAllKhachhang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string makhachhang = dataGridView_khachHang.SelectedRows[0].Cells[0].Value.ToString();
            if (modifyKhachHang.Delete(makhachhang))
            {
                dataGridView_khachHang.DataSource = modifyKhachHang.getAllKhachhang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không XÓA được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD.Text);
            string mahang= this.txt_maHang.Text;
            SqlMoney giaban=SqlMoney.Parse(this.txt_giaBan.Text);
            int soluong = Convert.ToInt16(this.txt_soLuong.Text);
            double mucgiamgia = Convert.ToDouble(this.txt_MucGG.Text);
            qLchiTietDonHang = new QLchiTietDonHang(sohoadon, mahang, giaban, soluong, mucgiamgia);
            if (modifyChiTet.insert(qLchiTietDonHang))
            {
                dataV_chiTietDatHang.DataSource = modifyChiTet.getAllchiTiet();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không thêm được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD.Text);
            string mahang = this.txt_maHang.Text;
            SqlMoney giaban = SqlMoney.Parse(this.txt_giaBan.Text);
            int soluong = Convert.ToInt16(this.txt_soLuong.Text);
            double mucgiamgia = Convert.ToDouble(this.txt_MucGG.Text);
            qLchiTietDonHang = new QLchiTietDonHang(sohoadon, mahang, giaban, soluong, mucgiamgia);
            if (modifyChiTet.Update(qLchiTietDonHang))
            {
                dataV_chiTietDatHang.DataSource = modifyChiTet.getAllchiTiet();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(dataV_chiTietDatHang.SelectedRows[0].Cells[0].Value.ToString());
            if (modifyChiTet.Delete(sohoadon))
            {
                dataV_chiTietDatHang.DataSource = modifyChiTet.getAllchiTiet();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không xóa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(this.txt_soHD_don.Text);
            string makhachhang = this.txt_maKH.Text;
            string manhanvien =this.txt_MaNV.Text;
            DateTime ngaydathang = this.date_DatHang.Value;
            DateTime ngaygiaohang = this.date_giaohang.Value;
            string noigiao =this.txt_noigiao.Text;
            qldonDatHang = new QLdonDatHang(sohoadon, makhachhang, manhanvien, ngaydathang, ngaygiaohang, noigiao);
            if (modifyDonDatHang.insert (qldonDatHang))
            {
                data_DonDatHang.DataSource = modifyDonDatHang.getAllDonDatHang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không thêm được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            qldonDatHang = new QLdonDatHang(sohoadon, makhachhang, manhanvien, ngaydathang, ngaygiaohang, noigiao);
            if (modifyDonDatHang.Update(qldonDatHang))
            {
                data_DonDatHang.DataSource = modifyDonDatHang.getAllDonDatHang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int sohoadon = Convert.ToInt32(data_DonDatHang.SelectedRows[0].Cells[0].Value.ToString());
            if (modifyDonDatHang.Delete(sohoadon))
            {
               data_DonDatHang.DataSource = modifyDonDatHang.getAllDonDatHang();
            }
            else
            {
                MessageBox.Show("Lỗi: " + "không xóa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
               int maloaihang = Convert.ToInt32( this.txt_maLoaiHang.Text);
            string tenloaihang = this.txt_tenLoaiHang.Text;
            qLloaiHang = new QLloaiHang(maloaihang, tenloaihang);   

                // tao demo thực hiện proceduce - chuỗi k phải sql nữa mà là tên proceduce
                // chuẩn bị tên proceduce:
                string query = "sp_loaihang_Insert";
                // new đối tượng thư viên để gọi các hàm trong thư viện:
                libDB lib= new libDB(chuoiketnoi);
                SqlCommand cmd = lib.GetCmd(query); // lấy về đối tượng sqlcomman

                // Cần phải truyền dũ liệu cho cmd 
                truyenParameter(ref cmd, qLloaiHang );
                //cmd.Parameters.Add("@Sohoadon", SqlDbType.Int).Value = chitietdonhang.Sohoadon;
                //cmd.Parameters.Add("@Mahang", SqlDbType.NVarChar, 10).Value = chitietdonhang.Mahang;
                //cmd.Parameters.Add("@Soluong", SqlDbType.SmallInt).Value = chitietdonhang.Soluong;
                //cmd.Parameters.Add("@Mucgiamgia", SqlDbType.Real).Value = chitietdonhang.Mucgiamga;
                //cmd.Parameters.Add("@Giaban", SqlDbType.Money).Value = chitietdonhang.Giaban;

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

            private void truyenParameter(ref SqlCommand cmd, QLloaiHang qLloaiHang)
            {

           cmd.Parameters.Add("@maloaihang", SqlDbType.Int).Value = qLloaiHang.Maloaihang;
            cmd.Parameters.Add("@tenloaihang", SqlDbType.NVarChar, 15).Value = qLloaiHang.Tenloaihang;
        }
        }
    }

