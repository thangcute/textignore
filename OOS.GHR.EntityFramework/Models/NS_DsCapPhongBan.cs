namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsCapPhongBan
	{
		[Key]
		public int CapPhongBanID { get; set; }

		public string TenCapPhongBan { get; set; }

		public string MaCapPhongBan { get; set; }

		public string TenCapPhongBanTA { get; set; }

		public int? Level { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public bool? IsActive { get; set; }
	}
}
