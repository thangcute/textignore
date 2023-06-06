namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_DanhGia
	{
		[Key]
		public int DanhGiaID { get; set; }

		public string MaDotDanhGia { get; set; }

		public string BoTieuChiStrID { get; set; }

		public string TenNguoiDanhGia { get; set; }

		public string TenBuocDanhGia { get; set; }

		public string TenXepLoaiDanhGia { get; set; }

		public string TenBuocTiepTheo { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NguoiDanhGiaID { get; set; }

		public int? NhomUserXetDuyetID { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public int? QuyTrinhBuocThucHienID { get; set; }

		public int? BuocThucHienTiepTheoID { get; set; }

		public int? XepLoaiDanhGiaID { get; set; }

		public DateTime? NgayGuiYeuCauDanhGia { get; set; }

		public DateTime? NgayDanhGia { get; set; }

		public decimal? HeSo { get; set; }

		public decimal? TongDiem { get; set; }

		public bool? IsTuDanhGia { get; set; }

		public int? XetDuyet { get; set; }

		public string YKienPheDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
