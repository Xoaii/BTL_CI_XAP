using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm.DoDatHang
{
    internal class QLdonDatHang
    {
        private int sohoadon;
        private string makhachhang;
        private string manhanvien;
        private DateTime ngaydathang;
        private DateTime ngaygiaohang;
        private string noigiaohang;

        public QLdonDatHang()
        {
        }

        public QLdonDatHang(int sohoadon, string makhachhang, string manhanvien, DateTime ngaydathang, DateTime ngaygiaohang, string noigiaohang)
        {
            this.sohoadon = sohoadon;
            this.makhachhang = makhachhang;
            this.manhanvien = manhanvien;
            this.ngaydathang = ngaydathang;
            this.ngaygiaohang = ngaygiaohang;
            this.noigiaohang = noigiaohang;
        }

        public int Sohoadon { get => sohoadon; set => sohoadon = value; }
        public string Makhachhang { get => makhachhang; set => makhachhang = value; }
        public string Manhanvien { get => manhanvien; set => manhanvien = value; }
        public DateTime Ngaydathang { get => ngaydathang; set => ngaydathang = value; }
        public DateTime Ngaygiaohang { get => ngaygiaohang; set => ngaygiaohang = value; }
        public string Noigiaohang { get => noigiaohang; set => noigiaohang = value; }
    }
}
