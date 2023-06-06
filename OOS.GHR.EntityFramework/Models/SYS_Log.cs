namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Log
	{
		[Key]
		public int LogID { get; set; }

		public string NguoiDungID { get; set; }

		public string HanhDong { get; set; }

		public string MoTa { get; set; }

		public string TenMay { get; set; }

		public DateTime? NgayThang { get; set; }

		public string Ip { get; set; }

		public string PhanHe { get; set; }
	}
}
