using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.Framework;
using OOS.GHR.HRIS.Models;
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
    public class BusinessTripController : BaseApiController
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>

        public async Task<object> Get()
        {
            var data = BusinessTripService.get();

            return new
            {
                Status = 1,
                Message = "CT",
                Data = data != null ? data.Result : null
            };
        }

        public async Task<object> Get(int id)
        {
            var data = BusinessTripService.get(id);

            return new
            {
                Status = 1,
                Message = "CT",
                Data = data != null ? data.Result : null
            };
        }

        //public async Task<object> Get()
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    MissionInfo LI = new MissionInfo(employee.NhanVienID);

        //    return new
        //    {
        //        Status = 1,
        //        Message = "",
        //        Data = LI.List.dsSource.Select().Select(x => new
        //        {
        //            Id = x["QTCongTacID"],
        //            NgayBatDau = x["NgayBatDau"],
        //            NgayKetThuc = x["NgayKetThuc"],
        //            LyDo = x["LyDo"],
        //            NoiCongTac = x["NoiCongTac"],
        //            CongViecCuThe = x["CongViecCuThe"],
        //            XetDuyet = x["XetDuyet"],
        //            Approver = x["NguoiDangChoDuyet"],
        //            //ApprovedDate
        //            ApproveComment = x["YKienPheDuyet"],
        //        })
        //    };
        //}

        //// GET api/<controller>/5
        //public async Task<object> Get(int id)
        //{
        //    OOS.GHR.EntityFramework.Models.NS_QTCongTac ct = await BusinessTripService.detail(id);
        //    return new
        //    {
        //        Status = 1,
        //        Message = "",
        //        Data = new
        //        {
        //            QTCongTacID = ct.QTCongTacID,
        //            NgayBatDau = ct.NgayBatDau,
        //            NgayKetThuc = ct.NgayKetThuc,
        //            LyDoCongTacID = ct.LyDoCongTacID,
        //            NoiCongTac = ct.NoiCongTac,
        //            CongViecCuThe = ct.CongViecCuThe
        //        }
        //    };
        //}

        [Route("api/BusinessTrip/GetReasonList")]
        [HttpGet]
        public async Task<object> GetReasonList()
        {
            return new
            {
                Status = 0,
                Message = "",
                Data = NS_DsLyDoCongTacList.GetNS_DsLyDoCongTacList().Select(x => new
                {
                    Id = x.LyDoCongTacID,
                    Name = x.TenLyDoCongTac
                })
            };
        }

        // POST api/<controller>
        public async Task<object> Post([FromBody] BusinessTripPostModel model)
        {
            //int ReasonId, string StartDate, string EndDate, string Place, string Description, int? EntityId
            if (string.IsNullOrEmpty(model.CongViecCuThe))
            {
                return new
                {
                    Status = 0,
                    Message = "Chưa nhập công việc cụ thể",
                    Data = (object)null
                };
            }

            var sDate = model.NgayBatDau;
            var eDate = model.NgayKetThuc;
            if (eDate <= sDate)
            {
                return new
                {
                    Status = 0,
                    Message = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc",
                    Data = (object)null
                };
            }
            //var user = DatabaseCache.DangNhap;
            var employee = DatabaseCache.NhanVien;
            //decimal sSoNgayNghi = 0;
            //bool AllowSave = true;
            //string notify = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.GetNotify_Business_Trip
            //(user.NhanVienID, user.NguoiDungID, sDate, eDate, model.LyDoCongTacID.GetValueOrDefault(0), out AllowSave, out sSoNgayNghi);
            //if (!AllowSave)
            //{
            //    return new
            //    {
            //        Status = 0,
            //        Message = DatabaseCache.GetText(notify),
            //        Data = (object)null
            //    };
            //}

            try
            {
                var entity = NS_QTCongTac.GetNS_QTCongTac(model.Id.GetValueOrDefault(0));
                bool IsNew = entity.QTCongTacID <= 0;
                bool XetDuyet_CT = XetDuyet.XD_Available(OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_CONGTAC);
                entity.CongViecCuThe = model.CongViecCuThe;
                entity.LyDoCongTacID = model.LyDoCongTacID.GetValueOrDefault(0);
                entity.NgayBatDau = sDate;
                entity.NgayKetThuc = eDate;
                entity.NoiCongTac = model.NoiCongTac;
                entity.IsNew = entity.QTCongTacID <= 0;
                entity.IsDirty = true;

                entity.NhanVienID = employee.NhanVienID;

                if (XetDuyet_CT)
                    entity.XetDuyet = 0;
                else
                    entity.XetDuyet = 1;
                //if (XetDuyet_CT && IsNew)
                //    entity.XetDuyet = 0;
                //else
                //{
                //    if (IsNew)
                //        entity.XetDuyet = 1;
                //}

                if (IsNew)
                {
                    OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.SetObjectThongTinNhanVien(entity.NhanVienID, entity);
                    entity.CreatedBy = DatabaseCache.NhanVien.ToString();
                }
                entity.Save();

                if (XetDuyet_CT && IsNew)
                {
                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_CongTac(entity);
                    return new
                    {
                        Status = 1,
                        Message = model.Id.GetValueOrDefault(0) > 0 ? DatabaseCache.GetText("Cập nhật quá trình công tác vào danh sách chờ phê duyệt !") : DatabaseCache.GetText("Đã thêm quá trình công tác vào danh sách chờ phê duyệt !"),
                        Data = (object)null
                    };
                }

                return new
                {
                    Status = 1,
                    Message = model.Id.GetValueOrDefault(0) > 0 ? DatabaseCache.GetText("Cập nhật quá trình công tác thành công !") : DatabaseCache.GetText("Lưu thông tin quá trình công tác thành công !"),
                    Data = (object)null
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình lưu dữ liệu: ") + ex.Message,
                    Data = (object)null
                };
            }
        }

        // DELETE api/<controller>/5
        public async Task<object> Delete(int id)
        {
            var employee = DatabaseCache.NhanVien;
            int nhanVienID = employee.NhanVienID;
            try
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTCongTac.DeleteWithApprove(id, nhanVienID, "");
                if (strErr != "" && strErr.Trim() != "Hủy đăng ký thành công !")
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText(strErr),
                        Data = (object)null
                    };
            }
            catch (Exception ex)
            {
                return new
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình xóa dữ liệu: ") + ex.Message,
                    Data = (object)null
                };
            }
            return new
            {
                Status = 1,
                Message = DatabaseCache.GetText("Xóa thông tin công tác thành công !"),
                Data = (object)null
            };
        }

        [Route("api/BusinessTrip/cancel")]
        [HttpPost] 
        public async Task<object> Cancel(int id, string reason) {
            var employee = DatabaseCache.NhanVien;
            int nhanVienID = employee.NhanVienID;
            try
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTCongTac.DeleteWithApprove(id, nhanVienID, "");
                if (strErr.Trim() != "Hủy đăng ký thành công !" && strErr != "")
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText(strErr),
                        Data = (object)null
                    };
            }
            catch (Exception ex)
            {
                return new
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình hủy dữ liệu: ") + ex.Message,
                    Data = (object)null
                };
            }
            return new
            {
                Status = 1,
                Message = DatabaseCache.GetText("Hủy thông tin công tác thành công !"),
                Data = (object)null
            };
        }
    }
}