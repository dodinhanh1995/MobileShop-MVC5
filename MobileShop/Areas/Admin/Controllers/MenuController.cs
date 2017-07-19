using Model.DAO;
using Model.EF;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MenuTypeSortParm = sortOrder == "menuType" ? "menuType_desc" : "menuType";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            return View(MenuDAO.Instance.GetListAll(sortOrder, searchString, page));
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] menuIds = form["menuId"].Split(',');

            foreach (string item in menuIds)
            {
                MenuDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            MenuTypeList();
            DisplayOrderList();
            TargetList();
            StatusList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (MenuDAO.Instance.CheckNameIsExist(menu.Text, menu.TypeID.Value))
                    ModelState.AddModelError("", "Tên menu đã tồn tại");
                else if (MenuDAO.Instance.Create(menu))
                {
                    SetAlert("Tạo mới menu <b>" + menu.Text + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới menu! Vui lòng thử lại.");
            }
            MenuTypeList();
            DisplayOrderList();
            TargetList();
            StatusList();
            return View(menu);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Menu menu = MenuDAO.Instance.GetDetail(id.Value);
            if (menu == null)
                return HttpNotFound();
            MenuTypeList(menu.TypeID ?? 0);
            DisplayOrderList(menu.DisplayOrder);
            TargetList(menu.Target);
            StatusList(menu.Status);
            return View(menu);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit( Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (MenuDAO.Instance.CheckNameIsExist(menu.Text, menu.TypeID.Value) && !MenuDAO.Instance.GetDetail(menu.Id).Text.Equals(menu.Text))
                    ModelState.AddModelError("", "Tên menu đã tồn tại");
                else if (MenuDAO.Instance.Update(menu))
                {
                    SetAlert("Cập nhật thông tin menu <b>" + menu.Text + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin! Vui lòng thử lại.");
            }
            MenuTypeList();
            DisplayOrderList();
            TargetList();
            StatusList();
            return View(menu);
        }

        public void StatusList(bool selected = true)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Hiển thị", Value = bool.TrueString, Selected = selected == true });
            items.Add(new SelectListItem { Text = "Ẩn", Value = bool.FalseString, Selected = selected == false });
            ViewBag.Status = items;
        }

        public void MenuTypeList(int selected = -1)
        {
            ViewBag.TypeId = new SelectList(MenuTypeDAO.Instance.GetListAll(), "Id", "Name", selected);
        }

        public void DisplayOrderList(int selected = -1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = selected == i });
            }
            ViewBag.DisplayOrder = items;
        }

        public void TargetList(string selected = "_self")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Mở trong trang", Value = "_self", Selected = selected == "_self" });
            items.Add(new SelectListItem { Text = "Mở trang con", Value = "_blank", Selected = selected == "_blank" });
            ViewBag.Target = items;
        }
    }
}