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
    public class TrainingProcessService
    {
        public static async Task<object> get()
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            bool QTHuy = ApproveService.Available("XD_XOA_DAOTAO").Result;
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

                var data = (from dt in db.NS_QTDaoTao
                            join kq in db.NS_DT_KetQuaDaoTao on dt.KetQuaDaoTaoID equals kq.KetQuaDaoTaoID into kqs
                            from kq in kqs.DefaultIfEmpty()
                            join sys_xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_QTDaoTao") on dt.QTDaoTaoID equals sys_xd.ObjectID into sys_xds
                            from sys_xd in sys_xds.DefaultIfEmpty()
                            where dt.NhanVienID == nhanvienId
                            select new
                            {
                                QTDaoTaoID = dt.QTDaoTaoID,
                                LopDaoTao = dt.LopDaoTao,
                                NoiDungDaoTao = dt.NoiDungDaoTao,
                                TuNgay = dt.TuNgay,
                                ToiNgay = dt.ToiNgay,
                                NoiDaoTao = dt.NoiDaoTao,
                                TenKetQuaDaoTao = kq.TenKetQuaDaoTao,
                                SoThangCamKetSauDaoTao = dt.SoThangCamKetSauDaoTao,
                                NgayKetThucCamKet = "",//dt.TuNgay.Value.AddMonths(1),
                                GhiChu = dt.GhiChu,
                                XetDuyetID = (int?)sys_xd.XetDuyetID,
                                XetDuyet = dt.XetDuyet,
                                TrangThai = sys_xd.TrangThai,
                                NguoiDangChoDuyet = sys_xd.NguoiDangChoDuyet,
                                NguoiDuyetCuoi = sys_xd.NguoiDuyetCuoi,
                                GhiChuXD = sys_xd.GhiChu,
                                NgayDuyetCuoi = sys_xd.NgayDuyetCuoi,
                                ModifyDate = sys_xd.ModifyDate
                            });

                List<int> xdStatus = new List<int>() { -1, 1 };
                if (data.Any())
                    return data.ToList().AsEnumerable().Select(c => new
                    {
                        QTDaoTaoID = c.QTDaoTaoID,
                        LopDaoTao = c.LopDaoTao, //string.IsNullOrEmpty(c.LopDaoTao) ? c.NoiDungDaoTao : c.LopDaoTao,
                        TuNgay = c.TuNgay,
                        ToiNgay = c.ToiNgay,
                        NoiDaoTao = c.NoiDaoTao,
                        TenKetQuaDaoTao = c.TenKetQuaDaoTao,
                        SoThangCamKetSauDaoTao = c.SoThangCamKetSauDaoTao,
                        NgayKetThucCamKet = c.SoThangCamKetSauDaoTao.GetValueOrDefault(0) > 0 ?
                                    c.TuNgay.Value.AddMonths((int)c.SoThangCamKetSauDaoTao.GetValueOrDefault(0))
                                    .AddDays((int)(c.SoThangCamKetSauDaoTao.GetValueOrDefault(0) - (int)c.SoThangCamKetSauDaoTao.GetValueOrDefault(0)) * 30)
                                    : c.ToiNgay,
                        GhiChu = c.GhiChu,
                        QuyTrinhHuy = QTHuy,
                        XetDuyet = ((c.TrangThai == 100 && c.XetDuyet == 0) ? 100 : c.XetDuyet),
                        Approver = (!string.IsNullOrEmpty(c.NguoiDangChoDuyet) ? c.NguoiDangChoDuyet : c.NguoiDuyetCuoi),
                        ApprovedDate =(
                            xdStatus.Contains(((c.TrangThai.GetValueOrDefault(0) == 100 && c.XetDuyet.GetValueOrDefault(0) == 0) ? 100 : c.XetDuyet.GetValueOrDefault(0))) ? (c.NgayDuyetCuoi == null ? (c.ModifyDate == null ? (DateTime?)null : c.ModifyDate.Value) : c.NgayDuyetCuoi.Value) : (DateTime?)null
                        ),
                        ApproverComment = (c.XetDuyet == -1 ? wfData.Where(x => x.XetDuyetID == (c.XetDuyetID ?? 0)).Select(x => x.LyDoTuChoi).FirstOrDefault() : c.GhiChuXD)
                    });
            }
            return null;
        }

        public static async Task<object> get(int id)
        {
            using (var db = new OosContext())
            {
                var data = db.NS_QTDaoTao.Where(x => x.QTDaoTaoID == id)
                    .Select(x => new {
                        QTDaoTaoID = x.QTDaoTaoID,
                        LopDaoTao = x.LopDaoTao,
                        NoiDungDaoTao = x.NoiDungDaoTao,
                        KhoaDaoTaoID = x.KhoaDaoTaoID,
                        TuNgay = x.TuNgay,
                        ToiNgay = x.ToiNgay,
                        NoiDaoTao = x.NoiDaoTao,
                        HeDaoTaoID = x.HeDaoTaoID,
                        NganhHocID = x.NganhHocID,
                        ChuyenNganhID = x.ChuyenNganhID,
                        VanBangID = x.VanBangID,
                        ChungChiID = x.ChungChiID,
                        KetQuaDaoTaoID = x.KetQuaDaoTaoID,
                        DiemSo = x.DiemSo,
                        FileAttachment = "",
                        XetDuyet = x.XetDuyet,
                        Approver = x.ApprovedBy,
                        ApprovedDate = x.ApprovedDate,
                        ApproveComment = "",
                    }).AsEnumerable().Select(x => new
                    {
                        QTDaoTaoID = x.QTDaoTaoID,
                        LopDaoTao = x.LopDaoTao,
                        NoiDungDaoTao = x.NoiDungDaoTao,
                        KhoaDaoTaoID = x.KhoaDaoTaoID,
                        TuNgay = x.TuNgay,
                        ToiNgay = x.ToiNgay,
                        NoiDaoTao = x.NoiDaoTao,
                        HeDaoTaoID = x.HeDaoTaoID,
                        NganhHocID = x.NganhHocID,
                        ChuyenNganhID = x.ChuyenNganhID,
                        VanBangID = x.VanBangID,
                        ChungChiID = x.ChungChiID,
                        KetQuaDaoTaoID = x.KetQuaDaoTaoID,
                        DiemSo = DataConvert.StringToDecimal(x.DiemSo),
                        FileAttachment = x.FileAttachment,
                        XetDuyet = x.XetDuyet,
                        Approver = x.Approver,
                        ApprovedDate = x.ApprovedDate,
                        ApproveComment = x.ApproveComment,
                    });
                if (data.Any())
                    return data.FirstOrDefault();
            }
            return null;
        }

        public static async Task<bool> save(TrainingProcessModel model)
        {
            bool rs = false;
            var ssInfo = OOS.GHR.Library.DatabaseCache.DangNhap;
            int nhanvienId = ssInfo.NhanVienID;
            DateTime actionTime = DateTime.Now;
            //
            bool isXd = XetDuyet.XD_Available("XD_DAOTAO");
            string LopDaoTao = model.LopDaoTao;
            if (string.IsNullOrEmpty(LopDaoTao))
                LopDaoTao = model.NoiDungDaoTao;
            using (var db = new OosContext())
            {
                //using (var transaction = db.Database.BeginTransaction())
                //{
                    try
                    {
                        NS_QTDaoTao entry = db.NS_QTDaoTao.Find(model.QTDaoTaoID);
                        if (entry == null)
                            entry = new NS_QTDaoTao();

                    entry.LopDaoTao = LopDaoTao; // model.LopDaoTao;
                        entry.NoiDungDaoTao = model.NoiDungDaoTao;
                        entry.KhoaDaoTaoID = model.KhoaDaoTaoID;
                        entry.TuNgay = model.TuNgay;
                        entry.ToiNgay = model.ToiNgay;
                        entry.NoiDaoTao = model.NoiDaoTao;
                        entry.HeDaoTaoID = model.HeDaoTaoID;
                        entry.NganhHocID = model.NganhHocID;
                        entry.ChuyenNganhID = model.ChuyenNganhID;
                        entry.VanBangID = model.VanBangID;
                        entry.ChungChiID = model.ChungChiID;
                        entry.KetQuaDaoTaoID = model.KetQuaDaoTaoID;
                        entry.DiemSo = model.DiemSo.ToString();

                        if (model.QTDaoTaoID.GetValueOrDefault(0) > 0)
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
                        if (isXd)
                            entry.XetDuyet = 0;
                        else
                            entry.XetDuyet = 1;

                        db.NS_QTDaoTao.Attach(entry);
                        db.Entry(entry).State = entry.QTDaoTaoID > 0 ? EntityState.Modified : EntityState.Added;
                        await db.SaveChangesAsync();
                        //
                        if (isXd)
                        {
                            string text = Library.DatabaseCache.GetText("Xét duyệt đào tạo") + ": " + ssInfo.ND_HoVaTen;
                            if (!XetDuyet_ThucHien.AddNewXetDuyet(QUYTRINH_MALOAI.XD_DAOTAO.ToString(), nhanvienId, "@QTDaoTaoID", entry.QTDaoTaoID, text, text, "NS_QTDaoTao", null))
                            {
                                //transaction.Rollback();
                                return false;
                            }
                        }
                    rs = true;
                    }
                    catch (Exception ex)
                    {
                        //transaction.Rollback();
                    }
                //}
            }
            //
            return rs;
        }

        public static async Task<string> delete(int id)
        {
            string rs = string.Empty;
            bool isXd = XetDuyet.XD_Available("XD_DAOTAO");
            using (var db = new OosContext())
            {
                try
                {
                    NS_QTDaoTao qtdt = db.NS_QTDaoTao.Find(id);
                    if (qtdt != null)
                    {
                        if (qtdt.XetDuyet == null || qtdt.XetDuyet == 0)
                        {
                            int _QTDaoTaoID = qtdt.QTDaoTaoID;
                            // Xoa ban ghi
                            db.NS_QTDaoTao.Remove(qtdt);
                            await db.SaveChangesAsync();
                            // Xoa xet duyet
                            if (isXd)
                                OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.Delete_XetDuyet(_QTDaoTaoID, QUYTRINH_MALOAI.XD_DAOTAO);
                        }
                    }
                    else
                        rs = "Không tìm thấy thông tin khóa học";
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
