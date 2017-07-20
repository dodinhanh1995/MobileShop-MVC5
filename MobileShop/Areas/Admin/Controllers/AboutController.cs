using Model.DAO;
using Model.EF;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            var about = AboutDAO.Instance.GetAbout();
            StatusList(about.Status);
            return View(about);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(About about)
        {
            if (ModelState.IsValid)
            {
                if (AboutDAO.Instance.Update(about))
                {
                    SetAlert("Cập nhật thông tin thành công!");
                    return RedirectToAction("Index");
                }
                SetAlert("Có lỗi xảy ra khi cập nhật! Vui lòng thử lại.");
            }
            StatusList();
            return View(about);
        }

        public string ChangeImage(int id, string image)
        {
            if (id.ToString() == null)
                return "Mã giới thiệu không tồn tại!";
            return AboutDAO.Instance.ChangeImage(image);
        }

        public bool ChangeStatus()
        {
            return AboutDAO.Instance.ChangeStatus();
        }

        public void StatusList(bool selected = true)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Hiển thị", Value = bool.TrueString, Selected = selected == true });
            items.Add(new SelectListItem { Text = "Ẩn", Value = bool.FalseString, Selected = selected == false });
            ViewBag.Status = items;
        }
    }
}