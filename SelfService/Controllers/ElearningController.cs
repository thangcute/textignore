using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOS.GHR.Library;
using OOS.GHR.BusinessAdapter.Trainning;
using OOS.GHR.SelfService.Models;
using DevExpress.Web.Mvc;

namespace OOS.GHR.SelfService.Controllers
{
    public class ElearningController : OOS.GHR.Framework.Controllers.BaseController
    {
        public ActionResult Index()
        {
            Models.ElearningModel EM = new Models.ElearningModel();
            return View(EM);
        }

        public ActionResult Joinning()
        {
            Models.ElearningModel EM = new Models.ElearningModel();
            DataTable dt = OOS.GHR.BusinessAdapter.Trainning.TrainningService.GetCourseList_Joinning(DatabaseCache.NhanVien.NhanVienID);

            EM.DangThamGia = new List<Trainning.Models.TrainningCourseInfoModel>();
            foreach (DataRow dr in dt.Rows)
            {
                Trainning.Models.TrainningCourseInfoModel CI = new Trainning.Models.TrainningCourseInfoModel();
                CI.GetFromRow(dr, 0);
                CI.LoadNoiDungDaoTao();

                OOS.GHR.BusinessAdapter.Global.Reflection.SetPropertiesFromRow(CI, dr);

                EM.DangThamGia.Add(CI);
            }

            return PartialView("_Joinning", EM);
        }

        public ActionResult Openning()
        {
            Models.ElearningModel EM = new Models.ElearningModel();

            DataTable dt = OOS.GHR.BusinessAdapter.Trainning.TrainningService.GetCourseList_Register
            (DatabaseCache.NhanVien.NhanVienID, DatabaseCache.NhanVien.ChucVuID, DatabaseCache.NhanVien.ChucDanhID);

            EM.BatBuocDangKyList = new List<Trainning.Models.TrainningCourseInfoModel>();
            EM.DuocPhepDangKy = new List<Trainning.Models.TrainningCourseInfoModel>();

            foreach(DataRow dr in dt.Rows)
            {
                Trainning.Models.TrainningCourseInfoModel CI = new Trainning.Models.TrainningCourseInfoModel();
                CI.GetFromRow(dr, 0);
                OOS.GHR.BusinessAdapter.Global.Reflection.SetPropertiesFromRow(CI, dr);

                if (CI.IsBatBuocDangKy)
                    EM.BatBuocDangKyList.Add(CI);
                else
                    EM.DuocPhepDangKy.Add(CI);
            }

            return PartialView("_Openning", EM);
        }

        public ActionResult Finished()
        {
            Models.ElearningModel EM = new Models.ElearningModel();
            DataTable dt = OOS.GHR.BusinessAdapter.Trainning.TrainningService.GetCourseList_Joinning(DatabaseCache.NhanVien.NhanVienID, "5,6");

            EM.DaThamGia = new List<Trainning.Models.TrainningCourseInfoModel>();
            foreach (DataRow dr in dt.Rows)
            {
                Trainning.Models.TrainningCourseInfoModel CI = new Trainning.Models.TrainningCourseInfoModel();
                CI.GetFromRow(dr, 0);
                CI.LoadNoiDungDaoTao();
                OOS.GHR.BusinessAdapter.Global.Reflection.SetPropertiesFromRow(CI, dr);

                EM.DaThamGia.Add(CI);
            }

            return PartialView("_Finished", EM);
        }

