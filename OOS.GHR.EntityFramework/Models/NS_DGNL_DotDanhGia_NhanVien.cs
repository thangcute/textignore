namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DGNL_DotDanhGia_NhanVien
	{
		[Key]
		public int DotDanhGiaNhanVienID { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public string MaDotDanhGia { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public string NhomNangLucStrID { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? XepLoaiDanhGiaID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public int? Thang { get; set; }

		public int? Quy { get; set; }

		public int? Nam { get; set; }

		public int? TrangThai { get; set; }

		public int? BuocThucHienTiepTheoID { get; set; }

		public int? NguoiXetDuyetTiepTheoID { get; set; }

		public int? NhomXetDuyetTiepTheoID { get; set; }

		public string GhiChu { get; set; }

		public int? TongDiem { get; set; }

		public decimal? DiemChuan { get; set; }

		public string MaNhanVien { get; set; }

		public string HoVaTen { get; set; }

		public string TenChucVu { get; set; }

		public string TenPhongBan { get; set; }

		public int? ChucVuID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucDanhID { get; set; }

		public bool? IsTuDanhGia { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
