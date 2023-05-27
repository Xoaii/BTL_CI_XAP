using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm.LoaiHang
{
    internal class QLloaiHang
    {
        private int maloaihang;
        private string tenloaihang;

        public QLloaiHang()
        {
        }

        public QLloaiHang(int maloaihang, string tenloaihang)
        {
            this.maloaihang = maloaihang;
            this.tenloaihang = tenloaihang;
        }

        public int Maloaihang { get => maloaihang; set => maloaihang = value; }
        public string Tenloaihang { get => tenloaihang; set => tenloaihang = value; }
    }
}