        public JsonResult Register_TrainningCourse(int? DotDaoTaoID)
        {
            NS_DT_DotDaoTao_NhanVien DN = new NS_DT_DotDaoTao_NhanVien();
            DN.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
            DN.DotDaoTaoID = DotDaoTaoID.Value;
            DN.XetDuyet = 0;
            DN.NgayDangKy = DateTime.Now;
            DN.HoVaTen = DatabaseCache.NhanVien.ToString();
            DN.TrangThai = 0;

            dynamic nvInfo = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetThongTinNhanVienByID(DN.NhanVienID);

            DN.HoVaTen = nvInfo.HoVaTen;
            DN.TenChucVu = nvInfo.TenChucVu;
            DN.TenPhongBan = nvInfo.TenPhongBan;

            DN.Do_Insert();

            OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet
            (OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_SS_DANGKY_THAMGIA_KHOADAOTAO, DN.NhanVienID, "DotDaoTaoNhanVienID", DN.DotDaoTao_NhanVienID,
            Translate("Đăng ký tham gia khóa đào tạo")+" :"+NS_DT_DotDaoTao.GetTenDotDaoTaoByID(DotDaoTaoID.Value),
            "", "NS_DT_DotDaoTao_NhanVien");

            return ReturnJSResult("");
        }

        public JsonResult Reject_Joinning(int? DotDaoTaoID, string LyDo)
        {
            NS_DT_DotDaoTao_NhanVien DN = new NS_DT_DotDaoTao_NhanVien();
            DN.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
            DN.DotDaoTaoID = DotDaoTaoID.Value;

            DN.XetDuyet = -2; //Từ chối tham gia
            DN.TrangThai = -2; //Từ chối tham gia

            DN.NgayDangKy = DateTime.Now;
            DN.HoVaTen = DatabaseCache.NhanVien.ToString();

            dynamic nvInfo = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetThongTinNhanVienByID(DN.NhanVienID);

            DN.HoVaTen = nvInfo.HoVaTen;
            DN.TenChucVu = nvInfo.TenChucVu;
            DN.TenPhongBan = nvInfo.TenPhongBan;
            DN.GhiChu = LyDo;
            DN.Do_Insert();

            OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet
            (OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_SS_TUCHOI_THAMGIA_KHOADAOTAO, DN.NhanVienID, "DotDaoTaoNhanVienID", DN.DotDaoTao_NhanVienID,
            Translate("Từ chối tham gia khóa đào tạo") + " :" + NS_DT_DotDaoTao.GetTenDotDaoTaoByID(DotDaoTaoID.Value),
            "", "NS_DT_DotDaoTao_NhanVien");

            return ReturnJSResult("");
        }

        public JsonResult SaveRating()
        {
            int DotDaoTaoID = Database.ToInt(Request.Form["DotDaoTaoID"]);
            int NoiDungID = Database.ToInt(Request.Form["NoiDungDaoTaoID"]);

            string strSQL = string.Format("DELETE NS_DT_DotDaoTao_NhanVienDanhGia WHERE NhanVienID={0} AND DotDaoTaoID={1} AND NoiDungDaoTaoID={2}",
            DatabaseCache.NhanVien.NhanVienID, DotDaoTaoID, NoiDungID);
            Database.ExecNonquery(strSQL);

            foreach (string name in Request.Form.AllKeys)
            {
                if (name.StartsWith("Hrate_gv_") && name.EndsWith(DotDaoTaoID.ToString()))
                {
                    string strName = name.Replace("Hrate_gv_", "");
                    int TieuChiID = Database.ToInt(strName.Split('_')[0]);
                    if (TieuChiID > 0)
                    {
                        string Value = Database.toString(Request.Form[name]);

                        if (Request.Form["Hrate_gv_cb_" + strName]=="1")
                            Value = ComboBoxExtension.GetValue<string>(name);

                        if (!string.IsNullOrEmpty(Value) || Database.ToInt(Value) > 0)
                        {
                            NS_DT_DotDaoTao_NhanVienDanhGia DN = new NS_DT_DotDaoTao_NhanVienDanhGia();
                            DN.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                            DN.DotDaoTaoID = DotDaoTaoID;
                            DN.DanhGia = Value;
                            DN.TieuChiDanhGiaID = TieuChiID;
                            DN.NoiDungDaoTaoID = NoiDungID;
                            DN.Do_Insert();
                        }
                    }
                }
            }

            return ReturnJSResult("");
        }

