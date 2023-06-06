namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_APILog
	{
		[Key]
		public int APILog { get; set; }

		public int? APIID { get; set; }

		public int? ApiWebhookID { get; set; }

		public long? ActivityID { get; set; }

		public string IPAddress { get; set; }

		public DateTime? ThoiGian { get; set; }

		public string ResponseResult { get; set; }
	}
}
