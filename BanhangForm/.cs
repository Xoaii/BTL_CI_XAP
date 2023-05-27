using BanhangForm.chitietDathang;
using BanhangForm.DoDatHang;
using BanhangForm.KhachHang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //đổ dữu liệu
            try
            {
                dataGridView1.DataSource = modify.getAllMatHang();
                dataGridView_khachHang.DataSource = modifyKhachHang.getAllKhachhang();
                dataV_chiTietDatHang.DataSource = modifyChiTet.getAllchiTiet();
                data_DonDatHang.DataSource=modifyDonDatHang.getAllDonDatHang(); 
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
    }
}
