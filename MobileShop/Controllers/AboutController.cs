using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View(Model.DAO.AboutDAO.Instance.GetAbout());
        }
    }
}