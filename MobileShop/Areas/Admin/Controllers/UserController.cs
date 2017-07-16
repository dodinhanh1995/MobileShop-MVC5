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
    }
}