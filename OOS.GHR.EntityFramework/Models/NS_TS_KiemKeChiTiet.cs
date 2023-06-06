namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_KiemKeChiTiet
	{
		[Key]
		public int KiemKeChiTietID { get; set; }

		public int? ThietBiID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? SoLuong { get; set; }

		public int? SoLuongKiemKe { get; set; }

		public string GhiChu { get; set; }

		public int? KiemKeID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
