namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_DoiTuongDanhGia
	{
		[Key]
		public int DoiTuongDanhGiaID { get; set; }

		public int? NguoiDanhGiaID { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public int? BuocThucHienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? NhanVienID { get; set; }

		public int? ChucVuID { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public int? ChucDanhID { get; set; }

		public int? NganhDocID { get; set; }

		public int? NguoiDuyetID { get; set; }

		public int? XetDuyet { get; set; }
	}
}
