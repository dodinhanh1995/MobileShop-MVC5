using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class MenuTypeDAO
    {
        static MenuTypeDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static MenuTypeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new MenuTypeDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        MenuTypeDAO() { }

        public List<MenuType> GetListAll()
        {
            return db.MenuTypes.OrderByDescending(x=>x.Id).ToList();
        }

        public bool Create(string name)
        {
            try
            {
                db.MenuTypes.Add(new MenuType { Name = name });
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public MenuType GetDetail(int id)
        {
            return db.MenuTypes.Find(id);
        }

        public bool CheckNameIsExist(string name)
        {
            return db.MenuTypes.Count(x => x.Name == name.Trim()) > 0;
        }

        public bool Update(int id, string name)
        {
            try
            {
                var temp = GetDetail(id);
                temp.Name = name;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var menuType = GetDetail(int.Parse(id));
                if (menuType != null)
                {
                    db.MenuTypes.Remove(menuType);
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
