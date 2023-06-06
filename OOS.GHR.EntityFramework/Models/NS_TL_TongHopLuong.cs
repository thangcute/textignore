namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_TongHopLuong
	{
		[Key]
		public int TongHopLuongID { get; set; }

		public int? BangLuongID { get; set; }

		public int? NhomLuongID { get; set; }

		public decimal? TienLuong { get; set; }
	}
}
