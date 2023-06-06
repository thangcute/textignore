namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class NS_TL_CC_DuLieuChamCong
	{
		[Key]
		public long DuLieuChamCongID { get; set; }

		public int? NhanVienID { get; set; }

		public int? MayChamCongID { get; set; }

		public DateTime? ThoiGian { get; set; }

		public bool? VaoRa { get; set; }

		public bool? NhapTay { get; set; }

		public string MaChamCong { get; set; }

		public string HoVaTen { get; set; }

		public int? PhongBanID { get; set; }

		public int? KhuVucID { get; set; }

		public bool? DuLieuKhongTongHop { get; set; }

		public string Latitude { get; set; }

		public string Longtitude { get; set; }

		public DbGeography Geo { get; set; }

		public string ImageURL { get; set; }
	}
}
