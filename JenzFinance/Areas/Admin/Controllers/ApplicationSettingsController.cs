using JenzFinance.DAL.DataConnection;
using JenzFinance.DAL.Entity;
using JenzFinance.Areas.Admin.Interfaces;
using JenzFinance.Areas.Admin.Services;
using JenzFinance.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JenzFinance.Areas.Admin.Components;

namespace JenzFinance.Areas.Admin.Controllers
{
    public class ApplicationSettingsController : Controller
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
        IApplicationSettingsService _settingService;
        public ApplicationSettingsController()
        {
            _settingService = new ApplicationSettingsService(new DatabaseEntities());
        }
        public ApplicationSettingsController(ApplicationSettingsService settingsService)
        {
            _settingService = settingsService;
        }
        DatabaseEntities db = new DatabaseEntities();
        #endregion

        public ActionResult Manage()
        {
            if (!Nav.CheckAuthorization(Request.Url.AbsolutePath))
            {
                throw new UnauthorizedAccessException();
            }
            return View(_settingService.GetApplicationSettings());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ApplicationSettingsVM Vmodel, HttpPostedFileBase LogoData, HttpPostedFileBase FaviconData)
        {
            if (ModelState.IsValid)
            {
                bool saveState = _settingService.UpdateApplicationSettings(Vmodel, LogoData, FaviconData);
                if(saveState == true)
                {
                    ViewBag.ShowAlert = true;
                    TempData["AlertMessage"] = "Application settings updated successfully.";
                    TempData["AlertType"] = "alert-success";
                }
            }
            return View(_settingService.GetApplicationSettings());
        }

        // Categories
     
    }
}