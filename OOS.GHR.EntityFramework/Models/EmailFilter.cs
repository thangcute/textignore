namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailFilter
	{
		[Key]
		public int EmailFilterID { get; set; }

		public string Header { get; set; }

		public string EmailFrom { get; set; }

		public string Body { get; set; }

		public string Subject { get; set; }

		public string Keywords { get; set; }

		public string EmailTo { get; set; }
	}
}
