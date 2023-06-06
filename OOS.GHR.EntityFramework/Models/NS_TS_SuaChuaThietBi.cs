namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_SuaChuaThietBi
	{
		[Key]
		public int SuaChuaThietBiID { get; set; }

		public int? ThietBiID { get; set; }

		public int? LanSuaChua { get; set; }

		public DateTime? NgaySuaChua { get; set; }

		public decimal? DonGia { get; set; }

		public string LyDoSua { get; set; }

		public string ChiTietSua { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
