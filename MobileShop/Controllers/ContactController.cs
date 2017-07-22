using BotDetect.Web.Mvc;
using Model.DAO;
using Model.EF;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {   
            ViewBag.Maps = MapDAO.Instance.GetDetailById(1);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, CaptchaValidation("CaptchaCode", "ContactCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Index([Bind(Exclude ="CreatedDate, Status")] Contact contact)
        {
            contact.CreatedDate = System.DateTime.Now;
            contact.Status = false;
            if (ModelState.IsValid)
            {
                if (ContactDAO.Instance.Create(contact))
                {
                    ViewBag.Success = "Gửi yêu cầu liên hệ thành công.";
                    return View();
                }
            }
            return View(contact);
        }

        
    }
}