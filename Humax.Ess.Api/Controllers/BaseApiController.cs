using Humax.Ess.Api.Models;
using Newtonsoft.Json;
using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers
{
    [Humax.Ess.Api.Filter.ExceptionFilter]
    public class BaseApiController : ApiController
    {
        public string Token = "";

        public string Domain = "";

        public int UserId = 0;

        public BaseApiController()
        {
            var _host = System.Web.HttpContext.Current.Request.Url.Host;
            if (!string.IsNullOrEmpty(_host.ToString()))
                Domain = _host.ToString();

            var _requests = System.Web.HttpContext.Current.Request;
            var _headers = _requests.Headers;
            try
            {
                if (_headers.AllKeys.Contains("Token"))
                {
                    this.Token = _headers.GetValues("Token").First();
                    this.UserId = 1;
                }
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Trả về API Reponse lỗi
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public ApiResponse ReturnError(string mess, int errcode = 0)
        {
            return new ApiResponse()
            {
                Status = errcode,
                Message = DatabaseCache.GetText(mess)
            };
        }


        /// <summary>
        /// Trả về API Reponse thành công
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public ApiResponse ReturnSuccess(string mess)
        {
            return new ApiResponse()
            {
                Status = 1,
                Message = DatabaseCache.GetText(mess)
            };
        }
    }
}