using Model.DAO;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return HttpNotFound();
            ViewBag.CategoryName = ProductCategoryDAO.Instance.GetDetail(categoryName);
            return View(ProductDAO.Instance.GetProductByCategory(categoryName));
        }
    }
}