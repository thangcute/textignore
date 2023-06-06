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
    public class WorkingService
    {
        public static async Task<object> get()
        {
			try
			{
				int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
				bool QTHuy = ApproveService.Available("XD_XOA_CHUYENCANBO").Result;
				using (var db = new OosContext())
				{
					var data = (from cnb in db.NS_QTChuyenCanBo where cnb.IsDeleted==false
								join pbc in db.PhongBans on cnb.PhongBanCuID equals pbc.PhongBanID into pbcs
								from pbc in pbcs.DefaultIfEmpty()
								join pbm in db.PhongBans on cnb.PhongBanMoiID equals pbm.PhongBanID into pbcm
								from pbm in pbcm.DefaultIfEmpty()
								join cvc in db.NS_DsChucVu on cnb.ChucVuCuID equals cvc.ChucVuID into cvcs
								from cvc in cvcs.DefaultIfEmpty()
								join cvm in db.NS_DsChucVu on cnb.ChucVuMoiID equals cvm.ChucVuID into cvms
								from cvm in cvms.DefaultIfEmpty()
								join cdc in db.NS_DsChucDanh on cnb.ChucDanhCuID equals cdc.ChucDanhID into cdcs
								from cdc in cdcs.DefaultIfEmpty()
								join cdm in db.NS_DsChucDanh on cnb.ChucDanhMoiID equals cdm.ChucDanhID into cdms
								from cdm in cdms.DefaultIfEmpty()
								join ldc in db.NS_DsLyDoChuyenCanBo on cnb.LyDoChuyenID equals ldc.LyDoChuyenCanBoID into ldcs
								from ldc in ldcs.DefaultIfEmpty()
								join ctc in db.SYS_ThongTinCongTy on pbm.CongTyID equals ctc.ID into ctcs
								from ctc in ctcs.DefaultIfEmpty()
								where cnb.NhanVienID == nhanvienId
								select new
								{
									Id = cnb.QTChuyenCanBoID,
									SoQuyetDinh = cnb.SoQuyetDinh,
									NgayQuyetDinh = cnb.NgayChuyen,
									NgayChuyen = cnb.NgayHieuLuc,
									NgayHetHan = cnb.NgayHetHan,
									TenPhongBanCu = pbc.Ten,
									PhongBanCuID = cnb.PhongBanCuID,
									TenChucVuCu = cvc.TenChucVu,
									ChucVuCuID = cnb.ChucVuCuID,
									TenChucDanhCu = cdc.TenChucDanh,
									ChucDanhCuID = cnb.ChucDanhCuID,
									TenPhongBanMoi = ctc.TenCongTy.Equals(null) ? pbm.Ten : ctc.TenCongTy + " - " + pbm.Ten,
									PhongBanMoiID = cnb.PhongBanMoiID,
									TenChucVuMoi = ldc.TenLyDoChuyenCanBo.Equals(null) ? cvm.TenChucVu : cvm.TenChucVu + " - " + ldc.TenLyDoChuyenCanBo,
									ChucVuMoiID = cnb.ChucVuMoiID,
									TenChucDanhMoi = ldc.TenLyDoChuyenCanBo.Equals(null) ? cdm.TenChucDanh : cdm.TenChucDanh + " - " + ldc.TenLyDoChuyenCanBo,
									ChucDanhMoiID = cnb.ChucDanhMoiID,
									TenLyDoChuyen = ldc.TenLyDoChuyenCanBo,
									LyDoChuyenID = cnb.LyDoChuyenID,
									LyDoChuyen = cnb.LyDoChuyen,
									CreatedDate = cnb.CreatedDate,
									CreatedBy = cnb.CreatedBy,
									QuyTrinhHuy = QTHuy,
									XetDuyet = cnb.XetDuyet,
									Approver = cnb.ApprovedBy,
									ApprovedDate = cnb.ApprovedDate,
									ApproveComment = "",
								});
					if (data.Any())
						return data.ToList();
				}
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
            return null;
        }

		public static async Task<object> get(int id)
        {
			using (var db = new OosContext())
            {
                try
                {
					var data = (from cnb in db.NS_QTChuyenCanBo
								where cnb.QTChuyenCanBoID == id
								select new
								{
									Id = cnb.QTChuyenCanBoID,
									SoQuyetDinh = cnb.SoQuyetDinh,
									NgayQuyetDinh = cnb.NgayChuyen,
									NgayChuyen = cnb.NgayHieuLuc,
									NgayHetHan = cnb.NgayHetHan,
									TenPhongBanCu = "",
									PhongBanCuID = cnb.PhongBanCuID,
									ChucVuCuID = cnb.ChucVuCuID,
									ChucDanhCuID = cnb.ChucDanhCuID,
									PhongBanMoiID = cnb.PhongBanMoiID,
									ChucVuMoiID = cnb.ChucVuMoiID,
									ChucDanhMoiID = cnb.ChucDanhMoiID,
									TenLyDoChuyen = "",
									LyDoChuyenID = cnb.LyDoChuyenID,
									LyDoChuyen = cnb.LyDoChuyen,
									FileAttachment = "",
									XetDuyet = cnb.XetDuyet,

								});
					if (data.Any())
						return data.FirstOrDefault();
				}
				catch(Exception ex)
                {

                }
			}
			return null;
        }

		public static async Task<bool> save(WorkingModel model)
        {
			bool rs = false;
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			using (var db = new OosContext())
            {
				NS_QTChuyenCanBo entry = db.NS_QTChuyenCanBo.Find(model.Id.GetValueOrDefault(0));
				if (entry == null)
					entry = new NS_QTChuyenCanBo();

				entry.SoQuyetDinh = model.SoQuyetDinh;
				entry.NgayChuyen = model.NgayQuyetDinh;
				entry.NgayHieuLuc = model.NgayChuyen;
				entry.NgayHetHan = model.NgayHetHan;
				entry.TenPhongBanCu = model.TenPhongBanCu;
				entry.PhongBanCuID = model.PhongBanCuID;
				entry.ChucVuCuID = model.ChucVuCuID;
				entry.ChucDanhCuID = model.ChucDanhCuID;
				entry.PhongBanMoiID = model.PhongBanMoiID;
				entry.ChucVuMoiID = model.ChucVuMoiID;
				entry.ChucDanhMoiID = model.ChucDanhMoiID;
				//entry.LyDoChuyen = model.TenLyDoChuyen;
				entry.LyDoChuyenID = model.LyDoChuyenID;
				entry.LyDoChuyen = model.LyDoChuyen;
				//entry.FileAttachment
				//entry.XetDuyet = model.SoQuyetDinh;
				if (model.Id.GetValueOrDefault(0) == 0)
					entry.NhanVienID = nhanvienId;

				db.NS_QTChuyenCanBo.Attach(entry);
				db.Entry(entry).State = model.Id.GetValueOrDefault(0) > 0 ? EntityState.Modified : EntityState.Added;
				rs = await db.SaveChangesAsync() > 0 ? true : false;
			}
			return rs;
        }
	}
}
