namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsLyDoThayDoiLuong
	{
		[Key]
		public int LyDoThayDoiLuongID { get; set; }

		public string TenLyDoThayDoiLuong { get; set; }

		public string TenLyDoThayDoiLuongTA { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public bool? IsActive { get; set; }
	}
}
