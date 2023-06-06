namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_LoaiQuyTrinh
	{
		[Key]
		public string MaLoaiQuyTrinh { get; set; }

		public string TenLoaiQuyTrinh { get; set; }

		public bool? IsUse { get; set; }

		public bool? BatBuocCauHinh { get; set; }

		public bool? QuyTrinhMacDinh { get; set; }

		public string SelectQuyTrinh { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
