using DevExpress.Web.Mvc;
using OOS.GHR.Library;
using OOS.GHR.Payroll.Models;
using OOS.GHR.SelfService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using OOS.GHR.HRIS.Models;
using OOS.GHR.Admin.Models;
using System.Globalization;
using OOS.GHR.Master.Models;
using OOS.GHR.Framework.Mvc;
using OOS.GHR.Framework.Controllers;
using OOS.GHR.Framework;
using OOS.GHR.Framework.DynamicUI;
using DevExpress.Web;
using OOS.GHR.BusinessAdapter.XetDuyet;

namespace OOS.GHR.SelfService.Controllers
{
    public class HomeController : OOS.GHR.Framework.Controllers.BaseController
    {
        [OOSAuthorization(Code = "PORTAL_Payslip")]
        public ActionResult Payslip(int? NhanVienID)
        {
           NhanVienID = DatabaseCache.DangNhap.NhanVienID;
           return PartialView("_Payslip", NhanVienID);
        }

        [OOSAuthorization(Code = "PORTAL_Attendance")]
        public ActionResult TimeSheet(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            return PartialView("_TimeSheet", NhanVienID.Value);
        }

        [OOSAuthorization(Code = "PORTAL_Trainning")]
        public ActionResult ELearning(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            OOS.GHR.HRIS.Models.ElearningInfo EI = new HRIS.Models.ElearningInfo();
            EI.LoadData(NhanVienID.Value);
            return PartialView("_ELearning", EI);
        }

        [OOSAuthorization(Code = "PORTAL_Leave")]
        public ActionResult Leave(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            LeaveInfo LI = new LeaveInfo(NhanVienID.Value);
            return PartialView("_Leave", LI);
        }

        [OOSAuthorization(Code = "PORTAL_OT")]
        public ActionResult OT(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            OTInfo LI = new OTInfo(NhanVienID.Value);
            LI.LoadData();
            return PartialView("_OT", LI);
        }

        [OOSAuthorization(Code = "PORTAL_Mission")]
        public ActionResult CT(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            MissionInfo LI = new MissionInfo(NhanVienID.Value);

            return PartialView("_Mission", LI);
        }

        public ActionResult Propose(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            ProposeInfo LI = new ProposeInfo(NhanVienID.Value);
            LI.LoadData(NhanVienID.Value);
            return PartialView("_Propose", LI);
        }

        public ActionResult TestingDialog(int? ID)
        {
            string strSQL =
            @"SELECT CauHoiTracNghiemID, TenCauHoi, DapAn1, DapAn2, DapAn3
            FROM NS_DT_CauHoiTracNghiem
            WHERE NS_DT_CauHoiTracNghiem.KhoaDaoTaoID=" + NS_DT_DotDaoTao_NoiDung.GetKhoaDaoTaoIDByID(ID.Value);

            ViewData["Title"] = "TRẮC NGHIỆM ONLINE - "+ NS_DT_DotDaoTao_NoiDung.GetTenNoiDungDaoTaoByID(ID.Value);

            return PartialView("_ElearningTest", Database.ExecTable(strSQL));
        }

        private DateTime ConvertDateTime(TimeSpan ts)
        {
            return new DateTime(2000, 1, 1, ts.Hours, ts.Minutes, ts.Seconds);
        }

        public ActionResult OTInfo(int? NhanVienID, int? DangKyCongID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            OOS.GHR.Payroll.Models.OTInfoModel detail = new OTInfoModel();

            if (DangKyCongID <= 0 || DangKyCongID==null)
            {
                detail.NhanVienID = NhanVienID.Value;
                detail.TuNgay = DateTime.Now;
                detail.CaLamViecID = NhanVien.GetCaLVIDByID(detail.NhanVienID);
            }
            else
            {
                OOS.GHR.Library.NS_TL_DangKyCong dkc = OOS.GHR.Library.NS_TL_DangKyCong.GetNS_TL_DangKyCong(DangKyCongID.Value);
                detail.CaLamViecID = dkc.CaLamViecID;
                detail.KyHieuChamCongID = dkc.KyHieuChamCongID;
                detail.LamThemTruocCa = dkc.LamThemTruocCa;

                detail.BDLamThemTruocCa = ConvertDateTime(dkc.BDLamThemTruocCa);
                detail.KTLamThemTruocCa = ConvertDateTime(dkc.KTLamThemTruocCa);

                detail.BDLamThemSauCa = ConvertDateTime(dkc.BDLamThemSauCa);
                detail.KTLamThemSauCa = ConvertDateTime(dkc.KTLamThemSauCa);

                detail.GioLamThem = dkc.GioLamThem;
                detail.GioLamThemTruocCa = dkc.GioLamThemTruocCa;

                detail.DangKyCongID = dkc.DangKyCongID;
                detail.TuNgay = dkc.NgayChamCong.Value;
                detail.ToiNgay = dkc.NgayChamCong.Value;
                detail.LyDoTangCa = dkc.LyDoTangCa;
                detail.LyDoTangCaID = dkc.LyDoTangCaID;
                detail.NhanVienID = dkc.NhanVienID;
                detail.YKienXetDuyet = dkc.YKienXetDuyet;
                detail.AnTangCa = dkc.AnTangCa;
            }
            return PartialView("_OTInfo", detail);
        }

