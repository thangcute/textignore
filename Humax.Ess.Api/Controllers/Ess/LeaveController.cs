using Humax.Ess.Api.Models;
using Newtonsoft.Json;
using OOS.GHR;
using OOS.GHR.Framework;
using OOS.GHR.HRIS.Models;
using OOS.GHR.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OOS.GHR.Framework.Mvc;
using OOS.GHR.Framework.Controllers;
using OOS.GHR.Services.Services.Ess;

namespace Humax.Ess.Api.Controllers.Ess
{
    public class LeaveController : BaseApiController
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<object> Get()
        {
            var data = LeaveService.get();

            return new
            {
                Status = 1,
                Message = "NP",
                Data = data != null ? data.Result : null
            };
        }

        public async Task<object> Get(int id)
        {
            var data = LeaveService.get(id);

            return new
            {
                Status = 1,
                Message = "NP",
                Data = data != null ? data.Result : null
            };
        }

        /// <summary>
        /// Get: Leave Summary
        /// </summary>
        /// <returns></returns>
        [Route("api/Leave/GetLeaveSummary")]
        [HttpGet]
        public async Task<object> GetLeaveSummary()
        {
            var employeeid = DatabaseCache.NhanVien.NhanVienID;
            var o = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.GetThongTinNghiBu(employeeid);
            var api = new
            {
                Status = 1,
                Message = "",
                Data = new
                {
                    SoPhepTheoQuyDinh = Database.ToDecimal(o.Rows[0]["SoPhepTheoQuyDinh"]),
                    SoPhepChuyenTuNamTruoc = Database.ToDecimal(o.Rows[0]["SoPhepChuyenTuNamTruoc"]),
                    SoPhepDaNghi = Database.ToDecimal(o.Rows[0]["SoPhepDaNghi"]),
                    SoPhepConLai = Database.ToDecimal(o.Rows[0]["SoPhepConLai"]),
                    NghiBuDuocHuong = Database.ToDecimal(o.Rows[0]["NghiBuDuocHuong"]),
                    NghiBuTonNamTruoc = Database.ToDecimal(o.Rows[0]["NghiBuTonNamTruoc"]),
                    NghiBuDaSuDung = Database.ToDecimal(o.Rows[0]["NghiBuDaSuDung"]),
                    NghiBuConLai = Database.ToDecimal(o.Rows[0]["NghiBuConLai"])
                }
            };
            return api;
        }

        [Route("api/Leave/GetLeaveReasonList")]
        [HttpGet]
        public async Task<object> GetLeaveReasonList()
        {
            var data = await LeaveService.getReason();
            return new
            {
                Status = 1,
                Message = "",
                Data = data
                //Data = OOS.GHR.Master.Models.UtilityDatasource.KyHieuChamCongNghiList().Select().Select(x => new
                //{
                //    Id = x["KyHieuChamCongID"],
                //    Name = x["Ten"]
                //})
                //Data = NS_TL_KyHieuChamCongList.GetNS_TL_KyHieuChamCongListFromWhere
                //("MoTa like N'%nghỉ%' Or Mota like '%leave%'", null)
                //.Select(x => new
                //{
                //    Id = x.KyHieuChamCongID,
                //    Name = x.MoTa
                //})
            };
        }

