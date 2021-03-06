﻿using Model.DAO;
using Model.EF;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MobileShop.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index()
        {
            return View(SlideDAO.Instance.GetListAll());
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            string[] slideIds = form["slideId"].Split(',');

            foreach (string item in slideIds)
            {
                SlideDAO.Instance.Delete(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            DisplayOrderList();
            TargetList();
            StatusList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                if (SlideDAO.Instance.CheckNameIsExist(slide.Name))
                    ModelState.AddModelError("Name", "Tên quảng cáo đã tồn tại");
                else if (SlideDAO.Instance.Create(slide))
                {
                    SetAlert("Tạo mới quảng cáo <b>" + slide.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo mới quảng cáo! Vui lòng thử lại.");
            }
            DisplayOrderList();
            TargetList();
            StatusList();
            return View(slide);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Slide slide = SlideDAO.Instance.GetDetail(id.Value);
            if (slide == null)
                return HttpNotFound();
            DisplayOrderList(slide.DisplayOrder);
            TargetList(slide.Target);
            StatusList(slide.Status);
            return View(slide);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit([Bind(Exclude = "CreatedDate")] Slide slide)
        {
            var oldSlide = SlideDAO.Instance.GetDetail(slide.Id);
            slide.Image = oldSlide.Image;
            slide.CreatedDate = oldSlide.CreatedDate;
            if (ModelState.IsValid)
            {
                if (SlideDAO.Instance.CheckNameIsExist(slide.Name) && oldSlide.Name != slide.Name)
                    ModelState.AddModelError("Name", "Tên quảng cáo đã tồn tại");
                else if (SlideDAO.Instance.Update(slide))
                {
                    SetAlert("Cập nhật thông tin quảng cáo <b>" + slide.Name + "</b> thành công.");
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin! Vui lòng thử lại.");
            }
            DisplayOrderList(slide.DisplayOrder);
            TargetList(slide.Target);
            StatusList(slide.Status);
            return View(slide);
        }

        public string ChangeImage(int id, string image)
        {
            if (id.ToString() == null)
                return "Mã quảng cáo không tồn tại!";
            return SlideDAO.Instance.ChangeImage(id, image);
        }

        public void TargetList(string selected = "_self")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Mở trong trang", Value = "_self", Selected = selected == "_self" });
            items.Add(new SelectListItem { Text = "Mở trang con", Value = "_blank", Selected = selected == "_blank" });
            ViewBag.Target = items;
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