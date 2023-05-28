using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanhangForm.NhanVien
{
    internal class nhanVien
    {
        private string manhanvien;
        private string ho;
        private string ten;
        private DateTime ngaysinh;
        private DateTime ngaylamviec;
        private string diachi;
        private string dienthoai;
        private SqlMoney luongcoban;
        private SqlMoney phucap;
        private int tuoi;

        public nhanVien()
        {
        }

        public nhanVien(string manhanvien, string ho, string ten, DateTime ngaysinh, DateTime ngaylamviec, string diachi, string dienthoai, SqlMoney luongcoban, SqlMoney phucap, int tuoi)
        {
            this.manhanvien = manhanvien;
            this.ho = ho;
            this.ten = ten;
            this.ngaysinh = ngaysinh;
            this.ngaylamviec = ngaylamviec;
            this.diachi = diachi;
            this.dienthoai = dienthoai;
            this.luongcoban = luongcoban;
            this.phucap = phucap;
            this.tuoi = tuoi;
        }

        public string Manhanvien { get => manhanvien; set => manhanvien = value; }
        public string Ho { get => ho; set => ho = value; }
        public string Ten { get => ten; set => ten = value; }
        public DateTime Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public DateTime Ngaylamviec { get => ngaylamviec; set => ngaylamviec = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Dienthoai { get => dienthoai; set => dienthoai = value; }
        public SqlMoney Luongcoban { get => luongcoban; set => luongcoban = value; }
        public SqlMoney Phucap { get => phucap; set => phucap = value; }
        public int Tuoi { get => tuoi; set => tuoi = value; }
    }
}
