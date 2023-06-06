namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DGNL_NangLuc
	{
		[Key]
		public int NangLucID { get; set; }

		public int? NhomNangLucID { get; set; }

		public string MaNangLuc { get; set; }

		public string TenNangLuc { get; set; }

		public string MoTa { get; set; }

		public bool? IsNangLucCotLoi { get; set; }

		public bool? IsUse { get; set; }

		public decimal? TrongSo { get; set; }

		public decimal? DiemKyVong { get; set; }

		public string CongThuc { get; set; }

		public int? ThuTu { get; set; }

		public int? LinhVucID { get; set; }

		public int? MucDoUuTien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
