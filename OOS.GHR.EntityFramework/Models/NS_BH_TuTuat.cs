namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_TuTuat
	{
		[Key]
		public int TuTuatID { get; set; }

		public string QuyetDinhSo { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public string NguoiKy { get; set; }

		public int? NguoiKyID { get; set; }

		public int? NguoiThamGiaBHID { get; set; }

		public DateTime? NgayMat { get; set; }

		public bool? XetDuyet { get; set; }

		public DateTime? NgayLap { get; set; }

		public int? TongSoTienThanhToan { get; set; }

		public int? MucLuongToiThieu { get; set; }

		public decimal? MucLuongDongBHXH { get; set; }

		public int? SoNguoiMat { get; set; }

		public string CheDoHuong { get; set; }

		public string GhiChu { get; set; }

		public int? DonViID { get; set; }
	}
}
