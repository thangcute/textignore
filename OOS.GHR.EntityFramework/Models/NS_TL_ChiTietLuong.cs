namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_ChiTietLuong
	{
		[Key]
		public int ChiTietLuongID { get; set; }

		public int? BangLuongID { get; set; }

		public int? LoaiLuongID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NguoiChinhSuaID { get; set; }

		public int? CongTyID { get; set; }

		public int? ThangTL { get; set; }

		public int? NamTL { get; set; }

		public bool? ChinhSua { get; set; }

		public DateTime? NgayChinhSua { get; set; }

		public string MaLoaiLuong { get; set; }

		public string TenLoaiLuong { get; set; }

		public decimal? TienLuong { get; set; }

		public string TienLuongStr { get; set; }

		public byte[] ETienLuong { get; set; }

		public string GhiChu { get; set; }
	}
}
