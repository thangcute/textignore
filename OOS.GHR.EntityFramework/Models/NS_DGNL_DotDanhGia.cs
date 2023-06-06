namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DGNL_DotDanhGia
	{
		[Key]
		public int DotDanhGiaID { get; set; }

		public string MaDotDanhGia { get; set; }

		public string TenDotDanhGia { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public string NhomNangLucStrID { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? NhomXepLoaiDanhGiaID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public int? Thang { get; set; }

		public int? Quy { get; set; }

		public int? Nam { get; set; }

		public int? TrangThai { get; set; }

		public string GhiChu { get; set; }

		public int? TongSoNhanVien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
