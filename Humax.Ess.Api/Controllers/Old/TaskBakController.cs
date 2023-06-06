using Humax.Ess.Api.Helpers;
using OOS.GHR;
using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.Framework;
using OOS.GHR.HRIS.Models;
using OOS.GHR.Library;
using OOS.GHR.Payroll.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Humax.Ess.Api.Controllers.Old
{
    public class TaskBakController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            Response.StatusCode = 500;
            filterContext.Result = new JsonResult
            {
                Data = new { Status = 0, error = filterContext.Exception.Message },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = filterContext.HttpContext.Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = Json((object)false);
                return;
            }
            if (!DatabaseCache.LoginSessionContainer.ContainsKey(token))
            {
                filterContext.Result = Json((object)false);
                return;
            }
            foreach (var filter in filterContext.ActionDescriptor.GetCustomAttributes(typeof(OOSAuthorization), false))
            {
                var desiredValue = filter as OOSAuthorization;
                if (desiredValue.Code != "")
                {
                    if (!Business.IsOKPermission(desiredValue.Code, DatabaseCache.LoginSessionContainer[token].QuyenList, false))
                    {
                        filterContext.Result = Json((object)false);
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        [HttpPost]
        //[OOSAuthorization(Code = "APROVE_LST")]
        public ActionResult GetApproveGroupList()
        {
            var Result = XetDuyet.GetXetDuyetListByGroup(DatabaseCache.DangNhap.NguoiDungID, new DateTime(1900, 1, 1), new DateTime(3000, 1, 1), 0);
            return Json(new
            {
                Status = 1,
                Data = Result.Select().Select(x => new
                {
                    Id = x["QuyTrinhID"].ToString(),
                    Name = x["TenQuyTrinh"].ToString(),
                    Count = x["SL"].ToString()
                })
            });
        }

        [HttpPost]
        public ActionResult GetApproveDetail(int Id)
        {
            if (Id == 0)
            {
                return Json(false);
            }
            WF_QuyTrinh_ThucHien qt = WF_QuyTrinh_ThucHien.GetWF_QuyTrinh_ThucHien(Id);
            SYS_XetDuyet xd = SYS_XetDuyet.GetSYS_XetDuyet(qt.XetDuyetID);
            return Json(new
            {
                Id = Id,
                Name = xd.TenXetDuyet,
                Description = EmailSender.GetHTMLMoTa(xd.QuyTrinhID, xd.ObjectID, qt.ApproveKey, qt.QuyTrinhThucHienID),
                PeopleRequest = Business.GetHoVaTenByUser(xd.NguoiTaoID),
                RequestDate = xd.NgayTao.Value.ToShortDateString() + " - " + xd.NgayTao.Value.ToShortTimeString(),
                Status = qt.TrangThai,
                RejectReason = qt.LyDoTuChoi,
                ApproveComment = qt.GhiChu,
                ReadOnly = (xd.NguoiTaoID == DatabaseCache.DangNhap.NhanVienID) || (xd.TrangThai != 0)
            });
        }

        [HttpPost]
        //[OOSAuthorization(Code = "APROVE_LST")]
        public ActionResult GetApproveList(int ApproveGroupId)
        {
            var Result = XetDuyet.GetXetDuyetAvailable(DatabaseCache.DangNhap.NguoiDungID, ApproveGroupId);
            if (Result == null)
            {
                return Json(new { Status = 0 });
            }
            return Json(new
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
            });
        }

        [HttpPost]
        public ActionResult Approve(int Id, string Comment)
        {
            if (Id <= 0)
                return Json(new { Status = 0 });

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
                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TaoBuocKeTiepXetDuyet(xd, qb.QuyTrinhBuocThucHienID, BuocTiepTheoID, conID, (int)TrangThai_XetDuyet.PROCESSING,
                    qt.NguoiDungXetDuyetID, null);
                else
                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.KetThucQuyTrinhXetDuyet(xd, "", Comment, DatabaseCache.DangNhap.NguoiDungID, conID);
            }
            catch
            {
            }
            ConnectionManager.Close(conID);

            return Json(new { Status = 1 });
        }

        [HttpPost]
        public ActionResult Reject(int Id, string Reason, string Comment)
        {
            if (string.IsNullOrEmpty(Reason))
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn phải nhập lý do từ chối!") });

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

                OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TuChoiXetDuyet(xd, qb, qt, conID);
            }
            catch { }
            ConnectionManager.Close(conID);

            return Json(new { Status = 1, Message = DatabaseCache.GetText("Từ chối phê duyệt thành công!") });
        }

        [HttpPost]
        public ActionResult ApproveList(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
                return Json(new { Status = 0 });
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
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TaoBuocKeTiepXetDuyet(xd, qb.QuyTrinhBuocThucHienID, BuocTiepTheoID, conID, (int)TrangThai_XetDuyet.PROCESSING,
                        qt.NguoiDungXetDuyetID, null);
                    else
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.KetThucQuyTrinhXetDuyet(xd, "", "", DatabaseCache.DangNhap.NguoiDungID, conID);
                }
            }
            catch
            {
            }
            ConnectionManager.Close(conID);

            return Json(new { Status = 1 });
        }

        [HttpPost]
        public ActionResult RejectList(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
                return Json(new { Status = 0 });
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

                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.TuChoiXetDuyet(xd, qb, qt, conID);
                }
            }
            catch { }
            ConnectionManager.Close(conID);

            return Json(new { Status = 1 });
        }

        [HttpPost]
        public ActionResult GetUserInfo()
        {
            var employee = DatabaseCache.NhanVien;
            if (employee == null)
            {
                return Json((object)false);
            }
            return Json(new
            {
                FullName = employee.Ho + " " + employee.HoTen,
                JobTitle = NS_DsChucVu.GetTenChucVuByID(employee.ChucVuID),
                Phone = employee.DienThoai,
                Email = employee.Email,
                Skype = employee.Skype,
                Address = employee.DiaChi,
                Picture = Business.AvatarToBinary(employee.Anh, employee.HoTen[0])
            });
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(string Phone, string Email, string Skype, string Address, string Picture)
        {
            var token = Request.Headers["Token"];
            var employee = DatabaseCache.LoginSessionContainer[token].NhanVien;
            if (employee == null)
            {
                return Json(new { Status = 0 });
            }
            if (Phone != null)
            {
                employee.DienThoai = Phone;
            }
            if (Email != null)
            {
                employee.Email = Email;
            }
            if (Skype != null)
            {
                employee.Skype = Skype;
            }
            if (Address != null)
            {
                employee.DiaChi = Address;
            }
            if (!string.IsNullOrEmpty(Picture))
            {
                employee.Anh = Convert.FromBase64String(Picture);
            }
            employee.Save();
            DatabaseCache.LoginSessionContainer[token].NhanVien = employee;
            return Json(new
            {
                Status = 1
            });
        }

        [HttpPost]
        public ActionResult GetBusinessTripReasonList()
        {
            return Json(new
            {
                Status = 0,
                Data = NS_DsLyDoCongTacList.GetNS_DsLyDoCongTacList().Select(x => new
                {
                    Id = x.LyDoCongTacID,
                    Name = x.TenLyDoCongTac
                })
            });
        }

        [HttpPost]
        public ActionResult GetWorkShiftList()
        {
            return Json(new
            {
                Status = 1,
                Data = NS_CaLamViecList.GetNS_CaLamViecList().Select(x => new
                {
                    Id = x.CaLamViecID,
                    Name = x.TenCa
                })
            });
        }

        [HttpPost]
        public ActionResult GetOTReasonList()
        {
            return Json(new
            {
                Status = 1,
                Data = NS_DsLyDoTangCaList.GetNS_DsLyDoTangCaList().Select(x => new
                {
                    Id = x.LyDoTangCaID,
                    Name = x.TenLyDoTangCa
                })
            });
        }

        [HttpPost]
        public ActionResult GetLeaveReasonList()
        {
            return Json(new
            {
                Status = 1,
                Data = NS_TL_KyHieuChamCongList.GetNS_TL_KyHieuChamCongListFromWhere
                ("MoTa like N'%nghỉ%' Or Mota like '%leave%'", null)
                .Select(x => new
                {
                    Id = x.KyHieuChamCongID,
                    Name = x.MoTa
                })
            });
        }

        [HttpPost]
        public ActionResult GetSystemCount()
        {
            var user = DatabaseCache.DangNhap;
            return Json(new
            {
                ApproveCount = XetDuyet.GetCountXetDuyetAvailable(user.NguoiDungID),
                NotifyCount = OOS.GHR.BusinessAdapter.Activity.GetActivityCount(DatabaseCache.DangNhap.NguoiDungID, 0, 0),
                WarningCount = OOS.GHR.BusinessAdapter.HeThong.CanhBaoHeThong.CountAllAvailable(user.NguoiDungID, user.NhanVienID),
            });
        }

        [HttpPost]
        public ActionResult CreateBusinessTrip(int ReasonId, string StartDate, string EndDate, string Place, string Description, int? EntityId)
        {
            if (string.IsNullOrEmpty(Description))
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Chưa nhập công việc cụ thể.") });
            }

            var sDate = StartDate.ToDatetime();
            var eDate = EndDate.ToDatetime();
            if (eDate <= sDate)
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.") });
            }
            var user = DatabaseCache.DangNhap;
            var employee = DatabaseCache.NhanVien;
            decimal sSoNgayNghi = 0;
            bool AllowSave = true;
            string notify = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.GetNotify_Business_Trip
            (user.NhanVienID, user.NguoiDungID, sDate, eDate, ReasonId, out AllowSave, out sSoNgayNghi);
            if (!AllowSave)
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText(notify) });
            }

            try
            {
                var entity = NS_QTCongTac.GetNS_QTCongTac(EntityId.GetValueOrDefault(0));
                bool IsNew = entity.QTCongTacID <= 0;
                bool XetDuyet_CT = XetDuyet.XD_Available(OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_CONGTAC);
                entity.CongViecCuThe = Description;
                entity.LyDoCongTacID = ReasonId;
                entity.NgayBatDau = sDate;
                entity.NgayKetThuc = eDate;
                entity.NoiCongTac = Place;
                entity.IsNew = entity.QTCongTacID <= 0;
                entity.IsDirty = true;

                entity.NhanVienID = employee.NhanVienID;

                if (XetDuyet_CT && IsNew)
                    entity.XetDuyet = 0;
                else
                {
                    if (IsNew)
                        entity.XetDuyet = 1;
                }

                if (IsNew)
                {
                    OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.SetObjectThongTinNhanVien(entity.NhanVienID, entity);
                    entity.CreatedBy = DatabaseCache.NhanVien.ToString();
                }
                entity.Save();

                if (XetDuyet_CT && IsNew)
                {
                    OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_CongTac(entity);
                    return Json(new { Status = 1, Message = DatabaseCache.GetText("Đã thêm quá trình công tác vào danh sách chờ phê duyệt !") });
                }
                return Json(new { Status = 1, Message = DatabaseCache.GetText("Lưu thông tin quá trình công tác thành công !") });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình lưu dữ liệu: ") + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateOT(string Date, int WorkShiftId, decimal BeforeHourPlus, string BeforeFrom, string BeforeTo,
            decimal AfterHourPlus, string AfterFrom, string AfterTo, int ReasonId, string Description)
        {
            try
            {
                //Kiểm tra cho phép Sửa / Xóa backdate với số ngày cho phép
                int BackDateDay = ConfigClass.NS_CC_SONGAYLUISUACHAMCONG;
                /////////////////////////////////////////////////////////////////////////
                var spl1 = BeforeFrom.Split(':');
                var spl2 = BeforeTo.Split(':');
                var spl3 = AfterFrom.Split(':');
                var spl4 = AfterTo.Split(':');
                var BeforeFromTime = new TimeSpan(spl1[0].ToInt(), spl1[1].ToInt(), 0);
                var BeforeToTime = new TimeSpan(spl2[0].ToInt(), spl2[1].ToInt(), 0);
                var AfterFromTime = new TimeSpan(spl3[0].ToInt(), spl3[1].ToInt(), 0);
                var AfterToTime = new TimeSpan(spl4[0].ToInt(), spl4[1].ToInt(), 0);
                var user = DatabaseCache.DangNhap;

                if (BeforeFromTime > BeforeToTime)
                {
                    return Json(new { Status = 0, Message = DatabaseCache.GetText("Từ giờ < tới giờ !") });
                }

                if (AfterFromTime > AfterToTime)
                {
                    return Json(new { Status = 0, Message = DatabaseCache.GetText("Từ giờ < tới giờ !") });
                }

                if (WorkShiftId == 0)
                {
                    return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn phải chọn ca làm việc, hoặc lý do !") });
                }
                var sDate = Date.ToDatetime();
                if (sDate < DateTime.Now.Date.AddDays(BackDateDay * -1))
                {
                    return Json(new { Status = 0, Message = string.Format(DatabaseCache.GetText("Không cho phép Backdate quá [{0}] Ngày !"), BackDateDay) });
                }

                if (ReasonId <= 0)
                {
                    return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn phải chọn lý do tăng ca !") });
                }

                //Kiểm tra ngày khóa công
                DateTime dtNgayKhoaCong = ConfigClass.CL_NGAYKHOACONGTHANG;
                if (dtNgayKhoaCong.Year > 2000)
                {
                    if (dtNgayKhoaCong > sDate || dtNgayKhoaCong > sDate)
                    {
                        return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu trước ngày khóa công !") });
                    }
                }
                string conID = System.DateTime.Now.Ticks.ToString();
                ConnectionManager.BeginConnection(conID);
                try
                {
                    var model = new OTInfoModel
                    {
                        TuNgay = sDate,
                        ToiNgay = sDate,
                        CaLamViecID = WorkShiftId,
                        GioLamThem = BeforeHourPlus,
                        GioLamThemTruocCa = AfterHourPlus,
                        BDLamThemTruocCa = DateTime.Today.Add(BeforeFromTime),
                        BDLamThemSauCa = DateTime.Today.Add(BeforeToTime),
                        KTLamThemSauCa = DateTime.Today.Add(AfterToTime),
                        KTLamThemTruocCa = DateTime.Today.Add(AfterFromTime),
                        LyDoTangCaID = ReasonId,
                        LyDoTangCa = Description,
                    };
                    OTInfoModel.SaveDangKyCa(model, BackDateDay, true);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    ConnectionManager.Close(conID);
                }

                return Json(new { Status = 1, Message = DatabaseCache.GetText("Đăng ký OT thành công !") });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Lỗi đăng ký OT !") });
            }
        }

        [HttpPost]
        public ActionResult CreateLeave(int ReasonId, string StartDate, string EndDate, int TotalDays,
            string Place, string Description, string Attachment, string FileName, int? EntityId)
        {
            if (string.IsNullOrEmpty(StartDate))
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Chưa nhập ngày bắt đầu.") });

            if (string.IsNullOrEmpty(EndDate))
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Chưa nhập ngày kết thúc.") });
            var sDate = StartDate.ToDatetime();
            var eDate = EndDate.ToDatetime();
            if (eDate <= sDate)
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.") });
            var user = DatabaseCache.DangNhap;
            var employee = DatabaseCache.NhanVien;
            if (ReasonId <= 0 && !ConfigClass.HSNS_NGHIPHEP_KHONGCANLYDO)
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Chưa chọn lý do nghỉ phép !") });

            if (OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.CheckTrungNghiPhep(sDate, eDate, user.NhanVienID, 0) > 0)
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Đăng ký nghỉ phép trùng ngày !") });

            try
            {
                var entity = NS_QTNghiPhep.GetNS_QTNghiPhep(EntityId.GetValueOrDefault(0));
                NS_QTNghiPhep oldEntity = null;
                if (entity.QTNghiPhepID > 0)
                    oldEntity = entity.Clone() as NS_QTNghiPhep;
                entity.NgayBatDau = sDate;
                entity.NgayKetThuc = eDate;
                entity.KyHieuChamCongID = ReasonId;
                entity.SoNgayNghi = TotalDays;
                entity.Des = Place;
                entity.LyDoNghi = Description;

                bool IsNew = entity.QTNghiPhepID <= 0;
                bool IsXetDuyet = OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.XD_Available
                (OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_NGHIPHEP) || (DatabaseCache.IsPortalUser);

                entity.IsNew = entity.QTNghiPhepID <= 0;
                entity.IsDirty = true;
                entity.Type = 1000;

                if (entity.NhanVienID == 0)
                    entity.NhanVienID = employee.NhanVienID;

                //Nếu là Tự xin nghỉ phép: Account portal acc -> XetDuyet luôn là 0
                if (entity.NhanVienID == DatabaseCache.DangNhap.NhanVienID && DatabaseCache.DangNhap.IsPortalAccount)
                    entity.XetDuyet = 0;
                if (entity.IsNew)
                    OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.SetObjectThongTinNhanVien(entity.NhanVienID, entity);
                entity.Save();

                if (oldEntity != null)
                {
                    string strHistory = OOS.GHR.BusinessAdapter.Global.DataHistory.GetHistoryModified(oldEntity, entity);
                    if (strHistory != "")
                        OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog(OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.HOSO_NHANSU, "Chỉnh sửa Qúa trình nghỉ phép", strHistory);
                }

                SortedList sl = new SortedList();
                sl.Add("@QTNghiPhepID", entity.QTNghiPhepID);
                Database.ExecNonquery("OnAfterSaveNghiPhep", sl, true);

                /////////////////// Lưu LOG
                if (entity.IsNew)
                    OOS.GHR.BusinessAdapter.Log_Action.LOG_ThemMoi(entity.NhanVienID, "Quá trình nghỉ phép");
                else
                    OOS.GHR.BusinessAdapter.Log_Action.LOG_ChinhSua(entity.NhanVienID, "Quá trình nghỉ phép", null, null);
                ///////////////////////////////////////////////////////////////////

                #region Save File Store
                string[] fileUploads = FileManagement.GetAllFiles("NS_QTNghiPhep_FileStore");
                foreach (string file in fileUploads)
                {
                    OOS.GHR.FileManagement.Move_And_Add_File(file, "NS_QTNghiPhep", entity.QTNghiPhepID);
                }
                #endregion

                //Nếu xét duyệt Hoặc không phải là thêm mới
                if (!IsXetDuyet || !IsNew)
                {
                    if (IsXetDuyet && !IsNew)
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_NghiPhep(entity);

                    return Json(new { Status = 1, Message = DatabaseCache.GetText("Lưu thông tin nghỉ phép thành công !") });
                }
                else
                {

                    if (!IsXetDuyet)
                        return Json(new { Status = 1, Message = DatabaseCache.GetText("Lưu thông tin nghỉ phép thành công !") });
                    else
                    {
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_NghiPhep(entity);
                        return Json(new { Status = 1, Message = DatabaseCache.GetText("Đã thêm yêu cầu nghỉ phép vào danh sách chờ phê duyệt !") });
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình lưu dữ liệu: ") + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetLeaveList()
        {
            var employee = DatabaseCache.NhanVien;
            LeaveInfo LI = new LeaveInfo(employee.NhanVienID);
            return Json(new
            {
                Status = 1,
                Data = LI.List.dsSource.Select().Select(x => new
                {
                    Id = x["QTNghiPhepID"],
                    StartDate = x["NgayBatDau"],
                    EndDate = x["NgayKetThuc"],
                    LeaveDays = x["SoNgayNghi"],
                    Reason = x["LyDo"],
                    Content = x["DienGiai"],
                    ApprovalStatus = x["XetDuyet"],
                    Approver = x["NguoiDangChoDuyet"],
                    Comment = x["YKienPheDuyet"]
                })
            });
        }

        [HttpPost]
        public ActionResult GetBusinessTripList()
        {
            var employee = DatabaseCache.NhanVien;
            MissionInfo LI = new MissionInfo(employee.NhanVienID);
            return Json(new
            {
                Status = 1,
                Data = LI.List.dsSource.Select().Select(x => new
                {
                    Id = x["QTCongTacID"],
                    StartDate = x["NgayBatDau"],
                    EndDate = x["NgayKetThuc"],
                    Reason = x["LyDo"],
                    Place = x["NoiCongTac"],
                    Content = x["CongViecCuThe"],
                    Approver = x["NguoiDangChoDuyet"],
                    Comment = x["YKienPheDuyet"]
                })
            });
        }

        [HttpPost]
        public ActionResult GetOTList()
        {
            var employee = DatabaseCache.NhanVien;
            OTInfo LI = new OTInfo(employee.NhanVienID);
            LI.LoadData();
            return Json(new
            {
                Status = 1,
                Data = (object)null,
                //Data = LI.OTList.Select(x => new
                //{
                //    Id = x.DangKyCongID,
                //    DayOfWeek = x.Thu,
                //    Date = x.NgayChamCong.ToShortDateString(),
                //    Details = x.InfoDetails,
                //    HtmlStatus = OOS.GHR.Master.Models.DataTableSource.GetHTMLStatus(x.XetDuyet),
                //    XetDuyet = x.XetDuyet,
                //    Approver = x.NguoiDangChoDuyet
                //})
            });
        }

        [HttpPost]
        public ActionResult DeleteLeave(int Id)
        {
            var employee = DatabaseCache.NhanVien;
            int nhanVienID = employee.NhanVienID;
            try
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.DeleteWithApprove(Id, nhanVienID, "");
                if (strErr != "")
                    return Json(new { mess = DatabaseCache.GetText(strErr) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mess = DatabaseCache.GetText("Xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = 1, Message = DatabaseCache.GetText("Xóa thông tin nghỉ phép thành công !") });
        }

        [HttpPost]
        public ActionResult DeleteBusinessTrip(int Id)
        {
            var employee = DatabaseCache.NhanVien;
            int nhanVienID = employee.NhanVienID;
            try
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTCongTac.DeleteWithApprove(Id, nhanVienID, "");
                if (strErr != "")
                    return Json(new { mess = DatabaseCache.GetText(strErr) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mess = DatabaseCache.GetText("Xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = 1, Message = DatabaseCache.GetText("Xóa thông tin công tác thành công !") });
        }

        [HttpPost]
        public ActionResult DeleteOT(int Id)
        {
            if (DatabaseCache.DangNhap.IsPortalAccount)
            {
                return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn không có quyền xóa đăng ký công !") });
            }

            if (Id > 0)
            {
                NS_TL_DangKyCong dkc = NS_TL_DangKyCong.GetNS_TL_DangKyCong(Id);

                //Kiểm tra ngày khóa công
                DateTime dtNgayKhoaCong = ConfigClass.CL_NGAYKHOACONGTHANG;
                if (dtNgayKhoaCong.Year > 2000)
                {
                    if (dtNgayKhoaCong > dkc.NgayChamCong)
                    {
                        return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu trước ngày khóa công !") });
                    }
                }

                if (OOS.GHR.BusinessAdapter.TienLuong.KhoaLuong.CheckKhoaCong(dkc.NgayChamCong.Value.Month, dkc.NgayChamCong.Value.Year, 0))
                    return Json(new { Status = 0, Message = DatabaseCache.GetText("Tháng chấm công đã khóa !") });

                if (dkc.XetDuyet == 1)
                {
                    //Kiểm tra cho phép Sửa / Xóa backdate với số ngày cho phép
                    int BackDateDay = ConfigClass.NS_CC_SONGAYLUISUACHAMCONG;
                    if (BackDateDay > 0)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            DateTime dt = dkc.NgayChamCong.Value.AddDays(i);
                            if (dt >= DateTime.Now)
                                break;

                            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                                BackDateDay++;
                        }

                        if (dkc.NgayChamCong.Value.AddDays(BackDateDay) < DateTime.Now)
                        {
                            return Json(new { Status = 0, Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu sau [" + BackDateDay.ToString() + "] ngày !") });
                        }
                    }
                    string Mota = OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.GetDangKyCa_Description(dkc, true);

                    OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog
                    (OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.CONG_LUONG,
                        OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen_MNV(dkc.NhanVienID), "Xóa đăng ký công backdate, diễn giải: " + Mota);

                    ///Nếu không định nghĩa Xóa đăng ký công -> Xóa luôn
                    if (OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.XD_Available(QUYTRINH_MALOAI.XD_XOALAMTHEMGIO))
                    {
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_HuyDangKyOT(dkc, Mota); //.AddXetDuyet_XoaDangKyCong
                        NS_TL_DangKyCong.SetXetDuyet((int)OOS.GHR.BusinessAdapter.XetDuyet.TrangThai_XetDuyet.CHOXOA, dkc.DangKyCongID);
                    }
                    else
                    {
                        NS_TL_DangKyCong.DeleteNS_TL_DangKyCong(Id);
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(Id, OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_DANGKYLAMTHEMGIO);
                        return Json(new { Status = 0, Message = DatabaseCache.GetText("Xóa thành công !") });
                    }

                    return Json(new { Status = 0, Message = DatabaseCache.GetText("Đã yêu cầu xóa đăng ký làm thêm, chờ xét duyệt !") });
                }
                else
                {
                    if (dkc.NgayChamCong <= DateTime.Now)
                        OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog
                        (OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.CONG_LUONG, "Xóa đăng ký công", OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.GetDangKyCa_Description(dkc, true));
                }

                NS_TL_DangKyCong.DeleteNS_TL_DangKyCong(Id);
                OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(Id, OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_DANGKYLAMTHEMGIO);
            }
            return Json(new { Status = 1, Message = DatabaseCache.GetText("Xóa thành công !") });
        }

        [HttpPost]
        public ActionResult GetDepartmentsList()
        {
            return Json(new
            {
                Status = 1,
                Data = PhongBanList.GetPhongBanList().Select(x => new
                {
                    Id = x.PhongBanID,
                    Name = x.Ten,
                    ParentId = x.PhongBanChaID
                })
            });
        }

        [HttpPost]
        public ActionResult GetMemberList(int DepartmentId, int PageIndex = 0, int PageSize = 10)
        {
            var memberList = Business.GetNhanVienList(DepartmentId, PageIndex, PageSize);
            return Json(new
            {
                Status = 1,
                Data = memberList.Select().Select(x => new
                {
                    Id = x["NhanVienID"],
                    Name = x["Ho"] + " " + x["HoTen"],
                    Email = x["Email"],
                    Phone = x["DienThoai"],
                    JobTitle = x["TenChucDanh"],
                    Picture = Business.AvatarToBinary(x["Anh"], x["HoTen"]?.ToString().FirstOrDefault() ?? 'O')
                })
            });
        }

        [HttpPost]
        public ActionResult GetEmployeeInfo(int EmployeeId)
        {
            var employee = NhanVien.GetNhanVien(EmployeeId);
            if (employee == null)
            {
                return Json((object)false);
            }
            return Json(new
            {
                FullName = employee.Ho + " " + employee.HoTen,
                JobTitle = NS_DsChucVu.GetTenChucVuByID(employee.ChucVuID),
                Phone = employee.DienThoai,
                Email = employee.Email,
                Skype = employee.Skype,
                Address = employee.DiaChi,
                Deparment = PhongBan.GetTenByID(employee.PhongBanID),
                Picture = Business.AvatarToBinary(employee.Anh, employee.HoTen[0])
            });
        }

        [HttpPost]
        public ActionResult SendGPS(string Latitude, string Longtitude, string Picture)
        {
            var user = DatabaseCache.DangNhap;
            var token = Request.Headers["Token"];
            var employee = DatabaseCache.LoginSessionContainer[token].NhanVien;

            string strFileName = "";
            if (Picture != "")
            {
                byte[] byt = Convert.FromBase64String(Picture);
                if (byt != null)
                    strFileName = OOS.GHR.FileManagement.SaveImageFile(byt, OOSDirectory.Images_Folder_Year);
            }

            NS_TL_CC_DuLieuChamCong DLCC = new NS_TL_CC_DuLieuChamCong();
            DLCC.ThoiGian = DateTime.Now;
            DLCC.NhapTay = true;
            DLCC.Latitude = Latitude;// decimal.Parse(Latitude);
            DLCC.Longtitude = Longtitude;// decimal.Parse(Longtitude);
            DLCC.MayChamCongID = -1; //GPS
            DLCC.NhanVienID = user.NhanVienID;
            DLCC.HoVaTen = user.ND_HoVaTen;
            DLCC.PhongBanID = employee.PhongBanID;
            DLCC.MaChamCong = employee.MaTheChamCong;
            DLCC.ImageURL = strFileName;
            DLCC.Do_Insert();

            return Json(new
            {
                Status = 1
            });
        }

        [HttpPost]
        public ActionResult GetPaySlipBak(int Month, int Year)
        {
            SortedList<int, ArrayList> lstChinhSua = new SortedList<int, ArrayList>();
            var nv = DatabaseCache.NhanVien;
            var dt = Business.LoadBangLuongNV
            (0, "", Month, Year, out lstChinhSua, "", "", nv.MaNhanVien);

            string contentStr = CConfig.GetConfig_ByCpyID("TL_PHIEULUONG_CONTENTID", DatabaseCache.NhanVien.CongTyID);

            int ContentID = Database.ToInt(contentStr);
            if (ContentID <= 0)
                ContentID = Database.ToInt(DatabaseBase.ExecScalar("SELECT TOP 1 EmailContentID FROM EmailContent WHERE ContentCode='" + contentStr + "'"));

            if (ContentID > 0 && dt.Rows.Count > 0)
            {
                string strContent = "";

                object o = DatabaseBase.ExecScalar("SELECT Content FROM EmailContent WHERE EmailContentID =" + ContentID + "");
                if (o == null) strContent = "";
                else
                    strContent = o.ToString();

                if (strContent != "")
                {
                    strContent = OOS.GHR.BusinessAdapter.Global.Email.GetHTMLString(dt.Rows[0], strContent);
                    var result = new { html = strContent };
                    return Json(new
                    {
                        Status = 1,
                        Content = result
                    });
                }
            }
            return Json(new
            {
                Status = 0,
                Content = DatabaseCache.GetText("Không có dữ liệu")
            });
        }

        [HttpPost]
        public ActionResult GetPaySlip(int Month, int Year)
        {
            SortedList<int, ArrayList> lstChinhSua = new SortedList<int, ArrayList>();
            var nv = DatabaseCache.NhanVien;
            var dt = OOS.GHR.Payroll.Models.PayrollModel.LoadBangLuongNV_Payslip(Month, Year, nv.NhanVienID);

            string contentStr = CConfig.GetConfig_ByCpyID("TL_PHIEULUONG_CONTENTID_MOBILE", DatabaseCache.NhanVien.CongTyID);

            int ContentID = Database.ToInt(contentStr);
            if (ContentID <= 0)
                ContentID = Database.ToInt(DatabaseBase.ExecScalar("SELECT TOP 1 EmailContentID FROM EmailContent WHERE ContentCode='" + contentStr + "'"));

            if (ContentID > 0 && dt.Rows.Count > 0)
            {
                string strContent = "";

                object o = DatabaseBase.ExecScalar("SELECT Content FROM EmailContent WHERE EmailContentID =" + ContentID + "");
                if (o == null) strContent = "";
                else
                    strContent = o.ToString();

                if (strContent != "")
                {
                    strContent = OOS.GHR.BusinessAdapter.Global.Email.GetHTMLString(dt.Rows[0], strContent);
                    var result = new { html = strContent };
                    return Json(new
                    {
                        Status = 1,
                        Content = result
                    });
                }
            }
            return Json(new
            {
                Status = 0,
                Content = DatabaseCache.GetText("Không có dữ liệu")
            });
        }

        [HttpPost]
        public ActionResult GetTimeSheet(int Month, int Year)
        {
            return Json(new
            {
                Status = 0,
                Content = DatabaseCache.GetText("Không có dữ liệu")
            });
        }

        [HttpPost]
        public ActionResult GetNotificationList()
        {
            return Json(new
            {
                Status = 1,
                Data = OOS.GHR.BusinessAdapter.Activity.GetActivity(DatabaseCache.DangNhap.NguoiDungID, 0, 0).Select(x => new
                {
                    Id = x.ActivityID,
                    Content = x.Content,
                    DateTime = x.DateTime,
                    Link = x.Link,
                    Time = x.Time
                })
            });
        }

        [HttpPost]
        public ActionResult GetWarningList()
        {
            return Json(new
            {
                Status = 1,
                Data = OOS.GHR.BusinessAdapter.HeThong.CanhBaoHeThong.LoadAllCanhBao().Select(x => new
                {
                    Id = x.ID,
                    Name = x.Name,
                    ResultCount = x.ResultCount,
                    URL = x.URL
                })
            });
        }
    }
}