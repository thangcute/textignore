using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOS.GHR.Library;
using System.Data;

namespace OOS.GHR.Services.Services.Ess
{
    public class ProfileService
    {
        public static async Task<object> GetHeadcountActivity(int NhanVienID)
        {
            int NVCount = Database.ToInt(Database.ExecScalar("SELECT Count(*) FROM NhanVien WHERE XetDuyet=1 AND NghiViec=0"));

            int PhongBanID = NhanVien.GetPhongBanIDByID(NhanVienID);

            string strWhere = "";
            if (NVCount > 200)
                strWhere = " AND NhanVien.PhongBanID in (SELECT Valu FROM GetPhongBanChilds(" + PhongBanID + "))";

            if (DatabaseCache.CongTyID != 0)
                strWhere += " AND NhanVien.CongTyID = " + DatabaseCache.CongTyID;

            string strLeaveList =
            @"SELECT NhanVien.NgaySinh, NhanVien.NhanVienID, Ho+' '+HoTen as HoVaTen, NhanVien.MaNhanVien, Email, DienThoai,
            A.GhiChu, NS_DsChucVu.TenChucVu, A.Type, NhanVien.Anh, PhongBan.Ten as PhongBan, ISNULL(NgayBatDauLam, GetDate()) NgayBatDauLam,  NS_DsChucDanh.TenChucDanh
            FROM NhanVien
            inner join
            (	SELECT NhanVien.NhanVienID, CT.NoiCongTac as GhiChu, 1 as Type FROM NhanVien
	            inner join
	            (select NhanVienID, NoiCongTac from NS_QTCongTac
	                where NS_QTCongTac.XetDuyet=1 AND convert(date, getdate()) between convert(date,ngaybatdau) and convert(date,NgayKetThuc)) as CT
	                on CT.NhanVienID = NhanVien.NhanVienID
                WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=0) " + strWhere + @"
	            union
                SELECT NhanVien.NhanVienID, CT.NoiCongTac as GhiChu, 4 as Type FROM NhanVien
	            inner join
	            (select NhanVienID, NoiCongTac from NS_QTCongTac
	                where  NS_QTCongTac.XetDuyet=1 AND convert(date, DATEADD(Day, 1, getdate())) between convert(date,ngaybatdau) and convert(date,NgayKetThuc)) as CT
	                on CT.NhanVienID = NhanVien.NhanVienID
                WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=0) " + strWhere + @"
	            union
	            SELECT NhanVien.NhanVienID, NP.LyDoNghi as GhiChu, 4 as Type FROM NhanVien
	            inner join
	            (select NhanVienID, LyDoNghi   from NS_QTNghiPhep
	             where SoNgayNghi>=1 AND XetDuyet=1 AND convert(date, DATEADD(Day, 1, getdate())) between convert(date,ngaybatdau) and convert(date,NgayKetThuc)) as NP
                on NP.NhanVienID = NhanVien.NhanVienID
                WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=0) " + strWhere + @"
	            union
	            SELECT NhanVien.NhanVienID, NP.LyDoNghi as GhiChu, 2 as Type FROM NhanVien
	            inner join
	            (select NhanVienID, LyDoNghi   from NS_QTNghiPhep
	             where  SoNgayNghi>=1 AND XetDuyet=1 AND convert(date, getdate()) between convert(date,ngaybatdau) and convert(date,NgayKetThuc)) as NP
                on NP.NhanVienID = NhanVien.NhanVienID
                WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=0) " + strWhere + @"
	            union
	            SELECT NhanVien.NhanVienID, '' as GhiChu, 3 as Type FROM NhanVien
                WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=0 AND Month(GetDate())=Month(NgayBatDauLam) AND Year(GetDate())=Year(NgayBatDauLam))
	            union
	            SELECT NhanVien.NhanVienID, '' as GhiChu, 5 as Type FROM NhanVien
                WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=1 AND Month(GetDate())=Month(NgayNghiViec) AND Year(GetDate())=Year(NgayNghiViec))
                ) as A
            on A.NhanVienID = NhanVien.NhanVienID
            inner join NS_DsChucVu on NS_DsChucVu.ChucVuID = NhanVien.ChucVuID
            inner join PhongBan on PhongBan.PhongBanID = NhanVien.PhongBanID
            left  join NS_DsChucDanh on NS_DsChucDanh.ChucDanhID = NhanVien.ChucDanhID
            WHERE (1=1 AND NhanVien.XetDuyet=1 AND NhanVien.NghiViec=0)";

            List<OOS.GHR.Core.HRIS.PersonalAvatar> LeaveList = new List<OOS.GHR.Core.HRIS.PersonalAvatar>();
            List<OOS.GHR.Core.HRIS.PersonalAvatar> LeaveTomorrowList = new List<OOS.GHR.Core.HRIS.PersonalAvatar>();
            List<OOS.GHR.Core.HRIS.PersonalAvatar> NewEmployeeList = new List<OOS.GHR.Core.HRIS.PersonalAvatar>();

            DataTable dtSource = Database.ExecTable(strLeaveList);

            DataRow[] drNvMoi = dtSource.Select("Type=3");
            DataRow[] drNvNghiPhep = dtSource.Select("Type=1 Or Type=2");
            DataRow[] drNvNghiViec = dtSource.Select("Type=5");

            var obj = new
            {

                NhanVienMoi = drNvMoi.Select(dr => new
                {
                    Id = dr["NhanVienID"],
                    HoVaTen = Database.toString(dr["HoVaTen"]),
                    TenChucVu = Database.toString(dr["TenChucVu"]),
                    TenPhongBan = Database.toString(dr["PhongBan"]),
                    GhiChu = Database.toString(dr["GhiChu"]),
                    TenChucDanh = Database.toString(dr["TenChucDanh"]),
                    Email = Database.toString(dr["Email"]),
                    DienThoai = Database.toString(dr["DienThoai"]),
                    NgaySinh = TypeTools.ConvertToDateTime(dr["NgaySinh"]),
                    NgayBatDauLam = TypeTools.ConvertToDateTime(dr["NgayBatDauLam"])
                })
            };

            foreach (DataRow dr in Database.ExecTable(strLeaveList).Rows)
            {
                int Type = Database.ToInt(dr["Type"]);
                OOS.GHR.Core.HRIS.PersonalAvatar PI = new OOS.GHR.Core.HRIS.PersonalAvatar();

                if (dr["Anh"] != null && dr["Anh"] != DBNull.Value)
                    PI.Img = (byte[])dr["Anh"];

                PI.MaNhanVien = Database.toString(dr["MaNhanVien"]);
                PI.HoVaTen = Database.toString(dr["HoVaTen"]);
                PI.ChucVu = Database.toString(dr["TenChucVu"]);
                PI.PhongBan = Database.toString(dr["PhongBan"]);
                PI.GhiChu = Database.toString(dr["GhiChu"]);
                PI.NhanVienID = Database.ToInt(dr["NhanVienID"]);

                if (dr["NgaySinh"] != null && dr["NgaySinh"] != DBNull.Value)
                    PI.NgaySinh = (DateTime)(dr["NgaySinh"]);

                if (dr["NgayBatDauLam"] != null && dr["NgayBatDauLam"] != DBNull.Value)
                    PI.NgayBatDauLam = (DateTime)(dr["NgayBatDauLam"]);

                PI.viewStyle = Core.HRIS.AvatarAction.VIEWPROFILE;
                switch (Type)
                {
                    case 2:
                    case 1:
                        LeaveList.Add(PI);
                        break;
                    case 4:
                        LeaveTomorrowList.Add(PI);
                        break;
                    case 3:
                        NewEmployeeList.Add(PI);
                        break;
                }
            }

            return obj;
        }

        public static async Task<object> activity()
        {
            bool IsAvatar = true;
            if (ConfigurationManager.AppSettings["IsAvatar"] != null && ConfigurationManager.AppSettings["IsAvatar"] == "0")
                IsAvatar = false;

            int checkDayNumber = -1;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("CheckDayNumber"))
            {
                int.TryParse(ConfigurationManager.AppSettings["CheckDayNumber"], out checkDayNumber);
                if (checkDayNumber <= 0)
                    checkDayNumber = 30;
            }

            DateTime actionTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime actionTimeNext = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //DateTime checkTime = actionTime.AddDays((0 - checkDayNumber));

            int maxDayOfMonth = (new DateTime(actionTime.Year, actionTime.Month, 1).AddMonths(1).AddDays(-1)).Day;
            DateTime _startDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            DateTime _endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-" + maxDayOfMonth + " 23:59:59");

            int CongTyID = DatabaseCache.NhanVien.CongTyID;

            using (var db = new OosContext())
            {
                List<int> employeeType = new List<int>() { 1, 2, 3 };

                var NhanVienMoi = (from nvn in db.NhanViens.Where(x=>x.CongTyID==CongTyID)
                                   join nvm in db.NhanViens.Where(x => x.NghiViec == false && x.CongTyID==CongTyID && x.NgayBatDauLam >= _startDate && x.NgayBatDauLam <= _endDate) on nvn.NhanVienID equals nvm.NhanVienID
                                   join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                   from cv in cvs.DefaultIfEmpty()
                                   join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                   from cd in cds.DefaultIfEmpty()
                                   join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                   from pb in pbs.DefaultIfEmpty()
                                   join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                                   from ctc in ctcs.DefaultIfEmpty()
                                   select new
                                   {
                                       Id = nvn.NhanVienID,
                                       HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                       MaNhanVien = nvn.MaNhanVien,
                                       TenChucVu = cv.TenChucVu,
                                       TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                       TenChucDanh = cd.TenChucDanh,
                                       Email = nvn.Email,
                                       DienThoai = nvn.DienThoai,
                                       NgaySinh = nvn.NgaySinh,
                                       NgayBatDauLam = nvn.NgayBatDauLam,
                                       NgayNghiViec = nvn.NgayNghiViec,
                                       NgayBatDauCongTac = (DateTime?)null,
                                       NgayKetThucCongTac = (DateTime?)null,
                                       NgayBatDauNghiPhep = (DateTime?)null,
                                       NgayKetThucNghiPhep = (DateTime?)null,
                                       Type = 3,
                                       Anh = nvn.Anh
                                   }
                                );

                var NhanVienNghiViec = (from nvn in db.NhanViens
                                        join nvc in db.NhanViens.Where(x => x.NghiViec == true && x.CongTyID == CongTyID && x.NgayNghiViec >= _startDate && x.NgayNghiViec < _endDate) on nvn.NhanVienID equals nvc.NhanVienID
                                        join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                        from cv in cvs.DefaultIfEmpty()
                                        join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                        from cd in cds.DefaultIfEmpty()
                                        join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                        from pb in pbs.DefaultIfEmpty()
                                        join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                                        from ctc in ctcs.DefaultIfEmpty()
                                        select new
                                        {
                                            Id = nvn.NhanVienID,
                                            HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                            MaNhanVien = nvn.MaNhanVien,
                                            TenChucVu = cv.TenChucVu,
                                            TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                            TenChucDanh = cd.TenChucDanh,
                                            Email = nvn.Email,
                                            DienThoai = nvn.DienThoai,
                                            NgaySinh = nvn.NgaySinh,
                                            NgayBatDauLam = nvn.NgayBatDauLam,
                                            NgayNghiViec = nvn.NgayNghiViec,
                                            NgayBatDauCongTac = (DateTime?)null,
                                            NgayKetThucCongTac = (DateTime?)null,
                                            NgayBatDauNghiPhep = (DateTime?)null,
                                            NgayKetThucNghiPhep = (DateTime?)null,
                                            Type = 1,
                                            Anh = nvn.Anh
                                        }
                                );

                var NghiPhep = (from nvn in db.NhanViens.Where(x=> x.CongTyID == CongTyID)
                                join np in db.NS_QTNghiPhep.Where(x => x.XetDuyet == 1 && (
                                    (x.NgayBatDau >= actionTime && x.NgayKetThuc <= actionTimeNext) ||
                                    (x.NgayBatDau >= actionTime && x.NgayKetThuc >= actionTimeNext) ||
                                    (x.NgayBatDau < actionTime && x.NgayKetThuc >= actionTime)
                                )) on nvn.NhanVienID equals np.NhanVienID
                                join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                from cv in cvs.DefaultIfEmpty()
                                join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                from cd in cds.DefaultIfEmpty()
                                join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                from pb in pbs.DefaultIfEmpty()
                                join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                                from ctc in ctcs.DefaultIfEmpty()
                                select new
                                {
                                    Id = nvn.NhanVienID,
                                    HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                    MaNhanVien = nvn.MaNhanVien,
                                    TenChucVu = cv.TenChucVu,
                                    TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                    TenChucDanh = cd.TenChucDanh,
                                    Email = nvn.Email,
                                    DienThoai = nvn.DienThoai,
                                    NgaySinh = nvn.NgaySinh,
                                    NgayBatDauLam = nvn.NgayBatDauLam,
                                    NgayNghiViec = nvn.NgayNghiViec,
                                    NgayBatDauCongTac = (DateTime?)null,
                                    NgayKetThucCongTac = (DateTime?)null,
                                    NgayBatDauNghiPhep = np.NgayBatDau,
                                    NgayKetThucNghiPhep = np.NgayKetThuc,
                                    Type = 2,
                                    Anh = nvn.Anh
                                });

                var CongTac = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                               join ct in db.NS_QTCongTac.Where(x => x.XetDuyet == 1 && (
                                   (x.NgayBatDau >= actionTime && x.NgayKetThuc <= actionTimeNext) ||
                                   (x.NgayBatDau >= actionTime && x.NgayKetThuc >= actionTimeNext) ||
                                   (x.NgayBatDau < actionTime && x.NgayKetThuc >= actionTime)
                               )) on nvn.NhanVienID equals ct.NhanVienID
                               join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                               from cv in cvs.DefaultIfEmpty()
                               join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                               from cd in cds.DefaultIfEmpty()
                               join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                               from pb in pbs.DefaultIfEmpty()
                               join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                               from ctc in ctcs.DefaultIfEmpty()
                               select new
                               {
                                   Id = nvn.NhanVienID,
                                   HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                   MaNhanVien = nvn.MaNhanVien,
                                   TenChucVu = cv.TenChucVu,
                                   TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                   TenChucDanh = cd.TenChucDanh,
                                   Email = nvn.Email,
                                   DienThoai = nvn.DienThoai,
                                   NgaySinh = nvn.NgaySinh,
                                   NgayBatDauLam = nvn.NgayBatDauLam,
                                   NgayNghiViec = nvn.NgayNghiViec,
                                   NgayBatDauCongTac = ct.NgayBatDau,
                                   NgayKetThucCongTac = ct.NgayKetThuc,
                                   NgayBatDauNghiPhep = (DateTime?)null,
                                   NgayKetThucNghiPhep = (DateTime?)null,
                                   Type = 2,
                                   Anh = nvn.Anh
                               });
                if((NhanVienMoi != null && NhanVienMoi.Any()) || 
                    (NhanVienNghiViec != null && NhanVienNghiViec.Any()) || 
                    (NghiPhep != null && NghiPhep.Any()) || 
                    (CongTac != null && CongTac.Any()))
                {
                    if(NhanVienMoi != null && NhanVienMoi.Any())
                    {
                        var data = NhanVienMoi.OrderBy(x => x.TenPhongBan).ThenBy(x=> x.MaNhanVien).ToList();

                        if (NhanVienNghiViec != null && NhanVienMoi.Any())
                            data.AddRange(NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        if (NghiPhep != null && NghiPhep.Any())
                            data.AddRange(NghiPhep.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        if (CongTac != null && CongTac.Any())
                            data.AddRange(CongTac.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        return data;
                    }
                    else if(NhanVienNghiViec != null && NhanVienNghiViec.Any())
                    {
                        var data = NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList();

                        if (NghiPhep != null && NghiPhep.Any())
                            data.AddRange(NghiPhep.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        if (CongTac != null && CongTac.Any())
                            data.AddRange(CongTac.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        return data;
                    }else if (NghiPhep != null && NghiPhep.Any())
                    {
                        var data = NghiPhep.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList();

                        if (CongTac != null && CongTac.Any())
                            data.AddRange(CongTac.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        return data;
                    }else if(CongTac != null && CongTac.Any())
                    {
                        var data = NghiPhep.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList();
                        return data;
                    }
                }
            }
            return null;
        }

        public static async Task<object> activity(int limit)
        {
            bool IsAvatar = true;
            if (ConfigurationManager.AppSettings["IsAvatar"] != null && ConfigurationManager.AppSettings["IsAvatar"] == "0")
                IsAvatar = false;

            int checkDayNumber = -1;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("CheckDayNumber"))
            {
                int.TryParse(ConfigurationManager.AppSettings["CheckDayNumber"], out checkDayNumber);
                if (checkDayNumber <= 0)
                    checkDayNumber = 30;
            }

            DateTime actionTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime actionTimeNext = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //DateTime checkTime = actionTime.AddDays((0 - checkDayNumber));

            int maxDayOfMonth = (new DateTime(actionTime.Year, actionTime.Month, 1).AddMonths(1).AddDays(-1)).Day;
            DateTime _startDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            DateTime _endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-" + maxDayOfMonth + " 23:59:59");
            
            var CongTyID = DatabaseCache.NhanVien.CongTyID;

            using (var db = new OosContext())
            {
                List<int> employeeType = new List<int>() { 1, 2, 3 };

                var NhanVienMoi = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                                   join nvm in db.NhanViens.Where(x => x.NghiViec == false && x.NgayBatDauLam >= _startDate && x.NgayBatDauLam <= _endDate) on nvn.NhanVienID equals nvm.NhanVienID
                                   join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                   from cv in cvs.DefaultIfEmpty()
                                   join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                   from cd in cds.DefaultIfEmpty()
                                   join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                   from pb in pbs.DefaultIfEmpty()
                                   join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                                   from ctc in ctcs.DefaultIfEmpty()
                                   select new
                                   {
                                       Id = nvn.NhanVienID,
                                       HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                       MaNhanVien = nvn.MaNhanVien,
                                       TenChucVu = cv.TenChucVu,
                                       TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                       TenChucDanh = cd.TenChucDanh,
                                       Email = nvn.Email,
                                       DienThoai = nvn.DienThoai,
                                       NgaySinh = nvn.NgaySinh,
                                       NgayBatDauLam = nvn.NgayBatDauLam,
                                       NgayNghiViec = nvn.NgayNghiViec,
                                       NgayBatDauCongTac = (DateTime?)null,
                                       NgayKetThucCongTac = (DateTime?)null,
                                       NgayBatDauNghiPhep = (DateTime?)null,
                                       NgayKetThucNghiPhep = (DateTime?)null,
                                       Type = 3,
                                       Anh = nvn.Anh
                                   }
                                );

                if (NhanVienMoi != null && NhanVienMoi.Any())
                    NhanVienMoi = NhanVienMoi.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).Take(limit);

                var NhanVienNghiViec = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                                        join nvc in db.NhanViens.Where(x => x.NghiViec == true && x.NgayNghiViec >= _startDate && x.NgayNghiViec < _endDate) on nvn.NhanVienID equals nvc.NhanVienID
                                        join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                        from cv in cvs.DefaultIfEmpty()
                                        join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                        from cd in cds.DefaultIfEmpty()
                                        join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                        from pb in pbs.DefaultIfEmpty()
                                        join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                                        from ctc in ctcs.DefaultIfEmpty()
                                        select new
                                        {
                                            Id = nvn.NhanVienID,
                                            HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                            MaNhanVien = nvn.MaNhanVien,
                                            TenChucVu = cv.TenChucVu,
                                            TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                            TenChucDanh = cd.TenChucDanh,
                                            Email = nvn.Email,
                                            DienThoai = nvn.DienThoai,
                                            NgaySinh = nvn.NgaySinh,
                                            NgayBatDauLam = nvn.NgayBatDauLam,
                                            NgayNghiViec = nvn.NgayNghiViec,
                                            NgayBatDauCongTac = (DateTime?)null,
                                            NgayKetThucCongTac = (DateTime?)null,
                                            NgayBatDauNghiPhep = (DateTime?)null,
                                            NgayKetThucNghiPhep = (DateTime?)null,
                                            Type = 1,
                                            Anh = nvn.Anh
                                        }
                                );

                if (NhanVienNghiViec != null && NhanVienNghiViec.Any())
                    NhanVienNghiViec = NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).Take(limit);

                var NghiPhep = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                                join np in db.NS_QTNghiPhep.Where(x => x.XetDuyet == 1 && (
                                    (x.NgayBatDau >= actionTime && x.NgayBatDau <= actionTimeNext) ||
                                    (x.NgayBatDau <= actionTimeNext && x.NgayKetThuc >= actionTimeNext) ||
                                    (x.NgayBatDau < actionTime && x.NgayKetThuc >= actionTime)
                                )) on nvn.NhanVienID equals np.NhanVienID
                                join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                from cv in cvs.DefaultIfEmpty()
                                join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                from cd in cds.DefaultIfEmpty()
                                join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                from pb in pbs.DefaultIfEmpty()
                                join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                                from ctc in ctcs.DefaultIfEmpty()
                                select new
                                {
                                    Id = nvn.NhanVienID,
                                    HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                    MaNhanVien = nvn.MaNhanVien,
                                    TenChucVu = cv.TenChucVu,
                                    TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                    TenChucDanh = cd.TenChucDanh,
                                    Email = nvn.Email,
                                    DienThoai = nvn.DienThoai,
                                    NgaySinh = nvn.NgaySinh,
                                    NgayBatDauLam = nvn.NgayBatDauLam,
                                    NgayNghiViec = nvn.NgayNghiViec,
                                    NgayBatDauCongTac = (DateTime?)null,
                                    NgayKetThucCongTac = (DateTime?)null,
                                    NgayBatDauNghiPhep = np.NgayBatDau,
                                    NgayKetThucNghiPhep = np.NgayKetThuc,
                                    Type = 2,
                                    Anh = nvn.Anh
                                });

                var CongTac = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                               join ct in db.NS_QTCongTac.Where(x => x.XetDuyet == 1 && (
                                   (x.NgayBatDau >= actionTime && x.NgayBatDau <= actionTimeNext) ||
                                   (x.NgayBatDau <= actionTimeNext && x.NgayKetThuc >= actionTimeNext) ||
                                   (x.NgayBatDau < actionTime && x.NgayKetThuc >= actionTime)
                               )) on nvn.NhanVienID equals ct.NhanVienID
                               join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                               from cv in cvs.DefaultIfEmpty()
                               join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                               from cd in cds.DefaultIfEmpty()
                               join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                               from pb in pbs.DefaultIfEmpty()
                               join ctc in db.SYS_ThongTinCongTy on pb.CongTyID equals ctc.ID into ctcs
                               from ctc in ctcs.DefaultIfEmpty()
                               select new
                               {
                                   Id = nvn.NhanVienID,
                                   HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                   MaNhanVien = nvn.MaNhanVien,
                                   TenChucVu = cv.TenChucVu,
                                   TenPhongBan = ctc.TenCongTy.Equals(null) ? pb.Ten : ctc.TenCongTy + " - " + pb.Ten,
                                   TenChucDanh = cd.TenChucDanh,
                                   Email = nvn.Email,
                                   DienThoai = nvn.DienThoai,
                                   NgaySinh = nvn.NgaySinh,
                                   NgayBatDauLam = nvn.NgayBatDauLam,
                                   NgayNghiViec = nvn.NgayNghiViec,
                                   NgayBatDauCongTac = ct.NgayBatDau,
                                   NgayKetThucCongTac = ct.NgayKetThuc,
                                   NgayBatDauNghiPhep = (DateTime?)null,
                                   NgayKetThucNghiPhep = (DateTime?)null,
                                   Type = 2,
                                   Anh = nvn.Anh
                               });

                if ((NghiPhep != null && NghiPhep.Any()) ||
                    (CongTac != null && CongTac.Any()))
                {
                    if (NghiPhep != null && NghiPhep.Any())
                    {
                        var data = NghiPhep.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList();

                        if (CongTac != null && CongTac.Any())
                            data.AddRange(CongTac.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        data = data.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).Take(limit).ToList();

                        if (NhanVienMoi != null && NhanVienMoi.Any())
                            data.AddRange(NhanVienMoi.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        if (NhanVienNghiViec != null && NhanVienNghiViec.Any())
                            data.AddRange(NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        return data.AsEnumerable().Select(x => new
                        {
                            Id = x.Id,
                            HoVaTen = x.HoVaTen,
                            MaNhanVien = x.MaNhanVien,
                            TenChucVu = x.TenChucVu,
                            TenPhongBan = x.TenPhongBan,
                            TenChucDanh = x.TenChucDanh,
                            Email = x.Email,
                            DienThoai = x.DienThoai,
                            NgaySinh = x.NgaySinh,
                            NgayBatDauLam = x.NgayBatDauLam,
                            NgayNghiViec = x.NgayNghiViec,
                            NgayBatDauCongTac = x.NgayBatDauCongTac,
                            NgayKetThucCongTac = x.NgayKetThucCongTac,
                            NgayBatDauNghiPhep = x.NgayBatDauNghiPhep,
                            NgayKetThucNghiPhep = x.NgayKetThucNghiPhep,
                            Type = x.Type,
                            Anh = IsAvatar ? x.Anh : (object)null
                        });
                    }
                    else
                    {
                        var data = CongTac.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).Take(limit).ToList();

                        if (NhanVienMoi != null && NhanVienMoi.Any())
                            data.AddRange(NhanVienMoi.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        if (NhanVienNghiViec != null && NhanVienNghiViec.Any())
                            data.AddRange(NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                        return data.Select(x => new
                        {
                            Id = x.Id,
                            HoVaTen = x.HoVaTen,
                            MaNhanVien = x.MaNhanVien,
                            TenChucVu = x.TenChucVu,
                            TenPhongBan = x.TenPhongBan,
                            TenChucDanh = x.TenChucDanh,
                            Email = x.Email,
                            DienThoai = x.DienThoai,
                            NgaySinh = x.NgaySinh,
                            NgayBatDauLam = x.NgayBatDauLam,
                            NgayNghiViec = x.NgayNghiViec,
                            NgayBatDauCongTac = x.NgayBatDauCongTac,
                            NgayKetThucCongTac = x.NgayKetThucCongTac,
                            NgayBatDauNghiPhep = x.NgayBatDauNghiPhep,
                            NgayKetThucNghiPhep = x.NgayKetThucNghiPhep,
                            Type = x.Type,
                            Anh = IsAvatar ? x.Anh : (object)null
                        });
                    }
                }
                else
                {
                    if((NhanVienMoi != null && NhanVienMoi.Any()) ||
                    (NhanVienNghiViec != null && NhanVienNghiViec.Any()))
                    {
                        if (NhanVienMoi != null && NhanVienMoi.Any())
                        {
                            var data = NhanVienMoi.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList();

                            if (NhanVienNghiViec != null && NhanVienNghiViec.Any())
                                data.AddRange(NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList());

                            return data.Select(x => new
                            {
                                Id = x.Id,
                                HoVaTen = x.HoVaTen,
                                MaNhanVien = x.MaNhanVien,
                                TenChucVu = x.TenChucVu,
                                TenPhongBan = x.TenPhongBan,
                                TenChucDanh = x.TenChucDanh,
                                Email = x.Email,
                                DienThoai = x.DienThoai,
                                NgaySinh = x.NgaySinh,
                                NgayBatDauLam = x.NgayBatDauLam,
                                NgayNghiViec = x.NgayNghiViec,
                                NgayBatDauCongTac = x.NgayBatDauCongTac,
                                NgayKetThucCongTac = x.NgayKetThucCongTac,
                                NgayBatDauNghiPhep = x.NgayBatDauNghiPhep,
                                NgayKetThucNghiPhep = x.NgayKetThucNghiPhep,
                                Type = x.Type,
                                Anh = IsAvatar ? x.Anh : (object)null
                            });
                        }
                        else
                        {
                            var data = NhanVienNghiViec.OrderBy(x => x.TenPhongBan).ThenBy(x => x.MaNhanVien).ToList();
                            return data.Select(x => new
                            {
                                Id = x.Id,
                                HoVaTen = x.HoVaTen,
                                MaNhanVien = x.MaNhanVien,
                                TenChucVu = x.TenChucVu,
                                TenPhongBan = x.TenPhongBan,
                                TenChucDanh = x.TenChucDanh,
                                Email = x.Email,
                                DienThoai = x.DienThoai,
                                NgaySinh = x.NgaySinh,
                                NgayBatDauLam = x.NgayBatDauLam,
                                NgayNghiViec = x.NgayNghiViec,
                                NgayBatDauCongTac = x.NgayBatDauCongTac,
                                NgayKetThucCongTac = x.NgayKetThucCongTac,
                                NgayBatDauNghiPhep = x.NgayBatDauNghiPhep,
                                NgayKetThucNghiPhep = x.NgayKetThucNghiPhep,
                                Type = x.Type,
                                Anh = IsAvatar ? x.Anh : (object)null
                            });
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<object> search(string search = "")
        {
            var CongTyID = DatabaseCache.NhanVien.CongTyID;

            using (var db = new OosContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    var data = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                                join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                from cv in cvs.DefaultIfEmpty()
                                join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                from cd in cds.DefaultIfEmpty()
                                join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                from pb in pbs.DefaultIfEmpty()
                                select new
                                {
                                    Id = nvn.NhanVienID,
                                    HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                    MaNhanVien = nvn.MaNhanVien,
                                    TenChucVu = cv.TenChucVu, //nvn.TT_TenChucVu,
                                    TenPhongBan = pb.Ten, //nvn.TT_TenPhongBan,
                                    TenChucDanh = cd.TenChucDanh, //nvn.TT_TenChucDanh,
                                    Email = nvn.Email,
                                    DienThoai = nvn.DienThoai,
                                    NgaySinh = nvn.NgaySinh,
                                    NgayBatDauLam = nvn.NgayBatDauLam,
                                    Anh = nvn.Anh
                                });

                    if (data.Any())
                        return data.ToList();
                }
                else
                {
                    var data = (from nvn in db.NhanViens.Where(x => x.CongTyID == CongTyID)
                                join cv in db.NS_DsChucVu on nvn.ChucVuID equals cv.ChucVuID into cvs
                                from cv in cvs.DefaultIfEmpty()
                                join cd in db.NS_DsChucDanh on nvn.ChucDanhID equals cd.ChucDanhID into cds
                                from cd in cds.DefaultIfEmpty()
                                join pb in db.PhongBans on nvn.PhongBanID equals pb.PhongBanID into pbs
                                from pb in pbs.DefaultIfEmpty()
                                where nvn.MaNhanVien.Contains(search) || (nvn.Ho + " " + nvn.HoTen).Contains(search)
                                select new
                                {
                                    Id = nvn.NhanVienID,
                                    HoVaTen = nvn.Ho + " " + nvn.HoTen,
                                    MaNhanVien = nvn.MaNhanVien,
                                    TenChucVu = cv.TenChucVu, //nvn.TT_TenChucVu,
                                    TenPhongBan = pb.Ten, //nvn.TT_TenPhongBan,
                                    TenChucDanh = cd.TenChucDanh, //nvn.TT_TenChucDanh,
                                    Email = nvn.Email,
                                    DienThoai = nvn.DienThoai,
                                    NgaySinh = nvn.NgaySinh,
                                    NgayBatDauLam = nvn.NgayBatDauLam,
                                    Anh = nvn.Anh
                                });
                    if (data.Any())
                        return data.ToList();
                }
            }
            return null;
        }
    }
}
