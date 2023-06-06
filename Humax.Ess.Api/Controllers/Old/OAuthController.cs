using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Humax.Ess.Api.Controllers.Old
{
    public class OAuthController : Controller
    {
        [HttpPost]
        public JsonResult GetChallenger(string UserName)
        {
            if (OOS.GHR.Security.checkForSQLInjection(UserName) || string.IsNullOrEmpty(UserName))
                return Json(new { Status = 0 }, JsonRequestBehavior.AllowGet);

            int challenger = (new Random()).Next(0, int.MaxValue);
            SYS_NguoiDung ND = OOS.GHR.BusinessAdapter.HeThong.Account.GetAccountName(UserName);

            if (ND == null || ND.NguoiDungID <= 0)
            {
                return Json(new
                {
                    Status = 0
                });
            }

            Session["UserInfo"] = ND;
            Session["Challenger"] = challenger;
            Session.Timeout = 1;

            return Json(new
            {
                Status = 1,
                Number = challenger
            });
        }

        [HttpPost]
        public JsonResult RequireLogin(string login)
        {
            if (OOS.GHR.Security.checkForSQLInjection(login) || string.IsNullOrEmpty(login))
                return Json(new { Status = 0 }, JsonRequestBehavior.AllowGet);

            if (Session["Challenger"] == null)
                return Json(new { Status = 0 });

            if (Session["UserInfo"] == null)
                return Json(new { Status = 0 });

            SYS_NguoiDung ND = Session["UserInfo"] as SYS_NguoiDung;
            if (ND == null)
                return Json(new { Status = 0 });


            string challenger = Session["Challenger"].ToString();
            if (challenger == "" || string.IsNullOrEmpty(challenger))
                return Json(new { Status = 0 });

            string compare = OOS.GHR.BusinessAdapter.Global.Global.GetMD5(challenger + ND.MatKhau.Replace("[", "").Replace("]", ""));
            if (login == compare)
            {
                string AID = Guid.NewGuid().ToString();
                string DeviceID = Request.UserHostAddress + Request.UserHostName;

                OOS.GHR.Core.Authentication.UpdateLoginStatus(ND.NguoiDungID, AID, DeviceID);
                return Json(new
                {
                    Status = 1,
                    AID = AID
                });
            }

            return Json(new
            {
                Status = 0
            });
        }
    }
}