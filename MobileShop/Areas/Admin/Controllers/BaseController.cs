using System.Web.Mvc;
using Common.Enum;

namespace MobileShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAlert(string message, int type = (int)EAlertMessage.Success)
        {
            TempData["AlertMessage"] = message;
            switch (type)
            {
                case (int)EAlertMessage.Danger :
                    TempData["AlertType"] = "danger";
                    break;
                case (int)EAlertMessage.Warning:
                    TempData["AlertType"] = "warning";
                    break;
                case (int)EAlertMessage.Success:
                    TempData["AlertType"] = "success";
                    break;
            }
        }
    }
}