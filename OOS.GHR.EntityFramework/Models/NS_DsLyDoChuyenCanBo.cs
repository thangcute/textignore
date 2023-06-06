namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsLyDoChuyenCanBo
	{
		[Key]
		public int LyDoChuyenCanBoID { get; set; }

		public string TenLyDoChuyenCanBo { get; set; }

		public string TenLyDoChuyenCanBoTA { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public bool? IsActive { get; set; }
	}
}
