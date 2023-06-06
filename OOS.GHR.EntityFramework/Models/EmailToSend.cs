namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailToSend
	{
		[Key]
		public int EmailToSendID { get; set; }

		public int? ContentID { get; set; }

		public string ContentTxt { get; set; }

		public string Email { get; set; }

		public string EmailQuery { get; set; }

		public int? EmailProfileID { get; set; }

		public string ParameterQuery { get; set; }

		public string Subject { get; set; }

		public int? Status { get; set; }

		public int CustomerID { get; set; }

		public string CC { get; set; }

		public string BCC { get; set; }

		public DateTime? SentDate { get; set; }
	}
}
