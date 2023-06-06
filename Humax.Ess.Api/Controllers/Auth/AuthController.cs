using Humax.Ess.Api.Helpers;
using Humax.Ess.Api.Models;
using Newtonsoft.Json;
using OOS.GHR.Library;
using OOS.GHR.Services.Helpers;
using OOS.GHR.Services.Models;
using OOS.GHR.Services.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Auth
{
    public class AuthController : ApiController
    {
        // POST api/<controller>
        public async Task<object> Post([FromBody] LoginModel model)
        {
            //string DeviceId, string FireBaseToken, string UserName, string PassWord

            if (ModelState.IsValid)
            {
                string HostName = Database.HostName;
                if (!string.IsNullOrEmpty(HostName))
                {
                    var Hostname = OOSSessionManager.AddNewHostname(HostName);
                    SYS_NguoiDung userInfo = OOS.GHR.Core.Authentication.Login(model.UserName, model.Password, false, "");
                    if (userInfo != null && userInfo.NguoiDungID > 0)
                    {
                        var token = OOS.GHR.Core.Authentication.CreatePublicKey();
                        //UserSessionData User = OOS.GHR.Core.Authentication.DoLogin(model.FireBaseToken, userInfo, token, null, Hostname);
                        UserSessionData User = OOS.GHR.Core.Authentication.DoLogin(string.IsNullOrEmpty(model.DeviceId) ? "" : model.DeviceId, userInfo, token, null, Hostname);

                        return new
                        {
                            Status = 1,
                            Token = token,
                            Message = "Thành công",
                            Data = new
                            {
                                EmployeeCode = User.NhanVien.MaNhanVien,
                                FullName = User.NhanVien.ToString(),
                                JobTitle = User.NhanVien.TenChucVu,
                                Picture = User.NhanVien.Anh,
                                Permissions = User.ToListQuyen()
                            }
                        };
                    }
                    else
                        return new
                        {
                            Status = 0,
                            Message = "Không lấy được thấy thông tin tài khoản",
                            Data = (object)null
                        };
                }
                else
                    return new
                    {
                        Status = 0,
                        Message = "Không lấy được thông tin HostName",
                        Data = (object)null
                    };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).FirstOrDefault().Message,
                    Data = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList()
                };
            }
        }

        [HttpGet]
        public async Task<ApiResponse> GetResult()
        {
            return new ApiResponse()
            {
                Status = 1,
                Data = new
                {
                    Id = 1
                }
            };
        }
    }
}