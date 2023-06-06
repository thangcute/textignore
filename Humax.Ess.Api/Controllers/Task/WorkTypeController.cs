using Humax.Ess.Api.Models;
using OOS.GHR.EntityFramework.Models.Task;
using OOS.GHR.Services.Services.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Task
{
    public class WorkTypeController : BaseApiController
    {
        // GET api/<controller>
        public async Task<ApiResponse> GetAsync(string conditions = "", int page = 1, int size = 15, bool all = false)
        {
            var data = await WorkTypeService.get(_conditions: conditions, _limit: (page > 0 ? size : 0), _offset: (page > 0 ? (page - 1) * size : 0), _all: all);
            return new ApiResponse()
            {
                Status = 1,
                Data = new
                {
                    Total = data.total,
                    Result = data.rs
                }
            };
        }

        // GET api/<controller>/5
        public async Task<ApiResponse> GetAsync(int id)
        {
            return new ApiResponse()
            {
                Status = 1,
                Data = await WorkTypeService.detail(id)
            };
        }

        // POST api/<controller>
        public async Task<ApiResponse> PostAsync([FromBody] TSK_CongViec_LoaiCongViec model)
        {
            DateTime actionTime = DateTime.Now;
            model.CreatedByID = this.UserId;
            model.CreatedDate = actionTime;

            if (await WorkTypeService.post(model))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Thêm trạng thái công việc thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Thêm trạng thái công việc không thành công"
                };
        }

        // PUT api/<controller>/5
        public async Task<ApiResponse> PutAsync(int id, [FromBody] TSK_CongViec_LoaiCongViec model)
        {
            DateTime actionTime = DateTime.Now;
            model.ModifyByID = this.UserId;
            model.ModifyDate = actionTime;

            if (await WorkTypeService.put(id, model))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Cập nhật trạng thái công việc thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Cập nhật trạng thái công việc không thành công"
                };
        }

        // DELETE api/<controller>/5
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            if (await WorkTypeService.delete(id))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Xóa trạng thái công việc thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Xóa trạng thái công việc không thành công"
                };
        }
    }
}