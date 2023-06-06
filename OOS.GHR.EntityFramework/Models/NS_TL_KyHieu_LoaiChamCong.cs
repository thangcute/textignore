namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_KyHieu_LoaiChamCong
	{
		[Key]
		public int KyHieu_LoaiChamCongID { get; set; }

		public int? KyHieuChamCongID { get; set; }

		public int? LoaiChamCongID { get; set; }

		public decimal? SoGioLam { get; set; }

		public decimal? SoGioLamNgayLe { get; set; }
	}
}
