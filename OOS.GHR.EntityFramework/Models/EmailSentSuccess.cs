namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailSentSuccess
	{
		[Key]
		public int EmailSendSuccessID { get; set; }

		public string Email { get; set; }

		public int? EmailContentID { get; set; }

		public DateTime? DateSent { get; set; }

		public int? TaskID { get; set; }

		public string EmailContent { get; set; }
	}
}
