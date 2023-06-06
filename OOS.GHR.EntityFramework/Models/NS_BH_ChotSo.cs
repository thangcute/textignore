namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_ChotSo
	{
		[Key]
		public int ChotSoID { get; set; }

		public int? BHXH_SL { get; set; }

		public decimal? BHXH_QL { get; set; }

		public decimal? BHXH_PD { get; set; }

		public int? BHYT_SL { get; set; }

		public decimal? BHYT_QL { get; set; }

		public decimal? BHYT_PD { get; set; }

		public int? BHTN_SL { get; set; }

		public decimal? BHTN_QL { get; set; }

		public decimal? BHTN_PD { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public int? DonViID { get; set; }

		public int? ChotSo { get; set; }

		public decimal? BHXL_QL { get; set; }
	}
}
