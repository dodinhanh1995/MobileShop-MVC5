using Model.EF;
using System.Linq;

namespace Model.DAO
{
    public class FooterDAO
    {
        static volatile FooterDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static FooterDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new FooterDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        FooterDAO()
        { }

        public Footer GetFooter()
        {
            return db.Footers.First();
        }

        public bool Update(string content)
        {
            try
            {
                GetFooter().Content = content;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool ChangeStatus()
        {
            Footer footer = GetFooter();
            footer.Status = !footer.Status;
            db.SaveChanges();
            return footer.Status;
        }
    }
}
