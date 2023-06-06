namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_ThoaiTruyThu
	{
		[Key]
		public int ThoaiTruyThuID { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public decimal? MucLuong { get; set; }

		public int? Type { get; set; }

		public string GhiChu { get; set; }

		public DateTime? NgayLap { get; set; }

		public int? SoThang { get; set; }

		public int? SoNam { get; set; }

		public int? MucTienHuong { get; set; }

		public int? NguoiLapID { get; set; }

		public int? NguoiThamGiaID { get; set; }

		public decimal? TyleDong { get; set; }

		public decimal? Tyle { get; set; }

		public int? DonViID { get; set; }
	}
}
