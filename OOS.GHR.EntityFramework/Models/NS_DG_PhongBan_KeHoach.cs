namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_PhongBan_KeHoach
	{
		[Key]
		public int PhongBanKeHoachID { get; set; }

		public int? PhongBanID { get; set; }

		public int? TieuChiID { get; set; }

		public decimal? MucTieu { get; set; }

		public decimal? TyTrong { get; set; }

		public string MoTa { get; set; }

		public int? Thang { get; set; }

		public int? Quy { get; set; }

		public int? Nam { get; set; }

		public string GhiChu { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
