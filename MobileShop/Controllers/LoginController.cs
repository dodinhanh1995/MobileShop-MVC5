using Common.Enum;
using MobileShop.Models;
using Model.DAO;
using Model.EF;
using System.Web.Mvc;
using System.Web.Security;

namespace MobileShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Username, model.Password))
                {
                    switch (UserDAO.Instance.CheckStatus(model.Username))
                    {
                        case (int)ELoginStatus.Inactive:
                            ModelState.AddModelError("", "Tải khoản chưa được kích hoạt!");
                            break;
                        case (int)ELoginStatus.IsLocked:
                            ModelState.AddModelError("", "Tải khoản đã bị khóa!");
                            break;
                        default:
                            FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                            if (model.Username == "admin" || model.Username == "admin@gmail.com")
                                return RedirectToAction("Index", "Admin/Dashboard");
                            else
                                return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            if (User.Identity.Name == "admin" || User.Identity.Name == "admin@gmail.com")
                return RedirectToAction("Index", "Login");
            return RedirectToAction("Index", "Home");
        }
    }
}