namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_NhuCau
	{
		[Key]
		public int NhuCauID { get; set; }

		public string TenNhuCauDaoTao { get; set; }

		public string TenNhuCauTA { get; set; }

		public int? PhongBanID { get; set; }

		public int? NguoiYeuCauID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? CongTyID { get; set; }

		public string KhoaDaoTaoID { get; set; }

		public int? HinhThucDaoTaoID { get; set; }

		public string PhongBanYeuCau { get; set; }

		public string NguoiYeuCau { get; set; }

		public int? SoLuongHocVien { get; set; }

		public long? ChiPhiDuKien { get; set; }

		public int? TrangThai { get; set; }

		public string GhiChu { get; set; }

		public string DonViDaoTao { get; set; }

		public string DiaDiemDaoTao { get; set; }

		public string DoiTuongDaoTao { get; set; }

		public string LyDoDaoTao { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string FileStoreID { get; set; }

		public string FileStoreName { get; set; }

		public int? ThoiLuongDuKien { get; set; }

		public string YKienPheDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
