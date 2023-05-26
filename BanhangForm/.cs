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
        Modify modify;
        QLmatHang qLmatHang;
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
            //đổ dữu liệu
            try
            {
                dataGridView1.DataSource = modify.getAllMatHang();

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
    }
}
