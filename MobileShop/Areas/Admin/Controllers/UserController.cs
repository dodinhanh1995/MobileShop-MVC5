using Common;
using MobileShop.Areas.Admin.Models;
using Model.DAO;
using Model.EF;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(UserDAO.Instance.GetListAll());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] userIds = form["userId"].Split(',');

            foreach (string item in userIds)
            {
                UserDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            StatusList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (UserDAO.Instance.CheckUsernameIsExist(userModel.UserName))
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");
                if (UserDAO.Instance.CheckEmailIsExist(userModel.Email))
                    ModelState.AddModelError("UserName", "Địa chỉ email đã tồn tại");
                else if (UserDAO.Instance.Create(new User
                {
                    UserName = userModel.UserName,
                    Password = userModel.Password,
                    Address = userModel.Address,
                    Email = userModel.Email,
                    FullName = userModel.FullName,
                    Phone = userModel.Phone,
                    Status = userModel.Status,
                }))
                {
                    SetAlert("Tạo người dùng <b>" + userModel.UserName + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới người dùng! Vui lòng thử lại.");
            }
            StatusList();
            return View(userModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = UserDAO.Instance.GetDetail(id.Value);
            if (user == null)
                return HttpNotFound();
            StatusList(user.Status);
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude ="CreatedDate, CreatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                if (UserDAO.Instance.CheckEmailIsExist(user.Email) && UserDAO.Instance.GetDetail(user.Id).Email != user.Email)
                    ModelState.AddModelError("UserName", "Địa chỉ email đã tồn tại");
                else if (UserDAO.Instance.Update(user))
                {
                    SetAlert("Cập nhật thông tin tài khoản <b>" + user.UserName + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin! Vui lòng thử lại.");
            }
            StatusList(user.Status);
            return View(user);
        }

        public void StatusList(int selected = -1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Chưa kích hoạt", Value = "0", Selected = selected == 0 });
            items.Add(new SelectListItem { Text = "Kích hoạt", Value = "1", Selected = selected == 1 });
            items.Add(new SelectListItem { Text = "Tạm khóa", Value = "2", Selected = selected == 2 });
            ViewBag.Status = items;
        }
    }
}