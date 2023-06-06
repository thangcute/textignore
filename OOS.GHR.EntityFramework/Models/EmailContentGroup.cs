namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailContentGroup
	{
		[Key]
		public int EmailContentGroupID { get; set; }

		public string Name { get; set; }

		public string GroupCode { get; set; }

		public int CustomerID { get; set; }

		public string Description { get; set; }
	}
}
