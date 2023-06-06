namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_News_Category
	{
		[Key]
		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Description { get; set; }

		public bool? ShowHomePage { get; set; }

		public int? STT { get; set; }
	}
}
