using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.Model
{
    public class DichVu_
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public  string LoaiDV { get; set; }
        public int GiaDV { get; set; }

        public DichVu_()
        {

        }

        public DichVu_(string maDV, string tenDV, string loaiDV, int giaDV)
        {
            MaDV = maDV;
            TenDV = tenDV;
            LoaiDV = loaiDV;
            GiaDV = giaDV;
        }
    }
}
