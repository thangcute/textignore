using Humax.Ess.Api.Helpers;
using OOS.GHR.Library;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Services.Ess;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class TrainingProcessController : BaseApiController
    {
        // GET api/<controller>
        public async Task<object> Get()
        {
            var data = TrainingProcessService.get();
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
            var data = TrainingProcessService.get(id);
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        // POST api/<controller>
        public async Task<object> Post([FromBody] TrainingProcessModel model)
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
                    bool isNew = model.QTDaoTaoID > 0 ? false : true;
                    var nhanvienId = DatabaseCache.NhanVien.NhanVienID;
                    //
                    if (await TrainingProcessService.save(model))
                    {
                        return new
                        {
                            Status = 1,
                            Message = isNew ? "Thêm mói Quá trình Đào tạo thành công" : "Cập nhật Quá trình Đào tạo thành công",
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
            string msg = await TrainingProcessService.delete(id);

            if (string.IsNullOrEmpty(msg))
            {
                return new
                {
                    Status = 1,
                    Message = "Xóa Quá trình Đào tạo thành công",
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