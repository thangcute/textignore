namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_CC_TongHopTheoNgay
	{
		[Key]
		public long TongHopTheoNgayID { get; set; }

		public int? NhanVienID { get; set; }

		public int? CaLamViecID { get; set; }

		public int? PhongBanID { get; set; }

		public int? CongTyID { get; set; }

		public string ID { get; set; }

		public string TenPhongBan { get; set; }

		public string TenCaLamViec { get; set; }

		public string KyHieuChamCong { get; set; }

		public DateTime? NgayChamCong { get; set; }

		public TimeSpan? GioDen { get; set; }

		public TimeSpan? GioVe { get; set; }

		public TimeSpan? GioRa { get; set; }

		public TimeSpan? GioVao { get; set; }

		public decimal? TGDiMuon { get; set; }

		public decimal? TGVeSom { get; set; }

		public decimal? TGLamViec { get; set; }

		public decimal? TGLamThem { get; set; }

		public decimal? TGRaNgoai { get; set; }

		public string GhiChu { get; set; }

		public bool? IsChinhSua { get; set; }

		public bool? IsNgayLe { get; set; }

		public bool? IsLamSangNgayHomSau { get; set; }

		public int? KyHieuChamCongID { get; set; }

		public decimal? TGLamThemDem { get; set; }

		public bool? Lock { get; set; }

		public string MaDotTongHopCong { get; set; }

		public bool? IsLamThemNgayHomSau { get; set; }

		public bool? IsDiMuonCoLyDo { get; set; }

		public bool? IsVeSomCoLyDo { get; set; }

		public string XacNhanLyDo { get; set; }

		public string YKienPheDuyet { get; set; }

		public TimeSpan? XacNhanGioDen { get; set; }

		public TimeSpan? XacNhanGioVe { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public decimal? LamChuNhat { get; set; }

		public decimal? DemChuNhat { get; set; }

		public decimal? LamNgayLe { get; set; }

		public decimal? DemNgayLe { get; set; }

		public decimal? SoPhutLamViecThucTe { get; set; }

		public bool? IsCN { get; set; }

		public decimal? LamDem { get; set; }

		public decimal? SoNgayNghi { get; set; }

		public int? HienThiGiaiTrinhCong { get; set; }
	}
}
