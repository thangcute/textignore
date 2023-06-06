namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailInbox
	{
		[Key]
		public int InboxID { get; set; }

		public int? ProfileID { get; set; }

		public int? EmailGroupID { get; set; }

		public int? ThiSinhID { get; set; }

		public string EmailFrom { get; set; }

		public string EmailTo { get; set; }

		public string EmailContent { get; set; }

		public string Subject { get; set; }

		public string CC { get; set; }

		public string BCC { get; set; }

		public DateTime? SentDate { get; set; }

		public DateTime? ReceiveDate { get; set; }

		public string FromName { get; set; }

		public string ToName { get; set; }

		public bool? IsRead { get; set; }

		public bool? IsAttachment { get; set; }

		public string SmallContent { get; set; }
	}
}
