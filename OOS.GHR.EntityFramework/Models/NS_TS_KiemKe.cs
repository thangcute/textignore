namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_KiemKe
	{
		[Key]
		public int KiemKeID { get; set; }

		public DateTime? NgayKiemKe { get; set; }

		public int? NhanVienKiemKeID { get; set; }

		public string GhiChu { get; set; }

		public int? PhongBanKiemKeID { get; set; }

		public string TenDotKiemKe { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
