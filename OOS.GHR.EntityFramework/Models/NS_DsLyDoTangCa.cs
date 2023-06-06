namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsLyDoTangCa
	{
		[Key]
		public int LyDoTangCaID { get; set; }

		public string TenLyDoTangCa { get; set; }

		public string TenLyDoTangCaTA { get; set; }

		public string MaLyDoTangCa { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public bool? IsActive { get; set; }
	}
}
