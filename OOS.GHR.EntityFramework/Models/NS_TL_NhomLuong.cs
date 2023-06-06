namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_NhomLuong
	{
		[Key]
		public int NhomLuongID { get; set; }

		public string TenNhomLuong { get; set; }

		public string GhiChu { get; set; }

		public int? ThuTu { get; set; }

		public int? CoCauLuongID { get; set; }

		public bool? KhongTinhLuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
