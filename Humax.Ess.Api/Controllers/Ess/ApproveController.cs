using Humax.Ess.Api.Helpers;
using Humax.Ess.Api.Models;
using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.Framework;
using OOS.GHR.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class ApproveController : BaseApiController
    {
        // GET api/<controller>/5 -- GetApproveDetail
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("")
                };
            }
            WF_QuyTrinh_ThucHien qt = WF_QuyTrinh_ThucHien.GetWF_QuyTrinh_ThucHien(id);
            SYS_XetDuyet xd = SYS_XetDuyet.GetSYS_XetDuyet(qt.XetDuyetID);
            return new ApiResponse()
            {
                Status = 1,
                Data = new {
                    Id = id,
                    Name = xd.TenXetDuyet,
                    Description = EmailSender.GetHTMLMoTa(xd.QuyTrinhID, xd.ObjectID, qt.ApproveKey, qt.QuyTrinhThucHienID),
                    PeopleRequest = Business.GetHoVaTenByUser(xd.NguoiTaoID),
                    RequestDate = xd.NgayTao.Value.ToShortDateString() + " - " + xd.NgayTao.Value.ToShortTimeString(),
                    Status = qt.TrangThai,
                    RejectReason = qt.LyDoTuChoi,
                    ApproveComment = qt.GhiChu,
                    ReadOnly = (xd.NguoiTaoID == DatabaseCache.DangNhap.NhanVienID) || (xd.TrangThai != 0)
                }
            };
        }

        [Route("api/Approve/GetApproveGroupList")]
        [HttpGet]
        //[OOSAuthorization(Code = "APROVE_LST")]
        public async Task<ApiResponse> GetApproveGroupList()
        {
            var Result = XetDuyet.GetXetDuyetListByGroup(DatabaseCache.DangNhap.NguoiDungID, new DateTime(1900, 1, 1), new DateTime(3000, 1, 1), 0);
            return new ApiResponse()
            {
                Status = 1,
                Data = Result.Select().Select(x => new
                {
                    Id = x["QuyTrinhID"].ToString(),
                    Name = x["TenQuyTrinh"].ToString(),
                    Count = x["SL"].ToString()
                })
            };
        }

        [Route("api/Approve/GetApproveList")]
        [HttpGet]
        //[OOSAuthorization(Code = "APROVE_LST")]
        public async Task<ApiResponse> GetApproveList(int ApproveGroupId)
        {
            var Result = XetDuyet.GetXetDuyetAvailable(DatabaseCache.DangNhap.NguoiDungID, ApproveGroupId);
            if (Result == null)
            {
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("")
                };
            }
            return new ApiResponse()
            {
                Status = 1,
                Data = Result.Select().Select(x => new
                {
                    FullName = x["NguoiTao"].ToString(),
                    JobTitle = x["ChucVu"].ToString(),
                    Date = Convert.ToDateTime(x["NgayTao"]).ToString("dd/MM/yyyy HH:mm"),
                    ApproveName = x["TenXetDuyet"].ToString(),
                    Id = x["QuyTrinhThucHienID"].ToString()
                })
            };
        }

        // POST api/<controller>
        public async Task<ApiResponse> Post(int Id, string Comment)
        {
            if (Id <= 0)
                return new ApiResponse()
                {
                    Status = 0,
                    Message = ""
                };

            string conID = Guid.NewGuid().ToString();
            ConnectionManager.BeginConnection(conID);
            var NhanVien = DatabaseCache.NhanVien;
            try
            {
                WF_QuyTrinh_ThucHien qt = WF_QuyTrinh_ThucHien.GetWF_QuyTrinh_ThucHien(Id, conID);
                qt.NguoiXetDuyet = NhanVien.ToString();
                qt.ChucVuNguoiXetDuyet = NS_DsChucVu.GetTenChucVuByID(NhanVien.ChucVuID);
                qt.ConnectionID = conID;
                qt.ApplyEdit();
                qt.TrangThai = 1;
                qt.GhiChu = Comment;
                qt.NgayThucHien = DateTime.Now;
                qt.Do_Update();

                SYS_XetDuyet xd = SYS_XetDuyet.GetSYS_XetDuyet(qt.XetDuyetID, conID);
                xd.ConnectionID = conID;
                xd.GhiChu = Comment;
                xd.NguoiDuyetCuoi = NhanVien.MaNhanVien + " - " + NhanVien.ToString();
                xd.NgayDuyetCuoi = DateTime.Now;

                WF_QuyTrinh_BuocThucHien qb = WF_QuyTrinh_BuocThucHien.GetWF_QuyTrinh_BuocThucHien(qt.QuyTrinhBuocThucHienID, conID);
                int BuocTiepTheoID = OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.GetBuocTiepTheo(qb.QuyTrinhID, qb.ThuTu, conID);
                if (BuocTiepTheoID > 0)
                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TaoBuocKeTiepXetDuyet(null, xd, qb.QuyTrinhBuocThucHienID, BuocTiepTheoID, conID, (int)TrangThai_XetDuyet.PROCESSING,
                    qt.NguoiDungXetDuyetID, null);
                else
                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.KetThucQuyTrinhXetDuyet(xd, "", Comment, DatabaseCache.DangNhap.NguoiDungID, conID);
            }
            catch
            {
            }
            ConnectionManager.Close(conID);

            return new ApiResponse()
            {
                Status = 1,
                Message = DatabaseCache.GetText("Phê duyệt Thành công.")
            };
        }

        [Route("api/Approve/ApproveList")]
        [HttpPost]
        public async Task<ApiResponse> ApproveList(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Bạn phải nhập danh sách phê duyệt!")
                };
            var list = Ids.Split(',').Select(x => x.ToInt());
            string conID = Guid.NewGuid().ToString();
            ConnectionManager.BeginConnection(conID);
            var NhanVien = DatabaseCache.NhanVien;
            try
            {
                foreach (var id in list)
                {
                    if (id <= 0)
                    {
                        continue;
                    }
                    WF_QuyTrinh_ThucHien qt = WF_QuyTrinh_ThucHien.GetWF_QuyTrinh_ThucHien(id, conID);
                    qt.NguoiXetDuyet = NhanVien.ToString();
                    qt.ChucVuNguoiXetDuyet = NS_DsChucVu.GetTenChucVuByID(NhanVien.ChucVuID);
                    qt.ConnectionID = conID;
                    qt.ApplyEdit();
                    qt.TrangThai = 1;
                    qt.NgayThucHien = DateTime.Now;
                    qt.Do_Update();

                    SYS_XetDuyet xd = SYS_XetDuyet.GetSYS_XetDuyet(qt.XetDuyetID, conID);
                    xd.ConnectionID = conID;
                    xd.NguoiDuyetCuoi = NhanVien.MaNhanVien + " - " + NhanVien.ToString();
                    xd.NgayDuyetCuoi = DateTime.Now;

                    WF_QuyTrinh_BuocThucHien qb = WF_QuyTrinh_BuocThucHien.GetWF_QuyTrinh_BuocThucHien(qt.QuyTrinhBuocThucHienID, conID);
                    int BuocTiepTheoID = OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.GetBuocTiepTheo(qb.QuyTrinhID, qb.ThuTu, conID);
                    if (BuocTiepTheoID > 0)
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TaoBuocKeTiepXetDuyet(null, xd, qb.QuyTrinhBuocThucHienID, BuocTiepTheoID, conID, (int)TrangThai_XetDuyet.PROCESSING,
                        qt.NguoiDungXetDuyetID, null);
                    else
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.KetThucQuyTrinhXetDuyet(xd, "", "", DatabaseCache.DangNhap.NguoiDungID, conID);
                }
            }
            catch
            {
            }
            ConnectionManager.Close(conID);

            return new ApiResponse()
            {
                Status = 1,
                Message = DatabaseCache.GetText("Phê duyệt Thành công.")
            };
        }

        [Route("api/Approve/Reject")]
        [HttpPost]
        public async Task<ApiResponse> Reject(int Id, string Reason, string Comment)
        {
            if (string.IsNullOrEmpty(Reason))
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Bạn phải nhập lý do từ chối!")
                };

            string conID = Guid.NewGuid().ToString();
            ConnectionManager.BeginConnection(conID);
            var NhanVien = DatabaseCache.NhanVien;
            try
            {
                WF_QuyTrinh_ThucHien qt = WF_QuyTrinh_ThucHien.GetWF_QuyTrinh_ThucHien(Id, conID);
                qt.NguoiXetDuyet = NhanVien.ToString();
                qt.ChucVuNguoiXetDuyet = NS_DsChucVu.GetTenChucVuByID(NhanVien.ChucVuID);
                qt.ConnectionID = conID;
                qt.ApplyEdit();
                qt.TrangThai = -1;
                qt.LyDoTuChoi = Reason;
                qt.NgayThucHien = DateTime.Now;
                qt.GhiChu = Comment;
                qt.Do_Update();

                SYS_XetDuyet xd = SYS_XetDuyet.GetSYS_XetDuyet(qt.XetDuyetID, conID);
                WF_QuyTrinh_BuocThucHien qb = WF_QuyTrinh_BuocThucHien.GetWF_QuyTrinh_BuocThucHien(qt.QuyTrinhBuocThucHienID, conID);

                if (xd.GhiChu == "")
                    xd.GhiChu = Reason + Comment;

                OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TuChoiXetDuyet(null, null, xd, qb, qt, conID);
            }
            catch { }
            ConnectionManager.Close(conID);

            return new ApiResponse()
            {
                Status = 1,
                Message = DatabaseCache.GetText("Từ chối phê duyệt thành công!")
            };
        }

        [Route("api/Approve/RejectList")]
        [HttpPost]
        public async Task<ApiResponse> RejectList(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
                return new ApiResponse()
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Bạn phải nhập lý danh sách chối!")
                };
            var list = Ids.Split(',').Select(x => x.ToInt());
            string conID = Guid.NewGuid().ToString();
            ConnectionManager.BeginConnection(conID);
            var NhanVien = DatabaseCache.NhanVien;
            try
            {
                foreach (var id in list)
                {
                    if (id <= 0)
                    {
                        continue;
                    }
                    WF_QuyTrinh_ThucHien qt = WF_QuyTrinh_ThucHien.GetWF_QuyTrinh_ThucHien(id, conID);
                    qt.NguoiXetDuyet = NhanVien.ToString();
                    qt.ChucVuNguoiXetDuyet = NS_DsChucVu.GetTenChucVuByID(NhanVien.ChucVuID);
                    qt.ConnectionID = conID;
                    qt.ApplyEdit();
                    qt.TrangThai = -1;
                    qt.NgayThucHien = DateTime.Now;
                    qt.Do_Update();

                    SYS_XetDuyet xd = SYS_XetDuyet.GetSYS_XetDuyet(qt.XetDuyetID, conID);
                    WF_QuyTrinh_BuocThucHien qb = WF_QuyTrinh_BuocThucHien.GetWF_QuyTrinh_BuocThucHien(qt.QuyTrinhBuocThucHienID, conID);

                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TuChoiXetDuyet(null, null, xd, qb, qt, conID);
                }
            }
            catch { }
            ConnectionManager.Close(conID);

            return new ApiResponse()
            {
                Status = 1,
                Message = DatabaseCache.GetText("Từ chối phê duyệt thành công!")
            };
        }
    }
}