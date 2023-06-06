namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_KhoaDaoTao
	{
		[Key]
		public int ChucVu_KhoaDaoTaoID { get; set; }

		public int? ChucVuID { get; set; }

		public int? KhoaDaoTaoID { get; set; }

		public string MoTa { get; set; }

		public int? ThoiHanDaoTao { get; set; }

		public int? ThoiHanCanhBao { get; set; }
	}
}
