namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsChucVu
	{
		[Key]
		public int ChucVuID { get; set; }

		public string TenChucVu { get; set; }

		public string MaChucVu { get; set; }

		public string DienGiai { get; set; }

		public string MoTaTuyenDung { get; set; }

		public int? QuanLyTrucTiepID { get; set; }

		public int? QuanLyGianTiepID { get; set; }

		public string TenChucVuTA { get; set; }

		public int? ChucDanhID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string Keyword { get; set; }

		public int? PhepNam { get; set; }

		public string NhomNangLucStr { get; set; }

		public string UV_GioiTinh { get; set; }

		public int? UV_TuTuoi { get; set; }

		public int? UV_ToiTuoi { get; set; }

		public int? UV_TrinhDoHocVan { get; set; }

		public int? UV_ChuyenNganh { get; set; }

		public int? UV_NamKinhNghiem { get; set; }

		public int? UV_HangTotNghiep { get; set; }

		public int? UV_ChucVuDamNhiem { get; set; }

		public string UV_DanhHieuKhenThuong { get; set; }

		public int? LoTrinhChuyenMonID { get; set; }

		public int? LoTrinhKhacID { get; set; }

		public int? LoTrinhQuanLyID { get; set; }

		public int? NhomNghiepVuID { get; set; }

		public int? NhomXepLoaiDanhGiaID { get; set; }

		public int? NhomChucVuID { get; set; }

		public int? MucLuong_Tu { get; set; }

		public int? MucLuong_Toi { get; set; }

		public bool? IsActive { get; set; }
	}
}
