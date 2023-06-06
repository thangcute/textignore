namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_TNLDBNN
	{
		[Key]
		public int TNLDBNNID { get; set; }

		public string QuyetDinhSo { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public string NguoiKy { get; set; }

		public int? NguoiKyID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public int? SoLanTaiNan { get; set; }

		public int? NguoiThamGiaBHID { get; set; }

		public string GhiChu { get; set; }

		public bool? XetDuyet { get; set; }

		public DateTime? NgayLap { get; set; }

		public int? SoNgayNghiTaiGia { get; set; }

		public int? SoNgayNghiTapChung { get; set; }

		public int? TongSoTienThanhToan { get; set; }

		public decimal? MucLuongDongBHXH { get; set; }

		public int? MucLuongToiThieu { get; set; }

		public decimal? PhanTramThuongTat { get; set; }

		public string CheDoHuong { get; set; }

		public bool? NghiDuongSuc { get; set; }

		public DateTime? NgayTaiNan { get; set; }

		public string NoiXayRaTaiNan { get; set; }

		public int? DonViID { get; set; }
	}
}
