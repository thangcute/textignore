namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTDongBHXH
	{
		[Key]
		public int QTDongBHXHID { get; set; }

		public int? NhanVienID { get; set; }

		public int? LoaiBienDongID { get; set; }

		public int? DonViID { get; set; }

		public int? NguoiThamGiaID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetthuc { get; set; }

		public string DonViCongViecChucVu { get; set; }

		public int? SoNam { get; set; }

		public int? SoThang { get; set; }

		public decimal? MucLuong { get; set; }

		public string GhiChu { get; set; }

		public string ChiTietCongViec { get; set; }

		public decimal? TyleDong { get; set; }

		public bool? IsDongTuNguyen { get; set; }

		public int? LuongToiThieu { get; set; }

		public DateTime? NgayBatDau_BD { get; set; }

		public DateTime? NgayKetThuc_BD { get; set; }

		public decimal? MucLuongBD { get; set; }

		public decimal? HeSo { get; set; }

		public decimal? TyleBD { get; set; }

		public decimal? HeSoPCCV { get; set; }

		public DateTime? NgayTraTheBHYT { get; set; }

		public bool? KhongTraTheBHYT { get; set; }

		public DateTime? NgayLapBD { get; set; }

		public string DotXetDuyetBD { get; set; }

		public string LyDoDieuChinhBD { get; set; }

		public string Ngach { get; set; }

		public string Bac { get; set; }

		public DateTime? ThangBaoCao { get; set; }

		public int? Type { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
