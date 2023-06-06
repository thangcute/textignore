namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_NhanVien_MucTieu
	{
		[Key]
		public int NhanVienMucTieuID { get; set; }

		public int? BoTieuChiID { get; set; }

		public string BoTieuChiStrID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? TieuChiChaID { get; set; }

		public int? TieuChiID { get; set; }

		public string TenTieuChi { get; set; }

		public string MoTa { get; set; }

		public string GhiChu { get; set; }

		public decimal? TrongSo { get; set; }

		public decimal? MucTieu { get; set; }

		public decimal? KH_MucTieu { get; set; }

		public decimal? GiaTriLonNhat { get; set; }

		public decimal? GiaTriNhoNhat { get; set; }

		public decimal? DiemTru { get; set; }

		public string DonViTinh { get; set; }

		public DateTime? Deadline { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
