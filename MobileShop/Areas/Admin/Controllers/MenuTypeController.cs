using Model.DAO;
using Model.EF;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class MenuTypeController : BaseController
    {
        // GET: Admin/MenuType
        public ActionResult Index()
        {
            return View(MenuTypeDAO.Instance.GetListAll());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] menuTypeIds = form["menuTypeId"].Split(',');

            foreach (string item in menuTypeIds)
            {
                MenuTypeDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Create(string name)
        {
            int flag = -1;
            if (MenuTypeDAO.Instance.CheckNameIsExist(name))
                flag = 2;
            else if (MenuTypeDAO.Instance.Create(name.Trim()))
                flag = 1;
            else
                flag = 0;
            return Json(new { Status = flag});
        }

        [HttpPost]
        public JsonResult Edit(int id, string name)
        {
            int flag = -1;
            if (MenuTypeDAO.Instance.CheckNameIsExist(name) && MenuTypeDAO.Instance.GetDetail(id).Name != name.Trim())
                flag = 2;
            else if (MenuTypeDAO.Instance.Update(id, name.Trim()))
                flag = 1;
            else
                flag = 0;
            return Json(new { Status = flag, Name = name });
        }
    }
}