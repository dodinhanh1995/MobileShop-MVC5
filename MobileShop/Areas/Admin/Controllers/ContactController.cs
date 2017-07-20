using Model.DAO;
using Model.EF;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StatusSortParm = string.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            return View(ContactDAO.Instance.GetListAll(sortOrder, searchString, page));
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] contactIds = form["contactId"].Split(',');

            foreach (string item in contactIds)
            {
                ContactDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Contact contact = ContactDAO.Instance.GetDetail(id.Value);
            if (contact == null)
                return HttpNotFound();
            return View(contact);
        }

        public bool ChangeStatus(int id)
        {
            return ContactDAO.Instance.ChangeStatus(id);
        }

    }
}