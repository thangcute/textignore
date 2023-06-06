namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_NangLuc
	{
		[Key]
		public int NhanVienNangLucID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NangLucID { get; set; }

		public decimal? TrongSo { get; set; }

		public decimal? DiemKyVong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
