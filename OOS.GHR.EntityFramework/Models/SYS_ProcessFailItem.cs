namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_ProcessFailItem
	{
		[Key]
		public int FailItemID { get; set; }

		public int? ItemID { get; set; }

		public string Name { get; set; }

		public int? ProcessID { get; set; }

		public string Reason { get; set; }

		public DateTime? CreatedTime { get; set; }
	}
}
