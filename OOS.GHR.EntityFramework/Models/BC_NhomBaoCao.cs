namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class BC_NhomBaoCao
	{
		[Key]
		public int NhomBaoCaoID { get; set; }

		public string TenNhom { get; set; }

		public string MoTa { get; set; }

		public int? ParentID { get; set; }

		public string MaNhom { get; set; }

		public string TenTA { get; set; }

		public int CustomerID { get; set; }

		public bool? Visible { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
