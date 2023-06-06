namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_HuuTri
	{
		[Key]
		public int HuuTriID { get; set; }

		public string QuyetDinhSo { get; set; }

		public DateTime? NgayNghi { get; set; }

		public int? NguoiThamGiaBHID { get; set; }

		public DateTime? NgayLap { get; set; }

		public decimal? LuongToiThieu { get; set; }

		public decimal? LuongBinhQuan { get; set; }

		public decimal? TongTienThanhToan { get; set; }

		public decimal? MucLuongDongBHXH { get; set; }

		public string GhiChu { get; set; }

		public int? NhanVienID { get; set; }
	}
}
