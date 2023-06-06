namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DGNL_NhomNangLuc
	{
		[Key]
		public int NhomNangLucID { get; set; }

		public string MaNhomNangLuc { get; set; }

		public string TenNhomNangLuc { get; set; }

		public bool? IsUse { get; set; }

		public int? ThuTu { get; set; }

		public decimal? TrongSo { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
