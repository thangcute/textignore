namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_KeHoach_PhongBan
	{
		[Key]
		public int KeHoach_PhongBanID { get; set; }

		public int? KeHoachID { get; set; }

		public int? PhongBanID { get; set; }

		public int? SoLuong { get; set; }
	}
}
