using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.DAO
{
    class YC_DichVuDAO
    {
        private static YC_DichVuDAO instance = new YC_DichVuDAO();
        private YC_DichVuDAO()
        {

        }
        public static YC_DichVuDAO getInstance()
        {
            return instance;
        }
        public List<YC_dichvu> GetAll()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.YC_dichvu.ToList();
            }

        }

        public YC_dichvu Get(int id_phien, string madv)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = (from yc in db.YC_dichvu
                        where yc.ID_Phien == id_phien && yc.MaDV.Equals(madv)
                        select new { Ma = yc.MaDV, Id = yc.ID_Phien, count = yc.Soluong }).ToArray();
                YC_dichvu ycdv = new YC_dichvu();
                ycdv.ID_Phien = rs[0].Id;
                ycdv.Soluong = rs[0].count;
                ycdv.MaDV = rs[0].Ma;
                return ycdv;
            }
        }

        public void Add(YC_dichvu x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.YC_dichvu.Add(x);
                db.SaveChanges();
            }
        }

        public void Delete(YC_dichvu ycdv)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var yc = db.YC_dichvu.FirstOrDefault(dv => dv.MaDV.Equals(ycdv.MaDV) && dv.ID_Phien.Equals(ycdv.ID_Phien));
                if (yc == null)
                    throw new Exception("Ma so khong hop le");
                db.YC_dichvu.Remove(yc);
                db.SaveChanges();
            }
        }

        public void Update(YC_dichvu x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var dichvu = db.YC_dichvu.FirstOrDefault(dv => dv.MaDV.Equals(x.MaDV) && dv.ID_Phien == x.ID_Phien);
                if (dichvu == null)
                    throw new Exception("Không được sửa ID!");
                dichvu.Soluong = x.Soluong;
                db.SaveChanges();
            }
        }
    }
}
