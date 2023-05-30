using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System;
using System.Drawing;
using System.Drawing.Printing;

namespace BanhangForm
{
    public partial class mayin : Form
    {
        string chuoiketnoi = @"Data Source=MSI\XOAII;Initial Catalog=BANHANG_DT;Integrated Security=True";
        SqlConnection SqlConnection = null;
        public mayin()
        {
            InitializeComponent();
        }

        private void mayin_Load(object sender, EventArgs e)
        {
            //int selectedHoaDon = 1;
            if (chuoiketnoi == null)
            {
                SqlConnection = new SqlConnection(chuoiketnoi);
            }
            string sql = @"DECLARE @randomInvoice INT;
SELECT TOP 1 @randomInvoice = Sohoadon
FROM dondathang
ORDER BY NEWID();

SELECT dondathang.Sohoadon, dondathang.makhachhang, dondathang.manhanvien, dondathang.ngaydathang, dondathang.ngaygiaohang, dondathang.noigiaohang, dondathang.Tongtien,
       chitietdathang.mahang, chitietdathang.giaban, chitietdathang.soluong, chitietdathang.mucgiamgia, mathang.tenhang, mathang.donvitinh
FROM dondathang
INNER JOIN chitietdathang ON chitietdathang.sohoadon = dondathang.Sohoadon
INNER JOIN mathang ON chitietdathang.mahang = mathang.mahang
WHERE dondathang.Sohoadon = @randomInvoice;

";
            SqlDataAdapter sqlData = new SqlDataAdapter(sql, chuoiketnoi);
            DataSet ds = new DataSet();
            sqlData.Fill(ds, "DataTble1");
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BanhangForm.Report1.rdlc";
            ReportDataSource report = new ReportDataSource();
            report.Name = "DataSet1";
            report.Value = ds.Tables["DataTble1"];
            this.reportViewer1.LocalReport.DataSources.Add(report);

            this.reportViewer1.RefreshReport();




        }
        }
        
    }


