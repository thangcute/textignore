namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_NhanVien_KhongDanhGia
	{
		[Key]
		public int NhanVienKhongDanhGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? BuocThucHienID { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public int? NguoiDanhGiaID { get; set; }
	}
}
