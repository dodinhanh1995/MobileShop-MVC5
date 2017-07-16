using Model.EF;
using System.Linq;
using System.Collections.Generic;

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
    }
}
