using Model.DAO;
using System.Web.Mvc;

namespace MobileShop.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        [ChildActionOnly]
        public PartialViewResult _HeaderTopDesc()
        {
            return PartialView(MenuDAO.Instance.GetAllByTypeId(3));
        }

        [ChildActionOnly]
        public PartialViewResult _MainMenu()
        {
            return PartialView(MenuDAO.Instance.GetAllByTypeId(4));
        }

        [ChildActionOnly]
        public PartialViewResult _LeftNavigation()
        {
            return PartialView(MenuDAO.Instance.GetAllByTypeId(5));
        }

        [ChildActionOnly]
        public PartialViewResult _Slider()
        {
            return PartialView(SlideDAO.Instance.GetSlider());
        }

        [ChildActionOnly]
        public PartialViewResult _Footer()
        {
            return PartialView(FooterDAO.Instance.GetFooter());
        }

        public PartialViewResult _RightSidebar()
        {
            return PartialView(MenuDAO.Instance.GetAllByTypeId(6));
        }
    }
}