        [Route("api/Leave/notify")]
        [HttpPost]
        public async Task<object> Notify([FromBody] DateTime NgayBatDau, DateTime NgayKetThuc, int KyHieuChamCongID, decimal SoNgayNghi)
        {
            var employee = DatabaseCache.NhanVien;

            SoNgayNghi = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.TinhNgayNghi(employee.NhanVienID, NgayBatDau, NgayKetThuc, true);

            string notify = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.GetNotify
            (employee.NhanVienID, NgayBatDau, NgayKetThuc, KyHieuChamCongID, SoNgayNghi, new SortedList(),
            out bool saved, out decimal sSoNgayNghi);

            return new
            {
                Status = 1,
                Message = "",
                Data = new
                {
                    AllowSave = saved,
                    SoNgayNghi = sSoNgayNghi,
                    Notify = notify
                }
            };
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<object> Post([FromBody] LeavePostModel model)
        {
            var entity = new NS_QTNghiPhep();
            NS_QTNghiPhep oldEntity = null;
            DateTime sDate;
            DateTime eDate;

            if (string.IsNullOrEmpty(model.NgayBatDau))
                return ReturnError("Chưa nhập ngày bắt đầu.");

            if (string.IsNullOrEmpty(model.NgayKetThuc))
                return ReturnError("Chưa nhập ngày kết thúc.");

            DateTime.TryParse(model.NgayBatDau, out sDate);
            DateTime.TryParse(model.NgayKetThuc, out eDate);

            sDate = sDate.ToString("dd/MM/yyyy HH:mm").ToDatetime("dd/MM/yyyy HH:mm");
            eDate = eDate.ToString("dd/MM/yyyy HH:mm").ToDatetime("dd/MM/yyyy HH:mm");

            //Nếu chỉnh sửa thông tin
            if (model.Id > 0)
            {
                entity = NS_QTNghiPhep.GetNS_QTNghiPhep(model.Id.GetValueOrDefault(0));
                if (entity.QTNghiPhepID > 0)
                    oldEntity = entity.Clone() as NS_QTNghiPhep;

                ////Nếu values>0 -> Chỉnh sửa dữ liệu | copy dữ liệu từ values-> entity
                if (model.Id > 0 && !string.IsNullOrEmpty(model.values) && model.values.Length > 0)
                {
                    JsonConvert.PopulateObject(model.values, entity);
                }
                else
                {
                    model.CopyPropertiesTo(entity);
                }
                ////////////////////////////////////////////////////////////////////////////////
            }

            if (eDate <= sDate)
                return ReturnError("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");

            var employee = DatabaseCache.NhanVien;
            if (model.KyHieuChamCongID <= 0 && !ConfigClass.HSNS_NGHIPHEP_KHONGCANLYDO)
                return ReturnError("Chưa chọn lý do nghỉ phép !");

            if (OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.CheckTrungNghiPhep(sDate, eDate, employee.NhanVienID, entity.QTNghiPhepID) > 0)
                return ReturnError("Đăng ký nghỉ phép trùng ngày !");

            if(string.IsNullOrEmpty(model.DienGiai))
                return ReturnError("Nội dung Diễn giải không để trống !");

            if (model.DienGiai.Length > 350)
                return ReturnError("Diễn giải không vượt quá 350 ký tự !");

            try
            {
                entity.KyHieuChamCongID = model.KyHieuChamCongID;
                entity.SoNgayNghi = model.SoNgayNghi;
                entity.LyDoNghi = model.DienGiai == null ? "" : model.DienGiai; // model.LyDoNghi;
                entity.Des = model.DienGiai;

                entity.NgayBatDau = sDate;
                entity.NgayKetThuc = eDate;

                //Địa điểm nghỉ -> bỏ luôn
                //entity.Des = model.DiaDiem == null ? "" : model.DiaDiem;

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
                    return ReturnSuccess(string.Format("{0} thông tin nghỉ phép thành công !", model.Id > 0 ? "Cập nhật" : "Lưu"));
                }
                else
                {

                    if (!IsXetDuyet)
                        return ReturnSuccess(string.Format("{0} thông tin nghỉ phép thành công !", model.Id > 0 ? "Cập nhật" : "Lưu"));
                    else
                    {
                        OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_NghiPhep(entity);
                        return ReturnSuccess(string.Format("Đã {0} yêu cầu nghỉ phép vào danh sách chờ phê duyệt !", model.Id > 0 ? "Cập nhật" : "Thêm"));
                    }
                }

            }
            catch (Exception ex)
            {
                return ReturnError("Xảy ra lỗi trong quá trình lưu dữ liệu: " + ex.Message);
            }
        }

