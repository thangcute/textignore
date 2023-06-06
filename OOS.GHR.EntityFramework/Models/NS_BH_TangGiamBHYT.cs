namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_TangGiamBHYT
	{
		[Key]
		public int TangGiamBHYTID { get; set; }

		public DateTime? Ngay { get; set; }

		public bool? IsTang { get; set; }

		public decimal? MucDong { get; set; }

		public decimal? Tyle { get; set; }

		public int? NguoiThamGiaID { get; set; }
	}
}
