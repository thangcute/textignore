namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class Poll_Item
	{
		[Key]
		public int PollItemID { get; set; }

		public int? PollID { get; set; }

		public string Name { get; set; }

		public int? ViewOrder { get; set; }

		public int? TotalVotes { get; set; }
	}
}
