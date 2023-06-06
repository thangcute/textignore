using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Models.Ess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Services.Ess
{
    public class TrainningService
    {
        /*
         * public static DataTable GetCourseList_Register(int NhanVienID, int ChucVuID, int ChucDanhID)
		{
			string command = string.Format("SELECT NS_DT_DotDaoTao.TrangThai, ISNULL(NS_DT_DotDaoTao.TrangThaiHocVien,0) TrangThaiHocVien, NS_DT_DotDaoTao.DotDaoTaoID, TenDotDaoTao, TuNgay, ToiNgay, \r\n            ISNULL(NS_DT_DotDaoTao.SoLuongHocVienDangKy,0) SLDangKy, ISNULL(NS_DT_DotDaoTao.SoLuongHocVien,0) SoLuongHocVien, IsBatBuocDangKy, \r\n            0 XetDuyet, 0 GV_DanhGiaID, 0 BG_DanhGiaID, 0 TC_DanhGiaID\r\n            FROM NS_DT_DotDaoTao \r\n            inner join(SELECT DISTINCT DotDaoTaoID FROM NS_DT_DotDaoTao_DangKyThamGia\r\n            WHERE (ChucVuID = {0} Or ChucDanhID = {1})) as DangKy on DangKy.DotDaoTaoID = NS_DT_DotDaoTao.DotDaoTaoID\r\n            WHERE {2} NOT IN(SELECT DISTINCT NhanVienID FROM NS_DT_DotDaoTao_NhanVien WHERE DotDaoTaoID= NS_DT_DotDaoTao.DotDaoTaoID)\r\n            AND NS_DT_DotDaoTao.TrangThai = 1", ChucVuID, ChucDanhID, NhanVienID);
			return DatabaseBase.ExecTable(command, false, "");
		}
		public static DataTable GetCourseList_Joinning(int NhanVienID, string strTrangThai = "1,2,3,4")
		{
			string command = string.Format("SELECT DISTINCT NS_DT_DotDaoTao.TrangThai, ISNULL(NS_DT_DotDaoTao.TrangThaiHocVien,0) TrangThaiHocVien, NS_DT_DotDaoTao.DotDaoTaoID, TenDotDaoTao, TuNgay, ToiNgay, \r\n            ISNULL(NS_DT_DotDaoTao.SoLuongHocVienDangKy,0) SLDangKy, ISNULL(NS_DT_DotDaoTao.SoLuongHocVien,0) SoLuongHocVien, IsBatBuocDangKy,\r\n            NS_DT_DotDaoTao_NhanVien.XetDuyet, NS_DT_DotDaoTao_NhanVien.NgayDangKy\r\n            FROM NS_DT_DotDaoTao_NhanVien \r\n            inner join NS_DT_DotDaoTao on NS_DT_DotDaoTao.DotDaoTaoID=NS_DT_DotDaoTao_NhanVien.DotDaoTaoID\r\n            WHERE NS_DT_DotDaoTao_NhanVien.NhanVienID={0} AND NS_DT_DotDaoTao.TrangThai in ({1})", NhanVienID, strTrangThai);
			return DatabaseBase.ExecTable(command, false, "");
		}
         */
        public static async Task<object> get(string type = "Openning")
        {
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            int chucvuId = OOS.GHR.Library.DatabaseCache.NhanVien.ChucVuID;
            int chucdanhId = OOS.GHR.Library.DatabaseCache.NhanVien.ChucDanhID;
            if (type == "Finished")
            {
				List<int> trangthai = new List<int>() { 5, 6 };
				using (var db = new OosContext())
                {
					var _data = (from ddtnv in db.NS_DT_DotDaoTao_NhanVien
								 join ddt in db.NS_DT_DotDaoTao on ddtnv.DotDaoTaoID equals ddt.DotDaoTaoID
								 join ddtkq in db.NS_DT_KetQuaDaoTao on ddtnv.KetQuaDaoTaoID equals ddtkq.KetQuaDaoTaoID into ddtkqs
								 from ddtkq in ddtkqs.DefaultIfEmpty()
								 where ddtnv.NhanVienID == nhanvienId
								 && trangthai.Contains(ddt.TrangThai.Value)
								 select new TrainingJoinningModel
								 {
									 Id = ddt.DotDaoTaoID,
									 TenKhoaHoc = ddt.TenDotDaoTao,
									 TuNgay = ddt.TuNgay,
									 ToiNgay = ddt.ToiNgay,
									 SoThangCamKet = ddt.SoThangCamKetDaoTao,
									 DiaDiemDaoTao = ddt.NoiDaoTao,
									 NguoiLienHe = ddt.NguoiLienHe,
									 TongDiem = ddtnv.TongDiem,
									 KetQua = ddtkq.TenKetQuaDaoTao,
									 NhanXet = ddtnv.NhanXet,
									 XetDuyet = ddtnv.XetDuyet,
									 NoiDung = (from ddtnd in db.NS_DT_DotDaoTao_NoiDung
												join ddnd in db.NS_DT_NoiDung_KetQua.Where(x => x.NhanVienID == nhanvienId) on ddtnd.NoiDungDaoTaoID equals ddnd.NoiDungDaoTaoID into ddnds
												from ddnd in ddnds.DefaultIfEmpty()
												join _ddtkq in db.NS_DT_KetQuaDaoTao on ddnd.KetQuaDaoTaoID equals _ddtkq.KetQuaDaoTaoID into _ddtkqs
												from _ddtkq in _ddtkqs.DefaultIfEmpty()
												where ddtnd.DotDaoTaoID == ddt.DotDaoTaoID
												select new TrainingContentModel
												{
													Id = ddtnd.NoiDungDaoTaoID,
													TenNoiDung = ddtnd.TenNoiDungDaoTao,
													TuNgay = ddtnd.TuNgay,
													ToiNgay = ddtnd.DenNgay,
													TongDiem = ddnd.DiemSo,
													KetQua = _ddtkq.TenKetQuaDaoTao,
													NhanXet = ddnd.NhanXet,
													TaiLieu = ddtnd.GiaoTrinhTaiLieu
												}
									 ).ToList()
								 });
					//.Distinct();
					if (_data.Any())
						return _data.ToList();
				}
				//DataTable data = Library.DatabaseBase.ExecTable(command, false, "");
				//DataTable data = OOS.GHR.BusinessAdapter.Trainning.TrainningService.GetCourseList_Joinning(nhanvienId, "5,6");
				return null;
            }
            else if(type == "Joinning")
            {
				List<int> trangthai = new List<int>() { 1, 2, 3, 4 };
				using (var db = new OosContext())
                {
					var _data = (from ddtnv in db.NS_DT_DotDaoTao_NhanVien
								 join ddt in db.NS_DT_DotDaoTao on ddtnv.DotDaoTaoID equals ddt.DotDaoTaoID
								 where ddtnv.NhanVienID == nhanvienId
								 && trangthai.Contains(ddt.TrangThai.Value)
								 select new TrainingJoinningModel
								 {
									 Id = ddt.DotDaoTaoID,
									 TenKhoaHoc = ddt.TenDotDaoTao,
									 TuNgay = ddt.TuNgay,
									 ToiNgay = ddt.ToiNgay,
									 SoThangCamKet = ddt.SoThangCamKetDaoTao,
									 DiaDiemDaoTao = ddt.NoiDaoTao,
									 NguoiLienHe = ddt.NguoiLienHe,
									 XetDuyet = ddtnv.XetDuyet,
									 TrangThai = ddtnv.TrangThai,
									 IsBatBuocDangKy = ddt.IsBatBuocDangKy,
									 NoiDung = (from ddtnd in db.NS_DT_DotDaoTao_NoiDung
                                                where ddtnd.DotDaoTaoID == ddt.DotDaoTaoID
												select new TrainingContentModel
                                                {
                                                    Id = ddtnd.NoiDungDaoTaoID,
                                                    TenNoiDung = ddtnd.TenNoiDungDaoTao,
                                                    TuNgay = ddtnd.TuNgay,
                                                    ToiNgay = ddtnd.DenNgay,
                                                    TaiLieu = ddtnd.GiaoTrinhTaiLieu
                                                }
                                     ).ToList()
                                 });
					//.Distinct();
					if (_data.Any())
                        return _data.ToList();
				}
				//DataTable data = OOS.GHR.BusinessAdapter.Trainning.TrainningService.GetCourseList_Joinning(nhanvienId);
				return null;
			}
            else
            {
				using (var db = new OosContext())
                {
					var distinctDk = db.NS_DT_DotDaoTao_DangKyThamGia.Where(x => x.ChucDanhID == chucdanhId
						|| x.ChucVuID == chucvuId
						).GroupBy(x => x.DotDaoTaoID).Select(g => new {
							DotDaoTaoID = g.Key.Value
						});

					var _data = (from ddt in db.NS_DT_DotDaoTao
								 join dk in distinctDk on ddt.DotDaoTaoID equals dk.DotDaoTaoID
								 where ddt.TrangThai == 1
								 select new
								 {
									 Id = ddt.DotDaoTaoID,
									 TenKhoaHoc = ddt.TenDotDaoTao,
									 TuNgay = ddt.TuNgay,
									 ToiNgay = ddt.ToiNgay,
									 SoThangCamKet = ddt.SoThangCamKetDaoTao,
									 DiaDiemDaoTao = ddt.NoiDaoTao,
									 IsBatBuocThamGia = ddt.IsBatBuocDangKy,
									 NguoiLienHe = ddt.NguoiLienHe,
									 NoiDung = (from ddtnd in db.NS_DT_DotDaoTao_NoiDung
												where ddtnd.DotDaoTaoID == ddt.DotDaoTaoID
												select new TrainingContentModel
												{
													Id = ddtnd.NoiDungDaoTaoID,
													TenNoiDung = ddtnd.TenNoiDungDaoTao,
													TuNgay = ddtnd.TuNgay,
													ToiNgay = ddtnd.DenNgay,
													TaiLieu = ddtnd.GiaoTrinhTaiLieu
												}),
									 DangKy = db.NS_DT_DotDaoTao_NhanVien.Where(x => x.DotDaoTaoID == ddt.DotDaoTaoID).Select(x => x.NhanVienID).ToList(),
									 XetDuyet = 0,
									 Approver = "",
									 ApprovedDate = (DateTime?)null,
									 ApproveComment = "",
								 }).AsEnumerable().Select(x => new {
									 Id = x.Id,
									 TenKhoaHoc = x.TenKhoaHoc,
									 TuNgay = x.TuNgay,
									 ToiNgay = x.ToiNgay,
									 SoThangCamKet = x.SoThangCamKet,
									 DiaDiemDaoTao = x.DiaDiemDaoTao,
									 IsBatBuocThamGia = x.IsBatBuocThamGia ?? false,
									 NguoiLienHe = x.NguoiLienHe,
									 NoiDung = x.NoiDung,
									 DangKy = (x.DangKy == null || !x.DangKy.Contains(nhanvienId)) ? 1 : 0,
									 XetDuyet = 0,
									 Approver = "",
									 ApprovedDate = (DateTime?)null,
									 ApproveComment = "",
								 });

					if(_data != null && _data.Any())
                    {
						return _data.Where(x => x.DangKy == 1).AsEnumerable().Select(x => new {
							Id = x.Id,
							TenKhoaHoc = x.TenKhoaHoc,
							TuNgay = x.TuNgay,
							ToiNgay = x.ToiNgay,
							SoThangCamKet = x.SoThangCamKet,
							DiaDiemDaoTao = x.DiaDiemDaoTao,
							IsBatBuocThamGia = x.IsBatBuocThamGia,
							NguoiLienHe = x.NguoiLienHe,
							NoiDung = x.NoiDung,
							XetDuyet = x.XetDuyet,
							Approver = x.Approver,
							ApprovedDate = x.ApprovedDate,
							ApproveComment = x.ApproveComment
						}).ToList();
					}
				}
				//string command = string.Format(@"SELECT NS_DT_DotDaoTao.DotDaoTaoID AS Id,
	   //                     TenDotDaoTao AS TenKhoaHoc,
	   //                     TuNgay,
	   //                     ToiNgay,
	   //                     NS_DT_DotDaoTao.SoThangCamKetDaoTao AS SoThangCamKet,
	   //                     NS_DT_DotDaoTao.NoiDaoTao AS DiaDiemDaoTao,
	   //                     ISNULL(IsBatBuocDangKy, 0) AS IsBatBuocThamGia,
	   //                     NS_DT_DotDaoTao.NguoiLienHe,
	   //                     0 XetDuyet
	   //                     FROM NS_DT_DotDaoTao
	   //                     inner join(SELECT DISTINCT DotDaoTaoID FROM NS_DT_DotDaoTao_DangKyThamGia
	   //                     WHERE (ChucVuID = {0} Or ChucDanhID = {1})) as DangKy on DangKy.DotDaoTaoID = NS_DT_DotDaoTao.DotDaoTaoID
	   //                     WHERE {2} NOT IN(SELECT DISTINCT NhanVienID FROM NS_DT_DotDaoTao_NhanVien WHERE DotDaoTaoID = NS_DT_DotDaoTao.DotDaoTaoID)
    //                    AND NS_DT_DotDaoTao.TrangThai = 1", chucvuId, chucdanhId, nhanvienId);
				//DataTable data = Library.DatabaseBase.ExecTable(command, false, "");
				////DataTable data = OOS.GHR.BusinessAdapter.Trainning.TrainningService.GetCourseList_Register(nhanvienId, chucvuId, chucdanhId);
				//return data;
				////--LEFT JOIN NS_DT_DotDaoTao_NhanVien as dtnv on dtnv.DotDaoTaoID = NS_DT_DotDaoTao.DotDaoTaoID and dtnv.NhanVienID={2}
			}
			return null;
		}

		public static async Task<bool> register(int id)
        {
			bool rs = false;
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			var nvInfo = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetThongTinNhanVienByID(nhanvienId);
			DateTime actionTime = DateTime.Now;
			using (var db = new OosContext())
            {
                try
                {
					NS_DT_DotDaoTao_NhanVien entry = db.NS_DT_DotDaoTao_NhanVien.Where(x => 
						x.NhanVienID == nhanvienId 
						&& x.DotDaoTaoID == id
					).FirstOrDefault();
					if(entry == null)
						entry = new NS_DT_DotDaoTao_NhanVien();
					// Them vao du lieu nhan vien dang ky hoc
					//NS_DT_DotDaoTao_NhanVien dn = new NS_DT_DotDaoTao_NhanVien();

					entry.NhanVienID = nhanvienId;
					entry.DotDaoTaoID = id;
					entry.XetDuyet = 0;
					entry.NgayDangKy = actionTime;
					entry.HoVaTen = nvInfo.HoVaTen;
					entry.TenChucVu = nvInfo.TenChucVu;
					entry.TenPhongBan = nvInfo.TenPhongBan;
					entry.TrangThai = 0;

					db.NS_DT_DotDaoTao_NhanVien.Attach(entry);
					db.Entry(entry).State = entry.DotDaoTao_NhanVienID > 0 ? EntityState.Modified : EntityState.Added;
					await db.SaveChangesAsync();

					// Them thong tin phe duyet
					OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet(
						OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_SS_DANGKY_THAMGIA_KHOADAOTAO,
						nhanvienId, "DotDaoTaoNhanVienID",
						entry.DotDaoTao_NhanVienID,
						OOS.GHR.Library.DatabaseCache.GetText("Đăng ký tham gia khóa đào tạo") + " :" + OOS.GHR.Library.NS_DT_DotDaoTao.GetTenDotDaoTaoByID(id),
						"",
						"NS_DT_DotDaoTao_NhanVien"
					);

					rs = true;
				}
				catch(Exception ex)
                {
					
                }
			}
			return rs;
        }


		public static async Task<bool> reject(int id, string reason="")
        {
			bool rs = false;
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			var nvInfo = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetThongTinNhanVienByID(nhanvienId);
			DateTime actionTime = DateTime.Now;
			using (var db = new OosContext())
            {
                try
                {
					NS_DT_DotDaoTao_NhanVien entry = db.NS_DT_DotDaoTao_NhanVien.Where(x =>
						x.NhanVienID == nhanvienId
						&& x.DotDaoTaoID == id
					).FirstOrDefault();
					if (entry == null)
						entry = new NS_DT_DotDaoTao_NhanVien();
					//NS_DT_DotDaoTao_NhanVien entry = new NS_DT_DotDaoTao_NhanVien();
					entry.NhanVienID = nhanvienId;
					entry.DotDaoTaoID = id;
					entry.XetDuyet = -2; //Từ chối tham gia
					entry.TrangThai = -2; //Từ chối tham gia
					entry.NgayDangKy = actionTime;
					entry.HoVaTen = nvInfo.HoVaTen;
					entry.TenChucVu = nvInfo.TenChucVu;
					entry.TenPhongBan = nvInfo.TenPhongBan;
					entry.GhiChu = reason;

					db.NS_DT_DotDaoTao_NhanVien.Attach(entry);
					db.Entry(entry).State = entry.DotDaoTao_NhanVienID > 0 ? EntityState.Modified : EntityState.Added;
					await db.SaveChangesAsync();

					OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet_ThucHien.AddNewXetDuyet(OOS.GHR.BusinessAdapter.XetDuyet.QUYTRINH_MALOAI.XD_SS_TUCHOI_THAMGIA_KHOADAOTAO,
						nhanvienId,
						"DotDaoTaoNhanVienID",
						entry.DotDaoTao_NhanVienID,
						OOS.GHR.Library.DatabaseCache.GetText("Từ chối tham gia khóa đào tạo") + " :" + OOS.GHR.Library.NS_DT_DotDaoTao.GetTenDotDaoTaoByID(id),
						"",
						"NS_DT_DotDaoTao_NhanVien"
					);

					rs = true;
                }catch(Exception ex)
                {

                }
            }
			return rs;
        }

		public static async Task<object> getEvaluate(int DotDaoTaoId)
        {
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			using (var db = new OosContext())
            {
				var data = from ddtnd in db.NS_DT_DotDaoTao_NoiDung
						   where ddtnd.DotDaoTaoID == DotDaoTaoId
						   select new
						   {
							   Id = ddtnd.NoiDungDaoTaoID,
							   TenNoiDung = ddtnd.TenNoiDungDaoTao,
							   TieuChiDanhGia = (from tcdg in db.NS_DT_DsTieuChiDanhGia
												 join nvdg in db.NS_DT_DotDaoTao_NhanVienDanhGia
													 .Where(x => x.NoiDungDaoTaoID == ddtnd.NoiDungDaoTaoID && x.DotDaoTaoID == DotDaoTaoId && x.NhanVienID == nhanvienId).Distinct()
													 on tcdg.TieuChiDanhGiaID equals nvdg.TieuChiDanhGiaID into nvdgs
												 from vndg in nvdgs.DefaultIfEmpty()
												 select new
												 {
													 Id = tcdg.TieuChiDanhGiaID,
													 NhomTieuChi = tcdg.NhomTieuChi,
													 TenTieuChi = tcdg.TenTieuChi,
													 DanhGia = vndg.DanhGia,
													 KieuDuLieu = tcdg.KieuDuLieu.Equals(null) ? "TextBox" : tcdg.KieuDuLieu,
													 GhiChu = tcdg.GhiChu
												 })
						   };

				if(data != null && data.Any())
					return data.ToList();
			}

			return (object)null;
        }

		public static async Task<object> getEvaluate(int id, int DotDaoTaoId = 0) // id: NoiDungKhoaDaoTaoId
        {
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			using (var db = new OosContext())
            {
				if(id > 0)
                {
					NS_DT_DotDaoTao_NoiDung nddt = db.NS_DT_DotDaoTao_NoiDung.Find(id);
					if (nddt != null)
					{
						try
						{
							var data = (from tcdg in db.NS_DT_DsTieuChiDanhGia
										join nvdg in db.NS_DT_DotDaoTao_NhanVienDanhGia
											.Where(x => x.NoiDungDaoTaoID == nddt.NoiDungDaoTaoID && x.DotDaoTaoID == nddt.DotDaoTaoID && x.NhanVienID == nhanvienId).Distinct()
											on tcdg.TieuChiDanhGiaID equals nvdg.TieuChiDanhGiaID into nvdgs
										from vndg in nvdgs.DefaultIfEmpty()
										select new
										{
											Id = tcdg.TieuChiDanhGiaID,
											NhomTieuChi = tcdg.NhomTieuChi,
											TenTieuChi = tcdg.TenTieuChi,
											DanhGia = vndg.DanhGia,
											KieuDuLieu = tcdg.KieuDuLieu.Equals(null) ? "TextBox" : tcdg.KieuDuLieu,
											GhiChu = tcdg.GhiChu
										});
							if (data.Any())
								return data.OrderBy(x => x.NhomTieuChi).ToList();
						}
						catch (Exception ex)
						{

						}
					}
                }
                else
                {
                    try
                    {
						var data = (from tcdg in db.NS_DT_DsTieuChiDanhGia
									join nvdg in db.NS_DT_DotDaoTao_NhanVienDanhGia
										.Where(x => x.NoiDungDaoTaoID == 0 && x.DotDaoTaoID == DotDaoTaoId && x.NhanVienID == nhanvienId).Distinct()
										on tcdg.TieuChiDanhGiaID equals nvdg.TieuChiDanhGiaID into nvdgs
									from vndg in nvdgs.DefaultIfEmpty()
									select new
									{
										Id = tcdg.TieuChiDanhGiaID,
										NhomTieuChi = tcdg.NhomTieuChi,
										TenTieuChi = tcdg.TenTieuChi,
										DanhGia = vndg.DanhGia,
										KieuDuLieu = tcdg.KieuDuLieu.Equals(null) ? "TextBox" : tcdg.KieuDuLieu,
										GhiChu = tcdg.GhiChu
									});
						if (data.Any())
							return data.ToList();
					}
					catch(Exception ex)
                    {

                    }
                }
            }
			return (object)null;
        }

		public static async Task<bool> saveEvaluate(int id, List<TrainingEvaluateModel> models)
        {
			bool rs = false;
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
			DateTime actionTime = DateTime.Now;
			using (var db = new OosContext())
            {
				NS_DT_DotDaoTao_NoiDung nddt = db.NS_DT_DotDaoTao_NoiDung.Find(id);
				if(nddt != null)
                {
					try
					{
                        if (models.Any())
                        {
							foreach(var model in models)
                            {
								NS_DT_DotDaoTao_NhanVienDanhGia nvdg = db.NS_DT_DotDaoTao_NhanVienDanhGia.Where(x => 
									x.NhanVienID == nhanvienId
									&& x.NoiDungDaoTaoID == nddt.NoiDungDaoTaoID
									&& x.DotDaoTaoID == nddt.DotDaoTaoID
									&& x.TieuChiDanhGiaID == model.Id
								).FirstOrDefault();

								if (nvdg == null)
									nvdg = new NS_DT_DotDaoTao_NhanVienDanhGia();

								nvdg.NhanVienID = nhanvienId;
								nvdg.NoiDungDaoTaoID = nddt.NoiDungDaoTaoID;
								nvdg.DotDaoTaoID = nddt.DotDaoTaoID;
								nvdg.TieuChiDanhGiaID = model.Id;
								nvdg.DanhGia = model.DanhGia;
								if(nvdg.NhanVienDanhGiaID < 1)
                                {
									nvdg.CreatedByID = nhanvienId;
									nvdg.CreatedDate = actionTime;
								}

								db.NS_DT_DotDaoTao_NhanVienDanhGia.Attach(nvdg);
								db.Entry(nvdg).State = nvdg.NhanVienDanhGiaID > 0 ? EntityState.Modified : EntityState.Added;
								await db.SaveChangesAsync();
							}
                        }
						rs = true;
					}
					catch (Exception ex)
					{

					}
				}
            }
			return rs;
        }
	}
}
