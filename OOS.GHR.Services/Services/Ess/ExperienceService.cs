using OOS.GHR.BusinessAdapter.XetDuyet;
using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Models.Ess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Ess
{
    public class ExperienceService
    {
        public static async Task<object> list(int id = 0)
        {
            bool QTHuy = ApproveService.Available("XD_XOA_KINHNGHIEM").Result;
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

                var data = (from ns_qtkn in db.NS_QTKinhNghiem
                            join sys_xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTKinhNghiem") on ns_qtkn.KinhNghiemID equals sys_xd.ObjectID into sys_xds
                            from sys_xd in sys_xds.DefaultIfEmpty()
                            where ns_qtkn.NhanVienID == id
                            select new
                            {
                                KinhNghiemID = ns_qtkn.KinhNghiemID,
                                NoiLamViec = ns_qtkn.NoiLamViec,
                                TuNgay = ns_qtkn.TuNgay,
                                ToiNgay = ns_qtkn.ToiNgay,
                                ChucVu = ns_qtkn.ChucVu,
                                MucLuong = ns_qtkn.MucLuong,
                                LyDoNghiViec = ns_qtkn.LyDoNghiViec,
                                XetDuyetID = (int?)sys_xd.XetDuyetID,
                                XetDuyet = ns_qtkn.XetDuyet,
                                TrangThai = sys_xd.TrangThai,
                                NguoiDangChoDuyet = sys_xd.NguoiDangChoDuyet,
                                NguoiDuyetCuoi = sys_xd.NguoiDuyetCuoi,
                                GhiChu = sys_xd.GhiChu,
                                NgayDuyetCuoi = sys_xd.NgayDuyetCuoi,
                                ModifyDate = sys_xd.ModifyDate
                            });

                List<int> xdStatus = new List<int>() { -1, 1 };
                if (data.Any())
                    return data.ToList().AsEnumerable().Select(d => new
                    {
                        KinhNghiemID = d.KinhNghiemID,
                        NoiLamViec = d.NoiLamViec,
                        TuNgay = d.TuNgay,
                        ToiNgay = d.ToiNgay,
                        ChucVu = d.ChucVu,
                        MucLuong = d.MucLuong,
                        MucLuongHienThi = string.Format("{0:#,0.00} vnđ", (d.MucLuong ?? 0)),
                        LyDoNghiViec = d.LyDoNghiViec,
                        QuyTrinhHuy = QTHuy,
                        XetDuyet = ((d.TrangThai == 100 && d.XetDuyet == 0) ? 100 : d.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(d.NguoiDangChoDuyet) ? d.NguoiDangChoDuyet : d.NguoiDuyetCuoi),
                        ApprovedDate =(
                            xdStatus.Contains(((d.TrangThai.GetValueOrDefault(0) == 100 && d.XetDuyet.GetValueOrDefault(0) == 0) ? 100 : d.XetDuyet.GetValueOrDefault(0))) ? (d.NgayDuyetCuoi == null ? (d.ModifyDate == null ? (DateTime?)null : d.ModifyDate.Value) : d.NgayDuyetCuoi.Value) : (DateTime?)null
                        ),
                        ApproverComment = (d.XetDuyet == -1 ? wfData.Where(c => c.XetDuyetID == (d.XetDuyetID ?? 0)).Select(c => c.LyDoTuChoi).FirstOrDefault() : d.GhiChu)
                    });
            }
            return null;
        }

        public static async Task<object> get(int id = 0)
        {
            using (var db = new OosContext())
            {
                var data = (from ns_qtkn in db.NS_QTKinhNghiem
                            join sys_xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTKinhNghiem") on ns_qtkn.KinhNghiemID equals sys_xd.ObjectID into sys_xds
                            from sys_xd in sys_xds.DefaultIfEmpty()
                            where ns_qtkn.KinhNghiemID == id
                            select new
                            {
                                KinhNghiemID = ns_qtkn.KinhNghiemID,
                                NoiLamViec = ns_qtkn.NoiLamViec,
                                TuNgay = ns_qtkn.TuNgay,
                                ToiNgay = ns_qtkn.ToiNgay,
                                ChucVu = ns_qtkn.ChucVu,
                                MucLuong = ns_qtkn.MucLuong,
                                NganhNgheID = ns_qtkn.NganhNgheID,
                                LyDoNghiViec = ns_qtkn.LyDoNghiViec,
                                XetDuyet = ((sys_xd.TrangThai == 100 && ns_qtkn.XetDuyet == 0) ? 100 : ns_qtkn.XetDuyet),
                                Approver = (sys_xd.TrangThai == 1 ? sys_xd.NguoiDuyetCuoi : sys_xd.NguoiDangChoDuyet),
                                ApprovedDate = sys_xd.NgayDuyetCuoi,
                                ApproveComment = sys_xd.GhiChu,
                            }).AsEnumerable().Select(d => new
                            {
                                KinhNghiemID = d.KinhNghiemID,
                                NoiLamViec = d.NoiLamViec,
                                TuNgay = d.TuNgay,
                                ToiNgay = d.ToiNgay,
                                ChucVu = d.ChucVu,
                                MucLuong = d.MucLuong,
                                MucLuongHienThi = string.Format("{0:#,0.00} vnđ", (d.MucLuong ?? 0)),
                                NganhNgheID = d.NganhNgheID,
                                LyDoNghiViec = d.LyDoNghiViec,
                                XetDuyet = d.XetDuyet,
                                Approver = d.Approver,
                                ApprovedDate = d.ApprovedDate,
                                ApproveComment = d.ApproveComment,
                            });
                if (data.Any())
                    return data.FirstOrDefault();
            }
            return null;
        }

        public static async Task<bool> save(ExperienceModel model)
        {
            bool rs = false;
            var ssInfo = OOS.GHR.Library.DatabaseCache.DangNhap;
            int NhanVienId = ssInfo.NhanVienID; //OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            DateTime actionTime = DateTime.Now;

            bool isXd = XetDuyet.XD_Available("XD_KINHNGHIEM");
            using (var db = new OosContext())
            {
                NS_QTKinhNghiem entry = new NS_QTKinhNghiem();
                entry.KinhNghiemID = model.KinhNghiemID.GetValueOrDefault(0);
                entry.NhanVienID = NhanVienId;
                entry.NoiLamViec = model.NoiLamViec;
                entry.TuNgay = model.TuNgay;
                entry.ToiNgay = model.ToiNgay;
                entry.ChucVu = model.ChucVu;
                entry.NganhNgheID = model.NganhNgheID;
                entry.MucLuong = model.MucLuong;
                entry.LyDoNghiViec = model.LyDoNghiViec;

                if(model.KinhNghiemID.GetValueOrDefault(0) > 0)
                {
                    entry.ModifyByID = NhanVienId;
                    entry.ModifyDate = actionTime;
                }
                else
                {
                    entry.CreatedByID = NhanVienId;
                    entry.CreatedDate = actionTime;
                }
                if (isXd)
                    entry.XetDuyet = 0;
                else
                    entry.XetDuyet = 1;

                db.NS_QTKinhNghiem.Attach(entry);
                db.Entry(entry).State = entry.KinhNghiemID > 0 ? EntityState.Modified : EntityState.Added;
                rs = await db.SaveChangesAsync() > 0 ? true : false;

                // check xet duyet
                if (isXd)
                {
                    string text = Library.DatabaseCache.GetText("Xét duyệt kinh nghiệm làm việc") + ": " + ssInfo.ND_HoVaTen;
                    XetDuyet_ThucHien.AddNewXetDuyet(QUYTRINH_MALOAI.XD_KINHNGHIEM.ToString(), NhanVienId, "@KinhNghiemID", entry.KinhNghiemID, text, text, "NS_QTKinhNghiem", null);
                }
            }
            return rs;
        }

        public static async Task<string> delete(int id)
        {
            string rs = string.Empty;
            bool isXd = XetDuyet.XD_Available("XD_KINHNGHIEM");
            using (var db = new OosContext())
            {
                try
                {
                    NS_QTKinhNghiem qtkn = db.NS_QTKinhNghiem.Find(id);
                    if(qtkn != null)
                    {
                        int _KinhNghiemID = qtkn.KinhNghiemID;
                        // 
                        db.NS_QTKinhNghiem.Remove(qtkn);
                        await db.SaveChangesAsync();
                        // Xet duyet
                        if(isXd)
                            OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(_KinhNghiemID, QUYTRINH_MALOAI.XD_KINHNGHIEM);
                    }
                    else
                        rs = "Không tìm thấy Quá trình Kinh nghiệm";
                }
                catch(Exception ex)
                {
                    rs = "Lỗi xử lý";
                }
            }
            return rs;
        }
    }
}
