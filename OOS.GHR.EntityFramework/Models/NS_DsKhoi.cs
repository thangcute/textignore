namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsKhoi
	{
		[Key]
		public int KhoiID { get; set; }

		public string TenKhoi { get; set; }

		public string MaKhoi { get; set; }

		public string MoTa { get; set; }

		public int? KhoiChaID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string TenKhoiTA { get; set; }
	}
}
