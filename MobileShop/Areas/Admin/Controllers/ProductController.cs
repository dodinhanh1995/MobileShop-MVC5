using Model.DAO;
using Model.EF;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            return View(ProductDAO.Instance.GetListAll(sortOrder, searchString, page));
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] productIds = form["productId"].Split(',');

            foreach (string item in productIds)
            {
                ProductDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            VATList();
            CategoryList();
            StatusList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (ProductDAO.Instance.CheckNameIsExist(product.Name))
                    ModelState.AddModelError("Name", "Tên sản phẩm đã tồn tại");
                else if (ProductDAO.Instance.Create(product))
                {
                    SetAlert("Tạo mới sản phẩm <b>" + product.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới sản phẩm! Vui lòng thử lại.");
            }
            VATList();
            CategoryList();
            StatusList();
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = ProductDAO.Instance.GetDetail(id.Value);
            if (product == null)
                return HttpNotFound();
            VATList(product.IncludeVAT);
            CategoryList(product.CategoryID);
            StatusList(product.Status);
            return View(product);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit([Bind(Exclude = "CreatedDate")] Product product)
        {
            var oldProduct = ProductDAO.Instance.GetDetail(product.Id);
            product.Image = oldProduct.Image;
            product.CreatedDate = oldProduct.CreatedDate;
            if (ModelState.IsValid)
            {
                if (ProductDAO.Instance.CheckNameIsExist(product.Name) && oldProduct.Name != product.Name)
                    ModelState.AddModelError("Name", "Tên sản phẩm đã tồn tại");
                else if (ProductDAO.Instance.Update(product))
                {
                    SetAlert("Cập nhật thông tin sản phẩm <b>" + product.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin! Vui lòng thử lại.");
            }
            VATList();
            CategoryList();
            StatusList();
            return View(product);
        }

        public string ConvertMetaTitle(string name)
        {
            return Common.Convertion.ToUnsignString(name);
        }

        public string ChangeImage(int id, string image)
        {
            if (id.ToString() == null)
                return "Mã sản phẩm không tồn tại!";
            return ProductDAO.Instance.ChangeImage(id, image);
        }

        public void StatusList(bool selected = true)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Hiển thị", Value = bool.TrueString, Selected = selected == true });
            items.Add(new SelectListItem { Text = "Ẩn", Value = bool.FalseString, Selected = selected == false });
            ViewBag.Status = items;
        }

        public void CategoryList(int selected = -1)
        {
            ViewBag.CategoryId = new SelectList(ProductCategoryDAO.Instance.GetListAll(), "Id", "Name", selected);
        }

        public void VATList(bool selected = true)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Đã có", Value = bool.TrueString, Selected = selected == true });
            items.Add(new SelectListItem { Text = "Chưa có", Value = bool.FalseString, Selected = selected == false });
            ViewBag.IncludeVAT = items;
        }
    }
}