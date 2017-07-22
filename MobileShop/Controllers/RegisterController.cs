using MobileShop.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MobileShop.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, "question", "answer", true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }
            return View(model);
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Địa chỉ email đã được sử dụng. Vui lòng nhập địa chỉ khác.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Mật khẩu không hợp lệ. Vui lòng nhập lại.";

                default:
                    return "Có lỗi xảy ra khi đăng ký. Vui lòng thử lại.";
            }
        }
        #endregion
    }
}