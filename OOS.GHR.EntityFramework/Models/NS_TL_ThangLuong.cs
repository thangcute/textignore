namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_ThangLuong
	{
		[Key]
		public int ThangLuongID { get; set; }

		public int? LuongCoBan { get; set; }

		public int? LuongTrachNhiem { get; set; }

		public string MaChucDanh { get; set; }

		public decimal? BacLuong { get; set; }
	}
}
