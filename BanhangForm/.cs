using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanhangForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void label5_Click(object sender, EventArgs e)
        {

        }
        Modify modify;

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

        }
    }
}
