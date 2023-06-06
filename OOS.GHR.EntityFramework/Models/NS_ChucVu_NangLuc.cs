namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_NangLuc
	{
		[Key]
		public int ChucVuNangLucID { get; set; }

		public int? ChucVuID { get; set; }

		public int? NangLucID { get; set; }

		public decimal? TrongSo { get; set; }

		public bool? IsNangLucCotLoi { get; set; }

		public int? DiemKyVong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
