using Model.EF;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Model.DAO
{
    public class UserDAO
    {
        static MobileShopDbContext db;

        static volatile UserDAO instance;

        static object key = new object();

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new UserDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        UserDAO() { }

        public List<User> GetListAll()
        {
            return db.Users.ToList();
        }

        public bool Create(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public User GetDetail(int id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(string id)
        {
            try
            {
                var user = db.Users.Find(int.Parse(id));
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
