using Humax.Ess.Api.Helpers;
using OOS.GHR.BusinessAdapter.HSNhanSu;
using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.Framework;
using OOS.GHR.Library;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Services.Ess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class ExperienceController : BaseApiController
    {
        // GET api/<controller>
        public async Task<object> Get()
        {
            int employeeId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            var data = ExperienceService.list(employeeId);
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        // GET api/<controller>/5
        public async Task<object> Get(int id)
        {
            var data = ExperienceService.get(id);
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        // POST api/<controller>
        public async Task<object> Post([FromBody] ExperienceModel model)
        {
            if (ModelState.IsValid)
            {
                // check thời gian kinh nghiệm
                if (model.ToiNgay < model.TuNgay)
                {
                    ModelState.Remove("Date");
                    ModelState.AddModelError("Date", "Ngày kết thúc không thể nhỏ hơn ngày bắt đầu làm việc");
                }

                if (ModelState.IsValid)
                {
                    if (await ExperienceService.save(model))
                    {
                        return new
                        {
                            Status = 1,
                            Message = model.KinhNghiemID.GetValueOrDefault(0) > 0 ? "Cập nhật Quá trình Kinh nghiệm thành công" : "Thêm mới Quá trình Kinh nghiệm thành công",
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

        // DELETE api/<controller>/5
        public async Task<object> Delete(int id)
        {
            string msg = await ExperienceService.delete(id);

            if (string.IsNullOrEmpty(msg))
            {
                return new
                {
                    Status = 1,
                    Message = "Xóa Quá trình Kinh nghiệm thành công",
                    Data = id
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = msg,
                    Data = id
                };
            }
        }
    }
}