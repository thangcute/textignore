namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DGNL_DanhGia_KetQua
	{
		[Key]
		public int DanhGiaKetQuaID { get; set; }

		public int? DotDanhGiaNhanVienID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DanhGiaID { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? NangLucID { get; set; }

		public int? NhomNangLucID { get; set; }

		public string TenNangLuc { get; set; }

		public string MaNangLuc { get; set; }

		public string TenNhomNangLuc { get; set; }

		public string MaNhomNangLuc { get; set; }

		public int? DiemNhap { get; set; }

		public decimal? TrongSo { get; set; }

		public int? Tyle { get; set; }

		public decimal? TongDiem { get; set; }

		public int? ChucVuID { get; set; }

		public int? DiemChuan { get; set; }

		public string DienGiai { get; set; }

		public bool? IsVongCuoi { get; set; }

		public string GhiChu { get; set; }

		public string TangBieuHien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
