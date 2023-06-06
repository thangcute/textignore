using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOS.GHR.Framework;
using OOS.GHR.SelfService.Models;
namespace OOS.GHR.SelfService.Controllers
{
    public class iSignSSController : Controller
    {
        [OOSAuthorization(Code = "PORTAL_iSIGN")]
        public ActionResult Index()
        {
            iSignModel sm = new iSignModel();
            sm.InitISign();

            return View(sm);
        }
    }
}