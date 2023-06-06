namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_NguoiChamDiem
	{
		[Key]
		public int ChucVuNguoiChamDiemID { get; set; }

		public int? NhanVienID { get; set; }

		public int? ChucVuVongThiID { get; set; }

		public int? Type { get; set; }

		public decimal? HeSo { get; set; }

		public int? NguoiChamDiemID { get; set; }
	}
}
