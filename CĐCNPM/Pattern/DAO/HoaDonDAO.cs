using CĐCNPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.DAO
{
    class HoaDonDAO
    {
        private static HoaDonDAO instance = new HoaDonDAO();
        private HoaDonDAO()
        {

        }
        public static HoaDonDAO getInstance()
        {
            return instance;
        }
        public Object Bill(String id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {   
                var query =
                   (from yc in db.YC_dichvu
                    join dv in db.Dichvus on yc.MaDV equals dv.MaDV
                    join pd in db.PhienDungPhongs on yc.ID_Phien equals pd.ID
                    where(pd.MSPhong.Equals(id))
                    select new
                    {
                        ID = pd.MSPhong,
                        TenDV = dv.TenDV,
                        GiaDV = dv.GiaDV,
                        SoLuong = yc.Soluong
                    }).ToList();
                return query;
            }
        }

        public List<Hoadon> get_ListBill(String id)
        {
            List<Hoadon> list = new List<Hoadon>();
            using (var db = new QuanLy_KaraokeEntities())
            {
                var query =
                   (from yc in db.YC_dichvu
                    join dv in db.Dichvus on yc.MaDV equals dv.MaDV
                    join pd in db.PhienDungPhongs on yc.ID_Phien equals pd.ID
                    where (pd.MSPhong.Equals(id) && pd.Thanhtoan == false)
                    select new
                    {
                        ID_Phien = pd.ID,
                        LoaiDV = dv.LoaiDV,
                        MaDV = dv.MaDV,
                        TenDV = dv.TenDV,
                        GiaDV = dv.GiaDV,
                        SoLuong = yc.Soluong
                    }).ToArray();
                
                for( int i = 0; i < query.Length; i++)
                {
                    Hoadon hoadon = new Hoadon();
                    hoadon.ID_Phien = (int) query[i].ID_Phien;
                    hoadon.LoaiDV = query[i].LoaiDV;
                    hoadon.MaDV = query[i].MaDV;
                    hoadon.TenDV = query[i].TenDV;
                    hoadon.SoLuong = (int) query[i].SoLuong;
                    hoadon.GiaDV = (int) query[i].GiaDV;
                    list.Add(hoadon);
                }

                return list;
            }
        }
    }
}
