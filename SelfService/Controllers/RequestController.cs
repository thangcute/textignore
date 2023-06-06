using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOS.GHR.SelfService.Models;
using OOS.GHR.Library;
using OOS.GHR.Framework;
using System.Collections;
using System.Data;
using OOS.GHR.BusinessAdapter;

namespace OOS.GHR.SelfService.Controllers
{
    public class RequestController : OOS.GHR.Framework.Controllers.BaseController
    {
        [OOSAuthorization(Code = "PORTAL_Request")]
        public ActionResult Index()
        {
            return View();
        }

        [OOSAuthorization(Code = "PORTAL_Request")]
        public ActionResult RequestManagement()
        {
            int NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            RequestInfo LI = new RequestInfo(NhanVienID);
            LI.LoadData(NhanVienID);

            return PartialView("_Request", LI);
        }

        [OOSAuthorization(Code = "PORTAL_Request")]
        public ActionResult RequestInfo(int NhanVienID, int DeXuatID)
        {
            RequestModel pm = new RequestModel();
            pm.DeXuatID = DeXuatID;
            pm.NgayDeXuat = DateTime.Now;

            if (DeXuatID > 0)
            {
                DX_DeXuat dx = DX_DeXuat.GetDX_DeXuat(DeXuatID);
                pm = dx.ToModel<RequestModel, DX_DeXuat>(pm);
            }
            pm.cm = new Admin.Models.CommentsModel(DeXuatID, "DX_DEXUAT");

            //Loadcontrols - Load cả dữ liệu
            pm.LoadControls(DeXuatID>0);

            return PartialView("_RequestInfo", pm);
        }

        [OOSAuthorization(Code = "PORTAL_Request")]
        public ActionResult RequestField(int LoaiDeXuatID)
        {
            RequestModel rm = new RequestModel();
            rm.LoaiDeXuatID = LoaiDeXuatID;

            //Chỉ cần load controls
            rm.LoadControls(false);

            return PartialView("_RequestField", rm.Controls.alControls);
        }

