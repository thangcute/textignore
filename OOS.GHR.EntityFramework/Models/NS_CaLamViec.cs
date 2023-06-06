namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_CaLamViec
	{
		[Key]
		public int CaLamViecID { get; set; }

		public int? CongTyID { get; set; }

		public string TenCa { get; set; }

		public string MaCa { get; set; }

		public int? KyHieuChamCongID { get; set; }

		public TimeSpan? GioBatDau1 { get; set; }

		public TimeSpan? GioKetThuc1 { get; set; }

		public TimeSpan? GioVaoXacDinhTu { get; set; }

		public TimeSpan? GioVaoXacDinhDen { get; set; }

		public TimeSpan? GioRaXacDinhTu { get; set; }

		public TimeSpan? GioRaXacDinhDen { get; set; }

		public TimeSpan? GioBatDau2 { get; set; }

		public TimeSpan? GioKetThuc2 { get; set; }

		public int? SoPhutChoPhepDenMuon { get; set; }

		public int? SoPhutChoPhepVeSom { get; set; }

		public int? SoPhutToiThieuTinhLamThemSauCa { get; set; }

		public int? SoPhutToiThieuTinhLamThemTruocCa { get; set; }

		public int? SoPhutLamTronTinhGioLamThem { get; set; }

		public bool? RaNgoaiBiTruGio { get; set; }

		public bool? CongNghiGiuCaVaoTGLam { get; set; }

		public int? Block { get; set; }

		public int? CachTinhGio { get; set; }

		public int? ThoiGianNghiGiuaCa { get; set; }

		public int? SoPhutToiDaLamThemSauCa { get; set; }

		public int? SoPhutToiDaLamThemTruocCa { get; set; }

		public TimeSpan? ThoiGianLamThemTruocCaXacDinhTu { get; set; }

		public TimeSpan? ThoiGianLamThemSauCaXacDinhToi { get; set; }

		public TimeSpan? NghiGiuaCaTu { get; set; }

		public TimeSpan? NghiGiuaCaToi { get; set; }

		public int? Block_LamThem { get; set; }

		public bool? IsLamThubay { get; set; }

		public bool? IsLamChuNhat { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? SoPhutLamViecTieuChuan { get; set; }

		public bool? IsActive { get; set; }
	}
}