        /// <summary>
        /// Hàm thực hiện update 
        /// </summary>
        /// <param name="model"></param>
        private void UpdateLeave(LeavePostModel model)
        {
            if (model.Id> 0 && !string.IsNullOrEmpty(model.values) && model.values.Length>0)
            {

            }    
        }


        // DELETE api/<controller>/5
        //[HttpDelete]
        public async Task<object> Delete(int id)
        {
            var employee = DatabaseCache.NhanVien;
            int nhanVienID = employee.NhanVienID;
            try
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.DeleteWithApprove(id, nhanVienID, "");
                if (strErr != "" && strErr != "Hủy đăng ký thành công !")
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText(strErr)
                    };
            }
            catch (Exception ex)
            {
                return new
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message)
                };
            }

            return new
            {
                Status = 1,
                Message = DatabaseCache.GetText("Xóa thông tin nghỉ phép thành công !")
            };
        }

        [Route("api/Leave/cancel")]
        [HttpPost]
        public async Task<object> Cancel(int id, string reason)
        {
            var employee = DatabaseCache.NhanVien;

            int nhanVienID = employee.NhanVienID;
            try
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.DeleteWithApprove(id, nhanVienID, "");

                if (strErr != "" && strErr != "Hủy đăng ký thành công !")
                    return new
                    {
                        Status = 0,
                        Message = DatabaseCache.GetText(strErr)
                    };
            }
            catch (Exception ex)
            {
                return new
                {
                    Status = 0,
                    Message = DatabaseCache.GetText("Xảy ra lỗi trong quá trình xóa dữ liệu: " + ex.Message)
                };
            }

            return new
            {
                Status = 1,
                Message = DatabaseCache.GetText("Hủy thông tin nghỉ phép thành công !")
            };
        }

        [HttpPost]
        [Route("api/Leave/CheckLeave")]
        public async Task<object> CheckLeave([FromBody] CheckLeaveModel model)
        {
            int nhanVienID = DatabaseCache.NhanVien.NhanVienID;
            if(model.QTNghiPhepID < 1)
            {
                if (OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.CheckTrungNghiPhep(model.StartDateTime.Value, model.EndDateTime.Value, nhanVienID, model.QTNghiPhepID.Value) > 0)
                    return new
                    {
                        Status = 0,
                        Message = "Check Leave",
                        Data = "Đăng ký nghỉ phép trùng ngày"
                    };
            }

            if (!string.IsNullOrEmpty(model.SoQuyetDinh))
            {
                if (OOS.GHR.BusinessAdapter.HSNhanSu.QLQuyetDinh.CheckTrungQuyetDinh(model.SoQuyetDinh, NS_QTNghiPhep.Table_Name, "QTNghiPhepID", model.QTNghiPhepID.Value, "SoQuyetDinh"))
                    return new
                    {
                        Status = 0,
                        Message = "Check Leave",
                        Data = "Trúng số quyết định"
                    };
            }

            return new
            {
                Status = 1,
                Message = "Check Leave",
                Data = ""
            };
        }

        [HttpPost]
        [Route("api/Leave/CheckNumber")]
        public async Task<object> CheckNumber([FromBody] CheckLeaveModel model)
        {
            int nhanVienID = DatabaseCache.NhanVien.NhanVienID;
            SortedList sortedList = new SortedList();
            //string notify = "";
            //bool allowsave = true;
            decimal num2 = 0m;

            num2 = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.TinhNgayNghi(nhanVienID, model.StartDateTime.Value, model.EndDateTime.Value, true);
            //decimal num3 = 0m;
            //notify = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.GetNotify(nhanVienID, model.StartDateTime.Value, model.EndDateTime.Value, model.KyHieuChamCongID.Value, num2, sortedList, out allowsave, out num3);
            //num2 = num3;

            return new
            {
                Status = 1,
                Message = "Check Number",
                Data = num2
            };
        }
    }
}