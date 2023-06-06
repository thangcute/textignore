namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_DanhGia_KetQua_Huy
	{
		[Key]
		public int DanhGiaKetQuaHuyID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DanhGiaID { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? NhomUserDanhGiaID { get; set; }

		public int? TieuChiID { get; set; }

		public int? TieuChiChaID { get; set; }

		public int? BoTieuChiID { get; set; }

		public string TenTieuChi { get; set; }

		public decimal? DiemNhap { get; set; }

		public decimal? TyLeChatLuong { get; set; }

		public decimal? GiaTriLonNhat { get; set; }

		public decimal? TrongSo { get; set; }

		public decimal? MucTieu { get; set; }

		public decimal? TongDiem { get; set; }

		public string GhiChu { get; set; }

		public string GiaTriStr { get; set; }

		public string YKienPhanHoi { get; set; }

		public DateTime? FinishedDate { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public decimal? TyleHoanThanh { get; set; }
	}
}
