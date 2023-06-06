namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_ChiTietCongThucLuong
	{
		[Key]
		public int ChiTietCongThucLuongID { get; set; }

		public int? LoaiLuongID { get; set; }

		public int? NhomCongThucLuongID { get; set; }

		public string CongThucTinh { get; set; }

		public string CongThucBackdate { get; set; }

		public int? ThuTu { get; set; }

		public string TenLoaiLuong { get; set; }

		public string GhiChu { get; set; }

		public int? ThuTuHienThi { get; set; }

		public int? LamTron { get; set; }

		public long? Color { get; set; }

		public int? TinhTaiDong { get; set; }

		public bool? IsNoUse { get; set; }
	}
}
