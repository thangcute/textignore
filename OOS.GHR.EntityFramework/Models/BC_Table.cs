namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class BC_Table
	{
		[Key]
		public int TableID { get; set; }

		public string Ten { get; set; }

		public string MoTa { get; set; }

		public int? DataSetID { get; set; }

		public int? X { get; set; }

		public int? Y { get; set; }

		public int CustomerID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
