using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm
{
    internal class QLchiTietDonHang
    {
        private int soHoaDon;
        private string mahang;
        private SqlMoney giaban;
        private int soLuong;
        private double mucgiamgia;

        public QLchiTietDonHang()
        {
        }

        public QLchiTietDonHang(int soHoaDon, string mahang, SqlMoney giaban, int soLuong, double mucgiamgia)
        {
            this.soHoaDon = soHoaDon;
            this.mahang = mahang;
            this.giaban = giaban;
            this.soLuong = soLuong;
            this.mucgiamgia = mucgiamgia;
        }

        public int SoHoaDon { get => soHoaDon; set => soHoaDon = value; }
        public string Mahang { get => mahang; set => mahang = value; }
        public SqlMoney Giaban { get => giaban; set => giaban = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public double Mucgiamgia { get => mucgiamgia; set => mucgiamgia = value; }
    }
}
