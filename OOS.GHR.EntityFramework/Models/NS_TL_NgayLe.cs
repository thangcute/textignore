namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_NgayLe
	{
		[Key]
		public int NgayLeID { get; set; }

		public string MoTa { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public int? Nam { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? CongTyID { get; set; }

		public bool? IsBuLe { get; set; }
	}
}
