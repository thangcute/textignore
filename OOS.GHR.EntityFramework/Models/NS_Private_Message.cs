namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_Private_Message
	{
		[Key]
		public int PrivateMessageID { get; set; }

		public string Message { get; set; }

		public int? UserFromID { get; set; }

		public int? UserToID { get; set; }

		public bool IsRead { get; set; }

		public bool IsSentMessage { get; set; }

		public DateTime? DateSent { get; set; }
	}
}
