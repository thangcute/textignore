namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_VongThi
	{
		[Key]
		public int ChucVuVongThiID { get; set; }

		public int? ChucVuID { get; set; }

		public int? VongThiID { get; set; }

		public string GhiChu { get; set; }

		public string TenVongThi { get; set; }

		public int? STT { get; set; }

		public decimal? TongDiemYeuCau { get; set; }

		public bool? IsDaoTao { get; set; }
	}
}
