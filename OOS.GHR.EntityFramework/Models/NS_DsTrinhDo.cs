namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsTrinhDo
	{
		[Key]
		public int TrinhDoID { get; set; }

		public string TenTrinhDo { get; set; }

		public string Ma { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? Level { get; set; }

		public string TenTrinhDoTA { get; set; }
	}
}