        public ActionResult DangKyOT(OOS.GHR.Payroll.Models.OTInfoModel model)
        {
            try
            {
                //Kiểm tra cho phép Sửa / Xóa backdate với số ngày cho phép
                int BackDateDay = ConfigClass.NS_CC_SONGAYLUISUACHAMCONG;
                /////////////////////////////////////////////////////////////////////////

                model.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                model.ToiNgay = model.TuNgay;

                if (model.BDLamThemSauCa > model.KTLamThemSauCa)
                {
                    if (model.GioLamThem > 0)
                        return ReturnErrorMessage(Translate("Bạn chỉ chọn 1 trong 2 giờ làm thêm sau ca:<br/>Chọn số giờ, hoặc chọn từ giờ - tới giờ !"));

                    return ReturnErrorMessage(Translate("Từ giờ < tới giờ !"));
                }

                if (model.BDLamThemTruocCa > model.KTLamThemTruocCa)
                {
                    if (model.GioLamThemTruocCa > 0)
                        return ReturnErrorMessage(Translate("Bạn chỉ chọn 1 trong 2 giờ làm thêm sau ca:<br/>Chọn số giờ, hoặc chọn từ giờ - tới giờ !"));

                    return ReturnErrorMessage(Translate("Từ giờ < tới giờ !"));
                }

                if (model.CaLamViecID <= 0 && model.KyHieuChamCongID <= 0)
                    return ReturnErrorMessage(Translate("Bạn phải chọn ca làm việc, hoặc lý do !"));

                if (model.TuNgay.Date < DateTime.Now.Date.AddDays(BackDateDay*-1))
                    return ReturnErrorMessage(Translate("Không cho phép Backdate quá [" + BackDateDay.ToString() + "] Ngày !"));

                if ((model.LyDoTangCa == "" || model.LyDoTangCa == null) && model.LyDoTangCaID <= 0)
                    return ReturnErrorMessage(Translate("Bạn phải chọn lý do tăng ca !"));

                //Kiểm tra ngày khóa công
                DateTime dtNgayKhoaCong = ConfigClass.CL_NGAYKHOACONGTHANG;
                if (dtNgayKhoaCong.Year > 2000)
                {
                    if (dtNgayKhoaCong > model.TuNgay || dtNgayKhoaCong > model.ToiNgay)
                        return ReturnErrorMessage(Translate("Bạn không điều chỉnh dữ liệu trước ngày khóa công !"));
                }

                string strErr = OOS.GHR.BusinessAdapter.SSService.GetNotify_OT(model.NhanVienID, model.CaLamViecID, model.TuNgay,
                model.GioLamThemTruocCa, 
                model.BDLamThemTruocCa.TimeOfDay, 
                model.KTLamThemTruocCa.TimeOfDay,
                model.GioLamThem, 
                model.BDLamThemSauCa.TimeOfDay, 
                model.KTLamThemSauCa.TimeOfDay,
                model.LyDoTangCaID, 
                model.LyDoTangCa, out bool Saved);

                if (!Saved)
                    return ReturnErrorMessage(Translate(strErr));

                OTInfoModel.SaveDangKyCa(model, BackDateDay);

                return ReturnErrorMessage("Đăng ký OT thành công !", 0);
            }
            catch (Exception ex)
            {
                return ReturnErrorMessage("Lỗi đăng ký OT !");
            }
        }

        public ActionResult Calendar(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            ViewData["HoVaTen"] = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen(NhanVienID.Value);
            ViewData["MaNhanVien"] = NhanVien.GetMaNhanVienByID(NhanVienID.Value);

            return View(NhanVienID);
        }

