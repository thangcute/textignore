namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailSentFail
	{
		[Key]
		public int EmailSentFailID { get; set; }

		public string Email { get; set; }

		public string ContentTxt { get; set; }

		public string Subject { get; set; }

		public string Reason { get; set; }

		public bool? IsHTMLFormat { get; set; }

		public int? TaskID { get; set; }

		public DateTime? Date { get; set; }
	}
}
