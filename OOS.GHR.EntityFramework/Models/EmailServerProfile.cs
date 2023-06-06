namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailServerProfile
	{
		[Key]
		public int EmailServerProfileID { get; set; }

		public string Name { get; set; }

		public string FromName { get; set; }

		public string FromEmail { get; set; }

		public bool? IsMainProfile { get; set; }

		public string SMTPServer { get; set; }

		public int? SMTPPort { get; set; }

		public string SMTPUserName { get; set; }

		public string SMTPPassword { get; set; }

		public bool? SMTPEnableSSL { get; set; }

		public bool? UseStartTLS { get; set; }

		public string POPServer { get; set; }

		public int? POPPort { get; set; }

		public string POPUserName { get; set; }

		public string POPPassword { get; set; }

		public bool? POPEnableSSL { get; set; }

		public bool? POPUseStartTLS { get; set; }

		public int? Type { get; set; }

		public int CustomerID { get; set; }

		public int? CongTyID { get; set; }

		public string ProfileUser { get; set; }

		public string ProfilePassword { get; set; }

		public int? POPSocketOption { get; set; }

		public int? SMTPSocketOption { get; set; }
	}
}
