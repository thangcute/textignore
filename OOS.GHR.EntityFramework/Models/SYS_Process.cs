namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Process
	{
		[Key]
		public int ProcessID { get; set; }

		public string ProcessCode { get; set; }

		public string Parameters { get; set; }

		public string Des { get; set; }

		public int? TotalItems { get; set; }

		public int? CompletedItems { get; set; }

		public int? FailItems { get; set; }

		public DateTime? TimeStart { get; set; }

		public DateTime? TimeFinished { get; set; }

		public int? Status { get; set; }

		public int? CPUUsage { get; set; }

		public int? MemoryUsage { get; set; }

		public string FailItemID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
