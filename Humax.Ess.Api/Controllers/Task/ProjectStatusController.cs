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
    public class ProjectStatusController : BaseApiController
    {
        // GET api/<controller>
        public async Task<ApiResponse> GetAsync(string conditions = "", int page = 1, int size = 15, bool all = false)
        {
            var data = await ProjectStatusService.get(_conditions: conditions, _limit: (page > 0 ? size : 0), _offset: (page > 0 ? (page - 1) * size : 0), _all: all);
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
                Data = await ProjectStatusService.detail(id)
            };
        }

        [Route("api/ProjectStatus/Status")]
        [HttpGet]
        public async Task<ApiResponse> StatusAsync()
        {
            return new ApiResponse()
            {
                Status = 1,
                Data = await ProjectStatusService.list()
            };
        }

        // POST api/<controller>
        public async Task<ApiResponse> PostAsync([FromBody] TSK_DuAn_TrangThai model)
        {
            DateTime actionTime = DateTime.Now;
            model.CreatedByID = this.UserId;
            model.CreatedDate = actionTime;

            if (await ProjectStatusService.post(model))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Thêm trạng thái dự án thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Thêm trạng thái dự án không thành công"
                };
        }

        // PUT api/<controller>/5
        public async Task<ApiResponse> PutAsync(int id, [FromBody] TSK_DuAn_TrangThai model)
        {
            DateTime actionTime = DateTime.Now;
            model.ModifyByID = this.UserId;
            model.ModifyDate = actionTime;

            if (await ProjectStatusService.put(id, model))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Cập nhật trạng thái dự án thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Cập nhật trạng thái dự án không thành công"
                };
        }

        // DELETE api/<controller>/5
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            if(await ProjectStatusService.delete(id))
                return new ApiResponse
                {
                    Status = 1,
                    Message = "Xóa trạng thái dự án thành công"
                };
            else
                return new ApiResponse
                {
                    Status = 0,
                    Message = "Xóa trạng thái dự án không thành công"
                };
        }
    }
}