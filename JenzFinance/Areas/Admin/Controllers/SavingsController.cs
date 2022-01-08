using JenzFinance.Areas.Admin.Components;
using JenzFinance.Areas.Admin.Interfaces;
using JenzFinance.Areas.Admin.Services;
using JenzFinance.Areas.Admin.ViewModels;
using JenzFinance.DAL.DataConnection;
using JenzFinance.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JenzFinance.Areas.Admin.Controllers
{
    public class SavingsController : Controller
    {
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //    filterContext.ExceptionHandled = true;

        //    //Log the error!!
        //    log.Error("Error trying to do something", filterContext.Exception);
        //    Global.ErrorMessage = filterContext.Exception.ToString();

        //    //Redirect or return a view, but not both.
        //    filterContext.Result = RedirectToAction("Error", "Error", new { area = "" });

        //}


        // Initializing dependency services
        #region Instanciation
        ISavingService _savingService;
        public SavingsController()
        {
            _savingService = new SavingService(new DatabaseEntities());
        }
        public SavingsController(SavingService savingService)
        {
            _savingService = savingService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion   
        
        public ActionResult ManageSavings(bool? Added, bool? Editted, bool? AddedSub, bool? EdittedSub)
        {
            if (!Nav.CheckAuthorization(Request.Url.AbsolutePath))
            {
                throw new UnauthorizedAccessException();
            }
            if (Added == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Category added successfully.";
            }
            if (Editted == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Category updated successfully.";
            }
            if (AddedSub == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Sub-category added successfully.";
            }
            if (EdittedSub == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Sub-category updated successfully.";
            }

            ViewBag.CategoryDD = new SelectList(db.SavingCategories.Where(x => x.IsDeleted == false && x.ParentID == null), "Id", "Description");
            ViewBag.SavingCategory = _savingService.GetSavingCategories();
            ViewBag.SubSavingCategory = _savingService.GetSubCategories();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateCategory(SavingCategoryVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _savingService.CreateSavingCategory(vmodel);
            }
            return RedirectToAction("ManageSavings", new { Added = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditCategory(SavingCategoryVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _savingService.EditSavingCategory(vmodel);
            }
            return RedirectToAction("ManageSavings", new { Editted = hasSaved });
        }
        public JsonResult GetCategory(int id)
        {
            var model = _savingService.GetSavingCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCategory(int id)
        {
            var model = _savingService.DeleteSavingCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Sub Categories
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateSubCategory(SavingCategoryVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _savingService.CreateSubCategory(vmodel);
            }
            return RedirectToAction("ManageSavings", new { AddedSub = hasSaved });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSubCategory(SavingCategoryVM vmodel)
        {
            bool hasSaved = false;
            if (ModelState.IsValid)
            {
                hasSaved = _savingService.EditSubCategory(vmodel);
            }
            return RedirectToAction("ManageSavings", new { EdiitedSub = hasSaved });
        }
        public JsonResult GetSubCategory(int id)
        {
            var model = _savingService.GetSubCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteSubCategory(int id)
        {
            var model = _savingService.DeleteSubCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActivateSaving(int id)
        {
            var model = _savingService.ActivateSubCategory(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Manage(bool? Updated, int SavingID) {
            if (Updated == true)
            {
                ViewBag.ShowAlert = true;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Updated successfully.";
            }

            var model = _savingService.GetSavingRecord(SavingID);
            return View(model);
        }

        public ActionResult UpdateSavingRecord(SavingRecordVM[] vmodel)
        {
            int SavedCount = 0;
            if(vmodel != null)
            {
                foreach (var data in vmodel)
                {
                    var status = _savingService.UpdateSavingRecord(data);
                    if (status)
                        SavedCount++;
                }
            }
            return Json(SavedCount, JsonRequestBehavior.AllowGet);
        }

    }
}