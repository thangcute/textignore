using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Ess
{
    public class BusinessTripService
    {
        /// <summary>
        /// Danh sách cong tac
        /// </summary>
        /// <returns>object :: List</returns>
        public static async Task<object> get()
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            bool QTHuy = ApproveService.Available("XD_XOA_CONGTAC").Result;
            using (var db = new OosContext())
            {
                var distinctData = db.WF_QuyTrinh_ThucHien.Where(x => x.NgayThucHien != null && x.TrangThai == -1).GroupBy(x => x.XetDuyetID).Select(g => new {
                    XetDuyetID = g.Key.Value,
                    NgayThucHien = g.Max(x => x.NgayThucHien),
                    QuyTrinhThucHienID = g.Max(x => x.QuyTrinhThucHienID)
                });

                List<WF_QuyTrinh_ThucHien> wfData = new List<WF_QuyTrinh_ThucHien>();
                if (distinctData != null)
                {
                    wfData = db.WF_QuyTrinh_ThucHien.Where(x => distinctData.Select(c => c.QuyTrinhThucHienID).ToList().Contains(x.QuyTrinhThucHienID)).ToList();
                }

                var data = (from ct in db.NS_QTCongTac
                            join ld in db.NS_DsLyDoCongTac on ct.LyDoCongTacID equals ld.LyDoCongTacID
                            join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTCongTac") on ct.QTCongTacID equals xd.ObjectID into xds
                            from xd in xds.DefaultIfEmpty()
                            where ct.NhanVienID == nhanvienId
                            orderby ct.QTCongTacID descending
                            select new
                            {
                                QTCongTacID = ct.QTCongTacID,
                                NgayBatDau = ct.NgayBatDau,
                                NgayKetThuc = ct.NgayKetThuc,
                                LyDoCongTacID = ct.LyDoCongTacID,
                                TenLyDoCongTac = ld.TenLyDoCongTac,
                                LyDoHuyDangKy = ct.LyDoHuyDangKy,
                                NoiCongTac = ct.NoiCongTac,
                                CongViecCuThe = ct.CongViecCuThe,
                                SoNgayCongTac = ct.SoNgayCongTac,
                                GhiChus = ct.GhiChu,
                                XetDuyetID = (int?)xd.XetDuyetID,
                                XetDuyet = ct.XetDuyet,
                                TrangThai = xd.TrangThai,
                                NguoiDangChoDuyet = xd.NguoiDangChoDuyet,
                                NguoiDuyetCuoi = xd.NguoiDuyetCuoi,
                                GhiChu = xd.GhiChu,
                                NgayDuyetCuoi = xd.NgayDuyetCuoi,
                                ModifyDate = xd.ModifyDate
                            });

                List<int> xdStatus = new List<int>() { -1, 1 };
                if (data != null && data.Any())
                {
                    return data.ToList().AsEnumerable().Select(x => new
                    {
                        Id = x.QTCongTacID,
                        NgayBatDau = x.NgayBatDau,
                        NgayKetThuc = x.NgayKetThuc,
                        Thu = DataConvert.DayOfWeek(x.NgayBatDau.Value),
                        SoNgayCongTac = x.SoNgayCongTac,
                        LyDoCongTacID = x.LyDoCongTacID,
                        LyDo = x.TenLyDoCongTac,
                        NoiCongTac = x.NoiCongTac,
                        CongViecCuThe = x.CongViecCuThe,
                        QuyTrinhHuy = QTHuy,
                        XetDuyet = ((x.TrangThai == 100 && x.XetDuyet == 0) ? 100 : x.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(x.NguoiDangChoDuyet) ? x.NguoiDangChoDuyet : x.NguoiDuyetCuoi),
                        ApprovedDate = (
                            xdStatus.Contains(((x.TrangThai.GetValueOrDefault(0) == 100 && x.XetDuyet.GetValueOrDefault(0) == 0) ? 100 : x.XetDuyet.GetValueOrDefault(0))) ? (x.NgayDuyetCuoi == null ? (x.ModifyDate == null ? "" : x.ModifyDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) : x.NgayDuyetCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss")) : ""
                        ),
                        ApproverComment = (x.XetDuyet == -1 ? wfData.Where(c => c.XetDuyetID == (x.XetDuyetID ?? 0)).Select(c => c.LyDoTuChoi).FirstOrDefault() : x.GhiChu)
                    });
                }
            }
            return null;
        }

        /// <summary>
        /// Chi tiết công tác
        /// </summary>
        /// <param name="id"></param>
        /// <returns>object</returns>
        public static async Task<object> get(int id)
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            using (var db = new OosContext())
            {
                var data = (from ct in db.NS_QTCongTac
                            join ld in db.NS_DsLyDoCongTac on ct.LyDoCongTacID equals ld.LyDoCongTacID
                            join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTCongTac") on ct.QTCongTacID equals xd.ObjectID into xds
                            from xd in xds.DefaultIfEmpty()
                            where ct.NhanVienID == nhanvienId
                            && ct.QTCongTacID == id
                            select new
                            {
                                QTCongTacID = ct.QTCongTacID,
                                NgayBatDau = ct.NgayBatDau,
                                NgayKetThuc = ct.NgayKetThuc,
                                LyDoCongTacID = ct.LyDoCongTacID,
                                TenLyDoCongTac = ld.TenLyDoCongTac,
                                LyDoHuyDangKy = ct.LyDoHuyDangKy,
                                NoiCongTac = ct.NoiCongTac,
                                CongViecCuThe = ct.CongViecCuThe,
                                SoNgayCongTac = ct.SoNgayCongTac,
                                GhiChus = ct.GhiChu,
                                XetDuyet = ct.XetDuyet,
                                TrangThai = xd.TrangThai,
                                NguoiDangChoDuyet = xd.NguoiDangChoDuyet,
                                NguoiDuyetCuoi = xd.NguoiDuyetCuoi,
                                GhiChu = xd.GhiChu,
                                NgayDuyetCuoi = xd.NgayDuyetCuoi
                            });

                if (data != null && data.Any())
                {
                    return data.ToList().AsEnumerable().Select(x => new
                    {
                        Id = x.QTCongTacID,
                        NgayBatDau = x.NgayBatDau,
                        NgayKetThuc = x.NgayKetThuc,
                        SoNgayCongTac = x.SoNgayCongTac,
                        LyDoCongTacID = x.LyDoCongTacID,
                        LyDo = x.TenLyDoCongTac,
                        NoiCongTac = x.NoiCongTac,
                        CongViecCuThe = x.CongViecCuThe,
                        XetDuyet = ((x.TrangThai == 100 && x.XetDuyet == 0) ? 100 : x.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(x.NguoiDangChoDuyet) ? x.NguoiDangChoDuyet : x.NguoiDuyetCuoi),
                        ApprovedDate = (x.NgayDuyetCuoi == null ? "" : x.NgayDuyetCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss")),
                        ApproverComment = x.GhiChu
                    }).FirstOrDefault();
                }
            }
            return null;
        }

        public static async Task<NS_QTCongTac> detail(int id = 0)
        {
            NS_QTCongTac rs = new NS_QTCongTac();
            using (var db = new OosContext())
            {
                rs = await db.NS_QTCongTac.FindAsync(id);
            }
            return rs;
        }
    }
}
