namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_NoiDung
	{
		[Key]
		public int NoiDungDaoTaoID { get; set; }

		public string TenNoiDungDaoTao { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? DonViDaoTaoID { get; set; }

		public string GiaoTrinhTaiLieu { get; set; }

		public string GiangVienDaoTao { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? DenNgay { get; set; }

		public decimal? ChiPhiCongTy { get; set; }

		public decimal? ChiPhiCaNhan { get; set; }

		public decimal? SoThangCamKetSauDaoTao { get; set; }

		public int? BoCauHoiTracNghiemID { get; set; }

		public decimal? ThoiLuongDuKien { get; set; }

		public int? GiangVienNoiBoID { get; set; }

		public int? KhoaDaoTaoID { get; set; }

		public DateTime? ThoiGianBatDauTracNghiem { get; set; }

		public DateTime? ThoiGianKetThucTracNghiem { get; set; }

		public int? SoDiemCanDat { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
