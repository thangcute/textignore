using OOS.GHR.Services.Utilitys;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Humax.Ess.Api.Controllers.Old
{
    public class HanetController : Controller
    {
        //[HttpPost]
        public ActionResult Index()
        {
            Commons.WLog(logMsg: "abc");
            if (Request.Params.AllKeys.Contains("code"))
            {
                Commons.WLog(logMsg: Request.Params["code"].ToString());
            }

            return Json(new
            {
                Status = 1
            },
                JsonRequestBehavior.AllowGet
            );
        }

        public JsonResult Authorize()
        {
            string response_type = Request.QueryString["response_type"].ToString();
            if (response_type != "Implicit" && response_type != "code")
                response_type = "Authorization Code";

            string client_id = "3e045fb8a48b6c59f46a808466e2cec8";

            var client = new RestClient("https://oauth.hanet.com/oauth2/authorize?response_type=" + response_type + "&client_id=" + client_id + "&redirect_uri=http://api.hronline.com.vn/api/v1/Hanet/Index&scope=full");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return Json(new
                {
                    Status = 1,
                    Code = response.StatusCode,
                    Content = response.Content
                },
                JsonRequestBehavior.AllowGet
            );
        }
    }
}