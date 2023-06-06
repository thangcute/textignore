namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_ChuyenThietBi
	{
		[Key]
		public int ChuyenThietBiID { get; set; }

		public int? ThietBiID { get; set; }

		public int? PhongBanCuID { get; set; }

		public int? NhanVienCuID { get; set; }

		public int? PhongBanMoiID { get; set; }

		public int? NhanVienMoiID { get; set; }

		public int? NguoiKyID { get; set; }

		public string SoQuyetDinh { get; set; }

		public DateTime? NgayChuyen { get; set; }

		public decimal? SoLuong { get; set; }

		public string LyDo { get; set; }

		public string GhiChu { get; set; }

		public int? ThietBiChaMoiID { get; set; }

		public int? ThietBiChaCuID { get; set; }

		public string MaDuAnMoi { get; set; }

		public string MaDuAnCu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
