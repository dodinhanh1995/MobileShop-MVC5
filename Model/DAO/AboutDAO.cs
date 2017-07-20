using Model.EF;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Model.DAO
{
    public class AboutDAO
    {
        static volatile AboutDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static AboutDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new AboutDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        AboutDAO()
        { }

        public About GetAbout()
        {
            return db.Abouts.First();
        }

        public bool Update(About about)
        {
            try
            {
                var temp = GetAbout();
                temp.Name = about.Name;
                temp.Detail = about.Detail;
                temp.MetaTitle = about.MetaTitle;
                temp.MetaKeywords = about.MetaKeywords;
                temp.MetaDescriptions = about.MetaDescriptions;
                temp.Status = about.Status;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool ChangeStatus()
        {
            About about = GetAbout();
            about.Status = !about.Status;
            db.SaveChanges();
            return about.Status;
        }

        public string ChangeImage(string image)
        {
            var about = GetAbout();
            if (about == null)
                return "Thông tin quảng cáo không tồn tại!";
            about.Image = image;
            db.SaveChanges();
            return "";
        }
    }
}
