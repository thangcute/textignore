namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_ThiTracNghiem
	{
		[Key]
		public int ThiTracNghiemID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? NoiDungDaoTaoID { get; set; }

		public int? BoCauHoiTracNghiemID { get; set; }

		public DateTime? GioBatDau { get; set; }

		public DateTime? GioKetThuc { get; set; }

		public int? TrangThai { get; set; }

		public int? ResetIndex { get; set; }

		public decimal? TongDiem { get; set; }

		public int? ThoiGianHoanThanh { get; set; }
	}
}
