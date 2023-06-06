using Humax.Ess.Api.Models;
using OOS.GHR.Framework;
using OOS.GHR.HRIS.Models;
using OOS.GHR.Library;
using OOS.GHR.Payroll.Models;
using OOS.GHR.BusinessAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.Services.Utilitys;
using OOS.GHR.Services.Services.Ess;
using OOS.Humax.ESSService.OT;
using OOS.GHR.BusinessAdapter.CongLuong.Models;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class OvertimeController : BaseApiController
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<object> Get(string MonthYear = "")
        {
            if (string.IsNullOrEmpty(MonthYear))
                MonthYear = DateTime.Now.ToString("yyyy-MM");

            var data = OvertimeService.get(MonthYear);

            return new
            {
                Status = 1,
                Message = "OT",
                Data = data != null ? data.Result : null
            };
        }

        public async Task<object> Get(int id)
        {
            var data = OvertimeService.get(id);

            return new
            {
                Status = 1,
                Message = "OT",
                Data = data != null ? data.Result : null
            };
        }

        //public async Task<object> Get()
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    OTInfo LI = new OTInfo(employee.NhanVienID);
        //    LI.LoadData();

        //    return new
        //    {
        //        Status = 1,
        //        Message = "",
        //        Data = LI.OTList.Select(x => new
        //        {
        //            Id = x.DangKyCongID,
        //            NgayDangKy = x.NgayChamCong,
        //            CaLamViec = x.CaLV,
        //            //CaLamViecID = 0,
        //            KyHieuChamCongID = x.KyHieuCC,
        //            SoGioLamThemTruocCa = x.GioLamThemTruocCa,
        //            SoGioLamThemSauCa = x.GioLamThem,
        //            BDLamThemTruocCa = x.BDLamThemTruocCa,
        //            KTLamThemTruocCa = x.KTLamThemTruocCa,
        //            BDLamThemSauCa = x.BDLamThemSauCa,
        //            KTLamThemSauCa = x.KTLamThemSauCa,
        //            LyDoTangCa = x.LyDoTangCa,
        //            //LyDoTangCaID = 0,
        //            XetDuyet = x.XetDuyet,
        //            Approver = x.NguoiDangChoDuyet,
        //            //ApprovedDate = (object)null,
        //            ApproveComment = x.YKienPheDuyet
        //        })
        //    };
        //}

        //public async Task<object> Get(int id)
        //{
        //    var employee = DatabaseCache.NhanVien;
        //    OTInfoModel detail = new OTInfoModel();
        //    NS_TL_DangKyCong dkc = OOS.GHR.Library.NS_TL_DangKyCong.GetNS_TL_DangKyCong(id);
        //    return new ApiResponse()
        //    {
        //        Status = 1,
        //        Message = "",
        //        Data = new
        //        {
        //            Id = dkc.DangKyCongID,
        //            NgayDangKy = dkc.NgayChamCong,
        //            //EmployeeId = dkc.NhanVienID,
        //            CaLamViecID = dkc.CaLamViecID,
        //            KyHieuChamCongID = dkc.KyHieuChamCongID,
        //            SoGioLamThemTruocCa = dkc.GioLamThemTruocCa,
        //            SoGioLamThemSauCa = dkc.GioLamThem,
        //            BDLamThemTruocCa = new DateTime(2000, 1, 1, dkc.BDLamThemTruocCa.Hours, dkc.BDLamThemTruocCa.Minutes, dkc.BDLamThemTruocCa.Seconds),
        //            KTLamThemTruocCa = new DateTime(2000, 1, 1, dkc.KTLamThemTruocCa.Hours, dkc.KTLamThemTruocCa.Minutes, dkc.KTLamThemTruocCa.Seconds),
        //            BDLamThemSauCa = new DateTime(2000, 1, 1, dkc.BDLamThemSauCa.Hours, dkc.BDLamThemSauCa.Minutes, dkc.BDLamThemSauCa.Seconds),
        //            KTLamThemSauCa = new DateTime(2000, 1, 1, dkc.KTLamThemSauCa.Hours, dkc.KTLamThemSauCa.Minutes, dkc.KTLamThemSauCa.Seconds),
        //            LyDoTangCaID = dkc.LyDoTangCaID,
        //            LyDoTangCa = dkc.LyDoTangCa,
        //            AnTangCa = dkc.AnTangCa,
        //            IsMoiTruongDacBiet = dkc.IsMoiTruongDacBiet,
        //            XetDuyet = dkc.XetDuyet,
        //            //Approver = dkc.,
        //            //ApprovedDate
        //            ApproveComment = dkc.YKienXetDuyet
        //        }
        //    };
        //}

        [Route("api/Overtime/GetOTReasonList")]
        [HttpGet]
        public async Task<object> GetOTReasonList()
        {
            return new
            {
                Status = 1,
                Message = "",
                Data = NS_DsLyDoTangCaList.GetNS_DsLyDoTangCaList().Select(x => new
                {
                    Id = x.LyDoTangCaID,
                    Name = x.TenLyDoTangCa
                })
            };
        }

        [Route("api/Overtime/GetOTWorkShift")]
        [HttpGet]
        public async Task<object> GetOTWorkShift(DateTime? date = null)
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            int CaLamViecId = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetCaLamViec(nhanvienId, date ?? DateTime.Now);

            var data = OvertimeService.getWorkShift(CaLamViecId);

            return new
            {
                Status = 1,
                Message = "",
                Data = data.Result
            };
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<object> Post([FromBody] OtPostModel model)
        {
            int NhanVienID = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;

            if (!string.IsNullOrEmpty(model.TuGio) || !string.IsNullOrEmpty(model.ToiGio))
            {
                OTRegModelBase OTModel = new OTRegModelBase();
                OTModel.NhanVienID = NhanVienID;
                OTModel.NgayDangKy = model.NgayDangKy;
                OTModel.LyDoTangCa = model.LyDoTangCa;
                OTModel.LyDoTangCaID = model.LyDoTangCaID??0;
                OTModel.BatDauLamThem = DateTime.Parse(model.NgayDangKy.ToString("yyyy/MM/dd") + " " + model.TuGio);
                OTModel.KetThucLamThem = DateTime.Parse(model.NgayDangKy.ToString("yyyy/MM/dd") + " " + model.ToiGio);

                string strValidate = OTModel.Validate(DatabaseCache.NhanVien.CongTyID);
                if (strValidate!="")
                {
                    return new
                    {
                        Status = 0,
                        Message = strValidate,
                        Data = (object)null
                    };
                }

                string strErr = OOS.GHR.BusinessAdapter.SSService.GetNotify_OT(OTModel.NhanVienID, OTModel.CaLamViecID, OTModel.NgayDangKy,
                0,
                OTModel.BatDauLamThem.TimeOfDay,
                OTModel.KetThucLamThem.TimeOfDay,
                0,
                new TimeSpan(),
                new TimeSpan(),
                OTModel.LyDoTangCaID,
                OTModel.LyDoTangCa, out bool Saved);

                if (!Saved)
                {
                    return new
                    {
                        Status = 0,
                        Message = strErr,
                        Data = (object)null
                    };
                }


                OTService.DangKyOTPortal(OTModel);

                return new
                {
                    Status = 1,
                    Message = DatabaseCache.GetText("Đăng ký OT thành công, chờ phê duyệt!"),
                    Data = (object)null
                };
            }
            else
            if (string.IsNullOrEmpty(model.TuGio) || string.IsNullOrEmpty(model.ToiGio))
            {
                try
                {
                    //Kiểm tra cho phép Sửa / Xóa backdate với số ngày cho phép
                    int BackDateDay = ConfigClass.NS_CC_SONGAYLUISUACHAMCONG;
                    /////////////////////////////////////////////////////////////////////////
                    ///
                    TimeSpan BDLamThemTruocCa = new TimeSpan();
                    TimeSpan KTLamThemTruocCa = new TimeSpan();

                    TimeSpan BDLamThemSauCa = new TimeSpan();
                    TimeSpan KTLamThemSauCa = new TimeSpan();
                    if (!string.IsNullOrEmpty(model.BDLamThemTruocCa) || !string.IsNullOrEmpty(model.KTLamThemTruocCa))
                    {
                        if (string.IsNullOrEmpty(model.BDLamThemTruocCa) || string.IsNullOrEmpty(model.KTLamThemTruocCa))
                        {
                            return new
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Thông tin trước ca không đủ"),
                                Data = (object)null
                            };
                        }

                        var spl1 = model.BDLamThemTruocCa.Split(':');
                        var spl2 = model.KTLamThemTruocCa.Split(':');

                        BDLamThemTruocCa = new TimeSpan(spl1[0].ToInt(), spl1[1].ToInt(), 0);
                        KTLamThemTruocCa = new TimeSpan(spl2[0].ToInt(), spl2[1].ToInt(), 0);

                        if (BDLamThemTruocCa > KTLamThemTruocCa)
                        {
                            return new
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Giờ bắt đầu phải phải hơn kết thúc !"),
                                Data = (object)null
                            };
                        }
                    }

                    if (!string.IsNullOrEmpty(model.BDLamThemSauCa) || !string.IsNullOrEmpty(model.KTLamThemSauCa))
                    {
                        if (string.IsNullOrEmpty(model.BDLamThemSauCa) || string.IsNullOrEmpty(model.KTLamThemSauCa))
                        {
                            return new
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Thông tin sau ca không đủ"),
                                Data = (object)null
                            };
                        }

                        var spl3 = model.BDLamThemSauCa.Split(':');
                        var spl4 = model.KTLamThemSauCa.Split(':');

                        BDLamThemSauCa = new TimeSpan(spl3[0].ToInt(), spl3[1].ToInt(), 0);
                        KTLamThemSauCa = new TimeSpan(spl4[0].ToInt(), spl4[1].ToInt(), 0);

                        if (BDLamThemSauCa > KTLamThemSauCa)
                        {
                            return new
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Giờ bắt đầu phải phải hơn kết thúc !"),
                                Data = (object)null
                            };
                        }
                    }
                    var user = DatabaseCache.DangNhap;


                    if (model.CaLamViecID == 0)
                    {
                        return new
                        {
                            Status = 0,
                            Message = DatabaseCache.GetText("Bạn phải chọn ca làm việc, hoặc lý do !"),
                            Data = (object)null
                        };
                    }
                    var sDate = model.NgayDangKy;
                    if (sDate < DateTime.Now.Date.AddDays(BackDateDay * -1))
                    {
                        return new
                        {
                            Status = 0,
                            Message = string.Format(DatabaseCache.GetText("Không cho phép Backdate quá [{0}] Ngày !"), BackDateDay),
                            Data = (object)null
                        };
                    }

                    if (model.LyDoTangCaID <= 0)
                    {
                        return new
                        {
                            Status = 0,
                            Message = DatabaseCache.GetText("Bạn phải chọn lý do tăng ca !"),
                            Data = (object)null
                        };
                    }

                    //Kiểm tra ngày khóa công
                    DateTime dtNgayKhoaCong = ConfigClass.CL_NGAYKHOACONGTHANG;
                    if (dtNgayKhoaCong.Year > 2000)
                    {
                        if (dtNgayKhoaCong > sDate || dtNgayKhoaCong > sDate)
                        {
                            return new
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu trước ngày khóa công !"),
                                Data = (object)null
                            };
                        }
                    }

                    string strErr = OOS.GHR.BusinessAdapter.SSService.GetNotify_OT(NhanVienID, (int)model.CaLamViecID, sDate,
                    (decimal)model.SoGioLamThemTruocCa,
                    BDLamThemTruocCa,
                    KTLamThemTruocCa,
                    (decimal)model.SoGioLamThemSauCa,
                    BDLamThemSauCa,
                    KTLamThemSauCa,
                    (int)model.LyDoTangCaID,
                    model.LyDoTangCa, out bool Saved);

                    if (!Saved)
                        return new
                        {
                            Status = 0,
                            Message = DatabaseCache.GetText(strErr),
                            Data = (object)null
                        };

                    var _model = new OTInfoModel
                    {
                        DangKyCongID = model.Id.GetValueOrDefault(0),
                        NhanVienID = NhanVienID,
                        TuNgay = sDate,
                        ToiNgay = sDate,
                        CaLamViecID = (int)model.CaLamViecID,
                        GioLamThemTruocCa = (decimal)model.SoGioLamThemTruocCa,
                        BDLamThemTruocCa = DateTime.Today.Add(BDLamThemTruocCa),
                        KTLamThemTruocCa = DateTime.Today.Add(KTLamThemTruocCa),
                        GioLamThem = (decimal)model.SoGioLamThemSauCa,
                        BDLamThemSauCa = DateTime.Today.Add(BDLamThemSauCa),
                        KTLamThemSauCa = DateTime.Today.Add(KTLamThemSauCa),
                        LyDoTangCaID = model.LyDoTangCaID.GetValueOrDefault(0),
                        LyDoTangCa = model.LyDoTangCa,
                        IsMoiTruongDacBiet = model.IsMoiTruongDacBiet.GetValueOrDefault(false),
                        AnTangCa = model.AnTangCa.GetValueOrDefault(false)
                    };
                    OTInfoModel.SaveDangKyCa(_model, BackDateDay, true);
                    string msg = "";
                    if (string.IsNullOrEmpty(msg))
                    {
                        return new
                        {
                            Status = 1,
                            Message = model.Id.GetValueOrDefault(0) > 0 ? DatabaseCache.GetText("Cập nhật OT thành công!") : DatabaseCache.GetText("Đăng ký OT thành công !"),
                            Data = (object)null
                        };
                    }
                    else
                    {
                        return new
                        {
                            Status = 0,
                            Message = msg,
                            Data = (object)null
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new
                    {
                        Status = 0,
                        Message = model.Id.GetValueOrDefault(0) > 0 ? DatabaseCache.GetText("Lỗi Cập nhật OT !") : DatabaseCache.GetText("Lỗi đăng ký OT !"),
                        Data = (object)null
                    };
                }
            }
            return null;
        }

        // DELETE api/<controller>/5
        public async Task<object> Delete(int id)
        {
            if (id > 0)
            {
                NS_TL_DangKyCong dkc = NS_TL_DangKyCong.GetNS_TL_DangKyCong(id);
                //Kiểm tra ngày khóa công
                DateTime dtNgayKhoaCong = ConfigClass.CL_NGAYKHOACONGTHANG;
                if (dtNgayKhoaCong.Year > 2000)
                {
                    if (dtNgayKhoaCong > dkc.NgayChamCong)
                    {
                        return new
                        {
                            Status = 0,
                            Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu trước ngày khóa công !"),
                            Data = (object)null
                        };
                    }
                }

                if (OOS.GHR.BusinessAdapter.TienLuong.KhoaLuong.CheckKhoaCong(dkc.NgayChamCong.Value.Month, dkc.NgayChamCong.Value.Year, 0))
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText("Tháng chấm công đã khóa !"),
                        Data = (object)null
                    };

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
                            return new ApiResponse()
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu sau [" + BackDateDay.ToString() + "] ngày !"),
                                Data = (object)null
                            };
                        }
                    }
                    string Mota = OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.GetDangKyCa_Description(dkc, true);

                    OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog
                    (OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.CONG_LUONG,
                        OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen_MNV(dkc.NhanVienID), "Xóa đăng ký công backdate, diễn giải: " + Mota);

                    ///Nếu không định nghĩa Xóa đăng ký công -> Xóa luôn
                    if (OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.XD_Available(QUYTRINH_MALOAI.XD_XOALAMTHEMGIO))
                    {
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_HuyDangKyOT(dkc, Mota);//.AddXetDuyet_XoaDangKyCong
                        NS_TL_DangKyCong.SetXetDuyet((int)OOS.GHR.BusinessAdapter.XetDuyet.TrangThai_XetDuyet.CHOXOA, dkc.DangKyCongID);
                    }
                    else
                    {
                        NS_TL_DangKyCong.DeleteNS_TL_DangKyCong(id);
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(id, OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_DANGKYLAMTHEMGIO);
                        return new
                        {
                            Status = 1,
                            Message = DatabaseCache.GetText("Xóa thành công !"),
                            Data = (object)null
                        };
                    }
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText("Đã yêu cầu xóa đăng ký làm thêm, chờ xét duyệt !"),
                        Data = (object)null
                    };
                }
                else
                {
                    if (dkc.NgayChamCong <= DateTime.Now)
                        OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog
                        (OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.CONG_LUONG, "Xóa đăng ký công", OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.GetDangKyCa_Description(dkc, true));
                }

                NS_TL_DangKyCong.DeleteNS_TL_DangKyCong(id);
                OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(id, OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_DANGKYLAMTHEMGIO);
            }
            return new
            {
                Status = 1,
                Message = DatabaseCache.GetText("Xóa thành công !"),
                Data = (object)null
            };
        }

        [Route("api/Overtime/cancel")]
        [HttpPost]
        public async Task<object> Cancel(int id, string reason) {
            if (id > 0)
            {
                NS_TL_DangKyCong dkc = NS_TL_DangKyCong.GetNS_TL_DangKyCong(id);
                //Kiểm tra ngày khóa công
                //DateTime dtNgayKhoaCong = ConfigClass.CL_NGAYKHOACONGTHANG;
                //if (dtNgayKhoaCong.Year > 2000)
                //{
                //    if (dtNgayKhoaCong > dkc.NgayChamCong)
                //    {
                //        return new
                //        {
                //            Status = 0,
                //            Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu trước ngày khóa công !"),
                //            Data = (object)null
                //        };
                //    }
                //}

                if (OOS.GHR.BusinessAdapter.TienLuong.KhoaLuong.CheckKhoaCong(dkc.NgayChamCong.Value.Month, dkc.NgayChamCong.Value.Year, 0))
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText("Tháng chấm công đã khóa !"),
                        Data = (object)null
                    };

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
                            return new ApiResponse()
                            {
                                Status = 0,
                                Message = DatabaseCache.GetText("Bạn không điều chỉnh dữ liệu sau [" + BackDateDay.ToString() + "] ngày !"),
                                Data = (object)null
                            };
                        }
                    }
                    string Mota = OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.GetDangKyCa_Description(dkc, true);

                    OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog
                    (OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.CONG_LUONG,
                        OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen_MNV(dkc.NhanVienID), "Hủy đăng ký công backdate, diễn giải: " + Mota);

                    ///Nếu không định nghĩa Xóa đăng ký công -> Xóa luôn
                    if (OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.XD_Available(QUYTRINH_MALOAI.XD_XOALAMTHEMGIO))
                    {
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_HuyDangKyOT(dkc, Mota);//.AddXetDuyet_XoaDangKyCong
                        NS_TL_DangKyCong.SetXetDuyet((int)OOS.GHR.BusinessAdapter.XetDuyet.TrangThai_XetDuyet.CHOXOA, dkc.DangKyCongID);
                    }
                    else
                    {
                        NS_TL_DangKyCong.DeleteNS_TL_DangKyCong(id);
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(id, OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_DANGKYLAMTHEMGIO);
                        return new
                        {
                            Status = 1,
                            Message = DatabaseCache.GetText("Hủy thành công !"),
                            Data = (object)null
                        };
                    }
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText("Đã yêu cầu Hủy đăng ký làm thêm, chờ xét duyệt !"),
                        Data = (object)null
                    };
                }
                else
                {
                    if (dkc.NgayChamCong <= DateTime.Now)
                        OOS.GHR.BusinessAdapter.Log.LogManager.LManager.AddNewLog
                        (OOS.GHR.BusinessAdapter.Log.ENUM_PHANHE.CONG_LUONG, "Hủy đăng ký công", OOS.GHR.BusinessAdapter.ChamCong.DangKyCong.GetDangKyCa_Description(dkc, true));
                }

                NS_TL_DangKyCong.DeleteNS_TL_DangKyCong(id);
                OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(id, OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_DANGKYLAMTHEMGIO);
            }

            return new
            {
                Status = 1,
                Message = DatabaseCache.GetText("Hủy thành công !"),
                Data = (object)null
            };
        }
    }
}