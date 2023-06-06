namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsTinHoc_TrinhDo
	{
		[Key]
		public int TrinhDoTinHocID { get; set; }

		public int? TinHocID { get; set; }

		public string TenTrinhDoTinHoc { get; set; }

		public string MaTrinhDoTinHoc { get; set; }

		public string TenTrinhDoTinHocTA { get; set; }

		public int? Level { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
