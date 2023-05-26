using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm
{
    internal class QLmatHang
    {
        private string maHang;
        private string tenHang;
        private string maCongTy;
        private string maloaihang;
        private int soluong;
        private string donviTinh;
        private SqlMoney giaHang;

        public QLmatHang()
        {
        }

        public QLmatHang(string maHang, string tenHang, string maCongTy, string maloaihang, int soluong, string donviTinh, SqlMoney giaHang)
        {
            this.maHang = maHang;
            this.tenHang = tenHang;
            this.maCongTy = maCongTy;
            this.maloaihang = maloaihang;
            this.soluong = soluong;
            this.donviTinh = donviTinh;
            this.giaHang = giaHang;
        }

        public string MaHang { get => maHang; set => maHang = value; }
        public string TenHang { get => tenHang; set => tenHang = value; }
        public string MaCongTy { get => maCongTy; set => maCongTy = value; }
        public string Maloaihang { get => maloaihang; set => maloaihang = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public string DonviTinh { get => donviTinh; set => donviTinh = value; }
        public SqlMoney GiaHang { get => giaHang; set => giaHang = value; }
    }
}
