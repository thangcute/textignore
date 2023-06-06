namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_PhanQuyen_NhanVien
	{
		[Key]
		public int PhanQuyenNhanVienID { get; set; }

		public int? NguoiDanhGiaID { get; set; }

		public int? NhanVienDuocDanhGiaID { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public int? BuocThucHienID { get; set; }

		public int? XetDuyet { get; set; }
	}
}