        #region Traắc nghiệm
        public ActionResult ExamOnline(int? DotDaoTaoID, int? NoiDungDaoTaoID)
        {
            Models.ExamOnlineModel model = new Models.ExamOnlineModel();
            if (NoiDungDaoTaoID>0)
            {
                ///TrangThai = -1 -> Đã xóa, và cho làm bài trắc nghiệm khác rồi
                string SQLSelect =
                string.Format("SELECT TOP 1 * FROM NS_DT_DotDaoTao_ThiTracNghiem WHERE DotDaoTaoID={0} AND NoiDungDaoTaoID={1} AND NhanVienID={2} AND TrangThai>=0",
                DotDaoTaoID.Value, NoiDungDaoTaoID.Value, DatabaseCache.NhanVien.NhanVienID);

                NS_DT_DotDaoTao_ThiTracNghiem dot = NS_DT_DotDaoTao_ThiTracNghiem.GetNS_DT_DotDaoTao_ThiTracNghiemStr(SQLSelect);
                if (dot.ThiTracNghiemID<=0)
                {
                    dot = new NS_DT_DotDaoTao_ThiTracNghiem();
                    dot.DotDaoTaoID = DotDaoTaoID.Value;
                    dot.NoiDungDaoTaoID = NoiDungDaoTaoID.Value;
                    dot.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                    dot.BoCauHoiTracNghiemID = NS_DT_DotDaoTao_NoiDung.GetBoCauHoiTracNghiemIDByID(NoiDungDaoTaoID.Value);
                    dot.ThoiGianHoanThanh = NS_DT_BoCauHoiTracNghiem.GetThoiGianHoanThanhByID(dot.BoCauHoiTracNghiemID);
                    dot.GioBatDau = null;
                    dot.TrangThai = 0;
                    dot.Do_Insert();
                }
                else
                {
                    SQLSelect = string.Format("SELECT CauHoiTracNghiemID FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID=" + dot.ThiTracNghiemID,
                    DotDaoTaoID.Value, NoiDungDaoTaoID.Value, DatabaseCache.NhanVien.NhanVienID);
                    model.Questions = new List<ObjectID>();
                    DataTable dt = Database.ExecTable(SQLSelect);
                    foreach(DataRow dr in dt.Rows)
                    {
                        model.Questions.Add(new ObjectID(dr[0].ToString(), dr[0].ToString()));
                    }
                }
                model.DotDaoTaoID = DotDaoTaoID.Value;
                model.NoiDungDaoTaoID = NoiDungDaoTaoID.Value;
                model.BoCauHoiTracNghiemID = dot.BoCauHoiTracNghiemID;
                model.ThiTracNghiemID = dot.ThiTracNghiemID;

                model.Status = dot.TrangThai;
                model.TenDotDaoTao = NS_DT_DotDaoTao.GetTenDotDaoTaoByID(DotDaoTaoID.Value);
                model.TenNoiDung = NS_DT_DotDaoTao_NoiDung.GetTenNoiDungDaoTaoByID(NoiDungDaoTaoID.Value);
                model.ResetIndex = dot.ResetIndex;

                if (dot.GioBatDau != null)
                    model.BatDau = dot.GioBatDau.Value;
                else
                    model.BatDau = null;

                if (dot.GioKetThuc!=null)
                    model.KetThuc = dot.GioKetThuc.Value;

                if (model.BatDau != null && model.Status==1)
                    model.TGSuDung = DateTime.Now - model.BatDau.Value;
                else
                    model.TGSuDung = new TimeSpan();

                model.ThoiGianHoanThanh = dot.ThoiGianHoanThanh - (int)model.TGSuDung.TotalMinutes;
                if (model.ThoiGianHoanThanh<=0)
                {
                    model.Status = 2;
                    model.KetThuc = DateTime.Now;

                    //Set giờ kết thúc
                    NS_DT_DotDaoTao_ThiTracNghiem.SetGioKetThuc(DateTime.Now, model.ThiTracNghiemID);

                    //Set trạng thái đã hoàn thành trắc nghiệm
                    NS_DT_DotDaoTao_ThiTracNghiem.SetTrangThai(2, model.ThiTracNghiemID);
                }

                model.TGHoanThanh = new TimeSpan(0, model.ThoiGianHoanThanh, 0);
                model.TotalAnswers = NS_DT_BoCauHoiTracNghiem.GetTongSoCauHoiByID(dot.BoCauHoiTracNghiemID);
                model.TotalQuestion = Database.ToInt(Database.ExecScalar("SELECT Count(*) FROM NS_DT_CauHoiTracNghiem WHERE BoCauHoiTracNghiemID=" + model.BoCauHoiTracNghiemID));

                //Nếu đã hoàn thành => Load toàn bộ các câu đã hoàn thành
                if (model.Status==2)
                    model.LoadFinishedAnswer();
            }
            Session["EXAMONLINE"] = model;
            return PartialView("_ExamOnline", model);
        }

