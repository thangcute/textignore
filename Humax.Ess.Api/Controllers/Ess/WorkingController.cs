using Humax.Ess.Api.Helpers;
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
    public class WorkingController : BaseApiController
    {
        // GET api/<controller>
        public async Task<object> Get()
        {
            var data = OOS.GHR.BusinessAdapter.HSNhanSu.QTDieuChuyenBoNhiem.GetQTLamViecJson(OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID); //WorkingService.get();
            return new
            {
                Status = 1,
                Message = "",
                Data = data
            };
        }

        // GET api/<controller>/5
        public async Task<object> Get(int id)
        {
            var data = WorkingService.get(id);
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        // POST api/<controller>
        public async Task<object> Post([FromBody] WorkingModel model)
        {
            if (ModelState.IsValid)
            {
                // check thời gian kinh nghiệm
                if (model.NgayChuyen < model.NgayQuyetDinh)
                {
                    ModelState.Remove("Date");
                    ModelState.AddModelError("Date", "Ngày Chuyển không được nhỏ hơn ngày Quyết định");
                }

                if (!string.IsNullOrEmpty(model.NgayHetHan.ToString()))
                {
                    if (model.NgayChuyen < model.NgayHetHan)
                    {
                        ModelState.Remove("Date");
                        ModelState.AddModelError("Date", "Ngày Chuyển không được nhỏ hơn ngày Hết hạn");
                    }
                }

                if (ModelState.IsValid)
                {
                    if (await WorkingService.save(model))
                    {
                        return new
                        {
                            Status = 1,
                            Message = model.Id.GetValueOrDefault(0) > 0 ? "Cập nhật Quá trình Làm việc thành công" : "Thêm mới Quá trình Làm việc thành công",
                            Data = model.Id.GetValueOrDefault(0) > 0 ? model.Id : (object)null
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

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}