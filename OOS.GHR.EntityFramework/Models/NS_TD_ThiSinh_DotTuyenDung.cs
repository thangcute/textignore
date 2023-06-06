namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_ThiSinh_DotTuyenDung
	{
		[Key]
		public int ThiSinhDotTuyenDungID { get; set; }

		public int? ThiSinhID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? ChucVuID { get; set; }

		public int? CaLamViecID { get; set; }

		public int? PhongBanID { get; set; }

		public int? VongThiID { get; set; }

		public int? TrangThai { get; set; }

		public string NguonUngVienID { get; set; }

		public string TrangThaiStr { get; set; }

		public string SoBaoDanh { get; set; }

		public int? MucLuongHienTai { get; set; }

		public int? MucLuongMongMuon { get; set; }

		public DateTime? ThoiGianHenPhongVan { get; set; }

		public DateTime? NgaySanSangDiLam { get; set; }

		public DateTime? NgayBatDauLam { get; set; }

		public DateTime? NgayHetThuViec { get; set; }

		public DateTime? NgayChuyenOffer { get; set; }

		public DateTime? NgayChuyenTuyenDung { get; set; }

		public DateTime? NgayNopHoSo { get; set; }

		public string GhiChu { get; set; }

		public string DeXuat { get; set; }

		public string BoPhanLamViec { get; set; }

		public string NoiLamViec { get; set; }

		public decimal? TongDiem { get; set; }

		public bool? IsEmailOfferSent { get; set; }

		public bool? IsEmailOnboardSent { get; set; }

		public bool? IsEmailFailSent { get; set; }

		public int? XetDuyet_Offer { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
