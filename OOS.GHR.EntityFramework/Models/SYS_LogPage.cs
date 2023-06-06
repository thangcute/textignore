namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_LogPage
	{
		[Key]
		public int LogPageID { get; set; }

		public int? NguoiDungID { get; set; }

		public string Page { get; set; }

		public string Parameters { get; set; }

		public DateTime? AccessDate { get; set; }

		public string Des { get; set; }

		public string DescTA { get; set; }
	}
}
