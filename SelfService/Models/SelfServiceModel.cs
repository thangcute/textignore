using DevExpress.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OOS.GHR.Master.Models;
using OOS.GHR.Library;
using System.Data;
using OOS.GHR.Payroll.Models;
using System.Drawing;

namespace OOS.GHR.SelfService.Models
{
    public class AttendanceMonthModel
    {
        public bool IsDiLamThuBay { get; set; }

        public bool IsDiLamChuNhat { get; set; }

        public DateTime NgayKhoaBangCong { get; set; }

        public List<DayAttendance_SelfServiceInfo> DayListInfo = null;

        public bool IsLockCongThang { get; set; }

        public DataTable TongHopCong = null;

        public AttendanceMonthModel(OOS.GHR.Payroll.Models.TimeAttendanceModel Model)
        {
            DayListInfo = new List<DayAttendance_SelfServiceInfo>();
            for (int iRow = 0; iRow < Model.dtSource.Rows.Count; iRow++)
            {
                OOS.GHR.SelfService.Models.DayAttendance_SelfServiceInfo info = new OOS.GHR.SelfService.Models.DayAttendance_SelfServiceInfo(Model.dtSource, Model.dtChinhSuaCong, iRow);
                DayListInfo.Add(info);
            }
            if (DatabaseCache.NhanVien.CaLVID > 0)
            {
                IsDiLamChuNhat = NS_CaLamViec.GetIsLamChuNhatByID(DatabaseCache.NhanVien.CaLVID);
                IsDiLamThuBay = NS_CaLamViec.GetIsLamThubayByID(DatabaseCache.NhanVien.CaLVID);
            }
            this.NgayKhoaBangCong = ConfigClass.CL_NGAYKHOACONGTHANG;
            this.IsLockCongThang = OOS.GHR.BusinessAdapter.TienLuong.KhoaLuong.CheckKhoaCong(DateTime.Now.Month, DateTime.Now.Year);

            TongHopCong = OOS.GHR.BusinessAdapter.ChamCong.ChamCong.GetBangTongHopCongThangWithDetails
            (DateTime.Now, 0, DatabaseCache.NhanVien.MaNhanVien, 0, out string ColumnCC);
        }

        public bool IsLocked(DateTime dt)
        {
            if (IsLockCongThang) return true;

            if (dt <= NgayKhoaBangCong) return true;

            return false;
        }
    }

    public class DayAttendance_SelfServiceInfo
    {
        public bool MustAddTime = false;

        public bool MustCT_NP = false;

        public string TG_Den = "";

        public string TG_Ra = "";

        public string TG_Vao = "";

        public string TG_Ve = "";

        public string ErrorMessage = "";

        public DataTableSource dtSource = null;

        private int Index = 0;

        public int NhanVienID = 0;

        public int TongHopTheoNgayID = 0;

        private string _NghiPhep_CongTac = "";

        public string GiaiTrinhDiMuon = "";
        public int XetDuyet_DiMuon = 0;

        public string GiaiTrinhVeSom = "";
        public int XetDuyet_VeSom = 0;

        public string GiaiTrinhThemGio = "";
        public int XetDuyet_ThemGio = 0;

