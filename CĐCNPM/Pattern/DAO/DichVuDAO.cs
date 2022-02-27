using CĐCNPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.DAO
{
    public class DichVuDAO
    {
        private static DichVuDAO instance = new DichVuDAO();
        private DichVuDAO()
        {

        }
        public static DichVuDAO getInstance()
        {
            return instance;
        }
        public List<Dichvu> GetAll()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Dichvus.ToList();
            }

        }

        public List<Dichvu> GetList()
        {
            List<Dichvu> list = new List<Dichvu>();
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = (from dv in db.Dichvus
                         select new { MaDV = dv.MaDV, TenDV = dv.TenDV, LoaiDV = dv.LoaiDV, GiaDV = dv.GiaDV }).ToArray();
                for(int i = 0; i < rs.Length; i++)
                {
                    Dichvu dv = new Dichvu();
                    dv.MaDV = rs[i].MaDV;
                    dv.LoaiDV = rs[i].LoaiDV;
                    dv.TenDV = rs[i].TenDV;
                    dv.GiaDV = rs[i].GiaDV;
                    list.Add(dv);
                }
                return list;
            }
        }

        public List<DichVu_> GetList_()
        {
            List<DichVu_> list = new List<DichVu_>();
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = (from dv in db.Dichvus
                          select new { MaDV = dv.MaDV, TenDV = dv.TenDV, LoaiDV = dv.LoaiDV, GiaDV = dv.GiaDV }).ToArray();
                for (int i = 0; i < rs.Length; i++)
                {
                    DichVu_ dv = new DichVu_();
                    dv.MaDV = rs[i].MaDV;
                    dv.LoaiDV = rs[i].LoaiDV;
                    dv.TenDV = rs[i].TenDV;
                    dv.GiaDV = rs[i].GiaDV.Value;
                    list.Add(dv);
                }
                return list;
            }
        }

        public List<DichVutheoTheLoai> GetServicebyCategory (string loaidv)
        {
            List<DichVutheoTheLoai> list = new List<DichVutheoTheLoai>();

            using (var db = new QuanLy_KaraokeEntities())
            {
                var query =
                   (from dv in db.Dichvus
                    where (dv.LoaiDV.Equals(loaidv))
                    select new
                    {
                        TenDV = dv.TenDV
                    }).ToArray();

                for (int i = 0; i < query.Length; i++)
                {
                    DichVutheoTheLoai dv = new DichVutheoTheLoai();
                    dv.Ten = query[i].TenDV;
                    list.Add(dv);
                }
                return list;
            }
        }

        public List<LoaiDichVu> GetCategory()
        {
            List<LoaiDichVu> list = new List<LoaiDichVu>();

            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = (from dv in db.Dichvus
                          select new { LoaiDV = dv.LoaiDV }).Distinct().ToArray();

                for (int i = 0; i < rs.Length; i++)
                {
                    LoaiDichVu ldv = new LoaiDichVu();
                    ldv.Loai = rs[i].LoaiDV;
                    list.Add(ldv);
                }
                return list;
            }
        }

        public Dichvu Get(string id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Dichvus.FirstOrDefault(dv => dv.MaDV.Equals(id));
            }
        }

        public Dichvu GetbyName(string Name)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Dichvus.FirstOrDefault(dv => dv.TenDV.Equals(Name));
            }
        }

        public void Add(Dichvu x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.DICH_VU(x.TenDV, x.LoaiDV, x.GiaDV);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var dichvu = db.Dichvus.FirstOrDefault(dv => dv.MaDV.Equals(id));
                if (dichvu == null)
                    throw new Exception("Ma so khong hop le");
                db.Dichvus.Remove(dichvu);
                db.SaveChanges();
            }
        }

        public void Update(Dichvu x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var dichvu = db.Dichvus.FirstOrDefault(dv => dv.MaDV.Equals(x.MaDV));
                if (dichvu == null)
                    throw new Exception("Không được sửa ID!");
                dichvu.TenDV = x.TenDV;
                dichvu.LoaiDV = x.LoaiDV;
                dichvu.GiaDV = x.GiaDV;
                db.SaveChanges();
            }
        }
    }
}
