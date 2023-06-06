namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_ProcessLog
	{
		[Key]
		public long ProcessLogID { get; set; }

		public int? ProcessID { get; set; }

		public DateTime? CreatedTime { get; set; }

		public string Mess { get; set; }

		public int? ItemID { get; set; }
	}
}
