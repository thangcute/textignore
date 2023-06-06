namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_ThanhLyThietBi
	{
		[Key]
		public int ThanhLyThietBiID { get; set; }

		public string SoQuyetDinh { get; set; }

		public string GhiChu { get; set; }

		public DateTime? NgayLap { get; set; }

		public string KhachHang { get; set; }

		public string LyDo { get; set; }

		public string HinhThucXuLy { get; set; }

		public string DaiDienCongTy { get; set; }

		public string DiaDiem { get; set; }

		public int? ThietBiID { get; set; }

		public int? NguoiKyID { get; set; }

		public decimal? SoLuong { get; set; }

		public decimal? DonGia { get; set; }

		public decimal? TongSoTien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
