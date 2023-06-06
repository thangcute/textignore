namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailGroup
	{
		[Key]
		public int EmailGroupID { get; set; }

		public string EmailGroupName { get; set; }

		public string Description { get; set; }
	}
}
