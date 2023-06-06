namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_MonThi
	{
		[Key]
		public int MonThiID { get; set; }

		public string TenMonThi { get; set; }

		public string MaMonThi { get; set; }

		public string MoTa { get; set; }

		public string TenMonThiTA { get; set; }

		public string ValueList { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
