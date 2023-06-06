namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TienTe_TyGia
	{
		[Key]
		public int TyGiaID { get; set; }

		public int? TienTeID { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public decimal? TyGia { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
