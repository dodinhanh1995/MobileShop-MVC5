using Model.EF;
using PagedList;
using System.Linq;

namespace Model.DAO
{
    public class ContactDAO
    {
        static volatile ContactDAO instance;
        static object key = new object();
        static MobileShopDbContext db;

        public static ContactDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new ContactDAO();
                        db = new MobileShopDbContext();
                    }
                }
                return instance;
            }
        }

        ContactDAO()
        { }

        public IPagedList<Contact> GetListAll(string sortOrder, string searchString, int? page)
        {
            var contacts = from p in db.Contacts select p;
            if (!string.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(x => x.Name.Contains(searchString) || x.Phone.Contains(searchString) || x.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "status_desc":
                    contacts = contacts.OrderByDescending(x => x.Status);
                    break;
                case "Date":
                    contacts = contacts.OrderBy(x => x.CreatedDate);
                    break;
                case "date_desc":
                    contacts = contacts.OrderByDescending(x => x.CreatedDate);
                    break;
                default:
                    contacts = contacts.OrderBy(x => x.Status);
                    break;
            }

            return contacts.ToPagedList(page ?? 1, 10);
        }

        public Contact GetDetail(int id)
        {
            return db.Contacts.Find(id);
        }

        public bool Delete(string id)
        {
            try
            {
                var contact = GetDetail(int.Parse(id));
                if (contact != null)
                {
                    db.Contacts.Remove(contact);
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

        public bool ChangeStatus(int id)
        {
            Contact contact = GetDetail(id);
            contact.Status = !contact.Status;
            db.SaveChanges();
            return contact.Status;
        }
    }
}
