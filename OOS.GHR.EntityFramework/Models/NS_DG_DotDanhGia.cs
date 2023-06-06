namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_DotDanhGia
	{
		[Key]
		public int DotDanhGiaID { get; set; }

		public string MaDotDanhGia { get; set; }

		public string TenDotDanhGia { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public string BoTieuChiStrID { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? XepLoaiDanhGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? BuocThucHienTiepTheoID { get; set; }

		public int? NguoiXetDuyetTiepTheoID { get; set; }

		public int? NhomUserXetDuyetTiepTheoID { get; set; }

		public int? QuanLyTrucTiepID { get; set; }

		public string MaNhanVien { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public int? NguoiXetDuyetMucTieuID { get; set; }

		public int? BuocXetDuyetMucTieuTiepTheoID { get; set; }

		public DateTime? NgayXetDuyetMucTieu { get; set; }

		public string HoVaTen_NguoiDuyetMucTieu { get; set; }

		public string ChucVu_NguoiDuyetMucTieu { get; set; }

		public string PhongBan_NguoiDuyetMucTieu { get; set; }

		public bool? IsThietLapMucTieu { get; set; }

		public bool? IsTuDanhGia { get; set; }

		public bool? IsVongCuoi { get; set; }

		public DateTime? NBD_DanhGia { get; set; }

		public DateTime? NKT_DanhGia { get; set; }

		public DateTime? NBD_NhapChiTieu { get; set; }

		public DateTime? NKT_NhapChiTieu { get; set; }

		public int? Thang { get; set; }

		public int? Quy { get; set; }

		public int? Nam { get; set; }

		public decimal? HeSoDanhGia { get; set; }

		public int? TrangThai { get; set; }

		public int? TrangThai_ThietLapMucTieu { get; set; }

		public decimal? TongDiem { get; set; }

		public string GhiChu { get; set; }

		public string LyDoTuChoiPheDuyetMucTieu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? HopDongID { get; set; }
	}
}
