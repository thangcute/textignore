namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class Poll_PollUser
	{
		[Key]
		public int PollUserID { get; set; }

		public int? UserID { get; set; }

		public int? PollItemID { get; set; }

		public string Comment { get; set; }

		public DateTime? Date { get; set; }

		public int? Rate { get; set; }
	}
}