        /// <summary>
        /// Xem lại câu trả lời trước đó
        /// </summary>
        /// <param name="OldID">ID của câu trả lời</param>
        /// <returns></returns>
        public ActionResult ViewOldQuestion(int? IDPre)
        {
            Models.ExamOnlineModel exam_model = Session["EXAMONLINE"] as Models.ExamOnlineModel;
            exam_model.QuestionIndex = Database.ToInt(Database.ExecScalar("SELECT Count(*) FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID=" + exam_model.ThiTracNghiemID));

            Models.ExamOnlineItemModel model = new Models.ExamOnlineItemModel();
            model.TotalCount = exam_model.TotalAnswers;
            model.TotalAnswers = exam_model.QuestionIndex;

            NS_DT_DotDaoTao_TraLoiTracNghiem TraLoi = null;
            NS_DT_CauHoiTracNghiem CauHoi = null;

            TraLoi = NS_DT_DotDaoTao_TraLoiTracNghiem.GetNS_DT_DotDaoTao_TraLoiTracNghiem(IDPre.Value);
            CauHoi = NS_DT_CauHoiTracNghiem.GetNS_DT_CauHoiTracNghiem(TraLoi.CauHoiTracNghiemID);
            model.QuestionIndex = TraLoi.QuestionIndex;

            model.CauHoiList = new List<ObjectID>();
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn1, 1));
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn2, 2));
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn3, 3));
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn4, 4));

            model.Ten = CauHoi.TenCauHoi;
            model.MoTa = CauHoi.DienGiai;
            model.Type = CauHoi.Type;

            model.DapAn = TraLoi.DapAn;
            model.DapAnStr = TraLoi.DapAnStr != null ? TraLoi.DapAnStr : "";

            exam_model.DapAnDung = CauHoi.DapAnDung;
            exam_model.CurrentItem = TraLoi;

            model.ID = TraLoi.TraLoiTracNghiemID;

            return PartialView("_ExamOnlineItem", model);
        }

        /// <summary>
        /// Hàm này dùng Tiếp tục câu trả lời tiếp theo : Lưu câu hỏi hiện tại (IDPre>0) : Sang câu hỏi mới (IDPre=0)
        /// </summary>
        /// <param name="IDD">ID của cau hỏi hiện tại</param>
        /// <param name="DapAnID">Đáp án người dùng chọn</param>
        /// <param name="IDPre">ID của câu hỏi trước đó</param>
        /// <returns></returns>
        public ActionResult NextQuestion(int? IDD, string DapAnID, int? IDPre)
        {
            Models.ExamOnlineModel exam_model = Session["EXAMONLINE"] as Models.ExamOnlineModel;
            exam_model.QuestionIndex = Database.ToInt(Database.ExecScalar("SELECT Count(*) FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID=" + exam_model.ThiTracNghiemID));

            //Update Kết Quả của câu hỏi trước
            if (IDD > 0 && DapAnID!=null && DapAnID!="")
            {
                int DA = Database.ToInt(DapAnID);
                if (DA > 0)
                {
                    string strUpdate =
                    string.Format("UPDATE NS_DT_DotDaoTao_TraLoiTracNghiem SET IsCorrect={0}, DiemSo={1}, DapAn={2} WHERE TraLoiTracNghiemID={3}",
                    DA == exam_model.DapAnDung ? "1" : "0", DA == exam_model.DapAnDung ? "1" : "0", DA, IDD.Value);

                    Database.ExecNonquery(strUpdate);
                }
                else
                    NS_DT_DotDaoTao_TraLoiTracNghiem.SetDapAnStr(DapAnID, IDD.Value);
            }
            else
            {
                if (exam_model.Status == 0 || exam_model.BatDau == null)
                {
                    NS_DT_DotDaoTao_ThiTracNghiem.SetGioBatDau(DateTime.Now, exam_model.ThiTracNghiemID);
                    NS_DT_DotDaoTao_ThiTracNghiem.SetTrangThai(1, exam_model.ThiTracNghiemID);
                }
            }

            Models.ExamOnlineItemModel model = new Models.ExamOnlineItemModel();

            //Trường hợp thêm câu hỏi tiếp theo
            if (exam_model.QuestionIndex < exam_model.TotalAnswers && IDPre<=0)
                model.QuestionIndex = exam_model.QuestionIndex + 1;
            else
                model.QuestionIndex = exam_model.QuestionIndex;

            model.TotalCount = exam_model.TotalAnswers;
            model.TotalAnswers = exam_model.QuestionIndex;

            NS_DT_DotDaoTao_TraLoiTracNghiem TraLoi = null;
            NS_DT_CauHoiTracNghiem CauHoi = null;

            //Kiểm tra xem câu cuối cùng đã trả lời hay chưa? Nếu chưa -> Move tới câu cuối cùng, chứ không thêm câu tiếp theo
            if (IDPre<=0 && IDD>0)
            {
                DataTable dt = Database.ExecTable("SELECT TOP 1 TraLoiTracNghiemID, DapAn FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID = " + exam_model.ThiTracNghiemID + " Order By TraLoiTracNghiemID Desc");
                if (dt.Rows.Count>0)
                {
                    int DapAn = Database.ToInt(dt.Rows[0]["DapAn"]);

                    //Chưa trả lời
                    if (DapAn<=0)
                    {
                        IDPre = Database.ToInt(dt.Rows[0]["TraLoiTracNghiemID"]);
                    }
                }
            }

            if (IDPre == null || IDPre <= 0)
            {
                //Nếu chưa hết câu trả lời (hoặc IDPre <0) -> Next sang câu tiếp theo
                if (exam_model.QuestionIndex < exam_model.TotalAnswers && IDPre<=0)
                {
                    Random R = new Random();
                    int Index = R.Next(1, exam_model.TotalQuestion - exam_model.QuestionIndex);

                    string strSQL = string.Format(
                    @"SELECT * FROM
                    (SELECT ROW_NUMBER () OVER (ORDER BY CauHoiTracNghiemID) AS RowNum, * 
                    FROM NS_DT_CauHoiTracNghiem WHERE BoCauHoiTracNghiemID={0}
                    AND CauHoiTracNghiemID not in (SELECT CauHoiTracNghiemID FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID={2})) AS Question WHERE Question.RowNum={1}",
                    exam_model.BoCauHoiTracNghiemID, Index, exam_model.ThiTracNghiemID);

                    CauHoi = NS_DT_CauHoiTracNghiem.GetNS_DT_CauHoiTracNghiemStr(strSQL);
                }
                else
                {
                    //Ngược lại sẽ tới câu cuối cùng
                    string strSQL = "SELECT TOP 1 * FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID = " + exam_model.ThiTracNghiemID + " Order By TraLoiTracNghiemID Desc";
                    TraLoi = NS_DT_DotDaoTao_TraLoiTracNghiem.GetNS_DT_DotDaoTao_TraLoiTracNghiemStr(strSQL);
                    CauHoi = NS_DT_CauHoiTracNghiem.GetNS_DT_CauHoiTracNghiem(TraLoi.CauHoiTracNghiemID);
                }
            }
            else
            {
                TraLoi = NS_DT_DotDaoTao_TraLoiTracNghiem.GetNS_DT_DotDaoTao_TraLoiTracNghiem(IDPre.Value);
                CauHoi = NS_DT_CauHoiTracNghiem.GetNS_DT_CauHoiTracNghiem(TraLoi.CauHoiTracNghiemID);
                model.QuestionIndex = TraLoi.QuestionIndex;
            }

            if (TraLoi == null)
            {
                TraLoi = new NS_DT_DotDaoTao_TraLoiTracNghiem();
                TraLoi.QuestionIndex = model.QuestionIndex;
                TraLoi.CauHoiTracNghiemID = CauHoi.CauHoiTracNghiemID;
                TraLoi.DapAn = 0;
                TraLoi.DapAnStr = "";
                TraLoi.IsCorrect = false;
                TraLoi.DiemSo = 0;
                TraLoi.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                TraLoi.NoiDungDaoTaoID = exam_model.NoiDungDaoTaoID;
                TraLoi.DotDaoTaoID = exam_model.DotDaoTaoID;
                TraLoi.ThiTracNghiemID = exam_model.ThiTracNghiemID;
                TraLoi.Do_Insert();
            }

            model.CauHoiList = new List<ObjectID>();
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn1, 1));
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn2, 2));
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn3, 3));
            model.CauHoiList.Add(new ObjectID(CauHoi.DapAn4, 4));

            model.Ten = CauHoi.TenCauHoi;
            model.MoTa = CauHoi.DienGiai;
            model.Type = CauHoi.Type;

            model.DapAn = TraLoi.DapAn;
            model.DapAnStr = TraLoi.DapAnStr!=null?TraLoi.DapAnStr:"";

            exam_model.DapAnDung = CauHoi.DapAnDung;
            exam_model.CurrentItem = TraLoi;

            model.ID = TraLoi.TraLoiTracNghiemID;

            return PartialView("_ExamOnlineItem", model);
        }

        public ActionResult ExamOnlineFinished()
        {
            Models.ExamOnlineModel exam_model = Session["EXAMONLINE"] as Models.ExamOnlineModel;
            List<ObjectID> lst = new List<ObjectID>();

            string SQLSelect = string.Format("SELECT TraLoiTracNghiemID, ISNULL(DapAn,0) DapAn FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID={0}",
            exam_model.ThiTracNghiemID);

            bool IsCompleted = true;
            DataTable dt = Database.ExecTable(SQLSelect);
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new ObjectID((lst.Count + 1).ToString(), (int)dr[0], dr[1].ToString()));
                if (dr[1].ToString() == "0")
                    IsCompleted = false;
            }

            IsCompleted = IsCompleted && (exam_model.TotalAnswers == exam_model.TotalQuestion);

            if (IsCompleted)
                ViewData["IsFinished"] = "1";
            else
                ViewData["IsFinished"] = "0";

            return PartialView("_ExamOnlineItemFinished", lst);
        }

        /// <summary>
        /// Reset lại thi trắc nghiệm
        /// </summary>
        /// <returns></returns>
        public JsonResult ResetResult()
        {
            Models.ExamOnlineModel exam_model = Session["EXAMONLINE"] as Models.ExamOnlineModel;

            string strSQL = "";
            strSQL = string.Format("DELETE FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE NhanVienID={0} AND NoiDungDaoTaoID={1}", 
            DatabaseCache.NhanVien.NhanVienID, exam_model.NoiDungDaoTaoID);

            Database.ExecNonquery(strSQL);

            NS_DT_DotDaoTao_ThiTracNghiem DOT = NS_DT_DotDaoTao_ThiTracNghiem.GetNS_DT_DotDaoTao_ThiTracNghiem(exam_model.ThiTracNghiemID);
            DOT.TrangThai = 0;
            DOT.GioBatDau  = null;
            DOT.GioKetThuc = null;
            DOT.ResetIndex = DOT.ResetIndex + 1;
            DOT.Do_Update();

            return ReturnJSResult("");
        }

        /// <summary>
        /// Kết thúc đợt trắc nghiệm
        /// </summary>
        /// <returns></returns>
        public JsonResult ExamFinished()
        {
            Models.ExamOnlineModel exam_model = Session["EXAMONLINE"] as Models.ExamOnlineModel;
            exam_model.Status = 2;
            exam_model.KetThuc = DateTime.Now;

            decimal tongdiem = Database.ToDecimal(Database.ExecScalar(string.Format(
            @"SELECT SUM(DiemSo) FROM NS_DT_DotDaoTao_TraLoiTracNghiem WHERE ThiTracNghiemID={0} AND IsCorrect=1", exam_model.ThiTracNghiemID)));

            //Set giờ kết thúc
            //Set trạng thái đã hoàn thành trắc nghiệm
            //Update kêt quả trắc nghiệm
            string strUTN = string.Format(
            "UPDATE NS_DT_DotDaoTao_ThiTracNghiem SET " +
            "ResetIndex=(SELECT Count(*) FROM NS_DT_DotDaoTao_ThiTracNghiem WHERE NhanVienID={2} AND NoiDungDaoTaoID={3} AND DotDaoTaoID={4})-1," +
            "GioKetThuc=GetDate(), TrangThai=2, TongDiem={0} " +
            "WHERE ThiTracNghiemID={1}",
            tongdiem, exam_model.ThiTracNghiemID, DatabaseCache.DangNhap.NhanVienID, exam_model.NoiDungDaoTaoID, exam_model.DotDaoTaoID);
            Database.ExecNonquery(strUTN);

            //Chuyển điểm đã đạt được sang kết quả của nhân viên
            string strSelectID = string.Format("SELECT NoiDung_KetQuaID FROM NS_DT_NoiDung_KetQua WHERE NoiDungDaoTaoID={0} AND NhanVienID={1}", exam_model.NoiDungDaoTaoID, DatabaseCache.DangNhap.NhanVienID);
            int ID = Database.ToInt(Database.ExecScalar(strSelectID));

            //Update Tổng điểm sang NoiDung_KetQua -> Chú ý là chỉ update vì có kết quả reset
            if (ID <= 0)
            {
                NS_DT_NoiDung_KetQua NK = new NS_DT_NoiDung_KetQua();
                NK.NhanVienID = DatabaseCache.DangNhap.NhanVienID;
                NK.NoiDungDaoTaoID = exam_model.NoiDungDaoTaoID;
                NK.DotDaoTaoID = exam_model.DotDaoTaoID;
                NK.DiemSo = tongdiem;
                NK.Do_Insert();
            }
            else
                NS_DT_NoiDung_KetQua.SetDiemSo(tongdiem, ID);

            return ReturnJSResult("");
        }

        public ActionResult ExamOnlineList()
        {
            Models.ExamOnlineModel exam_model = Session["EXAMONLINE"] as Models.ExamOnlineModel;
            exam_model.LoadFinishedAnswer();

            return PartialView("_ExamOnlineItemList", exam_model.AnswerList);
        }

        public ActionResult Course_Evaluation(int? NoiDungID, int? DotDaoTaoID, int? ReadOnly)
        {
            OOS.GHR.Trainning.Models.TrainningCourseContentInfoModel Model = new Trainning.Models.TrainningCourseContentInfoModel();
            Model.NoiDungDaoTaoID = NoiDungID.Value;
            Model.DotDaoTaoID = DotDaoTaoID.Value;
            Model.LoadTableDanhGia();
            Model.IsReadOnly = (ReadOnly == 1);

            return PartialView("_Evaluation", Model);
        }
        #endregion
    }
}