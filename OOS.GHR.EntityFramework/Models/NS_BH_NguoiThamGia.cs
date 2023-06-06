namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_NguoiThamGia
	{
		[Key]
		public int NguoiThamGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public DateTime? NgayDeNghiCapSo { get; set; }

		public DateTime? NgayCapSo { get; set; }

		public DateTime? NgayNghiViec { get; set; }

		public DateTime? NgayKyHopDong { get; set; }

		public DateTime? BHYTTuNgay { get; set; }

		public DateTime? BHYTToiNgay { get; set; }

		public DateTime? BHXHTuNgay { get; set; }

		public DateTime? BHXHToiNgay { get; set; }

		public DateTime? BHTNTuNgay { get; set; }

		public DateTime? BHTNToiNgay { get; set; }

		public DateTime? NgayTraTheBHYT { get; set; }

		public DateTime? NgayBatDauThamGiaDongBHXH { get; set; }

		public int? Status { get; set; }

		public string SoBaoHiem { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public bool? DTNgoaiCongTy { get; set; }

		public string HoVaTen { get; set; }

		public string GioiTinh { get; set; }

		public int? NoiDKKhamBanDauID { get; set; }

		public string CoQuanCapSoBH { get; set; }

		public int? LoaiBH { get; set; }

		public bool? SoCapMoi { get; set; }

		public decimal? MucLuongDongBH { get; set; }

		public string DoiTuongBHXH { get; set; }

		public string MaBV { get; set; }

		public string SoHopDong { get; set; }

		public int? LoaiHopDongID { get; set; }

		public string MaTinh { get; set; }

		public string MaTheBHYT { get; set; }

		public string GhiChu { get; set; }

		public int? NguoiLapID { get; set; }

		public int? DonViDangKyBHXHID { get; set; }

		public decimal? MucLuongDongBHCu { get; set; }

		public decimal? TyleDong { get; set; }

		public bool? DaCoSoBHXH { get; set; }

		public string ChucVuBHXH { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? SoThangBanDau { get; set; }

		public int? SoNamBanDau { get; set; }

		public decimal? MucLuongTBBanDau { get; set; }

		public DateTime? NgayGiamBHXH { get; set; }

		public string NoiDKKhamBanDau { get; set; }
	}
}
