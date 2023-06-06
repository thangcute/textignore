namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsNgachCongChuc
	{
		[Key]
		public int NgachCongChucID { get; set; }

		public string TenMaNgach { get; set; }

		public string ThoiHanNangLuong { get; set; }

		public string Ma { get; set; }

		public float? HeSoCoBan { get; set; }

		public int? ThuTu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string TenNgachCongChucTA { get; set; }
	}
}
