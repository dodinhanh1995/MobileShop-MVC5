using Model.DAO;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NewProducts = ProductDAO.Instance.ListAll();
            ViewBag.FeatureProducts = ProductDAO.Instance.ListAll(true);
            return View();
        }
    }
}