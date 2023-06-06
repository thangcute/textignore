namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class BC_RelationDetail
	{
		[Key]
		public int RelationDetailID { get; set; }

		public int? RelationID { get; set; }

		public int? ParentKeyID { get; set; }

		public int? ChildKeyID { get; set; }

		public int CustomerID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
