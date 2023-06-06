using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Humax.Ess.Api.App_Start;
using OOS.GHR.Library;
namespace Humax.Ess.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ///Khoi tao bo nho cho User
            OOSSessionManager.InitSessionManager();

            #region Register PlugIns
            OOS.GHR.Plugins.PlugInsManager.RegisterPlugIns("HANET FACEID", typeof(OOS.GHR.Plugins.FaceID.HanetFaceID));
            OOS.GHR.Plugins.PlugInsManager.RegisterPlugIns("SUPREMA", typeof(OOS.GHR.Plugins.FaceID.Suprema));
            #endregion        
        }
    }
}
