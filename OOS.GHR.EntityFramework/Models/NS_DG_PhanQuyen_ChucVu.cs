namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_PhanQuyen_ChucVu
	{
		[Key]
		public int PhanQuyenChucVuID { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public int? BuocThucHienID { get; set; }

		public int? PhongBanNguoiDanhGiaID { get; set; }

		public int? ChucVuDanhGiaID { get; set; }

		public int? PhongBanDuocDanhGiaID { get; set; }

		public int? ChucVuDuocDanhGiaID { get; set; }

		public int? XetDuyet { get; set; }
	}
}
