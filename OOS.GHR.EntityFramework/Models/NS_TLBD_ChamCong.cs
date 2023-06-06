namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TLBD_ChamCong
	{
		[Key]
		public int ChamCongID { get; set; }

		public int? NhanVienID { get; set; }

		public int? LoaiChamCongID { get; set; }

		public DateTime? NgayChamCong { get; set; }

		public decimal? SoGioLam { get; set; }

		public int? Type { get; set; }
	}
}
