namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsNhomChucDanh
	{
		[Key]
		public int NhomChucDanhID { get; set; }

		public string TenNhomChucDanh { get; set; }

		public string MaNhomChucDanh { get; set; }

		public string TenNhomChucDanhTA { get; set; }

		public int? ThuTu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
