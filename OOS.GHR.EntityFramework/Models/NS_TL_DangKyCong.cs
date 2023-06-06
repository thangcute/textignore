namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_DangKyCong
	{
		[Key]
		public int DangKyCongID { get; set; }

		public int? CongTyID { get; set; }

		public int? NhanVienID { get; set; }

		public int? KyHieuChamCongID { get; set; }

		public DateTime? NgayChamCong { get; set; }

		public int? CaLamViecID { get; set; }

		public int? LyDoTangCaID { get; set; }

		public string LyDoTangCa { get; set; }

		public decimal? GioLamThem { get; set; }

		public decimal? GioLamThemTruocCa { get; set; }

		public bool? LamThemTruocCa { get; set; }

		public TimeSpan? BDLamThemTruocCa { get; set; }

		public TimeSpan? KTLamThemTruocCa { get; set; }

		public TimeSpan? BDLamThemSauCa { get; set; }

		public TimeSpan? KTLamThemSauCa { get; set; }

		public bool? Lock { get; set; }

		public int? XetDuyet { get; set; }

		public int? XetDuyetCLV { get; set; }

		public bool? IsDangKyOT { get; set; }

		public decimal? TongSoGioDangKyLamThem { get; set; }

		public string DangKyStr { get; set; }

		public string YKienXetDuyet { get; set; }

		public string CanhBaoTangCa { get; set; }

		public bool? AnTangCa { get; set; }

		public bool? IsMoiTruongDacBiet { get; set; }

		public int? CreatedCLVByID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
