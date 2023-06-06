using System.Web.Http;
using WebActivatorEx;
using Humax.Ess.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Humax.Ess.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration.EnableSwagger(c => {
                        c.SingleApiVersion("v1", "Humax.Ess.Api");
            }).EnableSwaggerUi();
        }
    }
}
