namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_StoreChangeHistory
	{
		[Key]
		public int StoreChangeHistoryID { get; set; }

		public DateTime? NgayThayDoi { get; set; }

		public int? NguoiThayDoiID { get; set; }

		public string StoreName { get; set; }

		public string StoreContent { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
