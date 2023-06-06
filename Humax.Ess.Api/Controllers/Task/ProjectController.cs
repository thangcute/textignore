using Humax.Ess.Api.Models;
using OOS.GHR.EntityFramework.Models.Task;
using OOS.GHR.Library;
using OOS.GHR.Services.Models.Task;
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
    public class ProjectController : BaseApiController
    {
        #region thắng viết
        //get all trạng thái dự án
        [HttpGet]
        [Route("api/Project/getallTT")]
        public async Task<object> getallTT()
        {
            var res = await ProjectService.getTT();
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "không có dữ liệu!",

                };
            }
            else
            {
                return new
                {
                    Status = 1,
                    Message = "Thành công!",
                    data=res

                };
            }
        }
        //lấy danh sách dự án
        [HttpGet]
        [Route("api/Project/getproject")]
        public async Task<object> getproject(int page, int trangthaiId)
        {
            var userId = DatabaseCache.NhanVien.NhanVienID;
            var res = await ProjectService.getDA(page, userId, trangthaiId);
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "không có dữ liệu!",

                };
            }
            else
            {
                return new
                {
                    Status = 1,
                    Message = "Thành công!",
                    data = res

                };
            }
        }
        //lấy chi tiết
        [HttpGet]
        [Route("api/Project/getchitiet")]
        public async Task<object> getchitiet(int DuAnId)
        {
            var res = await ProjectService.getchitiet(DuAnId);
            if (res == null)
            {
                return new
                {
                    Status = 0,
                    Message = "không có dữ liệu!",

                };
            }
            else
            {
                return new
                {
                    Status = 1,
                    Message = "Thành công!",
                    data = res

                };
            }
        }
        //cập nhật trạng thái
        [HttpPost]
        [Route("api/Project/UpdateTrangThai")]
        public async Task<object> UpdateTrangThai([FromBody] TSK_DuAn model)
        {

            var repon = await ProjectService.updatetrangthai(model.TrangThaiID, model.DuAnID);
            if (repon)
            {
                return new
                {
                    Status = 1,
                    Message = "cập nhật trạng thái thành công !",
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "cập nhật trạng thái không thành công !"
                };
            }
        }
        [HttpPost]
        [Route("api/Project/AddNguoiThucHien")]
        //thêm người thực hiện
        public async Task<object> AddNguoiThucHien(TaskProjectModel model)
        {
            var userId = DatabaseCache.NhanVien.NhanVienID;
            var res = await ProjectService.insert(model, userId);
            if (res)
            {
                return new
                {
                    Status = 1,
                    Message = "Thêm người thực hiện thành công !",
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "Thêm người thực hiện không thành công !"
                };
            }
            
        }
        [HttpPost]
        [Route("api/Project/updatetiendo")]
        public async Task<object> updatetiendo(TSK_DuAn model)
        {
            var res = await ProjectService.updatetiendo(model.CachTinhTienDo, model.DuAnID);
            if (res)
            {
                return new
                {
                    Status = 1,
                    Message = "cập nhật tiến độ thành công!"
                };
            }
            else
            {
                return new
                {
                    Status = 0,
                    Message = "cập nhật tiến độ không thành công!"
                };
            }
        }

        #endregion
        //// GET api/<controller>
        //public async Task<ApiResponse> GetAsync(string conditions = "", int page = 1, int size = 15, bool all = false)
        //{
        //    var data = await ProjectService.get(_conditions: conditions, _limit: (page > 0 ? size : 0), _offset: (page > 0 ? (page - 1) * size : 0), _all: all);
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = new {
        //            Total = data.total,
        //            Result = data.rs
        //        }
        //    };
        //}

        //[Route("api/Project/GetAll")]
        //[HttpGet]
        //public async Task<ApiResponse> GetAsync()
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    var _data = await ProjectService.getAll(employee.NhanVienID);
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = _data.rs,
        //        Total = _data.total
        //    };
        //}

        //// GET api/<controller>/5
        //public async Task<ApiResponse> GetAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = await ProjectService.detail(id)
        //    };
        //}

        //[Route("api/Project/Persons")]
        //[HttpGet]
        //public async Task<ApiResponse> PersonsAsync(int id)
        //{
        //    ProjectPersonModel model = new ProjectPersonModel();
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = model,
        //        Message = "Cập nhật Trạng thái không thành công"
        //    };
        //}

        //// POST api/<controller>
        //public async Task<ApiResponse> PostAsync([FromBody] TaskProjectModel model)
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    DateTime actionTime = DateTime.Now;
        //    model.CreatedByID = employee.NhanVienID;
        //    model.CreatedDate = actionTime;

        //    if (await ProjectService.post(model))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Thêm Dự án thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm Dự án không thành công"
        //        };
        //    }
        //}

        ////// PUT api/<controller>/5
        ////public async Task<ApiResponse> PutAsync(int id, [FromBody] TaskProjectModel model)
        ////{
        ////    DateTime actionTime = DateTime.Now;
        ////    model.DuAn.ModifyByID = this.UserId;
        ////    model.DuAn.ModifyDate = actionTime;

        ////    if (await ProjectService.put(id, model))
        ////    {
        ////        return new ApiResponse()
        ////        {
        ////            Status = 1,
        ////            Message = "Cập nhật Dự án thành công"
        ////        };
        ////    }
        ////    else
        ////    {
        ////        return new ApiResponse()
        ////        {
        ////            Status = 0,
        ////            Message = "Cập nhật Dự án không thành công"
        ////        };
        ////    }
        ////}

        //// DELETE api/<controller>/5
        //public async Task<ApiResponse> DeleteAsync(int id)
        //{
        //    if(await ProjectService.delete(id))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa Dự án thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa Dự án thành công"
        //        };
        //    }
        //}

        ////Thay đổi trạng thái
        //[Route("api/Project/ChangeStatus")]
        //[HttpPut]
        //public async Task<ApiResponse> ChangeStatusAsync(int id, int statusId = 0)
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    if (await ProjectService.updateStatus(id, statusId, employee.NhanVienID))
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

        ////Cap nhat nguoi thuc hien
        //[Route("api/Project/ChangePersons")]
        //[HttpPut]
        //public async Task<ApiResponse> ChangePersonsAsync([FromBody] ProjectPersonModel model)
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    if (await ProjectService.updatePersons(model))
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

        ////Ds Người thực hiện theo Dư án
        //[Route("api/Project/EmployeeWorkByProject")]
        //[HttpGet]
        //public async Task<ApiResponse> EmployeeWorkByProjectAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = await ProjectService.getWorkByProject(id)
        //    };
        //}

        ////Thêm người thực hiện
        //[Route("api/Project/AddEmployeeWork")]
        //[HttpPost]
        //public async Task<ApiResponse> AddEmployeeWorkAsync(int id, int employeeId = 0, string position = "", string focusDate = "")
        //{
        //    if(employeeId < 1)
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm người thực hiện không thành công"
        //        };

        //    DateTime _focusDate = DateTime.Now;
        //    if (!DateTime.TryParse(focusDate, out _focusDate))
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Ngày giao nhiệm vụ không đúng định dạng"
        //        };

        //    if (await ProjectService.addWork(id, employeeId, position, _focusDate, this.UserId))
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Thêm người tham gia thành công"
        //        };
        //    }
        //    else
        //    {
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm người tham gia không thành công"
        //        };
        //    }
        //}

        ////Xóa người thực hiện
        //[Route("api/Project/DeleteEmployeeWork")]
        //[HttpDelete]
        //public async Task<ApiResponse> DeleteEmployeeWorkAsync(int id, int employeeId)
        //{
        //    if (employeeId < 1)
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa người thực hiện không thành công"
        //        };

        //    if(await ProjectService.deleteWork(id, employeeId))
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa người thực hiện thành công"
        //        };
        //    else
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa người thực hiện không thành công"
        //        };
        //}
        ////Thêm người theo dõi
        //[Route("api/Project/AddEmployeeControl")]
        //[HttpPost]
        //public async Task<ApiResponse> AddEmployeeControlAsync(int id, int employeeId = 0, string position = "")
        //{
        //    if (employeeId < 1)
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Thêm người theo dõi không thành công"
        //        };

        //    if (await ProjectService.addControl(id, employeeId, position, this.UserId))
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
        ////Xóa người theo dõi
        //[Route("api/Project/DeleteEmployeeControl")]
        //[HttpDelete]
        //public async Task<ApiResponse> DeleteEmployeeControlAsync(int id, int employeeId)
        //{
        //    if (employeeId < 1)
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa người theo dõi không thành công"
        //        };

        //    if (await ProjectService.deleteControl(id, employeeId))
        //        return new ApiResponse()
        //        {
        //            Status = 1,
        //            Message = "Xóa người theo dõi thành công"
        //        };
        //    else
        //        return new ApiResponse()
        //        {
        //            Status = 0,
        //            Message = "Xóa người theo dõi không thành công"
        //        };
        //}
        ////DS Người theo dõi theo Dự án
        //[Route("api/Project/EmployeeControlByProject")]
        //[HttpGet]
        //public async Task<ApiResponse> EmployeeControlByProjectAsync(int id)
        //{
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Data = await ProjectService.getControlByProject(id)
        //    };
        //}
    }
}