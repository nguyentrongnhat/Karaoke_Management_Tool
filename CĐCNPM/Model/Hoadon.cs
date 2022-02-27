using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.Model
{
    public class Hoadon
    {
        
        public int ID_Phien { get; set; }
        public string LoaiDV { get; set; }
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public int GiaDV { get; set; }
        public int SoLuong { get; set; }

        public int dongia { get; set; } = 0;

        public Hoadon()
        {
      
        }

        public Hoadon(int iD_Phien, string loaiDV, string maDV, string tenDV, int giaDV, int soLuong)
        {
            ID_Phien = iD_Phien;
            LoaiDV = loaiDV;
            MaDV = maDV;
            TenDV = tenDV;
            GiaDV = giaDV;
            SoLuong = soLuong;
        }

        public Hoadon(int iD_Phien, string loaiDV, string maDV, string tenDV, int giaDV, int soLuong, int dongia)
        {
            ID_Phien = iD_Phien;
            LoaiDV = loaiDV;
            MaDV = maDV;
            TenDV = tenDV;
            GiaDV = giaDV;
            SoLuong = soLuong;
            this.dongia = dongia;
        }
    }
}
