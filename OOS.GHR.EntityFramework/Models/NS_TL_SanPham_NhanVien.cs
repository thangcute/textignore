namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_SanPham_NhanVien
	{
		[Key]
		public int SanPhamNhanVienID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? SanPhamID { get; set; }

		public int? LenhSanXuatID { get; set; }

		public int? CongDoanID { get; set; }

		public decimal? SoLuong { get; set; }

		public DateTime? ThoiGianSanXuat { get; set; }

		public decimal? DonGia { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
