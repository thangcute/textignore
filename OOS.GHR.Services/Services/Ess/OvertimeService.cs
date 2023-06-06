using OOS.GHR.EntityFramework.Contexts;
using OOS.GHR.EntityFramework.Models;
using OOS.GHR.Services.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOS.Humax.ESSService.OT;
namespace OOS.GHR.Services.Services.Ess
{
    public class OvertimeService
    {
		/// <summary>
		/// Danh sách đăng ký OT
		/// </summary>
		/// <returns>object :: List</returns>
		public static async Task<object> get(string MonthYear = "")
        {
            DateTime actionTime = DateTime.Parse(MonthYear + "-01");
            int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;

            var items = OTService.LoadDanhSachDangKyOT(actionTime, nhanvienId);


            return items.ToList().AsEnumerable().Select(x => new 
            {
                Id = x.DangKyOTID,
                Thu = x.Thu,
                NgayDangKy = x.NgayDangKy,
                NghiLe = x.IsNghile,
                CaLamViec = "",
                KyHieuChamCongID=0,
                SoGioLamThemTruocCa=0,
                TongSoGioDangKyLamThem=0,
                SoGioLamThemSauCa=0,
                BDLamThemTruocCa= x.BatDauLamThem.ToString("HH:mm"),
                KTLamThemTruocCa= x.KetThucLamThem.ToString("HH:mm"),
                BDLamThemSauCa="",
                KTLamThemSauCa="",
                LyDoTangCa=x.LyDoTangCa,
                QuyTrinhHuy= OOS.GHR.BusinessAdapter.XetDuyet.XetDuyet.XD_Available("XD_XOALAMTHEMGIO"),
                XetDuyet=x.XetDuyet,
                Approver=x.NguoiDangChoDuyet,
                ApproverComment=x.YKienXetDuyet,
                ApprovedDate = x.NgayDuyet!=null?x.NgayDuyet.Value.ToString("dd/MM/yyyy HH:mm:ss"):""
            });


            //int maxDayOfMonth = (new DateTime(actionTime.Year, actionTime.Month, 1).AddMonths(1).AddDays(-1)).Day;
            //DateTime _startDate = DateTime.Parse(MonthYear + "-01 00:00:00");
            //DateTime _endDate = DateTime.Parse(MonthYear + "-" + maxDayOfMonth + " 23:59:59");

            //int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;
            //bool QTHuy = ApproveService.Available("XD_XOALAMTHEMGIO").Result;
            //using (var db = new OosContext())
            //         {
            //	var distinctData = db.WF_QuyTrinh_ThucHien.Where(x => x.NgayThucHien != null && x.TrangThai == -1).GroupBy(x => x.XetDuyetID).Select(g => new {
            //		XetDuyetID = g.Key.Value,
            //		NgayThucHien = g.Max(x => x.NgayThucHien),
            //		QuyTrinhThucHienID = g.Max(x => x.QuyTrinhThucHienID)
            //	});

            //	List<WF_QuyTrinh_ThucHien> wfData = new List<WF_QuyTrinh_ThucHien>();
            //	if (distinctData != null)
            //	{
            //		wfData = db.WF_QuyTrinh_ThucHien.Where(x => distinctData.Select(c => c.QuyTrinhThucHienID).ToList().Contains(x.QuyTrinhThucHienID)).ToList();
            //	}

            //	List<NS_TL_NgayLe> nlData = new List<NS_TL_NgayLe>();
            //             var nlObj = db.NS_TL_NgayLe.Where(x => x.TuNgay >= _startDate && x.ToiNgay <= _endDate);
            //	if(nlObj != null && nlObj.Any())
            //             {
            //		nlData = nlObj.ToList();
            //	}

            //	var data = (from dk in db.NS_TL_DangKyCong
            //                         join ld in db.NS_DsLyDoTangCa on dk.LyDoTangCaID equals ld.LyDoTangCaID
            //				join cl in db.NS_CaLamViec on dk.CaLamViecID equals cl.CaLamViecID
            //                         join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_TL_DangKyCong") on dk.DangKyCongID equals xd.ObjectID into xds
            //				from xd in xds.DefaultIfEmpty()
            //				where dk.NhanVienID == nhanvienId
            //				&& dk.NgayChamCong >= _startDate
            //				&& dk.NgayChamCong <= _endDate
            //				orderby dk.DangKyCongID descending
            //                         select new {
            //					DangKyCongID = dk.DangKyCongID,
            //					NgayChamCong = dk.NgayChamCong,
            //					CaLamViecID = dk.CaLamViecID,
            //					TenCa = cl.TenCa,
            //					Maca = cl.MaCa,
            //					GioBatDau1 = cl.GioBatDau1,
            //					GioKetThuc1 = cl.GioKetThuc1,
            //					GioBatDau2 = cl.GioBatDau2,
            //					GioKetThuc2 = cl.GioKetThuc2,
            //					KyHieuChamCongID = dk.KyHieuChamCongID,
            //					GioLamThemTruocCa = dk.GioLamThemTruocCa,
            //					TongSoGioDangKyLamThem = dk.TongSoGioDangKyLamThem,
            //					GioLamThem = dk.GioLamThem,
            //					BDLamThemTruocCa = dk.BDLamThemTruocCa,
            //					KTLamThemTruocCa = dk.KTLamThemTruocCa,
            //					BDLamThemSauCa = dk.BDLamThemSauCa,
            //					KTLamThemSauCa = dk.KTLamThemSauCa,
            //					LyDoTangCaID = dk.LyDoTangCaID,
            //					TenLyDoTangCa = ld.TenLyDoTangCa,
            //					LyDoTangCa = dk.LyDoTangCa,
            //					XetDuyetID = (int?)xd.XetDuyetID,
            //					XetDuyet = dk.XetDuyet,
            //					YKienXetDuyet = dk.YKienXetDuyet,
            //					TrangThai = xd.TrangThai,
            //					NguoiDangChoDuyet = xd.NguoiDangChoDuyet,
            //					NguoiDuyetCuoi = xd.NguoiDuyetCuoi,
            //					GhiChu = xd.GhiChu,
            //					NgayDuyetCuoi = xd.NgayDuyetCuoi,
            //					ModifyDate = xd.ModifyDate
            //				});

            //	List<int> xdStatus = new List<int>() { -1, 1 };
            //	if (data != null && data.Any())
            //             {
            //		return data.ToList().AsEnumerable().Select(x => new
            //		{
            //			Id = x.DangKyCongID,
            //			NgayDangKy = x.NgayChamCong,
            //			Thu = DataConvert.DayOfWeek(x.NgayChamCong.Value),
            //			NghiLe = (
            //				nlData.Where(c => c.TuNgay <= x.NgayChamCong.Value && c.ToiNgay.Value.AddDays(1) > x.NgayChamCong.Value).Count() > 0 ? true : false
            //			),
            //			CaLamViec = string.Format("{0}{1}", x.Maca, (
            //				((x.GioBatDau1 ?? TimeSpan.Parse("00:00:00")).ToString() == "00:00:00" && (x.GioBatDau2 ?? TimeSpan.Parse("00:00:00")).ToString() == "00:00:00") ? "" : ":" +
            //				(
            //					(x.GioBatDau1 ?? TimeSpan.Parse("00:00:00")).ToString() != "00:00:00" ? (x.GioBatDau1.ToString().Substring(0, 5) + "-" + (
            //						(x.GioKetThuc2 ?? TimeSpan.Parse("00:00:00")).ToString() != "00:00:00" ? x.GioKetThuc2.ToString().Substring(0, 5) : x.GioKetThuc1.ToString().Substring(0, 5)
            //					)) : (x.GioBatDau2.ToString().Substring(0, 5) + "-" + x.GioKetThuc2.ToString().Substring(0, 5))
            //				)
            //			)),
            //			KyHieuChamCongID = x.KyHieuChamCongID,
            //			SoGioLamThemTruocCa = x.GioLamThemTruocCa,
            //			TongSoGioDangKyLamThem = x.TongSoGioDangKyLamThem,
            //			SoGioLamThemSauCa = x.GioLamThem,
            //			BDLamThemTruocCa = x.BDLamThemTruocCa.ToString() != "00:00:00" ? x.BDLamThemTruocCa.ToString() : "",
            //			KTLamThemTruocCa = x.KTLamThemTruocCa.ToString() != "00:00:00" ? x.KTLamThemTruocCa.ToString() : "",
            //			BDLamThemSauCa = x.BDLamThemSauCa.ToString() != "00:00:00" ? x.BDLamThemSauCa.ToString() : "",
            //			KTLamThemSauCa = x.KTLamThemSauCa.ToString() != "00:00:00" ? x.KTLamThemSauCa.ToString() : "",
            //			LyDoTangCa = x.TenLyDoTangCa,
            //			QuyTrinhHuy = QTHuy,
            //			XetDuyet = ((x.TrangThai == 100 && x.XetDuyet == 0) ? 100 : x.XetDuyet),
            //			Approver = (!string.IsNullOrEmpty(x.NguoiDangChoDuyet) ? x.NguoiDangChoDuyet : x.NguoiDuyetCuoi),
            //			ApprovedDate = (
            //				xdStatus.Contains(((x.TrangThai.GetValueOrDefault(0) == 100 && x.XetDuyet.GetValueOrDefault(0) == 0) ? 100 : x.XetDuyet.GetValueOrDefault(0))) ? (x.NgayDuyetCuoi == null ? (x.ModifyDate == null ? "" : x.ModifyDate.Value.ToString("dd/MM/yyyy HH:mm:ss")) : x.NgayDuyetCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss")) : ""
            //			),
            //			ApproverComment = (x.XetDuyet == -1 ? wfData.Where(c => c.XetDuyetID == (x.XetDuyetID ?? 0)).Select(c => c.LyDoTuChoi).FirstOrDefault() : x.GhiChu)
            //		});
            //	}
            //         }

            return null;
        }

