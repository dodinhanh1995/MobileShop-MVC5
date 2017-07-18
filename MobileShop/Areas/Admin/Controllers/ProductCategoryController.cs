using System.Collections.Generic;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using System.Net;
using Common;

namespace MobileShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            return View(ProductCategoryDAO.Instance.GetListAll());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] categoryIds = form["categoryId"].Split(',');

            foreach (string item in categoryIds)
            {
                ProductCategoryDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            GetParentID();
            DisplayOrderList();
            StatusList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                if (ProductCategoryDAO.Instance.CheckNameIsExist(category.Name))
                    ModelState.AddModelError("Name", "Tên danh mục đã tồn tại");
                else if (ProductCategoryDAO.Instance.Create(category))
                {
                    SetAlert("Tạo mới danh mục <b>" + category.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới danh mục! Vui lòng thử lại.");
            }
            GetParentID();
            DisplayOrderList();
            StatusList();
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ProductCategory category = ProductCategoryDAO.Instance.GetDetail(id.Value);
            if (category == null)
                return HttpNotFound();
            GetParentID(category.ParentID.HasValue ? category.ParentID : null);
            DisplayOrderList(category.DisplayOrder);
            StatusList(category.Status);
            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "CreatedDate")] ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                if (ProductCategoryDAO.Instance.CheckNameIsExist(category.Name) && ProductCategoryDAO.Instance.GetDetail(category.Id).Name != category.Name.Trim())
                    ModelState.AddModelError("Name", "Tên danh mục đã tồn tại");
                else if (ProductCategoryDAO.Instance.Update(category))
                {
                    SetAlert("Cập nhật thông tin danh mục <b>" + category.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin! Vui lòng thử lại.");
            }
            GetParentID();
            DisplayOrderList();
            StatusList();
            return View(category);
        }

        public string ConvertMetaTitle(string name)
        {
            return Convertion.ToUnsignString(name);
        }

        public void GetParentID(int? selected = null)
        {
            ViewBag.ParentID = new SelectList(ProductCategoryDAO.Instance.GetListAll(), "Id", "Name", selected);
        }

        public void StatusList(bool selected = true)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Hiển thị", Value = bool.TrueString, Selected = selected == true });
            items.Add(new SelectListItem { Text = "Ẩn", Value = bool.FalseString, Selected = selected == false });
            ViewBag.Status = items;
        }

        public void DisplayOrderList(int selected = -1)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = selected == i });
            }
            ViewBag.DisplayOrder = list;
        }
    }
}