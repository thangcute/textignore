namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsLyDoCongTac
	{
		[Key]
		public int LyDoCongTacID { get; set; }

		public string MaLyDoCongTac { get; set; }

		public string TenLyDoCongTac { get; set; }

		public string TenLyDoCongTacTA { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
