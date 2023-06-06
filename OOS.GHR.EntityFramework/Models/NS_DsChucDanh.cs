namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsChucDanh
	{
		[Key]
		public int ChucDanhID { get; set; }

		public string TenChucDanh { get; set; }

		public string MaChucDanh { get; set; }

		public int? ThuTu { get; set; }

		public int? NhomChucDanhID { get; set; }

		public string TenChucDanhTA { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string Keyword { get; set; }

		public bool? IsActive { get; set; }
	}
}
