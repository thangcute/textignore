namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TLBD_QTDienBienLuongChiTiet
	{
		[Key]
		public int QTDienBienLuongChiTietID { get; set; }

		public int? QTDienBienLuongID { get; set; }

		public int? LoaiLuongID { get; set; }

		public string GhiChu { get; set; }

		public decimal? GiaTri { get; set; }

		public string GiaTriStr { get; set; }

		public string TenLoaiLuong { get; set; }

		public string MaLoaiLuong { get; set; }

		public string TenNhomLuong { get; set; }
	}
}