        public DayAttendance_SelfServiceInfo(DataTable source, DataTable chinhsua, int Index)
        {
            this.dtSource = (new DataTableSource(source));
            this.Index = Index;

            TimeSpan tgDen = dtSource.GetTimeSpan("GioDen", Index);
            if (tgDen.TotalMinutes > 0)
                TG_Den = tgDen.ToString();

            tgDen = dtSource.GetTimeSpan("GioRa", Index);
            if (tgDen.TotalMinutes > 0)
                TG_Ra = tgDen.ToString();

            tgDen = dtSource.GetTimeSpan("GioVao", Index);
            if (tgDen.TotalMinutes > 0)
                TG_Vao = tgDen.ToString();

            tgDen = dtSource.GetTimeSpan("GioVe", Index);
            if (tgDen.TotalMinutes > 0)
                TG_Ve = tgDen.ToString();

            _NghiPhep_CongTac = BusinessAdapter.SSService.GetNghiPhep_CongTac_ByDay(NgayThang, DatabaseCache.NhanVien.NhanVienID);

            DataRow[] drs = chinhsua.Select("Day=" + NgayThang.Day + " AND FieldName='TGVeSom'");
            if (drs.Length > 0)
            {
                GiaiTrinhVeSom = drs[0]["LyDoChinhSua"].ToString();
                XetDuyet_VeSom = (int)drs[0]["XetDuyet"];
            }

            drs = chinhsua.Select("Day=" + NgayThang.Day + " AND FieldName='TGDiMuon'");
            if (drs.Length > 0)
            {
                GiaiTrinhDiMuon = drs[0]["LyDoChinhSua"].ToString();
                XetDuyet_DiMuon = (int)drs[0]["XetDuyet"];
            }

            drs = chinhsua.Select("Day=" + NgayThang.Day + " AND FieldName='THEM_GIO'");
            if (drs.Length > 0)
            {
                GiaiTrinhThemGio = drs[0]["LyDoChinhSua"].ToString();
                XetDuyet_ThemGio = (int)drs[0]["XetDuyet"];
            }

            this.NhanVienID = dtSource.GetInt("NhanVienID", Index);
            this.TongHopTheoNgayID = dtSource.GetInt("TongHopTheoNgayID", Index);

            if (_NghiPhep_CongTac=="" && ((TG_Den+TG_Ve+TG_Ra+TG_Vao) !="") && TongHopTheoNgayID>0 && TG_LamViec<=0)
            {
                if (TG_Den == "" || TG_Ve == "")
                    MustAddTime = true;

                if ((TG_Ra == "" || TG_Vao == "") && (TG_Ra + TG_Vao != ""))
                    MustAddTime = true;
            }
        }

        public string DangKy
        {
            get
            {
                string strDangKy = dtSource.GetString("DangKy", Index);
                string strKyHieu = this.KyHieu;

                return strDangKy + ((strKyHieu != "" && !strDangKy.Contains(strKyHieu)) ? "|" + strKyHieu : "");
            }
        }

        public string KyHieu
        {
            get
            {
                return dtSource.GetString("KyHieu", Index);
            }
        }

        public string NghiPhep_CongTac
        {
            get { return _NghiPhep_CongTac; }
        }

        public decimal TG_LamViec
        {
            get
            { return (dtSource.GetDecimal("TGLamViec", Index)); }
        }

        public decimal TG_LamThem
        {
            get
            {
                return (dtSource.GetDecimal("TGLamThem", Index));
            }
        }

        public decimal DiMuon
        {
            get { return dtSource.GetDecimal("TGDiMuon", Index); }
        }

        public decimal VeSom
        {
            get { return dtSource.GetDecimal("TGVeSom", Index); }
        }

        public bool ThieuQuetThe { get { return false; } }

        public bool NghiVoLyDo { get { return false; } }

        public string Thu
        {
            get
            {
                return dtSource.GetString("Thu", Index);
            }
        }

        public DateTime NgayThang { get { return dtSource.GetDateTime("NgayChamCong", Index); } }

        public static XFormModel LostFingerModel(int ID)
        {
            DataTable dt = Database.ExecTable("SELECT * FROM NS_TL_CC_TongHopTheoNgay WHERE TongHopTheoNgayID="+ID);
            string Note = "";
            if (dt.Rows.Count>0)
                Note = ((DateTime)dt.Rows[0]["NgayChamCong"]).ToString(OOS.GHR.Framework.UI.UIFormat.DateFormat);

            XFormModel fm = new XFormModel("Thêm giờ chấm công", "frmLostFinger", "_TimeAttendance_LostFinger", 400, 250)
            {
                CallbackRouteValues = new { Controller = "Home", Action = "LostFingerLoadForm", Area = "SelfService" },
                drValues = dt.Rows.Count > 0 ? dt.Rows[0] : null,
                ClearContent = false,
                Note = Note
            };
            return fm;
        }

        public bool IsLocked
        {
            get
            {
                return false;
            }
        }
    }
}