namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_ChiPhiTuyenDung
	{
		[Key]
		public int ChiPhiTuyenDungID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public string MoTa { get; set; }

		public decimal? SoTien { get; set; }

		public int? NguonHoSoID { get; set; }

		public DateTime? NgayDangTin { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
