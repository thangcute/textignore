namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsChuyenMon
	{
		[Key]
		public int ChuyenMonID { get; set; }

		public string HeDaoTao { get; set; }

		public string NganhDaoTao { get; set; }

		public string TenChuyenMonTA { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string Keyword { get; set; }
	}
}
