using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CĐCNPM.DAO
{
    public class PhongDAO
    {
        private static PhongDAO instance = new PhongDAO();
        private PhongDAO() { }
        public static PhongDAO getInstance()
        {
            return instance;
        }
        public List<Phong> GetAll()
        {
            using (var db = new QuanLy_KaraokeEntities())
            { 
                return db.Phongs.ToList();
            }

        }
        public Object GetList()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = from p in db.Phongs
                    select new { MSPhong = p.MSPhong, LoaiPhong = p.LoaiPhong, TrangThai = p.Trangthai, GiaPhong = p.GiaPhong };
                return rs.ToList();
            }

        }
        public List<Phong> GetPhongThuong()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = (from p in db.Phongs
                          where p.LoaiPhong.Equals("Thường")
                          select p);
                return rs.ToList();
            }

        }
        public List<Phong> GetPhongVip()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var rs = (from p in db.Phongs
                          where p.LoaiPhong.Equals("Vip")
                          select p);
                return rs.ToList();
            }

        }
        public Phong Get(string id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Phongs.FirstOrDefault(p => p.MSPhong.Equals(id));
            }
        }
        public void Add(Phong x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.NHAP_PHONG(x.LoaiPhong, x.GiaPhong);
                db.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var phong = db.Phongs.FirstOrDefault(p => p.MSPhong.Equals(id));
                if (phong == null)
                    throw new Exception("Ma so khong hop le");
                db.Phongs.Remove(phong);
                db.SaveChanges();
            }
        }
        public void Update(Phong x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var phong = db.Phongs.FirstOrDefault(p => p.MSPhong.Equals(x.MSPhong));
                if (phong == null)
                    throw new Exception("Không được sửa ID!");
                phong.LoaiPhong = x.LoaiPhong;
                phong.Trangthai = x.Trangthai;
                phong.GiaPhong = x.GiaPhong;
                db.SaveChanges();
            }
        }
    }
}
