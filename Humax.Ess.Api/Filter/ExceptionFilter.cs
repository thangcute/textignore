using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Humax.Ess.Api.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string exceptionMessage = string.Empty;
            if (context.Exception.InnerException == null)
            {
                exceptionMessage = context.Exception.Message;

            }
            else
            {
                exceptionMessage = context.Exception.InnerException.Message;
            }

            //We can log this exception message to the file or database.  
            if (!string.IsNullOrEmpty(exceptionMessage))
            {
                Commons.WLog(logMsg: string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), exceptionMessage), fileName: "exception.txt");
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(exceptionMessage),
                    ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
                };
                context.Response = response;
            }
        }
    }
}