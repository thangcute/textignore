namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsKhuVuc
	{
		[Key]
		public int KhuVucID { get; set; }

		public string TenKhuVuc { get; set; }

		public string TenKhuVucTA { get; set; }

		public string MaKhuVuc { get; set; }

		public string ProviderName { get; set; }

		public string DiaChi { get; set; }

		public decimal? Latitude { get; set; }

		public decimal? Longtitude { get; set; }

		public int? CongTyID { get; set; }

		public bool? IsActive { get; set; }
	}
}
