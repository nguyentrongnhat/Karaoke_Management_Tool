using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance = new NhanVienDAO();
        private NhanVienDAO()
        {

        }
        public static NhanVienDAO getInstance()
        {
            return instance;
        }
        public List<NhanVien> GetAll()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.NhanViens.ToList();
            }

        }

        public NhanVien Get(string id)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.NhanViens.FirstOrDefault(nv => nv.MSNV.Equals(id));
            }
        }

        public NhanVien GetByCMND(string CMND)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.NhanViens.FirstOrDefault(nv => nv.CMND.Equals(CMND));
            }
        }

        public void Add(NhanVien x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.AUTO_IDNV(x.HoTen, x.NgaySinh, x.GioiTinh, x.CMND, x.DiaChi, x.DienThoai);
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
                else
                {
                    db.NhanViens.Remove(nhanvien);
                    db.SaveChanges();
                }      
            }
        }

        public void Update(NhanVien x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var nhanvien = db.NhanViens.FirstOrDefault(nv => nv.MSNV.Equals(x.MSNV));
                if (nhanvien == null)
                    throw new Exception("Không được sửa ID!");
                nhanvien.HoTen = x.HoTen;
                nhanvien.NgaySinh = x.NgaySinh;
                nhanvien.GioiTinh = x.GioiTinh;
                nhanvien.CMND = x.CMND;
                nhanvien.DiaChi = x.DiaChi;
                nhanvien.DienThoai = x.DienThoai;
                db.SaveChanges();
            }
        } 
    }
}
