namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Activity
	{
		[Key]
		public long ActivityID { get; set; }

		public int? ToNguoiDungID { get; set; }

		public int? ToPhongBanID { get; set; }

		public int? ToChucVuID { get; set; }

		public int? ToChucDanhID { get; set; }

		public string TieuDe { get; set; }

		public string NoiDung { get; set; }

		public string NoiDungMobile { get; set; }

		public string Link { get; set; }

		public string Icon { get; set; }

		public string Module { get; set; }

		public int? ObjectID { get; set; }

		public bool? IsSaw { get; set; }

		public bool? IsSentNotify { get; set; }

		public bool? IsSentToMobile { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? DueDate { get; set; }

		public DateTime? ActivityDate { get; set; }

		public int? ActivityType { get; set; }

		public DateTime RowIndex { get; set; }
	}
}