        [OOSAuthorization(Code = "PORTAL_Request")]
        public JsonResult AddAndEdit(RequestModel model)
        {
            if (model.LoaiDeXuatID <= 0)
                return ReturnErrorMessage(Translate("Bạn chưa chọn loại đề xuất !"));

            if (string.IsNullOrEmpty(model.TieuDe))
                return ReturnErrorMessage(Translate("Tiêu đề không được để trống !"));

            /////////////////////////////////// LẤY TRƯỜNG THÔNG TIN /////////////////////////////
            SortedList slParams = new SortedList();
            slParams.Add("@LoaiDeXuatID", model.LoaiDeXuatID);
            slParams.Add("@NguoiDungID", DatabaseCache.DangNhap.NguoiDungID);
            slParams.Add("@DeXuatID", model.DeXuatID);
            slParams.Add("@NgayDeXuat", model.NgayDeXuat);

            string TenLoaiDeXuat = DX_LoaiDeXuat.GetTenLoaiDeXuatByID(model.LoaiDeXuatID);

            DX_DeXuat_DuLieuDeXuatList ll = new DX_DeXuat_DuLieuDeXuatList();

            EmailContentBulder ECB = new EmailContentBulder();
            ECB.Caption = TenLoaiDeXuat + " - " + model.TieuDe;
            ECB.CaptionInfomation = Translate("Thông tin đề xuất");

            string NoiDung_ThuGon = TenLoaiDeXuat + " - " + model.TieuDe;
            DX_LoaiDeXuat_TruongDuLieuList ttList = DX_LoaiDeXuat_TruongDuLieuList.GetDX_LoaiDeXuat_TruongDuLieuListFromWhere("LoaiDeXuatID=" + model.LoaiDeXuatID, null);
            foreach (DX_LoaiDeXuat_TruongDuLieu tt in ttList)
            {
                string FieldName = "RF_" + tt.TruongDuLieuID.ToString();

                object oValue = Framework.DynamicUI.DevExtension.GetDevValueDType(FieldName, tt.KieuDuLieu, tt.ControlType);

                string value = oValue != null ? oValue.ToString() : "";
                string txtvalue = "";

                //Kiểm tra dữ liệu: Validate
                if (!tt.AllowNull)
                {
                    int ID = Database.ToInt(value);
                    if (value.Length <= 0 || oValue == null || ID < 0 
                    || (ID==0 && OOS.GHR.Framework.DynamicUI.ControlModel.IsNumberType(tt.KieuDuLieu, tt.ControlType)))
                    {
                        return ReturnErrorMessage(Translate(tt.TenTruongDuLieu + ": Không để trống !"));
                    }
                }

                string ct = tt.ControlType.ToLower();
                if ((ct == "multiselect") || ct.Contains("list") || (ct == "combobox") || (ct == "treeid"))
                    txtvalue = Database.toString(Request.Form[FieldName]);
                else
                    txtvalue = OOS.GHR.Framework.UI.UIFormat.GetDisplayValue(ct, value, null, "", "");

                DX_DeXuat_DuLieuDeXuat data = new DX_DeXuat_DuLieuDeXuat();
                data.TruongDuLieuID = tt.TruongDuLieuID;
                data.GiaTri = value;
                data.TenTruongDuLieu = tt.TenTruongDuLieu;

                if (tt.KieuDuLieu == "DateTime")
                {
                    data.GiaTriDateTime = Database.ConvertToDateTime(oValue);
                    txtvalue = txtvalue.Replace("00:00:00", "");

                    if (data.GiaTriDateTime.HasValue)
                        slParams.Add(FieldName, data.GiaTriDateTime);
                    else
                        slParams.Add(FieldName, DBNull.Value);
                }
                else
                    slParams.Add(FieldName, oValue);

                if (tt.LayThongTinMoTa)
                {
                    if (txtvalue != "" && Database.toString(value) == "")
                        txtvalue = "";

                    if (txtvalue!="")
                    {
                        NoiDung_ThuGon += " - " + tt.TenTruongDuLieu + ": " + txtvalue;

                        ECB.Values.Add(new KeyValuePair<string, string>(tt.TenTruongDuLieu, txtvalue));
                    }
                }

                data.GiaTriBinary = null;
                ll.Add(data);
            }
            /////////////////////////////////////////////////////////////

            ///////////////////// THỰC HIỆN KIỂM TRA DỮ LIỆU //////////////////////////////

            string strResponse = Translate("Lưu thông tin đề xuất thành công !");
            try
            {
                string MaLoai = DX_LoaiDeXuat.GetMaLoaiDeXuatByID(model.LoaiDeXuatID);
                DataTable dt = Database.ExecTable("ESS_REQUEST_" + MaLoai.ToUpper(), slParams, true);
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 1)
                {
                    bool Saved = (bool)dt.Rows[0][1];

                    string ResponseStr = dt.Rows[0][0].ToString();

                    if (!Saved)
                        return ReturnErrorMessage(ResponseStr);
                    else
                        strResponse = ResponseStr;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ///////////////////////////////////////////////////////////////////////////////

            DX_DeXuat entity = new DX_DeXuat();
            if (model.DeXuatID > 0)
                entity = DX_DeXuat.GetDX_DeXuat(model.DeXuatID);

            entity = model.ToEntity<DX_DeXuat, RequestModel>(entity);

            entity.NoiDung_ThuGon = NoiDung_ThuGon;
            entity.NguoiDeXuatID = DatabaseCache.DangNhap.NhanVienID;
            entity.IsNew = entity.DeXuatID <= 0;
            entity.IsDirty = true;
            entity.Save();

            //Lưu các Trường thông tin
            if (model.DeXuatID>0)
                OOS.GHR.BusinessAdapter.Request.RequestService.DeleteFieldsData(model.DeXuatID);

            foreach(DX_DeXuat_DuLieuDeXuat data in ll)
            {
                data.LoaiDeXuatID = entity.LoaiDeXuatID;
                data.DeXuatID = entity.DeXuatID;
                data.ApplyEdit();
                data.Do_Insert();
            }

            string htmlEmail = ECB.ToString();
            if (model.QuyTrinhID > 0)
            {
                BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet
                (model.QuyTrinhID, 0, new string[] { "@ID" }, new int[] { entity.DeXuatID }, NoiDung_ThuGon, htmlEmail, false, "DX_DEXUAT");
            }
            else
            {
                DX_LoaiDeXuat ldx = DX_LoaiDeXuat.GetDX_LoaiDeXuat(model.LoaiDeXuatID);
                if (ldx.XetDuyetCode != "")
                {
                    BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet
                    (ldx.XetDuyetCode,0, "@ID", entity.DeXuatID, NoiDung_ThuGon, htmlEmail, "DX_DEXUAT");
                }
            }
            #region Save File Store
            string[] fileUploads = FileManagement.GetAllFiles("DX_DeXuat_FileStore");
            foreach (string file in fileUploads)
            {
                OOS.GHR.FileManagement.Move_And_Add_File(file, "DX_DEXUAT", entity.DeXuatID);
            }
            #endregion

            return ReturnErrorMessage(Translate(strResponse), 0);
        }

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id">ID đối tượng xóa</param>
        /// <returns></returns>
        [OOSAuthorization(Code = "PORTAL_Request")]
        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                foreach (var item in id.ToListInt())
                {
                    OOS.GHR.BusinessAdapter.Request.RequestService.DeleteDeXuat(item);
                }
            }
            return Json(new { mess = Translate("Xóa thành công!") }, JsonRequestBehavior.AllowGet);
        }
    }
}