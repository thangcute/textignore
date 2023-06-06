using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOS.GHR.SelfService.Controllers
{
    public class CareerPathController : OOS.GHR.Framework.Controllers.BaseController
    {
        public ActionResult Index(int? ChucVuID)
        {
            Models.CareerPathModel model = new Models.CareerPathModel();
            model.LoadData(OOS.GHR.Library.DatabaseCache.NhanVien.ChucVuID);

            return View(model);
        }
    }
}