using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOS.GHR.Library;
using System.Globalization;

namespace OOS.GHR.Services.Services.Ess
{
    public class TimeSheetService
    {
        public static DataTable GetBangTongHopCongThangWithDetails(DateTime timeThang, int PhongBanID, int NhanVienId, string strNhanVien, int LoaiChamCongID, out string ColumnLCC)
        {
            int nBDCC = Library.ConfigClass.NBDCC;
            int nKTCC = Library.ConfigClass.NKTCC;
            DateTime dateTime = default(DateTime);
            DateTime dateTime2 = default(DateTime);
            bool flag = nBDCC > 0 && nKTCC > 0;
            if (flag)
            {
                dateTime = Library.DatabaseBase.GetFromDate(new DateTime(timeThang.AddMonths(-1).Year, timeThang.AddMonths(-1).Month, nBDCC));
                dateTime2 = Library.DatabaseBase.GetToDate(new DateTime(timeThang.Year, timeThang.Month, nKTCC));
            }
            else
            {
                dateTime = Library.DatabaseBase.GetFromDate(new DateTime(timeThang.Year, timeThang.Month, 1));
                dateTime2 = Library.DatabaseBase.GetToDate(new DateTime(timeThang.Year, timeThang.Month, DateTime.DaysInMonth(timeThang.Year, timeThang.Month)));
            }

            SortedList sortedList = new SortedList();
            sortedList.Add("@FromDate", dateTime);
            sortedList.Add("@ToDate", dateTime2);
            bool flag2 = LoaiChamCongID <= 0;
            if (flag2)
            {
                ColumnLCC = Library.DatabaseBase.ExecScalar(@"DECLARE @LoaiCCong NVARCHAR(4000)
                    SELECT @LoaiCCong = COALESCE(@LoaiCCong +',[' + TenLoai + ']','['+TenLoai+']')+''
                    FROM NS_TL_LoaiChamCong Order By NS_TL_LoaiChamCong.ThuTu
                    SELECT @LoaiCCong", false, "").ToString();
            }
            else
            {
                ColumnLCC = "[" + Library.NS_TL_LoaiChamCong.GetTenLoaiByID(LoaiChamCongID, "") + "]";
            }
            string text = OOS.GHR.BusinessAdapter.ChamCong.ChamCong.CreateColumnDateTime(nBDCC, nKTCC, timeThang);
            string text2 = "";
            bool flag3 = PhongBanID > 0;
            if (flag3)
            {
                text2 = string.Concat(new string[]
                {
            " AND NhanVien.PhongBanID in (SELECT VALU FROM GetPhongBanChild_WUser(",
            PhongBanID.ToString(),
            ",",
            Library.DatabaseCache.DangNhap.NguoiDungID.ToString(),
            "))"
                });
            }

            if(NhanVienId > 0)
            {
                text2 += string.Format(@" AND (NhanVien.NhanVienID={0}) ", NhanVienId);
            }

            bool flag4 = strNhanVien != "";
            if (flag4)
            {
                text2 += string.Format(@" AND ((NhanVien.Ho+' '+NhanVien.HoTen) like N'%{0}%'
                    OR NhanVien.MaNhanVien like N'%{0}%' 
                    OR NhanVien.CMTND like N'%{0}%'
                    OR NhanVien.MaNVCu like N'%{0}%')", strNhanVien);
            }
            bool flag5 = LoaiChamCongID > 0;
            string command;
            if (flag5)
            {
                command = string.Concat(new string[]
                {
            @"  SELECT ROW_NUMBER() OVER (Order By ThuTuPhongBan, Mã) as STT,  * FROM
                (
                SELECT NhanVien.MaNhanVien as N'Mã', NS_TL_ChamCong.SoGioLam,
                Convert(Nvarchar,Day(NS_TL_ChamCong.NgayChamCong))+'/'+Convert(Nvarchar,Month(NS_TL_ChamCong.NgayChamCong)) as Ngay,
                NhanVien.Ho+' '+NhanVien.HoTen as N'Họ và tên', PhongBan.Ten as N'Phòng ban', LCC1.*, NhanVien.NhanVienID as NvID, PhongBan.ThuTu as ThuTuPhongBan
                FROM NhanVien
                left join
                (
                    SELECT NgayChamCong, ISNULL(SoGioLam,0) as SoGioLam, NhanVienID FROM NS_TL_ChamCong 
                    WHERE NS_TL_ChamCong.NgayChamCong>=@FromDate And NS_TL_ChamCong.NgayChamCong<=@ToDate
                    AND NS_TL_ChamCong.LoaiChamCongID=",
            LoaiChamCongID.ToString(),
            @"
            ) as NS_TL_ChamCong on NhanVien.NhanVienID = NS_TL_ChamCong.NhanVienID
                inner join PhongBan on PhongBan.PhongBanID = NhanVien.PhongBanID
                left join
                (
                SELECT * FROM
                (
                SELECT NS_TL_ChamCong.NhanVienID, NS_TL_LoaiChamCong.TenLoai,
                NS_TL_ChamCong.SoGioLam
                FROM NS_TL_ChamCong
                inner join NS_TL_LoaiChamCong on NS_TL_LoaiChamCong.LoaiChamCongID = NS_TL_ChamCong.LoaiChamCongID
                inner join NhanVien on NhanVien.NhanVienID = NS_TL_ChamCong.NhanVienID
                WHERE NS_TL_ChamCong.NgayChamCong>=@FromDate And NS_TL_ChamCong.NgayChamCong<=@ToDate ",
            text2,
            @"
                ) as LCC
                PIVOT
                (
                SUM(SoGioLam)
                FOR TenLoai in (",
            ColumnLCC,
            @")
                ) as LCC2
                ) as LCC1 on LCC1.NhanVienID = NhanVien.NhanVienID
                WHERE (NhanVien.XetDuyet=1)  AND (IsNull(NhanVien.IsDeleted,0)=0)
                AND (NhanVien.NghiViec=0  Or NhanVien.NghiViec IS NULL OR (NhanVien.NghiViec=1 And ((NhanVien.NgayNghiViec<=@FromDate And NhanVien.NgayNghiViec>=@ToDate) Or NhanVien.NgayNghiViec>=@ToDate) )) ",
            text2,
            @"
                ) as CC
                PIVOT
                (
                SUM(SoGioLam)
                FOR Ngay in (",
            text,
            @") ) as CC1"
                });
            }
            else
            {
                command = string.Concat(new string[]
                {
            @"  SELECT ROW_NUMBER() OVER (Order By ThuTuPhongBan, Mã) as STT,  * FROM
                (
                SELECT NhanVien.MaNhanVien as N'Mã',
                NhanVien.Ho+' '+NhanVien.HoTen as N'Họ và tên', PhongBan.Ten as N'Phòng ban', LCC1.*, NhanVien.NhanVienID as NvID, PhongBan.ThuTu as ThuTuPhongBan
                FROM NhanVien
                inner join PhongBan on PhongBan.PhongBanID = NhanVien.PhongBanID
                left join
                (
                SELECT * FROM
                (
                SELECT NS_TL_ChamCong.NhanVienID, NS_TL_LoaiChamCong.TenLoai,
                NS_TL_ChamCong.SoGioLam
                FROM NS_TL_ChamCong
                inner join NS_TL_LoaiChamCong on NS_TL_LoaiChamCong.LoaiChamCongID = NS_TL_ChamCong.LoaiChamCongID
                inner join NhanVien on NhanVien.NhanVienID = NS_TL_ChamCong.NhanVienID
                WHERE NS_TL_ChamCong.NgayChamCong>=@FromDate And NS_TL_ChamCong.NgayChamCong<=@ToDate ",
            text2,
            @"
                ) as LCC
                PIVOT
                (
                SUM(SoGioLam)
                FOR TenLoai in (",
            ColumnLCC,
            @")
                ) as LCC2
                ) as LCC1 on LCC1.NhanVienID = NhanVien.NhanVienID
                WHERE (NhanVien.XetDuyet=1)  AND (IsNull(NhanVien.IsDeleted,0)=0)
                AND (NhanVien.NghiViec=0  Or NhanVien.NghiViec IS NULL OR (NhanVien.NghiViec=1 And ((NhanVien.NgayNghiViec<=@FromDate And NhanVien.NgayNghiViec>=@ToDate) Or NhanVien.NgayNghiViec>=@ToDate) )) ",
            text2,
            @"
                ) as CC"
                });
            }
            return Library.DatabaseBase.ExecTable(command, sortedList, false, "");
        }
        public static async Task<object> getSummary(string MonthYear = "")
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID; nhanvienId = 101;
            var dataSummary = OOS.GHR.BusinessAdapter.ChamCong.ChamCong.GetBangTongHopCongThangWithDetails(DateTime.Parse("2012-07-01"), 0, "", 0,
                DatabaseCache.NhanVien.NhanVienID, out string ColumnCC, true);
            if(dataSummary != null && dataSummary.Rows.Count > 0)
            {
                DataRow rowSummary = dataSummary.AsEnumerable().Where(p => p.Field<Int32>("NvID") == nhanvienId).FirstOrDefault();
                return rowSummary;
            }
            return null;
        }

        public static async Task<object> get(string MonthYear = "") // yyyy-MM
        {
            DateTime actionTime = DateTime.Now;
            DateTime nowDate = actionTime.Date;

            string _MonthYear = actionTime.ToString("yyyy-MM");
            if (!string.IsNullOrEmpty(MonthYear))
            {
                actionTime = DateTime.Parse(MonthYear + "-01");
                _MonthYear = MonthYear;
            }

            int maxDayOfMonth = (new DateTime(actionTime.Year, actionTime.Month, 1).AddMonths(1).AddDays(-1)).Day;

            DateTime _startDate = DateTime.Parse(_MonthYear + "-01");
            DateTime _endDate = DateTime.Parse(_MonthYear + "-" + maxDayOfMonth);

            List<TimeSheetModel> dataTimeSheet = new List<TimeSheetModel>();
            // Lay danh sach ngay trong thang
            List<TimeSheetModel> timeSheet = new List<TimeSheetModel>();
            for(int i = 0; i < maxDayOfMonth; i++)
            {
                DateTime checkDate = _startDate.AddDays(i);
                timeSheet.Add(new TimeSheetModel()
                {
                    TongHopTheoNgayID = 0,
                    NgayThang = checkDate,
                    Thu = DataConvert.DayOfWeek(checkDate, 0)
                });
            }

            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            //nhanvienId = 656;
            bool disable_giaitrinhdimuon = false;
            bool disable_giaitrinhvesom = false;
            bool disable_giaitrinhthieugio = false;
            using (var db = new OosContext())
            {
                ///Dữ liệu từ bảng NS_TL_CC_DuLieuChamCong
                var timekeeping = db.NS_TL_CC_DuLieuChamCong.Where(x => x.NhanVienID == nhanvienId && x.ThoiGian > nowDate).Select(x => new { 
                    ThoiGian = x.ThoiGian
                });

                var ts = CConfig.GetConfigBool("NS_ESS_DISABLE_GIAITRINHCONG"); 
                if (ts)
                {
                    disable_giaitrinhdimuon = true;
                    disable_giaitrinhvesom = true;
                    disable_giaitrinhthieugio = true;
                }

                // lay du lieu cham cong thang
                var dataCC = (from thc in db.NS_TL_CC_TongHopTheoNgay
                              join clv in db.NS_CaLamViec on thc.CaLamViecID equals clv.CaLamViecID into clvs
                              from clv in clvs.DefaultIfEmpty()
                              where thc.NhanVienID == nhanvienId
                                && thc.NgayChamCong >= _startDate
                                && thc.NgayChamCong <= _endDate
                              select new
                              {
                                  TongHopTheoNgayID = thc.TongHopTheoNgayID,
                                  NgayThang = thc.NgayChamCong.Value,
                                  CaLamViec = clv.TenCa,
                                  TG_Den = thc.GioDen,
                                  TG_Ra = thc.GioRa,
                                  TG_Vao = thc.GioVao,
                                  TG_Ve = thc.GioVe,
                                  NghiPhep_CongTac = "",
                                  DangKy = "",
                                  TG_LamViec = thc.TGLamViec,
                                  TG_LamThem = thc.TGLamThem,
                                  DiMuon = thc.TGDiMuon,
                                  VeSom = thc.TGVeSom,
                                  GiaiTrinhDiMuon = "",
                                  GiaiTrinhVeSom = "",
                                  GiaiTrinhThemGio = "",
                                  Lock = thc.Lock,
                                  IsLamThubay = clv.IsLamThubay,
                                  IsLamChuNhat = clv.IsLamChuNhat,
                                  XetDuyet = (int?)null,
                                  Approver = "",
                                  ApprovedDate = "",
                                  ApproveComment = ""
                              }).AsEnumerable().Select(x => new TimeSheetModel
                              {
                                  TongHopTheoNgayID = x.TongHopTheoNgayID,
                                  Thu = DataConvert.DayOfWeek(x.NgayThang, 0),
                                  CaLamViec = x.CaLamViec,
                                  NgayThang = x.NgayThang,
                                  TG_Den = x.TG_Den,
                                  TG_Ra = x.TG_Ra,
                                  TG_Vao = x.TG_Vao,
                                  TG_Ve = x.TG_Ve,
                                  NghiPhep_CongTac = "",
                                  DangKy = "",
                                  TG_LamViec = x.TG_LamViec,
                                  TG_LamThem = x.TG_LamThem,
                                  DiMuon = x.DiMuon,
                                  VeSom = x.VeSom,
                                  GiaiTrinhDiMuon = "",
                                  GiaiTrinhVeSom = "",
                                  GiaiTrinhThemGio = "",
                                  Lock = x.Lock,
                                  IsLamThubay = x.IsLamThubay,
                                  IsLamChuNhat = x.IsLamChuNhat,
                                  XetDuyet = x.XetDuyet,
                                  Approver = "",
                                  ApprovedDate = "",
                                  ApproveComment = ""
                              });
                if (dataCC.Any())
                {
                    List<DateTime> existCC = dataCC.Select(x => x.NgayThang).ToList();
                    var data = timeSheet.Where(x => !existCC.Contains(x.NgayThang)).Union(dataCC).OrderBy(x => x.NgayThang).ToList();
                    dataTimeSheet = data.ToList();
                }
                else
                    dataTimeSheet = timeSheet;
                //
                if(timekeeping != null && timekeeping.Any())
                {
                    DateTime dtNowAdd = nowDate.AddDays(1);

                    List<DateTime?> _timekeeping = timekeeping.Where(x=>x.ThoiGian < dtNowAdd).ToList().Select(x => x.ThoiGian).ToList();
                    int _count = _timekeeping.Count();
                    if (_count > 0)
                    {
                        TimeSpan _TG_Den = _timekeeping[0].Value.TimeOfDay;
                        TimeSpan _TG_Ra = _count > 2 ? _timekeeping[1].Value.TimeOfDay : new TimeSpan();
                        TimeSpan _TG_Vao = _count > 3 ? _timekeeping[2].Value.TimeOfDay : new TimeSpan();
                        TimeSpan _TG_Ve = _count > 1 ? _timekeeping[_count - 1].Value.TimeOfDay : new TimeSpan();

                        dataTimeSheet.Where(x => x.NgayThang == nowDate).ToList().ForEach(x =>
                        {
                            x.TG_Den = _TG_Den;
                            x.TG_Ra = _TG_Ra;
                            x.TG_Vao = _TG_Vao;
                            x.TG_Ve = _TG_Ve;
                        });
                    }
                }
                // Get di muon ve som - Them gio
                /* TGDiMuon-TGVeSom-THEM_GIO */
                var dataGtcc = db.NS_TL_CC_TongHopTheoNgay_ChinhSua.Where(x =>
                    x.NhanVienID == nhanvienId
                    && x.NgayChamCong >= _startDate
                    && x.NgayChamCong <= _endDate
                    && !x.NgayChamCong.Equals(null)
                ).Select(x => new
                {
                    TongHopTheoNgay_ChinhSuaID = x.TongHopTheoNgay_ChinhSuaID,
                    NgayChamCong = x.NgayChamCong,
                    FieldName = x.FieldName,
                    XetDuyet = x.XetDuyet
                });
                if (dataGtcc.Any())
                {
                    foreach(var item in dataGtcc)
                    {
                        DateTime? _NgayChamCong = item.NgayChamCong;
                        string _FieldName = item.FieldName;
                        int? _XetDuyet = item.XetDuyet;

                        string _TrangThaiDuyet = _XetDuyet == 1 ? "Đã duyệt" : (
                                _XetDuyet == -1 ? "Từ chối" : (
                                    _XetDuyet == 2 ? "Đang chờ xóa" : (
                                        _XetDuyet == -2 ? "Đã xóa/hủy" : (
                                            _XetDuyet == 100 ? "Đang duyệt" : "Đợi duyệt"
                                        )
                                    )
                                )
                            );

                        if(_FieldName == "TGDiMuon")
                        {
                            dataTimeSheet.Where(x => x.NgayThang == _NgayChamCong).ToList().ForEach(x =>
                            {
                                x.GiaiTrinhDiMuon = string.Format("Giải trình đi muộn - {0}", _TrangThaiDuyet);
                            });
                        }else if(_FieldName == "TGVeSom")
                        {
                            dataTimeSheet.Where(x => x.NgayThang == _NgayChamCong).ToList().ForEach(x =>
                            {
                                x.GiaiTrinhVeSom = string.Format("Giải trình về sớm - {0}", _TrangThaiDuyet);
                            });
                        }else if(_FieldName == "THEM_GIO")
                        {
                            dataTimeSheet.Where(x => x.NgayThang == _NgayChamCong).ToList().ForEach(x =>
                            {
                                x.GiaiTrinhThemGio = string.Format("Giải trình thêm giờ - {0}", _TrangThaiDuyet);
                            });
                        }
                    }
                }
                // Cong tac
                DateTime __endDate = _endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                var dataCt = db.NS_QTCongTac.Where(x =>
                    x.NhanVienID == nhanvienId
                    && (
                        (x.NgayBatDau >= _startDate && x.NgayBatDau <= __endDate) ||
                        (x.NgayKetThuc >= _startDate && x.NgayKetThuc <= __endDate) ||
                        (x.NgayBatDau < _startDate && x.NgayKetThuc > __endDate )
                    )
                ).Select(x => new
                {
                    QTCongTacID = x.QTCongTacID,
                    NgayBatDau = x.NgayBatDau,
                    NgayKetThuc = x.NgayKetThuc,
                    NoiCongTac = x.NoiCongTac,
                    CongViecCuThe = x.CongViecCuThe,
                    XetDuyet = x.XetDuyet,
                });

                if (dataCt.Any())
                {
                    foreach(var item in dataCt)
                    {
                        if(item.NgayBatDau != null && item.NgayKetThuc != null)
                        {
                            DateTime? _NgayBatDau = DateTime.Parse(item.NgayBatDau.Value.ToString("yyyy-MM-dd"));
                            DateTime? _NgayKetThuc = DateTime.Parse(item.NgayKetThuc.Value.ToString("yyyy-MM-dd"));
                            string _NoiCongTac = item.NoiCongTac;
                            string _CongViecCuThe = item.CongViecCuThe;
                            int? _XetDuyet = item.XetDuyet;

                            string _TrangThaiDuyet = _XetDuyet == 1 ? "Đã duyệt" : (
                                    _XetDuyet == -1 ? "Từ chối" : (
                                        _XetDuyet == 2 ? "Đang chờ xóa" : (
                                            _XetDuyet == -2 ? "Đã xóa/hủy" : (
                                                _XetDuyet == 100 ? "Đang duyệt" : "Đợi duyệt"
                                            )
                                        )
                                    )
                                );

                            dataTimeSheet.Where(x => x.NgayThang >= _NgayBatDau && x.NgayThang <= _NgayKetThuc).ToList().ForEach(x =>
                            {
                                x.NghiPhep_CongTac = string.Format("CT: {0}-{1} ", _NoiCongTac, _CongViecCuThe);
                            });
                        }
                    }
                }

                // Nghi phep
                var dataNp = db.NS_QTNghiPhep.Where(x =>
                    x.NhanVienID == nhanvienId
                    && (
                        (x.NgayBatDau >= _startDate && x.NgayBatDau <= __endDate) ||
                        (x.NgayKetThuc >= _startDate && x.NgayKetThuc <= __endDate) ||
                        (x.NgayBatDau < _startDate && x.NgayKetThuc > __endDate)
                    )
                ).Select(x => new
                {
                    QTNghiPhepID = x.QTNghiPhepID,
                    NgayBatDau = x.NgayBatDau,
                    NgayKetThuc = x.NgayKetThuc,
                    LyDoNghi = x.LyDoNghi,
                    XetDuyet = x.XetDuyet
                });

                if (dataNp.Any())
                {
                    foreach(var item in dataNp)
                    {
                        DateTime? _NgayBatDau = DateTime.Parse(item.NgayBatDau.Value.ToString("yyyy-MM-dd"));
                        DateTime? _NgayKetThuc = DateTime.Parse(item.NgayKetThuc.Value.ToString("yyyy-MM-dd"));
                        string _LyDoNghi = item.LyDoNghi;
                        int? _XetDuyet = item.XetDuyet;

                        string _TrangThaiDuyet = _XetDuyet == 1 ? "Đã duyệt" : (
                                    _XetDuyet == -1 ? "Từ chối" : (
                                        _XetDuyet == 2 ? "Đang chờ xóa" : (
                                            _XetDuyet == -2 ? "Đã xóa/hủy" : (
                                                _XetDuyet == 100 ? "Đang duyệt" : "Đợi duyệt"
                                            )
                                        )
                                    )
                                );

                        dataTimeSheet.Where(x => x.NgayThang >= _NgayBatDau && x.NgayThang <= _NgayKetThuc).ToList().ForEach(x =>
                        {
                            x.NghiPhep_CongTac += string.Format("NP: {0} ", _LyDoNghi);
                        });
                    }
                }
                // Dang ky
                var dataDk = (from dk in db.NS_TL_DangKyCong
                              join clv in db.NS_CaLamViec on dk.CaLamViecID equals clv.CaLamViecID into lvcs
                              from clv in lvcs.DefaultIfEmpty()
                              where dk.NhanVienID == nhanvienId
                              select new
                              {
                                  DangKyCongID = dk.DangKyCongID,
                                  NgayChamCong = dk.NgayChamCong,
                                  LyDoTangCa = dk.LyDoTangCa,
                                  TenCa = clv.TenCa,
                                  MaCa = clv.MaCa,
                                  BDLamThemTruocCa = dk.BDLamThemTruocCa.ToString(),
                                  KTLamThemTruocCa = dk.KTLamThemTruocCa.ToString(),
                                  BDLamThemSauCa = dk.BDLamThemSauCa.ToString(),
                                  KTLamThemSauCa = dk.KTLamThemSauCa.ToString(),
                                  DangKyStr = dk.DangKyStr
                              });
                if (dataDk.Any())
                {
                    foreach(var item in dataDk)
                    {
                        DateTime? _NgayChamCong = DateTime.Parse(item.NgayChamCong.Value.ToString("yyyy-MM-dd"));
                        string _LyDoTangCa = item.LyDoTangCa;
                        string _TenCa = item.TenCa;
                        string _MaCa = item.MaCa;
                        string _BDLamThemTruocCa = item.BDLamThemTruocCa;
                        string _KTLamThemTruocCa = item.KTLamThemTruocCa;
                        string _BDLamThemSauCa = item.BDLamThemSauCa;
                        string _KTLamThemSauCa = item.KTLamThemSauCa;
                        string _DangKyStr = item.DangKyStr;

                        dataTimeSheet.Where(x => x.NgayThang == _NgayChamCong).ToList().ForEach(x =>
                        {
                            x.DangKy = string.Format("Đăng ký: {0} ", _DangKyStr);
                        });
                    }
                }
            }
            return dataTimeSheet.AsEnumerable().Select(x => new {
                TongHopTheoNgayID = x.TongHopTheoNgayID,
                Thu = x.Thu,
                CaLamViec = x.CaLamViec,
                NgayChamCong = x.NgayThang,
                DisableGiaiTrinhDiMuon = (x.NgayThang <= nowDate) ? (
                    (x.Thu != "T7" && x.Thu != "CN") ? disable_giaitrinhdimuon : (
                        (x.Thu == "T7" && (x.IsLamThubay ?? false)) ||
                        (x.Thu == "CN" && (x.IsLamChuNhat ?? false))
                    ) ? disable_giaitrinhdimuon : true
                ) : true,
                DisableGiaiTrinhVeSom = (x.NgayThang <= nowDate) ? (
                    (x.Thu != "T7" && x.Thu != "CN") ? disable_giaitrinhdimuon : (
                        (x.Thu == "T7" && (x.IsLamThubay ?? false)) ||
                        (x.Thu == "CN" && (x.IsLamChuNhat ?? false))
                    ) ? disable_giaitrinhdimuon : true
                ) : true,
                DisableGiaiThemGio = (x.NgayThang <= nowDate) ? (
                    (x.Thu != "T7" && x.Thu != "CN") ? disable_giaitrinhdimuon : (
                        (x.Thu == "T7" && (x.IsLamThubay ?? false)) ||
                        (x.Thu == "CN" && (x.IsLamChuNhat ?? false))
                    ) ? disable_giaitrinhdimuon : true
                ) : true,
                NgayThang = x.NgayThang.ToString("dd/MM"),
                TG_Den = x.TG_Den != null ? (x.TG_Den.ToString() != "00:00:00" ? x.TG_Den.ToString().Substring(0, 5) : "") : "",
                TG_Ra = x.TG_Ra != null ? (x.TG_Ra.ToString() != "00:00:00" ? x.TG_Ra.ToString().Substring(0, 5) : "") : "",
                TG_Vao = x.TG_Vao != null ? (x.TG_Vao.ToString() != "00:00:00" ?x.TG_Vao.ToString().Substring(0, 5) : "") : "",
                TG_Ve = x.TG_Ve != null ? (x.TG_Ve.ToString() != "00:00:00" ? x.TG_Ve.ToString().Substring(0, 5) : "") : "",
                NghiPhep_CongTac = x.NghiPhep_CongTac,
                DangKy = x.DangKy,
                TG_LamViec = x.TG_LamViec,
                TG_LamThem = x.TG_LamThem,
                DiMuon = x.DiMuon,
                VeSom = x.VeSom,
                GiaiTrinhDiMuon = x.GiaiTrinhDiMuon,
                GiaiTrinhVeSom = x.GiaiTrinhVeSom,
                GiaiTrinhThemGio = x.GiaiTrinhThemGio,
                Lock = x.Lock,
                QuyTrinhHuy = false,
                IsLamThubay = x.IsLamThubay,
                IsLamChuNhat = x.IsLamChuNhat,
                IsNghiLe = false,
                XetDuyet = x.XetDuyet,
                Approver = x.Approver,
                ApprovedDate = x.ApprovedDate,
                ApproveComment = x.ApproveComment
            });
        }

        public static async Task<object> getMobile(string MonthYear = "", int nhanvienId = 0)
        {
            DateTime actionTime = DateTime.Now;
            string _MonthYear = actionTime.ToString("yyyy-MM");
            if (!string.IsNullOrEmpty(MonthYear))
            {
                actionTime = DateTime.Parse(MonthYear + "-01");
                _MonthYear = MonthYear;
            }
            int maxDayOfMonth = (new DateTime(actionTime.Year, actionTime.Month, 1).AddMonths(1).AddDays(-1)).Day;
            DateTime _startDate = DateTime.Parse(_MonthYear + "-01");
            DateTime _endDate = DateTime.Parse(_MonthYear + "-" + maxDayOfMonth);
            //
            if (nhanvienId == 0)
                nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;

            using (var db = new OosContext())
            {
                var dataCC = (from thc in db.NS_TL_CC_TongHopTheoNgay
                    join clv in db.NS_CaLamViec on thc.CaLamViecID equals clv.CaLamViecID into clvs
                    from clv in clvs.DefaultIfEmpty()
                    where thc.NhanVienID == nhanvienId
                    && thc.NgayChamCong >= _startDate
                    && thc.NgayChamCong <= _endDate
                    select new
                    {
                        TongHopTheoNgayID = thc.TongHopTheoNgayID,
                        NgayThang = thc.NgayChamCong.Value,
                        CaLamViec = clv.TenCa,
                        TG_Den = thc.GioDen,
                        TG_Ra = thc.GioRa,
                        TG_Vao = thc.GioVao,
                        TG_Ve = thc.GioVe,
                        TG_LamViec = thc.TGLamViec,
                        TG_LamThem = thc.TGLamThem,
                        DiMuon = thc.TGDiMuon,
                        VeSom = thc.TGVeSom,
                        Lock = thc.Lock,
                        IsLamThubay = clv.IsLamThubay,
                        IsLamChuNhat = clv.IsLamChuNhat
                    }).AsEnumerable().Select(x => new TimeSheetModel
                    {
                        TongHopTheoNgayID = x.TongHopTheoNgayID,
                        Thu = DataConvert.DayOfWeek(x.NgayThang),
                        CaLamViec = x.CaLamViec,
                        NgayThang = x.NgayThang,
                        TG_Den = x.TG_Den,
                        TG_Ra = x.TG_Ra,
                        TG_Vao = x.TG_Vao,
                        TG_Ve = x.TG_Ve,
                        NghiPhep_CongTac = "",
                        DangKy = "",
                        TG_LamViec = x.TG_LamViec,
                        TG_LamThem = x.TG_LamThem,
                        DiMuon = x.DiMuon,
                        VeSom = x.VeSom,
                        GiaiTrinhDiMuon = "",
                        GiaiTrinhVeSom = "",
                        GiaiTrinhThemGio = "",
                        Lock = x.Lock,
                        IsLamThubay = x.IsLamThubay,
                        IsLamChuNhat = x.IsLamChuNhat,
                        XetDuyet = 0,
                        Approver = "",
                        ApprovedDate = "",
                        ApproveComment = ""
                    });

                if (dataCC != null && dataCC.Any())
                {
                    return dataCC.ToList();
                }
            }
            //
            return null;
        }

        public static async Task<bool> Explain(ExplainModel model)
        {
            string FieldName = model.IsDimuon ? "TGDiMuon" : "TGVeSom";

            return SS_GiaiTrinh(model.NgayThang, model.LyDo, FieldName, 0);
        }

        public static bool SS_GiaiTrinh(DateTime NgayChamCong, string LyDo, string FieldName, decimal Value)
        {
            int ID =
            OOS.GHR.Library.Database.ToInt(OOS.GHR.Library.Database.ExecScalar(string.Format("SELECT Count(*) FROM [NS_TL_CC_TongHopTheoNgay_ChinhSua] WHERE NhanVienID={0} AND Convert(nvarchar, NgayChamCong, 23)='{1}' AND FieldName='{2}'",
            DatabaseCache.NhanVien.NhanVienID, NgayChamCong.ToString("yyyy-MM-dd"), FieldName)));
            if (ID <= 0)
            {
                // tinh so phut di muon ve som
                using (var db = new OosContext()){
                    OOS.GHR.EntityFramework.Models.NS_TL_CC_TongHopTheoNgay _thcn = db.NS_TL_CC_TongHopTheoNgay.Where(x => x.NhanVienID != null && (int)x.NhanVienID == DatabaseCache.NhanVien.NhanVienID && x.NgayChamCong == NgayChamCong).FirstOrDefault();
                    if(_thcn != null && _thcn.TongHopTheoNgayID > 0)
                    {
                        try
                        {
                            if (FieldName == "TGVeSom")
                                Value = _thcn.TGVeSom.GetValueOrDefault(0);
                            else
                                Value = _thcn.TGDiMuon.GetValueOrDefault(0);
                        }
                        catch
                        {

                        }
                    }
                }
                // end

                NS_TL_CC_TongHopTheoNgay_ChinhSua cs = new NS_TL_CC_TongHopTheoNgay_ChinhSua();
                cs.NhanVienID = DatabaseCache.NhanVien.NhanVienID;
                cs.NgayChamCong = NgayChamCong;
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

                return true;
            }
            else return false;
        }

        /// <summary>
        /// Giair trinhf thieu gio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<string> LostFinger(LostFingerModel model)
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;

            return SS_ThemGio(model.TG_Den, model.TG_Ra, model.TG_Vao, model.TG_Ve, model.Lydo, model.TongHopTheoNgayID, nhanvienId);
        }

        public static string SS_ThemGio(string GioDen, string GioRa, string GioVao, string GioVe, string LyDo, long? ID, int? NhanVienID)
        {
            if (ID != null && NhanVienID != null)
            {
                NS_TL_CC_TongHopTheoNgay ctiet = NS_TL_CC_TongHopTheoNgay.GetNS_TL_CC_TongHopTheoNgay(ID.Value);
                if (ctiet.TongHopTheoNgayID > 0)
                {
                    if (OOS.GHR.BusinessAdapter.TienLuong.KhoaLuong.CheckKhoaCong(ctiet.NgayChamCong.Value, DatabaseCache.CongTyID))
                    {
                        return "Bảng công đã khóa, không thể giải trình !";
                    }

                    int IDCS =
                    OOS.GHR.Library.Database.ToInt(OOS.GHR.Library.Database.ExecScalar(string.Format("SELECT ISNULL(TongHopTheoNgay_ChinhSuaID,0) FROM [NS_TL_CC_TongHopTheoNgay_ChinhSua] WHERE NhanVienID={0} AND Convert(nvarchar, NgayChamCong, 103)='{1}' AND FieldName='{2}'",
                    NhanVienID.Value, ctiet.NgayChamCong.Value.ToString("dd/MM/yyyy"), "THEM_GIO")));

                    OOS.GHR.BusinessAdapter.Global.DbConnector db = new BusinessAdapter.Global.DbConnector(OOS.GHR.Library.Database.ConnectionCC);
                    if (IDCS > 0)
                    {
                        int XetDuyet = NS_TL_CC_TongHopTheoNgay_ChinhSua.GetXetDuyetByID(IDCS);
                        if (XetDuyet <= 0)
                        {
                            NS_TL_CC_TongHopTheoNgay_ChinhSua.DeleteNS_TL_CC_TongHopTheoNgay_ChinhSua(IDCS);
                            OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(IDCS, BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_SS_GT_THEMGIO);

                            db.ExecNonquery(string.Format("DELETE FROM NS_TL_CC_DuLieuChamCong WHERE NhanVienID={0} AND Convert(nvarchar, ThoiGian, 103)='{1}' AND NhapTay=1 AND DuLieuKhongTongHop=1",
                            NhanVienID.Value, ctiet.NgayChamCong.Value.ToString("dd/MM/yyyy")));
                        }
                        else
                        {
                            return "Yêu cầu thêm giờ của bạn đang được phê duyệt r !";
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

                    if (ctiet.GioDen.TotalMinutes == 0 && GioDen != "" && GioDen != null)
                    {
                        cs.GiaTriMoi += "[Giờ đến: " + GioDen + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioDen + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    if (ctiet.GioVe.TotalMinutes == 0 && GioVe != "" && GioVe != null)
                    {
                        cs.GiaTriMoi += "[Giờ về: " + GioVe + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioVe + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    if (ctiet.GioRa.TotalMinutes == 0 && GioRa != "" && GioRa != null)
                    {
                        cs.GiaTriMoi += "[Giờ ra: " + GioRa + "] ";

                        DateTime dtNew = DateTime.ParseExact(ctiet.NgayChamCong.Value.ToString("yyyy-M-d") + " " + GioRa + ":00", "yyyy-M-d HH:mm:ss", CultureInfo.InvariantCulture);
                        db.ExecNonquery(
                        string.Format("INSERT INTO NS_TL_CC_DuLieuChamCong (NhanVienID, ThoiGian, NhapTay, HoVaTen, PhongBanID, DuLieuKhongTongHop, MayChamCongID, MaChamCong, VaoRa)" +
                        " VALUES ({0},'{1}',{2},N'{3}',{4},{5},0,'',0)",
                        NhanVienID, dtNew.ToString("M/d/yyyy HH:mm:ss"), 1, HoVaTen, NhanVien.GetPhongBanIDByID(NhanVienID.Value), 1));
                    }

                    if (ctiet.GioVao.TotalMinutes == 0 && GioVao != "" && GioVao != null)
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
            else return "Bạn chưa chọn ngày để giải trình !";

            return "";
        }

    }
}
