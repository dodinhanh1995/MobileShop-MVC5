using Model.EF;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using Common;

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
                user.Password = Encryptor.MD5Hash(user.Password);
                user.CreatedDate = DateTime.Now;
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

        public bool CheckUsernameIsExist(string username)
        {
            return db.Users.Count(x => x.UserName == username) > 0;
        }

        public bool CheckEmailIsExist(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }

        public bool Update(User user)
        {
            User userChange = db.Users.SingleOrDefault(x => x.Id == user.Id);
            try
            {
                if (userChange != null)
                {
                    userChange.FullName = user.FullName;
                    userChange.Address = user.Address;
                    userChange.Email = user.Email;
                    userChange.Phone = user.Phone;
                    userChange.Status = user.Status;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
            
        }

        public bool Delete(string id)
        {
            try
            {
                var user = db.Users.Find(int.Parse(id));
                if (user != null && user.UserName != "admin")
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
