using Model.DAO;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(UserDAO.Instance.GetListAll());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] userIds = form["userId"].Split(',');

            foreach (string item in userIds)
            {
                UserDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}