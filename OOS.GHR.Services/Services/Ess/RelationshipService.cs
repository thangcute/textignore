using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Models.Ess;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OOS.GHR.Services.Services.Ess
{
    public class RelationshipService
    {
        public static async Task<object> get()
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            bool QTHuy = ApproveService.Available("XD_XOA_NHANTHAN").Result;
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

                var data = (from dt in db.NS_QTQuanHeGiaDinh
                            join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTQuanHeGiaDinh") on dt.QTQuanHeGiaDinhID equals xd.ObjectID into xds
                            from xd in xds.DefaultIfEmpty()
                            where dt.NhanVienID == nhanvienId
                            select new
                            {
                                QTQuanHeGiaDinhID = dt.QTQuanHeGiaDinhID,
                                HoTen = dt.HoTen,
                                QuanHe = dt.QuanHe,
                                NgaySinh = dt.NgaySinh,
                                GiamTru = dt.GiamTru,
                                NgayBatDauGiamTru = dt.NgayBatDauGiamTru,
                                //
                                GioiTinh = dt.GioiTinh,
                                CMND = dt.CMTND,
                                DienThoaiLienHe = dt.DienThoaiLienHe,
                                DiaChiThuongTru = dt.DiaChiThuongTru,
                                NgheNghiepHienNay = dt.NgheNghiepHienNay,
                                MaSoThue = dt.MaSoThue,
                                GiayKhaiSinhSo = dt.GiayKhaiSinhSo,
                                NoiDangKyGiayKhaiSinh = dt.NoiDangKyGiayKhaiSinh,
                                QuocTich = dt.QuocTich,
                                QuanHe_ChuHo = dt.QuanHe_ChuHo,
                                LoaiGiayToID = dt.LoaiGiayToID,
                                SoHoKhau = dt.SoHoKhau,
                                QuyenSo = dt.QuyenSo,
                                MaSoBHXH = dt.MaSoBHXH,
                                GhiChu = dt.GhiChu,
                                //
                                XetDuyetID = (int?)xd.XetDuyetID,
                                XetDuyet = dt.XetDuyet,
                                TrangThai = xd.TrangThai,
                                NguoiDangChoDuyet = xd.NguoiDangChoDuyet,
                                NguoiDuyetCuoi = xd.NguoiDuyetCuoi,
                                GhiChuXD = xd.GhiChu,
                                NgayDuyetCuoi = xd.NgayDuyetCuoi,
                                ModifyDate = xd.ModifyDate
                            });

                List<int> xdStatus = new List<int>() { -1, 1 };
                if (data.Any())
                    return data.ToList().AsEnumerable().Select(d => new {
                        QTQuanHeGiaDinhID = d.QTQuanHeGiaDinhID,
                        HoTen = d.HoTen,
                        QuanHe = d.QuanHe,
                        NgaySinh = d.NgaySinh,
                        GiamTru = d.GiamTru,
                        NgayBatDauGiamTru = d.NgayBatDauGiamTru,
                        //
                        GioiTinh = d.GioiTinh,
                        CMND = d.CMND,
                        DienThoaiLienHe = d.DienThoaiLienHe,
                        DiaChiThuongTru = d.DiaChiThuongTru,
                        NgheNghiepHienNay = d.NgheNghiepHienNay,
                        MaSoThue = d.MaSoThue,
                        GiayKhaiSinhSo = d.GiayKhaiSinhSo,
                        NoiDangKyGiayKhaiSinh = d.NoiDangKyGiayKhaiSinh,
                        QuocTich = d.QuocTich,
                        QuanHe_ChuHo = d.QuanHe_ChuHo,
                        LoaiGiayToID = d.LoaiGiayToID,
                        SoHoKhau = d.SoHoKhau,
                        QuyenSo = d.QuyenSo,
                        MaSoBHXH = d.MaSoBHXH,
                        GhiChu = d.GhiChu,
                        QuyTrinhHuy = QTHuy,
                        //
                        XetDuyet = ((d.TrangThai == 100 && d.XetDuyet == 0) ? 100 : d.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(d.NguoiDangChoDuyet) ? d.NguoiDangChoDuyet : d.NguoiDuyetCuoi),
                        ApprovedDate = (
                            xdStatus.Contains((d.TrangThai.GetValueOrDefault(0) == 100 && d.XetDuyet.GetValueOrDefault(0) == 0) ? 100 : d.XetDuyet.GetValueOrDefault(0)) ? (d.NgayDuyetCuoi == null ? (d.ModifyDate == null ? (DateTime?)null : d.ModifyDate.Value) : d.NgayDuyetCuoi.Value) : (DateTime?)null
                        ),
                        ApproverComment = (d.XetDuyet == -1 ? wfData.Where(c => c.XetDuyetID == (d.XetDuyetID ?? 0)).Select(c => c.LyDoTuChoi).FirstOrDefault() : d.GhiChuXD)
                    });
            }
            return null;
        }

        public static async Task<object> get(int id)
        {
            using (var db = new OosContext())
            {
                var data = (from qh in db.NS_QTQuanHeGiaDinh
                            join xp in db.NS_DsXaPhuong on qh.XaPhuongID equals xp.XaPhuongID into xps
                            from xp in xps.DefaultIfEmpty()
                            where qh.QTQuanHeGiaDinhID == id
                            select new
                            {
                                QTQuanHeGiaDinhID = qh.QTQuanHeGiaDinhID,
                                QuanHe = qh.QuanHe,
                                HoTen = qh.HoTen,
                                NgaySinh = qh.NgaySinh,
                                GioiTinh = qh.GioiTinh,
                                GiamTru = qh.GiamTru,
                                CMND = qh.CMTND,
                                DienThoaiLienHe = qh.DienThoaiLienHe,
                                DiaChiThuongTru = qh.DiaChiThuongTru,
                                NgheNghiepHienNay = qh.NgheNghiepHienNay,
                                MaSoThue = qh.MaSoThue,
                                GiayKhaiSinhSo = qh.GiayKhaiSinhSo,
                                NoiDangKyGiayKhaiSinh = qh.NoiDangKyGiayKhaiSinh,
                                QuocTich = qh.QuocTich,
                                //QuocTichID = x.QuocTich,
                                TinhID = qh.TinhID,
                                QuanHuyenID = qh.QuanHuyenID,
                                XaPhuongID = qh.XaPhuongID,
                                TenXaPhuong = xp.TenXaPhuong,
                                QuanHe_ChuHo = qh.QuanHe_ChuHo,
                                LoaiGiayToID = qh.LoaiGiayToID,
                                SoHoKhau = qh.SoHoKhau,
                                QuyenSo = qh.QuyenSo,
                                MaSoBHXH = qh.MaSoBHXH,
                                GhiChu = qh.GhiChu,
                                XetDuyet = qh.XetDuyet,
                                Approver = qh.ApprovedBy,
                                ApprovedDate = qh.ApprovedDate,
                                ApproveComment = "",
                                FileAttachment = "",
                            });
                    //db.NS_QTQuanHeGiaDinh.Where(x => x.QTQuanHeGiaDinhID == id)
                    //.Select(x => new
                    //{
                    //    QTQuanHeGiaDinhID = x.QTQuanHeGiaDinhID,
                    //    QuanHe = x.QuanHe,
                    //    HoTen = x.HoTen,
                    //    NgaySinh = x.NgaySinh,
                    //    GioiTinh = x.GioiTinh,
                    //    GiamTru = x.GiamTru,
                    //    CMND = x.CMTND,
                    //    DienThoaiLienHe = x.DienThoaiLienHe,
                    //    DiaChiThuongTru = x.DiaChiThuongTru,
                    //    NgheNghiepHienNay = x.NgheNghiepHienNay,
                    //    MaSoThue = x.MaSoThue,
                    //    GiayKhaiSinhSo = x.GiayKhaiSinhSo,
                    //    NoiDangKyGiayKhaiSinh = x.NoiDangKyGiayKhaiSinh,
                    //    QuocTich = x.QuocTich,
                    //    //QuocTichID = x.QuocTich,
                    //    TinhID = x.TinhID,
                    //    QuanHuyenID = x.QuanHuyenID,
                    //    XaPhuongID = x.XaPhuongID,
                    //    QuanHe_ChuHo = x.QuanHe_ChuHo,
                    //    LoaiGiayToID = x.LoaiGiayToID,
                    //    SoHoKhau = x.SoHoKhau,
                    //    QuyenSo = x.QuyenSo,
                    //    MaSoBHXH = x.MaSoBHXH,
                    //    GhiChu = x.GhiChu,
                    //    XetDuyet = x.XetDuyet,
                    //    Approver = x.ApprovedBy,
                    //    ApprovedDate = x.ApprovedDate,
                    //    ApproveComment = "",
                    //    FileAttachment = "",
                    //});
                if (data.Any())
                    return data.FirstOrDefault();
            }
            return null;
        }

        public static async Task<bool> save(RelationshipModel model)
        {
            bool rs = false;
            var ssInfo = OOS.GHR.Library.DatabaseCache.DangNhap;
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            DateTime actionTime = DateTime.Now;

            //BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_QHNT
            bool isXd = XetDuyet.XD_Available("XD_QHNT");
            using (var db = new OosContext())
            {
                try
                {
                    int TinhID = 0;
                    int QuanHuyenID = 0;
                    int XaPhuongID = model.XaPhuongID ?? 0;
                    NS_DsXaPhuong xp = db.NS_DsXaPhuong.Find(XaPhuongID);
                    if (xp != null)
                    {
                        TinhID = xp.TinhID ?? 0;
                        QuanHuyenID = xp.QuanHuyenID ?? 0;
                    }

                    NS_QTQuanHeGiaDinh entry = db.NS_QTQuanHeGiaDinh.Find(model.QTQuanHeGiaDinhID);
                    if (entry == null)
                        entry = new NS_QTQuanHeGiaDinh();

                    entry.QuanHe = model.QuanHe;
                    entry.HoTen = model.HoTen;
                    entry.NgaySinh = model.NgaySinh;
                    entry.GioiTinh = model.GioiTinh;
                    entry.CMTND = model.CMND;
                    entry.DienThoaiLienHe = model.DienThoaiLienHe;
                    entry.DiaChiThuongTru = model.DiaChiThuongTru;
                    entry.NgheNghiepHienNay = model.NgheNghiepHienNay;
                    entry.MaSoThue = model.MaSoThue;
                    entry.GiayKhaiSinhSo = model.GiayKhaiSinhSo;
                    entry.NoiDangKyGiayKhaiSinh = model.NoiDangKyGiayKhaiSinh;
                    entry.QuocTich = model.QuocTich;
                    entry.TinhID = TinhID;
                    entry.QuanHuyenID = QuanHuyenID;
                    entry.XaPhuongID = XaPhuongID;
                    entry.QuanHe_ChuHo = model.QuanHe_ChuHo;
                    entry.LoaiGiayToID = model.LoaiGiayToID;
                    entry.SoHoKhau = model.SoHoKhau;
                    entry.QuyenSo = model.QuyenSo;
                    entry.MaSoBHXH = model.MaSoBHXH;
                    entry.GhiChu = model.GhiChu;
                    if (isXd)
                        entry.XetDuyet = 0;
                    else
                        entry.XetDuyet = 1;

                    entry.ApprovedBy = "";

                    if(model.QTQuanHeGiaDinhID > 0)
                    {
                        entry.ModifyByID = nhanvienId;
                        entry.ModifyDate = actionTime;
                    }
                    else
                    {
                        entry.NhanVienID = nhanvienId;
                        entry.CreatedByID = nhanvienId;
                        entry.CreatedDate = actionTime;
                    }

                    db.NS_QTQuanHeGiaDinh.Attach(entry);
                    db.Entry(entry).State = entry.QTQuanHeGiaDinhID > 0 ? EntityState.Modified : EntityState.Added;
                    await db.SaveChangesAsync();

                    // check xet duyet
                    if (isXd)
                    {
                        string text = Library.DatabaseCache.GetText("Xét duyệt Quan hệ Nhân thân") + ": " + ssInfo.ND_HoVaTen;
                        XetDuyet_ThucHien.AddNewXetDuyet(QUYTRINH_MALOAI.XD_QHNT.ToString(), nhanvienId, "@QTQuanHeGiaDinhID", entry.QTQuanHeGiaDinhID, text, text, "NS_QTQuanHeGiaDinh", null);
                    }

                    rs = true;
                }
                catch(Exception ex)
                {

                }
            }
            return rs;
        }

        public static async Task<string> delete(int id)
        {
            string rs = string.Empty;
            bool isXd = XetDuyet.XD_Available("XD_QHNT");
            using (var db = new OosContext())
            {
                try
                {
                    NS_QTQuanHeGiaDinh qhgd = db.NS_QTQuanHeGiaDinh.Find(id);
                    if (qhgd != null)
                    {
                        int _QTQuanHeGiaDinhID = qhgd.QTQuanHeGiaDinhID;
                        // 
                        db.NS_QTQuanHeGiaDinh.Remove(qhgd);
                        await db.SaveChangesAsync();
                        // Xet duyet
                        if (isXd)
                            OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(_QTQuanHeGiaDinhID, QUYTRINH_MALOAI.XD_QHNT);
                    }
                    else
                        rs = "Không tìm thấy Quan hệ Nhân thân";
                }
                catch (Exception ex)
                {
                    rs = "Lỗi xử lý";
                }
            }
            return rs;
        }
    }
}
