using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;

namespace MobileShop.Areas.Admin.Controllers
{
    public class MapController : BaseController
    {
        public ActionResult Index()
        {
            return View(MapDAO.Instance.GetListAll());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] mapIds = form["mapId"].Split(',');

            foreach (string item in mapIds)
            {
                MapDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            StatusList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(Map map)
        {
            if (ModelState.IsValid)
            {
                if (MapDAO.Instance.CheckNameIsExist(map.Name))
                    ModelState.AddModelError("Name", "Tên bản đồ đã tồn tại");
                else if (MapDAO.Instance.Create(map))
                {
                    SetAlert("Tạo mới bản đồ <b>" + map.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới bản đồ! Vui lòng thử lại.");
            }
            StatusList();
            return View(map);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Map map = MapDAO.Instance.GetDetailById(id.Value);
            if (map == null)
                return HttpNotFound();
            
            StatusList(map.Status);
            return View(map);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(Map map)
        {
            if (ModelState.IsValid)
            {
                if (MapDAO.Instance.CheckNameIsExist(map.Name) && MapDAO.Instance.GetDetailById(map.Id).Name != map.Name)
                    ModelState.AddModelError("Name", "Tên bản đồ đã tồn tại");
                else if (MapDAO.Instance.Update(map))
                {
                    SetAlert("Cập nhật thông tin bản đồ <b>" + map.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin! Vui lòng thử lại.");
            }
            StatusList();
            return View(map);
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
