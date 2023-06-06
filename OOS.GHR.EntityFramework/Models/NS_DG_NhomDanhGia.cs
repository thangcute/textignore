namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_NhomDanhGia
	{
		[Key]
		public int NhomDanhGiaID { get; set; }

		public string MaNhomDanhGia { get; set; }

		public string TenNhomDanhGia { get; set; }

		public string TenNhomDanhGiaTA { get; set; }

		public string CodeFormat { get; set; }

		public decimal? HeSo { get; set; }

		public int? NBD_DanhGia { get; set; }

		public int? NKT_DanhGia { get; set; }

		public int? NBD_NhapChiTieu { get; set; }

		public int? NKT_NhapChiTieu { get; set; }

		public bool? IsUse { get; set; }

		public bool? IsThietLapMucTieu { get; set; }

		public bool? IsTuDanhGia { get; set; }

		public bool? IsShowDeXuat { get; set; }

		public bool? IsThietLapTrungTamNangLuc { get; set; }

		public int? NhomXepLoaiDanhGiaID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? HTMLPheDuyetMucTieuID { get; set; }

		public int? HTMLPheDuyetDanhGiaID { get; set; }

		public string ColumnTuDanhGia { get; set; }

		public string ColumnThietLapChiTieu { get; set; }

		public string ColumnQuanLyPheDuyet { get; set; }
	}
}
