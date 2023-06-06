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
    public class WorkRepeatController : BaseApiController
    {
        public async Task<ApiResponse> GetAsync(string conditions = "", int page = 1, int size = 15, bool all = false)
        {
            var data = await WorkRepeatService.get(_conditions: conditions, _limit: (page > 0 ? size : 0), _offset: (page > 0 ? (page - 1) * size : 0), _all: all);
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

        public async Task<ApiResponse> GetAsync(int id)
        {
            return new ApiResponse()
            {
                Status = 1,
                Data = await WorkRepeatService.detail(id)
            };
        }

        public async Task<ApiResponse> PostAsync([FromBody] TSK_CongViecLap model)
        {
            if (await WorkRepeatService.post(model))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Thêm công việc lặp thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Thêm công việc lặp không thành công"
                };
        }

        public async Task<ApiResponse> PutAsync(int id, [FromBody] TSK_CongViecLap model)
        {
            if (await WorkRepeatService.put(id, model))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Cập nhật công việc lặp thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Cập nhật công việc lặp không thành công"
                };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            if (await WorkRepeatService.delete(id))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Xóa công việc lặp thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Xóa công việc lặp không thành công"
                };
        }

        [Route("api/WorkRepeat/ChangeActive")]
        [HttpPut]
        public async Task<ApiResponse> ChangeActiveAsync(int id)
        {
            if (await WorkRepeatService.changeActive(id))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Thay đổi Active thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Thay đổi Active không thành công"
                };
        }
    }
}