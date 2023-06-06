namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_ActivityLog
	{
		[Key]
		public long ActivityLogID { get; set; }

		public int? DuAnID { get; set; }

		public int? CongViecID { get; set; }

		public int? CongViecChaID { get; set; }

		public string TieuDe { get; set; }

		public string Mota { get; set; }

		public bool? IsSaw { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? ActivityDate { get; set; }

		public DateTime RowIndex { get; set; }
	}
}
