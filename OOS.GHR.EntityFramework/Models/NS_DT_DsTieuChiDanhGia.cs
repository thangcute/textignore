namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DsTieuChiDanhGia
	{
		[Key]
		public int TieuChiDanhGiaID { get; set; }

		public string NhomTieuChi { get; set; }

		public string TenTieuChi { get; set; }

		public string GhiChu { get; set; }

		public string KieuDuLieu { get; set; }

		public bool? IsUse { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string QueryData { get; set; }
	}
}
