using Model.DAO;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index(int? id)
        {
            if (id == null)
                return HttpNotFound();
            return View(ProductDAO.Instance.GetDetail(id.Value));
        }

        [ChildActionOnly]
        public ActionResult _RelatedProduct(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var related = ProductDAO.Instance.GetRelatedProductById(id.Value);
            if (related == null)
                return HttpNotFound();
            return PartialView(related);
        }
    }
}