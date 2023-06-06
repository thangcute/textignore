namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailContent
	{
		[Key]
		public int EmailContentID { get; set; }

		public string Content { get; set; }

		public bool? IsHTMLFormat { get; set; }

		public string Subject { get; set; }

		public string Name { get; set; }

		public string ContentCode { get; set; }

		public int? EmailContentGroupID { get; set; }

		public string SelectDataSource { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
