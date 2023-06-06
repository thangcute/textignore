namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class PhongBan_History
	{
		[Key]
		public int PhongBanHistoryID { get; set; }

		public int? PhongBanChaID { get; set; }

		public int PhongBanID { get; set; }

		public int? KhoiID { get; set; }

		public int? ChucVuLanhDaoID { get; set; }

		public int? CapPhongBanID { get; set; }

		public string MaPhongBan { get; set; }

		public string Ten { get; set; }

		public string TenPhongBanTA { get; set; }

		public long? ThuTu { get; set; }

		public string MaPBHoachToan { get; set; }

		public string MaChiNhanhHoachToan { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public DateTime? NgayHetHieuLuc { get; set; }

		public bool? IsDeleted { get; set; }

		public string Operation { get; set; }

		public int? CongTyID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
