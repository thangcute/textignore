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
    public class RelationshipController : BaseApiController
    {
        // GET api/<controller>
        public async Task<object> Get()
        {
            var data = RelationshipService.get();
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        public async Task<object> Get(int id)
        {
            var data = RelationshipService.get(id);
            return new
            {
                Status = 1,
                Message = "",
                Data = data != null ? data.Result : null
            };
        }

        // POST api/<controller>
        public async Task<object> Post([FromBody] RelationshipModel model)
        {
            if (ModelState.IsValid)
            {
                bool isNew = model.QTQuanHeGiaDinhID > 0 ? false : true;
                if (await RelationshipService.save(model))
                {
                    return new
                    {
                        Status = 1,
                        Message = isNew ? "Thêm mói Quan hệ Nhân thân thành công" : "Cập nhật Quan hệ Nhân thân thành công",
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

        // DELETE api/<controller>/5
        public async Task<object> Delete(int id)
        {
            string msg = await RelationshipService.delete(id);

            if (string.IsNullOrEmpty(msg))
            {
                return new
                {
                    Status = 1,
                    Message = "Xóa Quan hệ Nhân thân thành công",
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