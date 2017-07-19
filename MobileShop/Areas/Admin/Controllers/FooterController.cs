using Model.DAO;
using Model.EF;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            return View(FooterDAO.Instance.GetFooter());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (FooterDAO.Instance.Update(f["Content"]))
            {
                SetAlert("Cập nhật thành công!");
                return RedirectToAction("Index");
            }
            SetAlert("Có lỗi xảy ra khi cập nhật! Vui lòng thử lại.");
            return View();
        }

        public bool ChangeStatus()
        {
            return FooterDAO.Instance.ChangeStatus();
        }
    }
}