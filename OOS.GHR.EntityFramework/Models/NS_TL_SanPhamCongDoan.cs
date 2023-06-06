namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_SanPhamCongDoan
	{
		[Key]
		public int SanPhamCongDoanID { get; set; }

		public int? SanPhamID { get; set; }

		public int? CongDoanID { get; set; }

		public decimal? DonGia { get; set; }

		public int? ThuTu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
