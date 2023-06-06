using Humax.Ess.Api.Models;
using OOS.GHR.EntityFramework.Models.Task;
using OOS.GHR.Library;
using OOS.GHR.Services.Models.Task;
using OOS.GHR.Services.Services.News;
using OOS.GHR.Services.Services.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;

namespace Humax.Ess.Api.Controllers.Task
{
    public class WorkController : BaseApiController
    {
        //-----------------thắng viết--------------
        //get quá hạn
        [HttpGet]
        [Route("api/Work/Getquahan")]
        public async Task<object> Getquahan(int page)
        {
            var userId = DatabaseCache.NhanVien.NhanVienID;
            var res = await WorkService.getquahan(page, userId);
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "không có công việc nào quá hạn!"
                };
            }
            return new
            {
                Status = 1,
                Message = "Thành công!",
                data = res
            };

        }
        //get công việc
        [HttpGet]
        [Route("api/Work/Getcv")]
        public async Task<object> Getcv(int page, int type)
        {
            var userId = DatabaseCache.NhanVien.NhanVienID;
            var res = await WorkService.getcv(page, userId, type);
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "không có công việc nào!"
                };
            }
            return new
            {
                Status = 1,
                Message = "Thành công!",
                data = res
            };
        }
        //get chi tiết
        [HttpGet]
        [Route("api/Work/Getchitiet")]
        public async Task<object> Getchitiet(int id)
        {

            var res = await WorkService.getchitietcv(id);
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "không có công việc nào đang thực hiện!"
                };
            }
            return new
            {
                Status = 1,
                Message = "Thành công!",
                data = res
            };
        }
        //cập nhật check list
        [HttpPost]
        [Route("api/Work/UpdateCheckList")]
        public async Task<object> UpdateCheckList(TSK_CongViec_CheckList model)
        {
            var res = await WorkService.updatechecklist(model.CheckListID, model.Tyle);
            if (res)
            {
                return new
                {
                    Status = 1,
                    Message = "cập nhật check list thành công!"
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "cập nhật check list không thành công!"
                };
            }
        }



        //cập nhật thời gian
        [HttpPost]
        [Route("api/Work/UpdateTime")]
        public async Task<object> UpdateTime([FromBody] TSK_CongViec model)
        {

            var repon = await WorkService.updatetime(model.CongViecID);
            if (repon)
            {
                return new
                {
                    Status = 1,
                    Message = "cập nhật thời gian thành công !",
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "Công việc đã bắt đầu !"
                };
            }
        }
        [Route("api/Work/GetTienDo")]
        public async Task<object> GetTienDo()
        {
            var res = await WorkService.gettiendo();
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "lỗi!",
                };
            }
            return new
            {
                Status = 1,
                Message = "Thành công!",
                data = res
            };
        }

        //// GET api/<controller>
        //public async Task<ApiResponse> GetAsync(string conditions = "", int page = 1, int size = 15, bool all = false)
        //{
        //    var data = await WorkService.get(_conditions: conditions, _limit: (page > 0 ? size : 0), _offset: (page > 0 ? (page - 1) * size : 0), _all: all);
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = new
        //        {
        //            Total = data.total,
        //            Result = data.rs
        //        }
        //    };
        //}

        //// GET api/<controller>/5
        //public async Task<ApiResponse> GetAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = (object)null//await WorkService.detail(id)
        //    };
        //}

        // POST api/<controller>
        //public async Task<ApiResponse> PostAsync([FromBody] TaskWorkModel model)
        //{
        //    DateTime actionTime = DateTime.Now;
        //    model.CongViec.CreatedByID = this.UserId;
        //    model.CongViec.CreatedDate = actionTime;

        //    if (await WorkService.post(model))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Thêm Công viêc thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm Công viêc không thành công"
        //        };
        //    }
        //}

        //// PUT api/<controller>/5
        //public async Task<ApiResponse> PutAsync(int id, [FromBody] TaskWorkModel model)
        //{
        //    DateTime actionTime = DateTime.Now;
        //    model.CongViec.ModifyByID = this.UserId;
        //    model.CongViec.ModifyDate = actionTime;

        //    if (await WorkService.put(id, model))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Cập nhật Công viêc thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Cập nhật Công viêc không thành công"
        //        };
        //    }
        //}

        // DELETE api/<controller>/5
        //public async Task<ApiResponse> DeleteAsync(int id)
        //{
        //    if (await WorkService.delete(id))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa Công viêc thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa Công viêc thành công"
        //        };
        //    }
        //}

        //// Thay đổi trạng thái
        //[Route("api/Work/ChangeStatus")]
        //[HttpPut]
        //public async Task<ApiResponse> ChangeStatusAsync(int id, int statusId = 0)
        //{
        //    if (await WorkService.updateStatus(id, statusId, this.UserId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Cập nhật Trạng thái thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Cập nhật Trạng thái không thành công"
        //        };
        //    }
        //}

        //// Checklist theo cong viec - **
        //[Route("api/Work/GetCheckListByWork")]
        //[HttpGet]
        //public async Task<ApiResponse> GetCheckListByWorkAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = await WorkService.getCheckList(id)
        //    };
        //}

        //// Thêm checklist
        //[Route("api/Work/AddCheckListByWork")]
        //[HttpPost]
        //public async Task<ApiResponse> AddCheckListByWorkAsync(int id, [FromBody] TSK_CongViec_CheckList model)
        //{
        //    if (model.CongViecID == null || model.CongViecID == 0)
        //        model.CongViecID = id;

        //    if (await WorkService.addCheckList(model))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Thêm CheckList thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm CheckList không thành công"
        //        };
        //    }
        //}

        //// Xóa checklist
        //[Route("api/Work/DeleteCheckList")]
        //[HttpDelete]
        //public async Task<ApiResponse> DeleteCheckListAsync(int id, int checkListId)
        //{
        //    if (await WorkService.deleteCheckList(id, checkListId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa CheckList thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa CheckList thành công"
        //        };
        //    }
        //}

        //// Nguoi lien quan theo cong viec - **
        //[Route("api/Work/GetEmployeeRelatedByWork")]
        //[HttpGet]
        //public async Task<ApiResponse> GetEmployeeRelatedByWorkAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = await WorkService.getEmployeeRelated(id)
        //    };
        //}

        //// Thêm người liên quan
        //[Route("api/Work/AddEmployeeRelatedByWork")]
        //[HttpPost]
        //public async Task<ApiResponse> AddEmployeeRelatedByWorkAsync(int id, [FromBody]  TSK_CongViec_NguoiPhoiHopThucHien model)
        //{
        //    if (model.CongViecID == null || model.CongViecID == 0)
        //        model.CongViecID = id;

        //    if (await WorkService.addEmployeeRelated(model))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Thêm người phối hợp thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm người phối hợp không thành công"
        //        };
        //    }
        //}

        //// Xóa người liên quan
        //[Route("api/Work/DeleteEmployeeRelatedByWork")]
        //[HttpDelete]
        //public async Task<ApiResponse> DeleteEmployeeRelatedByWorkAsync(int id, int employeeId)
        //{
        //    if (await WorkService.deleteEmployeeRelated(id, employeeId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa CheckList thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa CheckList thành công"
        //        };
        //    }
        //}

        //// Nguoi theo doi theo cong viec - **
        //[Route("api/Work/GetEmployeeControlByWork")]
        //[HttpGet]
        //public async Task<ApiResponse> GetEmployeeControlByWorkAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = await WorkService.getEmployeeControl(id)
        //    };
        //}

        //// Thêm người theo doi
        //[Route("api/Work/AddEmployeeControlByWork")]
        //[HttpPost]
        //public async Task<ApiResponse> AddEmployeeControlByWorkAsync(int id, [FromBody] TSK_CongViec_NguoiTheoDoi model)
        //{
        //    if (model.CongViecID == null || model.CongViecID == 0)
        //        model.CongViecID = id;

        //    if (await WorkService.addEmployeeControl(model))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Thêm người theo dõi thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm người theo dõi không thành công"
        //        };
        //    }
        //}

        //// Xóa ngươi theo doi
        //[Route("api/Work/DeleteEmployeeControlByWork")]
        //[HttpDelete]
        //public async Task<ApiResponse> DeleteEmployeeControlByWorkAsync(int id, int employeeId)
        //{
        //    if (await WorkService.deleteEmployeeControl(id, employeeId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa CheckList thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa CheckList thành công"
        //        };
        //    }
        //}

        //// Coppy công việc
        //[Route("api/Work/CopyWork")]
        //[HttpPost]
        //public async Task<ApiResponse> CopyWorkAsync(int id)
        //{
        //    if (await WorkService.copyWork(id, this.UserId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Sao chép công việc thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Sao chép công việc không thành công"
        //        };
        //    }
        //}

        //// Cập nhật tiến độ

        //// Phê duyệt

        //// Comment

        //// Thay đổi Phân công
        //[Route("api/Work/ChangeEmployeeAssign")]
        //[HttpPut]
        //public async Task<ApiResponse> ChangeEmployeeAssignAsync(int id, int employeeId = 0)
        //{
        //    if (await WorkService.changeEployeeAssign(id, employeeId, this.UserId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Cập nhật người thực hiện thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Cập nhật người thực hiện không thành công"
        //        };
        //    }
        //}
    }
}