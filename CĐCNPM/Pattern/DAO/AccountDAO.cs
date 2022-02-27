using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CĐCNPM.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = new AccountDAO();
        private AccountDAO() { }
        public static AccountDAO getInstance()
        {
            return instance;
        }
        public List<Account> GetAll()
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Accounts.ToList();
            }

        }
        public Account Get(string username, string password)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Accounts.FirstOrDefault(ac => ac.Password.Equals(password) && ac.Username.Equals(username));
            }
        }
        public Account GetbyUsername(string username)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                return db.Accounts.FirstOrDefault(ac => ac.Username.Equals(username));
            }
        }
        public void Add(Account x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                db.Accounts.Add(x);
                db.SaveChanges();
            }
        }
        public void Delete(string username, string password)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var acc = db.Accounts.FirstOrDefault(ac => ac.Username.Equals(username) && ac.Password.Equals(password));
                if (acc == null)
                    throw new Exception("Ma so khong hop le");
                db.Accounts.Remove(acc);
                db.SaveChanges();
            }
        }
        public void Update(Account x)
        {
            using (var db = new QuanLy_KaraokeEntities())
            {
                var acc = db.Accounts.FirstOrDefault(ac => ac.Password.Equals(x.Password) && ac.Username.Equals(x.Username));
                if (acc == null)
                    throw new Exception("Không được sửa ID!");
                acc.isAdmin = x.isAdmin;
                db.SaveChanges();
            }
        }
    }
}
