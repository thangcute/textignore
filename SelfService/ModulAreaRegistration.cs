using System.Web.Mvc;
namespace OOS.GHR.SelfService
{
    public class ModulAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SelfService";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SelfService",
                "SelfService/{controller}/{action}/{id}",
                new { controller = "SelfService", action = "Index", area = "", id = "" },
                new[] { "OOS.GHR.SelfService.Controllers" }
            );
        }
    }
}