		/// <summary>
		/// Chi tiết đăng ký OT
		/// </summary>
		/// <param name="id"></param>
		/// <returns>object</returns>
		public static async Task<object> get(int id)
		{
			int nhanvienId = OOS.GHR.Library.DatabaseCache.DangNhap.NhanVienID;

            var x = OOS.GHR.Library.NS_TL_DangKyOT.GetNS_TL_DangKyOT(id);

            var CaLV = OOS.GHR.BusinessAdapter.HSNhanSu.DBProvider.GetCaLamViec(nhanvienId, x.NgayDangKy.Value);

            OOS.GHR.Library.NS_CaLamViec caLV = new Library.NS_CaLamViec();
            if (CaLV > 0)
                caLV = OOS.GHR.Library.NS_CaLamViec.GetNS_CaLamViec(CaLV);

            var rs =  new
            {
                Id = x.DangKyOTID,
                NgayDangKy = x.NgayDangKy,
                Thu = DataConvert.DayOfWeek(x.NgayDangKy.Value),
                CaLamViecID = CaLV,
                CaLamViec = caLV.TenCa,
                KyHieuChamCongID=0,
                SoGioLamThemTruocCa=0,
                SoGioLamThemSauCa=0,
                BDLamThemTruocCa = "",
                KTLamThemTruocCa = "",
                BDLamThemSauCa = "",
                KTLamThemSauCa = "",
                LyDoTangCaID = x.LyDoTangCaID,
                LyDoTangCa = x.LyDoTangCa,
                AnTangCa = x.AnTangCa,
                IsMoiTruongDacBiet = 0,
                XetDuyet = x.XetDuyet,
                Approver = x.ApprovedBy,
                ApprovedDate = x.ApprovedDate!=null?x.ApprovedDate.Value.ToString("yyyy/MM/dd"):"",
                ApproveComment = x.YKienXetDuyet
            };

            return rs;

            //using (var db = new OosContext())
            //{
            //	var data = (from dk in db.NS_TL_DangKyCong
            //				join ld in db.NS_DsLyDoTangCa on dk.LyDoTangCaID equals ld.LyDoTangCaID
            //				join cl in db.NS_CaLamViec on dk.CaLamViecID equals cl.CaLamViecID
            //				join xd in db.SYS_XetDuyet.Where(x => x.ObjectCode == "NS_TL_DangKyCong") on dk.DangKyCongID equals xd.ObjectID into xds
            //				from xd in xds.DefaultIfEmpty()
            //				where dk.NhanVienID == nhanvienId && dk.DangKyCongID == id
            //				select new
            //				{
            //					DangKyCongID = dk.DangKyCongID,
            //					NgayChamCong = dk.NgayChamCong,
            //					CaLamViecID = dk.CaLamViecID,
            //					TenCa = cl.TenCa,
            //					Maca = cl.MaCa,
            //					GioBatDau1 = cl.GioBatDau1,
            //					GioKetThuc1 = cl.GioKetThuc1,
            //					GioBatDau2 = cl.GioBatDau2,
            //					GioKetThuc2 = cl.GioKetThuc2,
            //					KyHieuChamCongID = dk.KyHieuChamCongID,
            //					GioLamThemTruocCa = dk.GioLamThemTruocCa,
            //					GioLamThem = dk.GioLamThem,
            //					BDLamThemTruocCa = dk.BDLamThemTruocCa,
            //					KTLamThemTruocCa = dk.KTLamThemTruocCa,
            //					BDLamThemSauCa = dk.BDLamThemSauCa,
            //					KTLamThemSauCa = dk.KTLamThemSauCa,
            //					LyDoTangCaID = dk.LyDoTangCaID,
            //					TenLyDoTangCa = ld.TenLyDoTangCa,
            //					LyDoTangCa = dk.LyDoTangCa,
            //					AnTangCa = dk.AnTangCa,
            //					IsMoiTruongDacBiet = dk.IsMoiTruongDacBiet,
            //					XetDuyet = dk.XetDuyet,
            //					YKienXetDuyet = dk.YKienXetDuyet,
            //					TrangThai = xd.TrangThai,
            //					NguoiDangChoDuyet = xd.NguoiDangChoDuyet,
            //					NguoiDuyetCuoi = xd.NguoiDuyetCuoi,
            //					GhiChu = xd.GhiChu,
            //					NgayDuyetCuoi = xd.NgayDuyetCuoi
            //				});
            //	if (data != null && data.Any())
            //	{
            //		return data.ToList().AsEnumerable().Select(x => new
            //		{
            //			Id = x.DangKyCongID,
            //			NgayDangKy = x.NgayChamCong,
            //			Thu = DataConvert.DayOfWeek(x.NgayChamCong.Value),
            //			CaLamViecID = x.CaLamViecID,
            //			CaLamViec = string.Format("{0}{1}", x.Maca, (
            //				((x.GioBatDau1 ?? TimeSpan.Parse("00:00:00")).ToString() == "00:00:00" && (x.GioBatDau2 ?? TimeSpan.Parse("00:00:00")).ToString() == "00:00:00") ? "" : ":" +
            //				(
            //					(x.GioBatDau1 ?? TimeSpan.Parse("00:00:00")).ToString() != "00:00:00" ? (x.GioBatDau1.ToString().Substring(0, 5) + "-" + (
            //						(x.GioKetThuc2 ?? TimeSpan.Parse("00:00:00")).ToString() != "00:00:00" ? x.GioKetThuc2.ToString().Substring(0, 5) : x.GioKetThuc1.ToString().Substring(0, 5)
            //					)) : (x.GioBatDau2.ToString().Substring(0, 5) + "-" + x.GioKetThuc2.ToString().Substring(0, 5))
            //				)
            //			)),
            //			KyHieuChamCongID = x.KyHieuChamCongID,
            //			SoGioLamThemTruocCa = x.GioLamThemTruocCa,
            //			SoGioLamThemSauCa = x.GioLamThem,
            //			//BDLamThemTruocCa = DateTime.Parse(string.Format("2000-01-01 {0}", (x.BDLamThemTruocCa ?? TimeSpan.Parse("00:00:00")).ToString())),
            //			//KTLamThemTruocCa = DateTime.Parse(string.Format("2000-01-01 {0}", (x.KTLamThemTruocCa ?? TimeSpan.Parse("00:00:00")).ToString())),
            //			//BDLamThemSauCa = DateTime.Parse(string.Format("2000-01-01 {0}", (x.BDLamThemSauCa ?? TimeSpan.Parse("00:00:00")).ToString())),
            //			//KTLamThemSauCa = DateTime.Parse(string.Format("2000-01-01 {0}", (x.KTLamThemSauCa ?? TimeSpan.Parse("00:00:00")).ToString())),
      //      BDLamThemTruocCa = (x.BDLamThemTruocCa == null) ? (DateTime?)null : DateTime.Parse(string.Format("2000-01-01 {0}", x.BDLamThemTruocCa)),
						//KTLamThemTruocCa = (x.KTLamThemTruocCa == null) ? (DateTime?)null : DateTime.Parse(string.Format("2000-01-01 {0}", x.KTLamThemTruocCa)),
						//BDLamThemSauCa = (x.BDLamThemSauCa == null) ? (DateTime?)null : DateTime.Parse(string.Format("2000-01-01 {0}", x.BDLamThemSauCa)),
						//KTLamThemSauCa = (x.KTLamThemSauCa == null) ? (DateTime?)null : DateTime.Parse(string.Format("2000-01-01 {0}", x.KTLamThemSauCa)),
						//LyDoTangCaID = x.LyDoTangCaID,
						//LyDoTangCa = x.LyDoTangCa,
						//AnTangCa = x.AnTangCa,
						//IsMoiTruongDacBiet = x.IsMoiTruongDacBiet,
						//XetDuyet = ((x.TrangThai == 100 && x.XetDuyet == 0) ? 100 : x.XetDuyet),
						//Approver = (!string.IsNullOrEmpty(x.NguoiDangChoDuyet) ? x.NguoiDangChoDuyet : x.NguoiDuyetCuoi),
						//ApprovedDate = (x.NgayDuyetCuoi == null ? "" : x.NgayDuyetCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss")),
						//ApproveComment = x.YKienXetDuyet
            //		}).FirstOrDefault();
            //	}
            //}

            return null;
		}

		public static async Task<object> getWorkShift(int id = 0)
        {
			//sqlQuery = string.Format("SELECT [LyDoTangCaID] AS [Id], [TenLyDoTangCa] AS [Name], '' AS [Des] FROM {0}", Type);
			using (var db = new OosContext())
            {
				var data = db.NS_CaLamViec.Select(x => new { 
					Id = x.CaLamViecID,
					Name = x.TenCa,
					Des = ""
				});

				if(data != null && data.Any())
                {
					if (id > 0)
						return data.Where(x => x.Id == id).ToList();
					else
						return data.ToList();
                }
			}
			return null;
        }
	}
}
