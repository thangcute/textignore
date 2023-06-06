namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_TongHopChamCong
	{
		[Key]
		public int TongHopChamCongID { get; set; }

		public int? BangLuongID { get; set; }

		public int? LoaiChamCongID { get; set; }

		public float? TongSoGioLam { get; set; }
	}
}
