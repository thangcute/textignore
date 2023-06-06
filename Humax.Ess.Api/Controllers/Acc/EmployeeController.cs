using Humax.Ess.Api.Helpers;
using Humax.Ess.Api.Models;
using OOS.GHR.Library;
using OOS.GHR.Services.Services.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Humax.Ess.Api.Models.Ess;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Services.Models;
using System.Data;

namespace Humax.Ess.Api.Controllers.Acc
{
    public class EmployeeController : BaseApiController
    {
        // GET api/Employee/id
        [HttpGet]
        public ApiResponse Get(int id)
        {
            try
            {
                var employee = NhanVien.GetNhanVien(id);
                if (employee == null)
                {
                    return new ApiResponse()
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText("Không tìm thấy thông tin.")
                    };
                }
                return new ApiResponse()
                {
                    Status = 1,
                    Data = new Humax.Ess.Api.Models.Ess.UserInfoModel
                    {
                        EmployeeId = employee.NhanVienID,
                        EmployeeCode = employee.MaNhanVien,
                        FullName = employee.Ho + " " + employee.HoTen,
                        JobTitle = NS_DsChucVu.GetTenChucVuByID(employee.ChucVuID),
                        Picture = Business.AvatarToBase64String(employee.Anh, employee.HoTen[0])
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = ex.Message
                };
            }
        }

        [Route("api/Employee/Search")]
        [HttpPost]
        public ApiResponse Search([FromBody] SelectFilterModel model)
        {
            int _limit = 10;
            if (model.size.GetValueOrDefault(0) > 0)
                _limit = (int)model.size;

            int _page = (model.page > 0) ? model.page : 1;
            int _offset = (_page - 1) * _limit;
            string _search = model.search;
            try
            {
                string sqlTotal = @"select count(1)
                                    from NhanVien
                                    where ISNULL(NghiViec, 0)=0 " +
                                    (string.IsNullOrEmpty(model.search) ? @"and 1=1" : @"and ((Ho + ' ' + HoTen) like N'%" + model.search + @"%' or (REPLICATE('0', 5 - LEN(MaNhanVien)) + MaNhanVien) like '%" + model.search + @"%')");

                string sqlQuery = @"select nv.NhanVienID as [Id]
                                        ,(nv.Ho + ' ' + nv.HoTen + ' - ' + (REPLICATE('0', 5 - LEN(nv.MaNhanVien)) + nv.MaNhanVien)) as [Name]
                                        ,'' as [Des]
                                        ,nv.[Anh] as [Picture]
                                        ,nv.Email
                                        ,nv.DienThoai as [Mobile]
                                        ,pb.Ten as [Department]
                                        ,cv.TenChucVu as [JobPosition]
                                        ,cd.TenChucDanh as [JobTitle]
                                    from NhanVien as nv
                                    left join NS_DsChucVu as cv on nv.ChucVuID=cv.ChucVuID
                                    left join NS_DsChucDanh as cd on nv.ChucDanhID=cd.ChucDanhID
                                    left join PhongBan as pb on nv.PhongBanID=pb.PhongBanID
                                    where ISNULL(nv.NghiViec, 0)=0 " +
                                    (string.IsNullOrEmpty(model.search) ? @"and 1=1" : @"and ((nv.Ho + ' ' + nv.HoTen) like N'%" + model.search + @"%' or (REPLICATE('0', 5 - LEN(nv.MaNhanVien)) + nv.MaNhanVien) like '%" + model.search + @"%')")
                                    + @" order by NhanVienID";
                if (!model.all.GetValueOrDefault(false))
                {
                    sqlQuery += @" offset " + _offset + @" rows fetch next " + _limit + @" rows only";
                }
                                    
                string conID = OOS.GHR.Library.Database.Connection;


                DataTable result = OOS.GHR.Library.Database.ExecTable(sqlQuery, false, conID);

                int totalRow = (int)OOS.GHR.Library.Database.ExecScalar(sqlTotal, false, conID);

                return new ApiResponse()
                {
                    Status = 1,
                    Data = result,
                    Message = totalRow.ToString(),
                    Total = totalRow
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = ""
                };
            }
        }

        // GET api/Employee/NewEmployees
        [Route("api/Employee/NewEmployees")]
        [HttpGet]
        public ApiResponse NewEmployees(int count)
        {
            try
            {
                using (var db = new OosContext())
                {
                    string sql = "SELECT * FROM [dbo].[NhanVien]";
                    var employees = db.NhanViens.SqlQuery(sql);
                    if (employees.Any())
                    {
                        return new ApiResponse()
                        {
                            Status = 1,
                            Data = employees.OrderByDescending(x => x.NgayBatDauLam).Take(count)
                            .Select(x => new Humax.Ess.Api.Models.Ess.UserInfoModel
                            {
                                EmployeeId = x.NhanVienID,
                                EmployeeCode = x.MaNhanVien,
                                FullName = x.Ho + " " + x.HoTen,
                                JobTitle = x.ChucVuID.HasValue ? NS_DsChucVu.GetTenChucVuByID(x.ChucVuID.Value) : "",
                                Picture = Business.AvatarToBase64String(x.Anh, x.HoTen[0]),
                            }).ToList()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = ex.Message
                };
            }

            return new ApiResponse()
            {
                Status = 0,
                Message = "Không có nhân viên nào"
            };
        }

        // GET api/<controller>
        public async Task<ApiResponse> List(string conditions = "", int page = 1, int size = 15, bool all = false)
        {
            var data = await EmployeeService.get(_conditions: conditions, _limit: (page > 0 ? size : 0), _offset: (page > 0 ? (page - 1) * size : 0), _all: all);
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
    }
}