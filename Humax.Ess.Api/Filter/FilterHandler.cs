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
using System.Web;

namespace Humax.Ess.Api.Filter
{
    public class FilterHandler : DelegatingHandler
    {
        public FilterHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request
            , CancellationToken cancellationToken
        )
        {
            string localPath = request.RequestUri.LocalPath.ToLower();
            if(localPath != "/api/auth" && !localPath.Contains("/swagger"))
            {
                string Token = "";
                var _headers = request.Headers;
                if (_headers.Contains("Token"))
                {
                    Token = _headers.GetValues("Token").First();
                }

                if (string.IsNullOrEmpty(Token))
                {
                    return request.CreateResponse<object>(HttpStatusCode.OK
                    , new
                    {
                        Status = 0,
                        Message = "Không tìm thấy thông tin Token",
                        Data = (object)null //JsonConvert.SerializeObject(_headers)
                    }
                    , new MediaTypeHeaderValue("application/json")
                    {
                        CharSet = Encoding.UTF8.WebName
                    });
                }

                if (OOSSessionManager.GetSessionData(Token) == null)
                {
                    return request.CreateResponse<object>(HttpStatusCode.OK
                    , new
                    {
                        Status = 0,
                        Message = "Phiên đã hết hạn",
                        Data = (object)null //JsonConvert.SerializeObject(_headers)
                    }
                    , new MediaTypeHeaderValue("application/json")
                    {
                        CharSet = Encoding.UTF8.WebName
                    });
                }
            }

            if (request.Method == HttpMethod.Post)
            {
                if (request.Content != null)
                {
                    string requestBody = await request.Content.ReadAsStringAsync();
                }
            }

            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);
            
            var responseBody = new object();
            if (result.Content != null)
            {
                responseBody = await result.Content.ReadAsStringAsync();
            }

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return request.CreateResponse<object>(HttpStatusCode.OK
                    , new
                    {
                        Status = 0,
                        Message = responseBody,
                        Data = (object)null //JsonConvert.SerializeObject(_headers)
                    }
                    , new MediaTypeHeaderValue("application/json")
                    {
                        CharSet = Encoding.UTF8.WebName
                    });
            }
            else
                return result;
        }
    }
}

//if (0 > 1)//_log.Path.Contains("/swagger/")
//{
//    return result;
//}
//else
//{
//    if (result.StatusCode == System.Net.HttpStatusCode.OK)
//    {
//        return request.CreateResponse<ApiResponseMessage>(HttpStatusCode.OK
//        , new ApiResponseMessage()
//        {
//            code = result.StatusCode.ToString(),
//            status = result.StatusCode.ToString(),
//            message = result.ReasonPhrase.ToString(),
//            data = JsonConvert.DeserializeObject(responseBody.ToString())
//        }
//        , new MediaTypeHeaderValue("application/json")
//        {
//            CharSet = Encoding.UTF8.WebName
//        });
//    }
//    else
//    {
//        return request.CreateResponse<ApiResponseMessage>(HttpStatusCode.OK
//        , new ApiResponseMessage()
//        {
//            code = result.StatusCode.ToString(),
//            status = result.StatusCode.ToString(),
//            message = result.ReasonPhrase.ToString(),
//            data = responseBody
//        }
//        , new MediaTypeHeaderValue("application/json")
//        {
//            CharSet = Encoding.UTF8.WebName
//        });
//    }
//}