        public JsonResult GetPhieuLuong(int? NhanVienID, string ThangNam)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            if (NhanVienID.HasValue && NhanVienID.Value > 0)
            {
                SortedList<int, ArrayList> lstChinhSua = new SortedList<int, ArrayList>();

                int Thang = int.Parse(ThangNam.Substring(0, ThangNam.IndexOf("/")));
                int Nam = int.Parse(ThangNam.Substring(ThangNam.IndexOf("/") + 1));

                DataTable dt = null;
                dt = OOS.GHR.Payroll.Models.PayrollModel.LoadBangLuongNV_Payslip(Thang, Nam, NhanVienID.Value);

                string contentStr = CConfig.GetConfig_ByCpyID("TL_PHIEULUONG_CONTENTID", DatabaseCache.NhanVien.CongTyID);
                int ContentID = Database.ToInt(contentStr);
                if (ContentID<=0)
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
                        return Json(result);
                    }
                }
            }
            var rs = new { html = Translate("Không có dữ liệu") };
            return Json(rs);
        }

        public ActionResult GetChamCong(int? NhanVienID, string ThangNam)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            TimeAttendanceModel model = new TimeAttendanceModel();

            int nhanVienID = DatabaseCache.DangNhap.NhanVienID;

            if (DatabaseCache.IsPortalUser || nhanVienID == 0)
                nhanVienID = DatabaseCache.DangNhap.NhanVienID;

            int Thang = int.Parse(ThangNam.Substring(0, ThangNam.IndexOf("/")));
            int Nam = int.Parse(ThangNam.Substring(ThangNam.IndexOf("/") + 1));

            DateTime dtTuNgay = Database.GetFromDate(new DateTime(Nam, Thang, 1));
            DateTime dtToiNgay = Database.GetToDate(new DateTime(Nam, Thang, DateTime.DaysInMonth(Nam, Thang)));
             
            DataTable dt = OOS.GHR.Payroll.Controllers.TimeAttendanceController.GetDSTongHopCong_TheoNgay
            (0, dtTuNgay, dtToiNgay, 0, nhanVienID, 0, "", "");

            model.dtTuNgay = dtTuNgay;
            model.dtToiNgay = dtToiNgay;
            model.dtSource = dt;

            string strSQL =
            string.Format(@"SELECT XetDuyet, Convert(date, NgayChamCong) NgayChamCong, day(NgayChamCong) as Day, FieldName, GiaTriCu, GiaTriMoi, LyDoChinhSua 
            FROM NS_TL_CC_TongHopTheoNgay_ChinhSua WHERE NhanVienID={0} AND NgayChamCong between @TuNgay And @ToiNgay", nhanVienID);

            SortedList sl = new SortedList();
            sl.Add("@TuNgay", dtTuNgay);
            sl.Add("@ToiNgay", dtToiNgay);

            model.dtChinhSuaCong = Database.ExecTable(strSQL, sl);

            return PartialView("_TimeSheetDetails", new Models.AttendanceMonthModel(model));
        }

        public ActionResult GetAppointment(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            string strSQL =
            string.Format(
            @"SELECT QTCongTacID as ID,  NgayBatDau, NgayKetThuc, NoiCongTac as Sub, XetDuyet, 1 as Type 
            FROM NS_QTCongTac WHERE NhanVienID={0}
            union
            SELECT QTNghiPhepID as ID, NgayBatDau, NgayKetThuc, LyDoNghi as Sub, XetDuyet, 2 as Type 
            FROM NS_QTNghiPhep  WHERE NhanVienID={0}", NhanVienID.Value);

            List<AppointmentModel> al = new List<AppointmentModel>();
            DataTable dt = Database.ExecTable(strSQL);
            foreach(DataRow dr in dt.Rows)
            {
                AppointmentModel CM = new AppointmentModel();
                CM.ID = (int)dr["ID"];
                CM.Start = (DateTime)dr["NgayBatDau"];
                CM.End = (DateTime)dr["NgayKetThuc"];
                CM.Subject = dr["Sub"].ToString();
                CM.Status = (int)dr["XetDuyet"];
                CM.Type = (int)dr["Type"];
                CM.Label = CM.Subject;
                CM.Description = CM.Subject;
                al.Add(CM);
            }
            SchedulerDataObject SD = new SchedulerDataObject(NhanVienID.Value);
            SD.Appointments = al;

            return PartialView("_CaView", SD);
        }

        public ActionResult Index(int? ID)
        {
            if (ID == null || ID <= 0 || DatabaseCache.IsPortalUser)
                ID = DatabaseCache.DangNhap.NhanVienID;

            var m = new SS_ProfilePreviewModel();
            m.NhanVienID = ID.Value;
            m.LoadImageProfile();
            m.LoadAI();
            m.LoadFeed();
            m.LoadProfileNV(m.NhanVienID);

            ViewData["lbCaption"] = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen(m.NhanVienID);
            ViewData["ProfileName"] = ViewData["lbCaption"];

            return View(m);
        }

        public ActionResult News(int? ID, int? NewsID)
        {
            if (ID == null || ID <= 0 || DatabaseCache.IsPortalUser)
                ID = DatabaseCache.DangNhap.NhanVienID;

            var m = new SS_ProfilePreviewModel();
            m.LoadProfile(ID.Value);
            m.Article = NS_News_Article.GetNS_News_Article(NewsID.Value);

            ViewData["lbCaption"] = m.profile["HoVaTen"].ToString();
            ViewData["ProfileName"] = m.profile["HoVaTen"].ToString();

            return View(m);
        }

        public ActionResult Profile(string Ac, int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            var m = new SS_ProfilePreviewModel();
            m.Action = Ac;
            m.NhanVienID = NhanVienID.Value;
            m.LoadImageProfile();
            m.LoadProfileNV(m.NhanVienID);

            ViewData["lbCaption"] = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen(m.NhanVienID);
            ViewData["ProfileName"] = ViewData["lbCaption"];

            return View(m);
        }

        public ActionResult ProfileOverview(int? NhanVienID)
        {
            NhanVienID = DatabaseCache.DangNhap.NhanVienID;

            var m = new SS_ProfilePreviewModel();
            m.IsPortal = true;
            m.LoadProfile(NhanVienID.Value);
            m.NhanVienID = NhanVienID.Value;

            ViewData["lbCaption"] = m.profile["HoVaTen"].ToString();
            ViewData["ProfileName"] = m.profile["HoVaTen"].ToString();

            return PartialView("Profile/_Profile", m);
        }

        public PdfResult GetFileBaiGiang(int NoiDungID)
        {
            //var dbRecord = NS_DT_DotDaoTao_NoiDung.GetFileDaoTaoByID(NoiDungID);
            //string name = NS_DT_DotDaoTao_NoiDung.GetFileNameByID(NoiDungID);
            //return new PdfResult(dbRecord, name);
            return null;
        }

        public ActionResult RegisterTrainning(int ID)
        {
            int DotDaoTaoID = NS_DT_DotDaoTao_NoiDung.GetDotDaoTaoIDByID(ID);
            NS_DT_DotDaoTao_NhanVien NN = new NS_DT_DotDaoTao_NhanVien();
            NN.NhanVienID = DatabaseCache.DangNhap.NhanVienID;
            NN.DotDaoTaoID = DotDaoTaoID;

            Database.ExecNonquery("DELETE NS_DT_DotDaoTao_NoiDung WHERE NhanVienID="+NN.NhanVienID+" AND DotDaoTaoID="+NN.DotDaoTaoID);

            NN.Do_Insert();

            var rs = new { html = "OK" };
            return Json(rs);
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            ContentResult CR =  BinaryImageEditExtension.GetCallbackResult();
            ModelState.Clear();
            return CR;
        }

        public ActionResult EditProfile(int? NhanVienID)
        {
            int nhanVienId = DatabaseCache.DangNhap.NhanVienID;

            var nhanVien = NhanVien.GetNhanVien(nhanVienId);
            //if (nhanVien.Anh != null && nhanVien.Anh.Length <= 100)
            //{
            //    nhanVien.Anh = BusinessAdapter.Global.ImageTools.ByteFromImageFile
            //    (BusinessAdapter.HSNhanSu.HSNhanSu.GetThumbnails(nhanVien.HoTen[0], 128, 50));
            //}

            NhanVienEditModel model = nhanVien.ToModel<NhanVienEditModel, NhanVien>();
            model.FullName = nhanVien.Ho + " " + nhanVien.HoTen;
            ViewData["lbCaption"] = model.MaNhanVien.ToUpper() + " - " + model.FullName.ToUpper();

            ViewData["ProfileName"] = model.MaNhanVien + " - " + model.FullName;

            if (nhanVien.Anh != null)
            {
                string header = string.Format("data:image/{0};base64,", "unknown");
                model.AnhPro = string.Format("{0}{1}", header, Convert.ToBase64String(nhanVien.Anh));
            }

            model.DiaChiTT = new DiaChi_ChiTietModel
            ("DiaChiTT", "DiaChiThuongTru", Translate("Địa chỉ thường trú:"), nhanVien.DiaChiThuongTru, nhanVien.DiaChiTT_XaPhuongID,
            nhanVien.DiaChiTT_QuanHuyenID, nhanVien.DiaChiTT_TinhID, nhanVien.DiaChiTT_SoNha);

            model.DiaChiHT = new DiaChi_ChiTietModel
            ("DiaChi", "DiaChi", Translate("Địa chỉ hiện tại:"), nhanVien.DiaChi, nhanVien.DiaChi_XaPhuongID,
            nhanVien.DiaChi_QuanHuyenID, nhanVien.DiaChi_TinhID, nhanVien.DiaChi_SoNha);

            model.HSNS = new ucDynamicDBUI("DBUI_HSNS_NHANVIEN_CHUNG_ESS", model.NhanVienID, EditorCaptionPosition.Top);
            model.HSNS.Column = 3;
            model.HSNS.CreateTable = true;

            return PartialView("_EditProfile", model);
        }

        [HttpPost]
        public JsonResult SavePhotoImage()
        {
            byte[] o = BinaryImageEditExtension.GetValue<byte[]>("imgProfileImage");
            Console.WriteLine(Request.Form);

            int NhanVienID = Database.ToInt(Request.Form["NhanVienID"]);
            if (o != null && NhanVienID>0)
            {
                OOS.GHR.Library.NhanVien.SetAnh(OOS.GHR.BusinessAdapter.Global.ImageTools.GetPictureProfileBytes(o), NhanVienID);
            }
            else
            {
                //Xóa ảnh
                o = new byte[10];
                NhanVien.SetAnh(o, NhanVienID);
            }
            var result = new { mess = "OK" };
            return Json(result);
        }

        #region Giair trình đi sớm / về muộn
        [HttpPost]
        public JsonResult SS_GiaiTrinh(string NgayThang, string LyDo, string FieldName, decimal Value)
        {
            int ID =
            Database.ToInt(Database.ExecScalar(string.Format("SELECT Count(*) FROM [NS_TL_CC_TongHopTheoNgay_ChinhSua] WHERE NhanVienID={0} AND Convert(nvarchar, NgayChamCong, 23)='{1}' AND FieldName='{2}'",
            DatabaseCache.NhanVien.NhanVienID, NgayThang, FieldName)));
            if (ID<=0)
            {
                NS_TL_CC_TongHopTheoNgay_ChinhSua cs = new NS_TL_CC_TongHopTheoNgay_ChinhSua();
                cs.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                cs.NgayChamCong = DateTime.ParseExact(NgayThang, "yyyy-MM-dd", null);
                cs.FieldName = FieldName;
                cs.LyDoChinhSua = LyDo;
                cs.NgayChinhSua = DateTime.Now;
                cs.XetDuyet = 0;
                cs.GiaTriMoi = ((int)(Value)).ToString();
                cs.GiaTriCu = ((int)(Value)).ToString();
                cs.Do_Insert();

                if (FieldName.Contains("DiMuon"))
                    BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_SS_GiaiTrinh_DiMuon(cs);
                else
                    BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_SS_GiaiTrinh_VeSom(cs);
            }
            return ReturnJSResult("Giải trình thành công. Đang chờ phê duyệt !");
        }
        #endregion

        public JsonResult SubmitChanges()
        {
            string SS_NgaySinh = EditorExtension.GetValue<string>("SS_NgaySinh");
            string SS_DiaChi = EditorExtension.GetValue<string>("SS_DiaChi");
            string SS_DiaChiTT = EditorExtension.GetValue<string>("SS_DiaChiTT");
            string SS_QueQuan = EditorExtension.GetValue<string>("SS_QueQuan");
            string SS_NoiSinh = EditorExtension.GetValue<string>("SS_NoiSinh");
            string SS_DienThoai = EditorExtension.GetValue<string>("SS_DienThoai");
            string SS_Email = EditorExtension.GetValue<string>("SS_Email");
            string SS_TTHonNhan = EditorExtension.GetValue<string>("SS_TTHonNhan");

            string SS_CMTND = EditorExtension.GetValue<string>("SS_CMTND");
            string SS_NgayCap = EditorExtension.GetValue<string>("SS_NgayCap");
            string SS_NoiCap = EditorExtension.GetValue<string>("SS_NoiCap");
            string SS_SoHoChieu = EditorExtension.GetValue<string>("SS_SoHoChieu");
            string SS_NgayHetHanHC = EditorExtension.GetValue<string>("SS_NgayHetHanHC");
            string SS_SoTaiKhoan = EditorExtension.GetValue<string>("SS_SoTaiKhoan");
            string SS_NganHang = EditorExtension.GetValue<string>("SS_NganHang");

            var result = new { mess = Translate("Chỉnh sửa thông tin thành công !") };

            NhanVien _NVien = DatabaseCache.NhanVien;
            bool XD_SUATTNHANVIEN = BusinessAdapter.XetDuyet.XetDuyet.XD_Available("XD_SUATTNHANVIEN");
            if (XD_SUATTNHANVIEN)
            {
                SYS_LichSuThayDoiThongTin tt = SYS_LichSuThayDoiThongTin.GetSYS_LichSuThayDoiThongTinStr
                ("SELECT * FROM SYS_LichSuThayDoiThongTin WHERE TenBang='NhanVien' AND IDValue=" + _NVien.NhanVienID + " AND XetDuyet=0");
                tt.TenBang = "NhanVien";
                tt.KeyFieldName = "NhanVienID";
                tt.NgayThayDoi = Database.GetServerDate();
                tt.XetDuyet = 0;
                tt.IDValue = _NVien.NhanVienID;
                tt.Mota = "Xét duyệt thay đổi thông tin nhân viên: " + _NVien.ToString();
                tt.IsNew = tt.LichSuThayDoiThongTinID <= 0;
                tt.IsDirty = true;
                tt.Save();

                //Xóa toàn bộ lịch sử cũ / ghi đè thông tin mới lên
                if (tt.LichSuThayDoiThongTinID > 0)
                    Database.ExecNonquery("DELETE FROM SYS_LichSuThayDoiThongTinChiTiet WHERE LichSuThayDoiThongTinID=" + tt.LichSuThayDoiThongTinID);

                if (!OOS.GHR.BusinessAdapter.Global.CompareTools.IsEqualDate(SS_NgaySinh, _NVien.NgaySinh))
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "NgaySinh", _NVien.NgaySinh, BusinessAdapter.Global.TypeTools.ConvertToDateTime(SS_NgaySinh));

                if (SS_DiaChi != null && SS_DiaChi.Length > 0 && SS_DiaChi != _NVien.DiaChi)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "DiaChi", _NVien.DiaChi, SS_DiaChi);

                if (SS_DiaChiTT != null && SS_DiaChiTT.Length > 0 && SS_DiaChiTT != _NVien.DiaChiThuongTru)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "DiaChiThuongTru", _NVien.DiaChiThuongTru, SS_DiaChiTT);

                if (SS_QueQuan != null && SS_QueQuan.Length > 0 && SS_QueQuan != _NVien.QueQuan)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "QueQuan", _NVien.QueQuan, SS_QueQuan);

                if (SS_NoiSinh != null && SS_NoiSinh.Length > 0 && SS_NoiSinh != _NVien.NoiSinh)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "NoiSinh", _NVien.NoiSinh, SS_NoiSinh);

                if (SS_DienThoai != null && SS_DienThoai.Length > 0 && SS_DienThoai != _NVien.DienThoai)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "DienThoai", _NVien.DienThoai, SS_DienThoai);

                if (SS_Email != null && SS_Email.Length > 0 && SS_Email != _NVien.Email)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "Email", _NVien.Email, SS_Email);

                if (SS_TTHonNhan != null && SS_TTHonNhan.Length > 0 && SS_TTHonNhan != _NVien.TinhTrangHonNhan)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "TinhTrangHonNhan", _NVien.TinhTrangHonNhan, SS_TTHonNhan);


                if (SS_CMTND != null && SS_CMTND.Length > 0 && SS_CMTND != _NVien.CMTND)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "CMTND", _NVien.CMTND, SS_CMTND);

                if (!OOS.GHR.BusinessAdapter.Global.CompareTools.IsEqualDate(SS_NgayCap, _NVien.NgayCapCMT))
                        BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "NgayCapCMT", _NVien.NgayCapCMT, SS_NgayCap);

                if (SS_NoiCap != null && SS_NoiCap.Length > 0 && SS_NoiCap != _NVien.NoiCapCMT)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "NoiCapCMT", _NVien.NoiCapCMT, SS_NoiCap);

                if (SS_SoHoChieu != null && SS_SoHoChieu.Length > 0 && SS_SoHoChieu != _NVien.SoHoChieu)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "SoHoChieu", _NVien.SoHoChieu, SS_SoHoChieu);

                if (!OOS.GHR.BusinessAdapter.Global.CompareTools.IsEqualDate(SS_NgayHetHanHC, _NVien.NgayHetHanHoChieu))
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "NgayHetHanHoChieu", _NVien.NgayHetHanHoChieu, SS_NgayHetHanHC);

                if (SS_SoTaiKhoan != null && SS_SoTaiKhoan.Length > 0 && SS_SoTaiKhoan != _NVien.TaiKhoanNganHang)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "TaiKhoanNganHang", _NVien.TaiKhoanNganHang, SS_SoTaiKhoan);

                if (SS_NganHang != null && SS_NganHang.Length > 0 && SS_NganHang != _NVien.TenNganHang)
                    BusinessAdapter.Global.DataHistory.LuuThongTinLichSu(tt.LichSuThayDoiThongTinID, "TenNganHang", _NVien.TenNganHang, SS_NganHang);

                BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet(tt, "XD_SUATTNHANVIEN");

                result = new { mess = Translate("Chỉnh sửa thông tin thành công, Chờ phê duyệt !") };
            }
            else
            {
                NhanVien _NVienOld = _NVien.Clone();

                if (SS_NgaySinh != null && SS_NgaySinh.Length > 0 && (_NVien.NgaySinh == null || SS_NgaySinh != _NVien.NgaySinh.Value.ToString("dd/MM/yyyy")))
                    _NVien.NgaySinh = BusinessAdapter.Global.TypeTools.ConvertToDateTime(SS_NgaySinh);

                if (SS_DiaChi != null && SS_DiaChi.Length > 0 && SS_DiaChi != _NVien.DiaChi)
                    _NVien.DiaChi = SS_DiaChi;

                if (SS_DiaChiTT != null && SS_DiaChiTT.Length > 0 && SS_DiaChiTT != _NVien.DiaChiThuongTru)
                    _NVien.DiaChiThuongTru = SS_DiaChiTT;

                if (SS_QueQuan != null && SS_QueQuan.Length > 0 && SS_QueQuan != _NVien.QueQuan)
                    _NVien.QueQuan = SS_QueQuan;

                if (SS_NoiSinh != null && SS_NoiSinh.Length > 0 && SS_NoiSinh != _NVien.NoiSinh)
                    _NVien.NoiSinh = SS_NoiSinh;

                if (SS_DienThoai != null && SS_DienThoai.Length > 0 && SS_DienThoai != _NVien.DienThoai)
                    _NVien.DienThoai = SS_DienThoai;

                if (SS_Email != null && SS_Email.Length > 0 && SS_Email != _NVien.Email)
                    _NVien.Email = SS_Email;

                if (SS_TTHonNhan != null && SS_TTHonNhan.Length > 0 && SS_TTHonNhan != _NVien.TinhTrangHonNhan)
                    _NVien.TinhTrangHonNhan = SS_TTHonNhan;

                if (SS_CMTND != null && SS_CMTND.Length > 0 && SS_CMTND != _NVien.CMTND)
                    _NVien.CMTND = SS_CMTND;

                if (SS_NgayCap != null && SS_NgayCap.Length > 0 && (_NVien.NgayCapCMT == null || SS_NgayCap != _NVien.NgayCapCMT.Value.ToString("dd/MM/yyyy")))
                    _NVien.NgayCapCMT = BusinessAdapter.Global.TypeTools.ConvertToDateTime(SS_NgayCap);

                if (SS_NoiCap != null && SS_NoiCap.Length > 0 && SS_NoiCap != _NVien.NoiCapCMT)
                    _NVien.NoiCapCMT = SS_NoiCap;

                if (SS_SoHoChieu != null && SS_SoHoChieu.Length > 0 && SS_SoHoChieu != _NVien.SoHoChieu)
                    _NVien.SoHoChieu = SS_SoHoChieu;

                if (SS_NgayHetHanHC != null && SS_NgayHetHanHC.Length > 0 && (_NVien.NgayHetHanHoChieu == null || SS_NgayHetHanHC != _NVien.NgayHetHanHoChieu.Value.ToString("dd/MM/yyyy")))
                    _NVien.NgayHetHanHoChieu = BusinessAdapter.Global.TypeTools.ConvertToDateTime(SS_NgayHetHanHC);

                if (SS_SoTaiKhoan != null && SS_SoTaiKhoan.Length > 0 && SS_SoTaiKhoan != _NVien.TaiKhoanNganHang)
                    _NVien.TaiKhoanNganHang = SS_SoTaiKhoan;

                if (SS_NganHang != null && SS_NganHang.Length > 0 && SS_NganHang != _NVien.TenNganHang)
                    _NVien.TenNganHang = SS_NganHang;

                _NVien.Do_Update();

                string strHistory = OOS.GHR.BusinessAdapter.Global.DataHistory.GetHistoryModified(_NVienOld, _NVien);
                if (strHistory != "")
                    BusinessAdapter.Log.LogManager.LManager.AddNewLog(BusinessAdapter.Log.ENUM_PHANHE.HOSO_NHANSU, "Chỉnh sửa thông tin nhân viên", strHistory);
            }

            return Json(result);
        }

        public JsonResult SubmitSaveProfile(NhanVienEditModel nvEdit)
        {
            var result = new { mess = Translate("Chỉnh sửa thông tin thành công !") };

            NhanVien _NVienUpdate = NhanVien.GetNhanVien(DatabaseCache.NhanVien.NhanVienID);
            NhanVien _NVienOld = _NVienUpdate.Clone();

            bool Changed = false;

            #region Update Thông Tin Từ nvEDIT -> NHân Viên
            System.Reflection.PropertyInfo[] pis = nvEdit.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo pi in pis)
            {
                object oNewValue = pi.GetValue(nvEdit);
                if (oNewValue!=null && Database.toString(oNewValue)!="")
                {
                    System.Reflection.PropertyInfo opi = _NVienOld.GetType().GetProperty(pi.Name);
                    if (opi!=null)
                    {
                        object oOldValue = opi.GetValue(_NVienOld);

                        if (oNewValue.GetType()==typeof(int) || oNewValue.GetType() == typeof(Int32) || oNewValue.GetType() == typeof(Int64)
                            || oNewValue.GetType() == typeof(decimal))
                        {
                            if (Database.ToInt(oNewValue) == 0)
                                continue;
                        }

                        if (oNewValue.GetType()==typeof(DateTime))
                        {
                            DateTime dt = (DateTime)oNewValue;
                            if (dt.Year <= 1900)
                                continue;
                        }

                        if (Database.toString(oNewValue.ToString()) != Database.toString(oOldValue.ToString()))
                        {
                            Changed = true;
                            opi.SetValue(_NVienUpdate, oNewValue);
                        }
                    }
                }
            }
            #endregion

            bool XD_SUATTNHANVIEN = BusinessAdapter.XetDuyet.XetDuyet.XD_Available("XD_SUATTNHANVIEN");
            if (XD_SUATTNHANVIEN)
            {
                SYS_LichSuThayDoiThongTin tt = SYS_LichSuThayDoiThongTin.GetSYS_LichSuThayDoiThongTinStr
                ("SELECT * FROM SYS_LichSuThayDoiThongTin WHERE TenBang='NhanVien' AND IDValue=" + _NVienUpdate.NhanVienID + " AND XetDuyet=0");
                tt.TenBang = "NhanVien";
                tt.KeyFieldName = "NhanVienID";
                tt.NgayThayDoi = Database.GetServerDate();
                tt.XetDuyet = 0;
                tt.IDValue = _NVienUpdate.NhanVienID;
                tt.Mota = "Xét duyệt thay đổi thông tin nhân viên: " + _NVienUpdate.ToString();
                tt.IsNew = tt.LichSuThayDoiThongTinID <= 0;
                tt.IsDirty = true;
                tt.Save();

                OOS.GHR.BusinessAdapter.Global.DataHistory.LuuThongTinLichSuObject
                    (tt.LichSuThayDoiThongTinID, _NVienOld, _NVienUpdate);

                ucDynamicDBUI UI = new ucDynamicDBUI("DBUI_HSNS_NHANVIEN_CHUNG_ESS", _NVienUpdate.NhanVienID, EditorCaptionPosition.Top, true);
                UI.CreateModifyHistory(tt.LichSuThayDoiThongTinID, !tt.IsNew);
                
                BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet(tt, "XD_SUATTNHANVIEN");
                result = new { mess = Translate("Chỉnh sửa thông tin thành công, Chờ phê duyệt !") };

                #region Save File Store
                string[] fileUploads = FileManagement.GetAllFiles("SYS_LichSuThayDoiThongTin_FileStore");
                foreach (string file in fileUploads)
                {
                    OOS.GHR.FileManagement.Move_And_Add_File(file, "SYS_LichSuThayDoiThongTin", tt.LichSuThayDoiThongTinID);
                }
                #endregion
            }
            else
            {
                ucDynamicDBUI UI = new ucDynamicDBUI("DBUI_HSNS_NHANVIEN_CHUNG_ESS", _NVienUpdate.NhanVienID, EditorCaptionPosition.Top, true);
                UI.UpdateAllValue(_NVienUpdate.NhanVienID);

                if (Changed)
                    _NVienUpdate.Do_Update();

                string strHistory = OOS.GHR.BusinessAdapter.Global.DataHistory.GetHistoryModified(_NVienOld, _NVienUpdate);
                if (strHistory != "")
                    BusinessAdapter.Log.LogManager.LManager.AddNewLog(BusinessAdapter.Log.ENUM_PHANHE.HOSO_NHANSU, "Chỉnh sửa thông tin nhân viên", strHistory);
            }

            return Json(result);
        }

        public ActionResult GetProfileProcess(string module)
        {
            return PartialView("Profile/_Profile_Process", module);
        }

        #region Finger / Lost Add
        public ActionResult LostFingerLoadForm(int? ID)
        {
            return PartialView("UserForm\\XForm", OOS.GHR.SelfService.Models.DayAttendance_SelfServiceInfo.LostFingerModel(ID!=null?ID.Value:0));
        }

        [HttpPost]
        public JsonResult SS_ThemGio(string GioDen, string GioRa, string GioVao, string GioVe, string LyDo, long? ID, int? NhanVienID)
        {
            if (ID != null && NhanVienID != null)
            {
                NS_TL_CC_TongHopTheoNgay ctiet = NS_TL_CC_TongHopTheoNgay.GetNS_TL_CC_TongHopTheoNgay(ID.Value);
                if (ctiet.TongHopTheoNgayID > 0)
                {
                    int IDCS =
                    Database.ToInt(Database.ExecScalar(string.Format("SELECT ISNULL(TongHopTheoNgay_ChinhSuaID,0) FROM [NS_TL_CC_TongHopTheoNgay_ChinhSua] WHERE NhanVienID={0} AND Convert(nvarchar, NgayChamCong, 103)='{1}' AND FieldName='{2}'",
                    NhanVienID.Value, ctiet.NgayChamCong.Value.ToString("dd/MM/yyyy"), "THEM_GIO")));

                    OOS.GHR.BusinessAdapter.Global.DbConnector db = new BusinessAdapter.Global.DbConnector(Database.ConnectionCC);
                    if (IDCS>0)
                    {
                        int XetDuyet = NS_TL_CC_TongHopTheoNgay_ChinhSua.GetXetDuyetByID(IDCS);
                        if (XetDuyet <= 0)
                        {
                            NS_TL_CC_TongHopTheoNgay_ChinhSua.DeleteNS_TL_CC_TongHopTheoNgay_ChinhSua(IDCS);
                            OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(IDCS, BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_SS_GT_THEMGIO);

                            db.ExecNonquery(string.Format("DELETE FROM NS_TL_CC_DuLieuChamCong WHERE NhanVienID={0} AND Convert(nvarchar, NgayChamCong, 103)='{1}' AND NhapTay=1 AND DuLieuKhongTongHop=1",
                            NhanVienID.Value, ctiet.NgayChamCong.Value.ToString("dd/MM/yyyy")));
                        }
                        else
                        {
                            return ReturnJSResult("Yêu cầu thêm giờ của bạn đang được phê duyệt r !");
                        }
                    }

                    string HoVaTen = BusinessAdapter.HSNhanSu.DBProvider.GetHoVaTen(NhanVienID.Value);

                    NS_TL_CC_TongHopTheoNgay_ChinhSua cs = new NS_TL_CC_TongHopTheoNgay_ChinhSua();
                    cs.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                    cs.NgayChamCong = ctiet.NgayChamCong;
                    cs.FieldName = "THEM_GIO";
                    cs.LyDoChinhSua = LyDo;
                    cs.NgayChinhSua = DateTime.Now;
                    cs.XetDuyet = 0;
                    cs.GiaTriCu = "";

                    if (ctiet.GioDen.TotalMinutes == 0 && GioDen != "" && GioDen!=null)
                    {
                        cs.GiaTriMoi += "[Giờ đến: " + GioDen + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioDen + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    if (ctiet.GioVe.TotalMinutes == 0 && GioVe != "" && GioVe!=null)
                    {
                        cs.GiaTriMoi += "[Giờ về: " + GioVe + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioVe + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    if (ctiet.GioRa.TotalMinutes == 0 && GioRa != "" && GioRa!=null)
                    {
                        cs.GiaTriMoi += "[Giờ ra: " + GioRa + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioRa + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    if (ctiet.GioVao.TotalMinutes == 0 && GioVao != "" && GioVao!=null)
                    {
                        cs.GiaTriMoi += "[Giờ vào: " + GioVao + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioVao + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    cs.Do_Insert();
                    db.CloseConnection();

                    BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddXetDuyet_SS_GiaiTrinh_ThemGio(cs, HoVaTen);
                }
            }
            return ReturnJSResult("Giải trình thành công. Đang chờ phê duyệt !");
        }
        #endregion

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        #region Nghỉ phép hủy / Coong taác
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id">ID đối tượng xóa</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteLeave(int? id, string LyDo)
        {
            int nhanVienID = DatabaseCache.NhanVien.NhanVienID;
            if (id > 0 && id != null && nhanVienID > 0)
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTNghiPhep.DeleteWithApprove(id.Value, nhanVienID, LyDo);
                if (strErr!="")
                    return Json(new { mess = Translate(strErr) }, JsonRequestBehavior.AllowGet);
            }


            if (nhanVienID == 0)
                return Json(new { mess = Translate("Xóa không thành công!") }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { mess = Translate("Hủy đăng ký thành công !") }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteMission(int? id, string LyDo)
        {
            int nhanVienID = DatabaseCache.NhanVien.NhanVienID;
            if (id > 0 && id != null && nhanVienID > 0)
            {
                string strErr = OOS.GHR.BusinessAdapter.HSNhanSu.QTCongTac.DeleteWithApprove(id.Value, nhanVienID, LyDo);
                if (strErr != "")
                    return Json(new { mess = Translate(strErr) }, JsonRequestBehavior.AllowGet);
            }

            if (nhanVienID == 0)
                return Json(new { mess = Translate("Xóa không thành công!") }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { mess = Translate("Hủy đăng ký thành công !") }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class PdfResult : FileResult
    {
        private const String DefaultFileName = "file.pdf";
        private readonly Byte[] _byteArray;

        public PdfResult(Byte[] byteArray, String fileName = DefaultFileName)
            : base(MediaTypeNames.Application.Pdf)
        {
            _byteArray = byteArray;
            FileDownloadName = fileName;
        }
        protected override void WriteFile(HttpResponseBase response) { response.BinaryWrite(_byteArray); }
    }
}