namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsThamNien
	{
		[Key]
		public int ThamNienID { get; set; }

		public decimal? TuNam { get; set; }

		public decimal? ToiNam { get; set; }

		public decimal? HeSo { get; set; }

		public string Name { get; set; }

		public int? ThuTu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
