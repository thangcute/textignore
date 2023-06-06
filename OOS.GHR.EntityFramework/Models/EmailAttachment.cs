namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailAttachment
	{
		[Key]
		public string EmailAttachmentID { get; set; }

		public string FileSourceName { get; set; }

		public string FileName { get; set; }

		public long? Size { get; set; }

		public string Directory { get; set; }

		public int? InboxID { get; set; }

		public int? ThiSinhID { get; set; }
	}
}
