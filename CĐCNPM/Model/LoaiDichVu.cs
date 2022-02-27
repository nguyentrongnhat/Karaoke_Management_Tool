using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.Model
{
    public class LoaiDichVu
    {
        public string Loai { get; set; }
        public LoaiDichVu()
        {

        }

        public LoaiDichVu(string loai)
        {
            Loai = loai;
        }
    }
}
