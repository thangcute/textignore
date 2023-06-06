using Humax.Ess.Api.Helpers;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Library;
using OOS.GHR.SelfService.Models;
using OOS.GHR.Services.Helpers;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Services.Acc;
using OOS.GHR.Services.Services.Ess;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class ProfileController : BaseApiController
    {
        // GET api/<controller>
        public async Task<object> Get()
        {
            int NhanVienID = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            var employee = await EmployeeService.getInfo(NhanVienID);
            return new
            {
                Status = 1,
                Message = "Employee Info",
                Data = employee
            };
        }

        public async Task<object> Get(int id)
        {
            var employee = await EmployeeService.getInfo(id);
            return new
            {
                Status = 1,
                Message = "Employee Info By Id",
                Data = employee
            };
        }

        // NS_NhanVien_NghiViec
        // nghiviec=1, Ngaynghiviec
        //NS_QTNghiPhep
        //NV mơi, NgayVaoLam
        [HttpGet]
        [Route("api/Profile/Activity")]
        public async Task<object> GetActivity(int limit = 0)
        {
            if (limit > 0)
            {
                var data = ProfileService.activity(limit);
                return new
                {
                    Status = 1,
                    Message = "Activity",
                    Data = data.Result
                };
            }
            else
            {
                var data = ProfileService.activity();
                return new
                {
                    Status = 1,
                    Message = "Activity",
                    Data = data.Result
                };
            }
        }

        [HttpGet]
        [Route("api/Profile/Search")]
        public async Task<object> GetSearch(string search = "", string all = "false")
        {
            bool is_all = false;
            bool.TryParse(all, out is_all);
            if (!is_all)
            {
                if (string.IsNullOrEmpty(search))
                {
                    ModelState.Remove("Search");
                    ModelState.AddModelError("Search", "Thông tin tìm kiếm không để trống");
                }

                if (ModelState.IsValid)
                {
                    var data = ProfileService.search(search);
                    return new
                    {
                        Status = 1,
                        Message = "",
                        Data = data.Result
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
            else
            {
                var data = ProfileService.search();
                return new
                {
                    Status = 1,
                    Message = "",
                    Data = data.Result
                };
            }
        }
        

        // POST api/<controller>
        public async Task<object> Post([FromBody] ProfilePutModel model)
        {
            var employeeId = DatabaseCache.NhanVien.NhanVienID;

            if (ModelState.IsValid)
            {
                if (await EmployeeService.put(employeeId, model))
                {
                    return new
                    {
                        Status = 1,
                        Message = "Cập nhật thông tin thành công, Chờ phế duyệt",
                        Data = (object)null
                    };
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = "Error",
                        Data = (object)null
                    };
                }
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

        [Route("api/Profile/Update_TTHopDong")]
        public async Task<object> Update_TTHopDong([FromBody] ProfilePutModel model)
        {
            var employeeId = DatabaseCache.NhanVien.NhanVienID;

            if (ModelState.IsValid)
            {

                if (await EmployeeService.put_TTHopDong(employeeId, model))
                {
                    return new
                    {
                        Status = 1,
                        Message = "Cập nhật thông tin thành công, Chờ phế duyệt",
                        Data = (object)null
                    };
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = "Error",
                        Data = (object)null
                    };
                }
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
        
        [Route("api/Profile/Update_Avata")]
        public async Task<object> Update_Avata([FromBody] string Picture)
        {
            var employeeId = DatabaseCache.NhanVien.NhanVienID;

            if (ModelState.IsValid)
            {
                if (await EmployeeService.put_Avata(employeeId, Picture))
                {
                    return new
                    {
                        Status = 1,
                        Message = "Cập nhật Avata thành công",
                        Data = (object)null
                    };
                }
                else
                {
                    return new
                    {
                        Status = 0,
                        Message = "Error",
                        Data = (object)null
                    };
                }
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

    }
}