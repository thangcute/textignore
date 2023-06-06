namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_LoaiThongTin
	{
		[Key]
		public int LoaiThongTinID { get; set; }

		public int? NguoiLapID { get; set; }

		public string TenLoaiThongTin { get; set; }

		public DateTime? NgayLap { get; set; }

		public string GhiChu { get; set; }

		public string KieuDuLieu { get; set; }

		public string ValueField { get; set; }

		public string NameField { get; set; }

		public string SQLQuery { get; set; }

		public bool? AllowNull { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
