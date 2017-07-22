using Model.EF;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Model.DAO
{
    public class MapDAO
    {
        static MapDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static MapDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new MapDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        MapDAO() { }

        public List<Map> GetListAll()
        {
            return db.Maps.ToList();
        }

        public bool Create(Map map)
        {
            try
            {
                db.Maps.Add(map);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Map GetDetailById(int id)
        {
            return db.Maps.Find(id);
        }

        public bool CheckNameIsExist(string name)
        {
            return db.Maps.Count(x => x.Name == name.Trim()) > 0;
        }

        public bool Update(Map map)
        {
            try
            {
                db.Set<Map>().AddOrUpdate(map);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var map = GetDetailById(int.Parse(id));
                if (map != null)
                {
                    db.Maps.Remove(map);
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
