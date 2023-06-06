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
    public class LeaveService
    {
        /// <summary>
        /// Danh sách nghỉ phép
        /// </summary>
        /// <returns>object :: List</returns>
        public static async Task<object> get()
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            bool QTHuy = ApproveService.Available("XD_XOA_NGHIPHEP").Result;
            using (var db = new OosContext())
            {
                var distinctData = db.WF_QuyTrinh_ThucHien.Where(x => x.NgayThucHien != null && x.TrangThai == -1).GroupBy(x => x.XetDuyetID).Select(g => new {
                    XetDuyetID = g.Key.Value,
                    NgayThucHien = g.Max(x => x.NgayThucHien),
                    QuyTrinhThucHienID = g.Max(x => x.QuyTrinhThucHienID)
                });

                List<WF_QuyTrinh_ThucHien> wfData = new List<WF_QuyTrinh_ThucHien>();
                if(distinctData != null)
                {
                    wfData = db.WF_QuyTrinh_ThucHien.Where(x => distinctData.Select(c => c.QuyTrinhThucHienID).ToList().Contains(x.QuyTrinhThucHienID)).ToList();
                }

                var data = (from np in db.NS_QTNghiPhep
                            join ld in db.NS_TL_KyHieuChamCong on np.KyHieuChamCongID equals ld.KyHieuChamCongID
                            join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTNghiPhep") on np.QTNghiPhepID equals xd.ObjectID into xds
                            from xd in xds.DefaultIfEmpty()
                            where np.NhanVienID == nhanvienId
                            orderby np.QTNghiPhepID descending
                            select new
                            {
                                QTNghiPhepID = np.QTNghiPhepID,
                                NgayBatDau = np.NgayBatDau,
                                NgayKetThuc = np.NgayKetThuc,
                                LyDoNghi = np.LyDoNghi,
                                LyDoHuyDangKy = np.LyDoHuyDangKy,
                                KyHieuChamCongID = np.KyHieuChamCongID,
                                TenLyDoNghi = ld.MoTa,
                                SoNgayNghi = np.SoNgayNghi,
                                NghiPhep_GhiChu = np.GhiChu,
                                //DienGai = np.Des,
                                DienGai = np.LyDoNghi,
                                XetDuyetID = (int?)xd.XetDuyetID,
                                XetDuyet = np.XetDuyet,
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
                        Id = x.QTNghiPhepID,
                        NgayBatDau = x.NgayBatDau,
                        NgayKetThuc = x.NgayKetThuc,
                        Thu = DataConvert.DayOfWeek(x.NgayBatDau.Value),
                        LoaiNghi = x.TenLyDoNghi,
                        KyHieuChamCongID = x.KyHieuChamCongID,
                        SoNgayNghi = x.SoNgayNghi,
                        DienGiai = x.DienGai,
                        DiaDiem = "",
                        Notify = "",
                        QuyTrinhHuy = QTHuy,
                        XetDuyet = ((x.TrangThai == 100 && x.XetDuyet == 0) ? 100 : x.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(x.NguoiDangChoDuyet) ? x.NguoiDangChoDuyet : x.NguoiDuyetCuoi),
                        ApprovedDate =(
                            xdStatus.Contains(((x.TrangThai.GetValueOrDefault(0) == 100 && x.XetDuyet.GetValueOrDefault(0) == 0) ? 100 : x.XetDuyet.GetValueOrDefault(0))) ? (x.NgayDuyetCuoi == null ? (x.ModifyDate == null ? "" : x.ModifyDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) : x.NgayDuyetCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss")) : ""
                        ),
                        ApproverComment = (x.XetDuyet == -1 ? wfData.Where(c => c.XetDuyetID == (x.XetDuyetID ?? 0)).Select(c => c.LyDoTuChoi).FirstOrDefault() : x.GhiChu)
                    });
                }
            }
            return null;
        }

        /// <summary>
        /// Chi tiết nghỉ phép
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<object> get(int id, int xetduyet = -99)
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            using (var db = new OosContext())
            {
                var data = (from np in db.NS_QTNghiPhep
                            join ld in db.NS_TL_KyHieuChamCong on np.KyHieuChamCongID equals ld.KyHieuChamCongID
                            join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTNghiPhep") on np.QTNghiPhepID equals xd.ObjectID into xds
                            from xd in xds.DefaultIfEmpty()
                            where np.NhanVienID == nhanvienId
                            && np.QTNghiPhepID == id
                            select new
                            {
                                QTNghiPhepID = np.QTNghiPhepID,
                                NgayBatDau = np.NgayBatDau,
                                NgayKetThuc = np.NgayKetThuc,
                                LyDoNghi = np.LyDoNghi,
                                LyDoHuyDangKy = np.LyDoHuyDangKy,
                                KyHieuChamCongID = np.KyHieuChamCongID,
                                TenLyDoNghi = ld.MoTa,
                                SoNgayNghi = np.SoNgayNghi,
                                NghiPhep_GhiChu = np.GhiChu,
                                //DienGai = np.Des,
                                DienGai = np.LyDoNghi,
                                XetDuyet = np.XetDuyet,
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
                        Id = x.QTNghiPhepID,
                        NgayBatDau = x.NgayBatDau,
                        NgayKetThuc = x.NgayKetThuc,
                        LoaiNghi = x.TenLyDoNghi,
                        KyHieuChamCongID = x.KyHieuChamCongID,
                        SoNgayNghi = x.SoNgayNghi,
                        DienGiai = x.DienGai,
                        DiaDiem = "",
                        Notify = "",
                        XetDuyet = ((x.TrangThai == 100 && x.XetDuyet == 0) ? 100 : x.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(x.NguoiDangChoDuyet) ? x.NguoiDangChoDuyet : x.NguoiDuyetCuoi),
                        ApprovedDate = (x.NgayDuyetCuoi == null ? "" : x.NgayDuyetCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss")),
                        ApproverComment = x.GhiChu
                    }).FirstOrDefault();
                }
            }
            return null;
        }

        public static async Task<object> detail(int id = 0)
        {
            using (var db = new OosContext())
            {
                var dt = await db.NS_QTNghiPhep.FindAsync(id);
                if (dt != null)
                    return new
                    {
                        Id = dt.QTNghiPhepID,
                        NgayBatDau = dt.NgayBatDau,
                        NgayKetThuc = dt.NgayKetThuc,
                        LoaiNghi = dt.TenLoaiNghi,
                        KyHieuChamCongID = dt.KyHieuChamCongID,
                        SoNgayNghi = dt.SoNgayNghi,
                        DienGiai = dt.Des,
                        DiaDiem = "",
                        Notify = "",
                        XetDuyet = dt.XetDuyet
                    };
            }
            return null;
        }

        public static async Task<object> getReason()
        {
            using (var db = new OosContext())
            {
                var dt = db.NS_TL_KyHieuChamCong.Where(x => x.IsNghiPhep == true)
                    .Select(x => new
                    {
                        Id = x.KyHieuChamCongID,
                        Name = x.MoTa
                    });

                if (dt != null)
                    return dt.ToList();
            }
            return null;
        }
    }
}
