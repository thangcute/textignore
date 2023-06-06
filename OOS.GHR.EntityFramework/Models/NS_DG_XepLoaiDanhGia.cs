namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_XepLoaiDanhGia
	{
		[Key]
		public int XepLoaiDanhGiaID { get; set; }

		public int? NhomXepLoaiID { get; set; }

		public string TenXepLoaiDanhGia { get; set; }

		public decimal? Min { get; set; }

		public decimal? Max { get; set; }

		public int? Rating { get; set; }

		public decimal? HeSoTinhLuong { get; set; }

		public string TenXepLoaiDanhGiaTA { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
