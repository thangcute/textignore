namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_ChiPhi
	{
		[Key]
		public int ChiPhiDaoTaoID { get; set; }

		public string MoTa { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? NhomChiPhiID { get; set; }

		public decimal? SoTien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
