using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
    public partial class ChiTietDonHang : Form
    {
        private DataTable dondathang;
        public  ChiTietDonHang(DataTable dondathang)
        {
            InitializeComponent();
            dataV_chiTietDatHang.DataSource= dondathang;
            this.dondathang = dondathang;
           
        }

        private void ForIN2_Load(object sender, EventArgs e)
        {
            
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int yPos = 50;
            string title = "HÓA ĐƠN THANH TOÁN";
            XFont titleFont = new XFont("Arial", 16, XFontStyle.Bold);
            XSize titleSize = gfx.MeasureString(title, titleFont);
            gfx.DrawString(title, titleFont, XBrushes.Black, new XRect((page.Width - titleSize.Width) / 2, yPos, page.Width, titleSize.Height), XStringFormats.TopLeft);
            // Vẽ tên trường
            yPos += (int)titleSize.Height + 50;
            gfx.DrawString("Mã Hàng", new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, new XRect(50, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
            gfx.DrawString("Tên Hàng", new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, new XRect(350, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
            gfx.DrawString("Số Lượng", new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, new XRect(150, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
            gfx.DrawString("Giá", new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, new XRect(250, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
            

            yPos += 20;
            decimal TongTien = 0;
            foreach (DataRow row in dondathang.Rows)
            {
                string itemCode = row["mahang"].ToString();
                string itemName = row["tenhang"].ToString();
                int quantity = int.Parse(row["soluong"].ToString());
                decimal unitPrice = decimal.Parse(row["giaban"].ToString());
               
                
                

                decimal itemAmount = quantity * unitPrice;
                TongTien += itemAmount;

                gfx.DrawString(itemCode, new XFont("Arial", 12), XBrushes.Black, new XRect(50, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
                gfx.DrawString(quantity.ToString(), new XFont("Arial", 12), XBrushes.Black, new XRect(150, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
                gfx.DrawString(unitPrice.ToString(), new XFont("Arial", 12), XBrushes.Black, new XRect(250, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
                gfx.DrawString(itemName, new XFont("Arial", 12), XBrushes.Black, new XRect(350, yPos, page.Width - 100, 20), XStringFormats.TopLeft);

                yPos += 20;
               
            }

            gfx.DrawString("Tổng Tiền", new XFont("Arial", 12, XFontStyle.Bold), XBrushes.Black, new XRect(250, yPos, page.Width - 100, 20), XStringFormats.TopLeft);
            gfx.DrawString(TongTien.ToString(), new XFont("Arial", 12), XBrushes.Black, new XRect(350, yPos, page.Width - 100, 20), XStringFormats.TopLeft);


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (.pdf)|.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                document.Save(filePath);
                MessageBox.Show("Xuất Hóa Đơn Thành Công!");

            }*/
            mayin mayin = new mayin();
            mayin.ShowDialog();
        }
    }
}
