namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsThonXom
	{
		[Key]
		public int ThonXomID { get; set; }

		public string TenThonXom { get; set; }

		public string MaThonXom { get; set; }

		public int? XaPhuongID { get; set; }
	}
}
