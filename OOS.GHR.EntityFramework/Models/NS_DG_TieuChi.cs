namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_TieuChi
	{
		[Key]
		public int TieuChiID { get; set; }

		public int? BoTieuChiID { get; set; }

		public int? TieuChiChaID { get; set; }

		public string TenTieuChi { get; set; }

		public string TenTieuChiTA { get; set; }

		public string MaTieuChi { get; set; }

		public decimal? TrongSo { get; set; }

		public decimal? MucTieu { get; set; }

		public string MoTa { get; set; }

		public bool? IsUse { get; set; }

		public bool? IsDeleted { get; set; }

		public bool? IsGroup { get; set; }

		public int? ThuTu { get; set; }

		public decimal? DiemTru { get; set; }

		public string CongThuc { get; set; }

		public string CongThucQuy { get; set; }

		public string CongThucNam { get; set; }

		public bool? IsNhapGiaTri { get; set; }

		public int? LamTron { get; set; }

		public decimal? GiaTriLonNhat { get; set; }

		public decimal? GiaTriNhoNhat { get; set; }

		public string DonViTinh { get; set; }

		public string ValueList { get; set; }

		public bool? IsReadOnly { get; set; }

		public bool? IsFixed { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
