namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsNgoaiNgu_TrinhDo
	{
		[Key]
		public int TrinhDoNgoaiNguID { get; set; }

		public int? NgoaiNguID { get; set; }

		public string TenTrinhDoNgoaiNgu { get; set; }

		public string MaTrinhDoNgoaiNgu { get; set; }

		public string TenTrinhDoNgoaiNguTA { get; set; }

		public string TenNgoaiNguTA { get; set; }

		public int? Level { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
