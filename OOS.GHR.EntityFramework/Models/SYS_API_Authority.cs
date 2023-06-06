namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_API_Authority
	{
		[Key]
		public int APIAuthorityID { get; set; }

		public int? APIID { get; set; }

		public string CodeName { get; set; }

		public string StoreView { get; set; }

		public bool? Enable { get; set; }

		public DateTime? DueDate { get; set; }

		public DateTime? ActiveDate { get; set; }
	}
}
