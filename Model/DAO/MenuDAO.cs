using Model.EF;
using PagedList;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Model.DAO
{
    public class MenuDAO
    {
        static MenuDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static MenuDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new MenuDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        MenuDAO()
        { }

        public IPagedList<Menu> GetListAll(string sortOrder, string searchString, int? page)
        {
            var menu = from m in db.Menus select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(x => x.Text.Contains(searchString)  || x.MenuType.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(x => x.Text);
                    break;
                case "menuType":
                    menu = menu.OrderBy(x => x.TypeID);
                    break;
                case "menuType_desc":
                    menu = menu.OrderByDescending(x => x.TypeID);
                    break;
                default:
                    menu = menu.OrderBy(x => x.Text);
                    break;
            }

            return menu.ToPagedList(page ?? 1, 10);
        }

        public bool Create(Menu menu)
        {
            try
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Menu GetDetail(int id)
        {
            return db.Menus.Find(id);
        }

        public bool CheckNameIsExist(string text, int typeId)
        {
            return db.Menus.Count(x => x.Text == text.Trim() && x.TypeID == typeId) > 0;
        }

        public bool Update(Menu menu)
        {
            try
            {
                db.Set<Menu>().AddOrUpdate(menu);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var menu = GetDetail(int.Parse(id));
                if (menu != null)
                {
                    db.Menus.Remove(menu);
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
