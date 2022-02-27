using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.DAO
{
    class PhienDungPhongDAO
    {
        private static PhienDungPhongDAO instance = new PhienDungPhongDAO();
        private PhienDungPhongDAO()
        {

        }
        public static PhienDungPhongDAO getInstance()
        {
            return instance;
        }
        public List<PhienDungPhong> GetAll()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.PhienDungPhongs.ToList();
            }

        }

        public float Diff(TimeSpan Start, TimeSpan End)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = db.GET_DIFF(Start, End).ToArray();
                return (float)rs[0].Value;
            }
        }

        public Object GetList()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = from p in db.PhienDungPhongs
                         select new {ID = p.ID, MaPhong = p.MSPhong, NgayDung = p.Ngaydung, GioBatDau = p.Batdau, GioKetThuc = p.Ketthuc, ThanhToan = p.Thanhtoan, Dongia = p.Dongia};
                return rs.ToList();
            }

        }

        public List<PhienDungPhong> Fillter(DateTime fromi, DateTime to)
        {
            using(var db  =  new QuanLy_KaraokeEntities())
            {
                List<PhienDungPhong> list = new List<PhienDungPhong>();
                var query = (from p in db.PhienDungPhongs
                         where p.Ngaydung >= fromi
                         where p.Ngaydung <= to
                         select new { ID = p.ID, MaPhong = p.MSPhong, NgayDung = p.Ngaydung, GioBatDau = p.Batdau, 
                             GioKetThuc = p.Ketthuc, ThanhToan = p.Thanhtoan, Dongia = p.Dongia }).ToArray();
                for (int i = 0; i < query.Length; i++)
                {
                    PhienDungPhong p = new PhienDungPhong();
                    p.ID = query[i].ID;
                    p.MSPhong = query[i].MaPhong;
                    p.Ngaydung = query[i].NgayDung;
                    p.Batdau = query[i].GioBatDau;
                    p.Ketthuc = query[i].GioKetThuc;
                    p.Thanhtoan = query[i].ThanhToan;
                    p.Dongia = query[i].Dongia;
                    list.Add(p);
                }
                return list;
            }
            
        }

        public void Create(string MSPhong)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.PHIEN_DUNG_PHONG(MSPhong);
                db.SaveChanges();
            }
        }

        public List<PhienDungPhong> Get_Session_NotEnd(string MSPhong, bool Thanhtoan)
        {
            List<PhienDungPhong> list = new List<PhienDungPhong>();
            using (var db = new QuanLy_KaraokeEntities())
            {
                var query =
                   (from pdp in db.PhienDungPhongs
                    where (pdp.MSPhong.Equals(MSPhong) && pdp.Thanhtoan == false)
                    select new
                    {
                        ID = pdp.ID,
                        MSPhong = pdp.MSPhong,
                        Ngaydung = pdp.Ngaydung,
                        Batdau = pdp.Batdau,
                        Ketthuc = pdp.Ketthuc,
                        Thanhtoan = pdp.Thanhtoan,
                        Dongia = pdp.Dongia
                    }).ToArray();
                for(int i = 0; i < query.Length; i++)
                {
                    PhienDungPhong p = new PhienDungPhong();
                    p.ID = query[i].ID;
                    p.MSPhong = query[i].MSPhong;
                    p.Ngaydung = query[i].Ngaydung;
                    p.Batdau = query[i].Batdau;
                    p.Ketthuc = query[i].Ketthuc;
                    p.Thanhtoan = query[i].Thanhtoan;
                    p.Dongia = query[i].Dongia;
                    list.Add(p);
                }

                return list;
            }
        }

        public void Add(NhanVien x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.NhanViens.Add(x);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var nhanvien = db.NhanViens.FirstOrDefault(nv => nv.MSNV.Equals(id));
                if (nhanvien == null)
                    throw new Exception("Ma so khong hop le");
                db.NhanViens.Remove(nhanvien);
                db.SaveChanges();
            }
        }

        public void Update(PhienDungPhong x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var phien = db.PhienDungPhongs.FirstOrDefault(pdp => pdp.ID.Equals(x.ID));
                if (phien == null)
                    throw new Exception("Không được sửa ID!");
                phien.Thanhtoan = x.Thanhtoan;
                phien.Dongia = x.Dongia;
                phien.Ketthuc = x.Ketthuc;
                db.SaveChanges();
            }
        }
       
    }
}
