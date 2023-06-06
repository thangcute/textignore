namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_MonThi
	{
		[Key]
		public int ChucVuMonThiID { get; set; }

		public int? MonThiID { get; set; }

		public int? ChucVuVongThiID { get; set; }

		public int? ChucVuID { get; set; }

		public decimal? HeSo { get; set; }
	}
}
