namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_LichSuThayDoiThongTinChiTiet
	{
		[Key]
		public int LichSuThayDoiThongTinChiTietID { get; set; }

		public int? LichSuThayDoiThongTinID { get; set; }

		public string TruongThongTin { get; set; }

		public string GiaTriCu { get; set; }

		public string GiaTriMoi { get; set; }

		public string MoTaGiaTriCu { get; set; }

		public string MoTaGiaTriMoi { get; set; }

		public string KieuDuLieu { get; set; }

		public decimal? GiaTriDouble { get; set; }

		public DateTime? GiaTriDateTime { get; set; }
	}
}
