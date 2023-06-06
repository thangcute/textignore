using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOS.GHR.Library;
using OOS.GHR.SelfService.Models;
namespace OOS.GHR.SelfService.Controllers
{
    public class OnBoardingController : OOS.GHR.Framework.Controllers.BaseController
    {
        public ActionResult Index()
        {
            OnBoardingModel OM = new OnBoardingModel();
            return View(OM);
        }

        public ActionResult JobDescription()
        {
            return PartialView("_JobInfo", NS_DsChucVu.GetDienGiaiByID(DatabaseCache.NhanVien.ChucVuID));
        }

        [HttpGet]
        public JsonResult GetJobDescription(int? id)
        {
            return ReturnJSValue(NS_DsChucVu.GetDienGiaiByID(id.Value));
        }
    }
}