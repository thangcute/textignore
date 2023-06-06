namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTNghiPhep
	{
		[Key]
		public int QTNghiPhepID { get; set; }

		public int? CongTyID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NguoiThamGiaBHID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? KyHieuChamCongID { get; set; }

		public int? DonViID { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string SoQuyetDinh { get; set; }

		public string NguoiKy { get; set; }

		public string LyDoNghi { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string GhiChu { get; set; }

		public int? Type { get; set; }

		public int? NguoiKyID { get; set; }

		public string TenCoQuanYTe { get; set; }

		public decimal? MucDongBHXH { get; set; }

		public decimal? SoTienDuocHuong { get; set; }

		public decimal? SoTienDuocHuongBD { get; set; }

		public int? SoNgayHuongTroCap { get; set; }

		public decimal? SoNgayNghi { get; set; }

		public int? SoNgayNghi1 { get; set; }

		public int? SoNgayNghi2 { get; set; }

		public int? NguoiLapID { get; set; }

		public DateTime? NgayLap { get; set; }

		public DateTime? NgayLapBD { get; set; }

		public int? NguoiSuaID { get; set; }

		public DateTime? NgaySua { get; set; }

		public bool? DaBaoCao { get; set; }

		public string ThoiGianDongBHXH { get; set; }

		public int? SoNgayNghiLuyKeTuDauNam { get; set; }

		public int? SoNgayNghiTapChung { get; set; }

		public decimal? MucLuongToiThieu { get; set; }

		public string DieuKienTinhHuong { get; set; }

		public decimal? SoNgayDuocNghiThem { get; set; }

		public string TenLoaiNghi { get; set; }

		public string LyDoHuyDangKy { get; set; }

		public string Des { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
