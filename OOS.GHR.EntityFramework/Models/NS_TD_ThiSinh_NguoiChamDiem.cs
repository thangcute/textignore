namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_ThiSinh_NguoiChamDiem
	{
		[Key]
		public int ThiSinhNguoiChamDiemID { get; set; }

		public int? VongThiID { get; set; }

		public int? ThiSinhID { get; set; }

		public int? NguoiChamDiemID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public decimal? TongDiem { get; set; }

		public decimal? HeSo { get; set; }

		public decimal? TongDiemChuaNhanHS { get; set; }

		public string NhanXet { get; set; }

		public string KetQua { get; set; }

		public int? TrangThai { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
