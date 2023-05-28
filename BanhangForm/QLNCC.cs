using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm.NCC
{
    internal class QLNCC
    {
        private string macongty;
        private string tencongty;
        private string tengiaodich;
        private string diachi;
        private string dienthoai;
        private string fax;
        private string email;

        public QLNCC()
        {
        }

        public QLNCC(string macongty, string tencongty, string tengiaodich, string diachi, string dienthoai, string fax, string email)
        {
            this.macongty = macongty;
            this.tencongty = tencongty;
            this.tengiaodich = tengiaodich;
            this.diachi = diachi;
            this.dienthoai = dienthoai;
            this.fax = fax;
            this.email = email;
        }

        public string Macongty { get => macongty; set => macongty = value; }
        public string Tencongty { get => tencongty; set => tencongty = value; }
        public string Tengiaodich { get => tengiaodich; set => tengiaodich = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Dienthoai { get => dienthoai; set => dienthoai = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }
    }
}
