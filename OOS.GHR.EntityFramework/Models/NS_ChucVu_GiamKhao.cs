namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_GiamKhao
	{
		[Key]
		public int ChucVuGiamKhaoID { get; set; }

		public int? ChucVuID { get; set; }

		public int? GiamKhaoID { get; set; }

		public decimal? HeSo { get; set; }

		public string GhiChu { get; set; }
	}
}
