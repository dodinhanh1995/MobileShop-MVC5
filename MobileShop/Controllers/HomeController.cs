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

        public ActionResult Search(string s, string key, int? page)
        {
            if (s != null)
                page = 1;
            else
                s = key;
            ViewBag.CurrentFilter = s;
            return View(ProductDAO.Instance.SearchList(s, page));
        }

        public JsonResult ListName(string term)
        {
            var data = ProductDAO.Instance.ListName(term);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}