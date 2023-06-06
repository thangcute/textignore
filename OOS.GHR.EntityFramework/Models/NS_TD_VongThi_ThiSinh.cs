namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_VongThi_ThiSinh
	{
		[Key]
		public int VongThiThiSinhID { get; set; }

		public int ThiSinhID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? VongThiID { get; set; }

		public string TenVongThi { get; set; }

		public string NhanXet { get; set; }

		public decimal? TongDiem { get; set; }

		public string DiaDiemPhongVan { get; set; }

		public DateTime? NgayPhongVan { get; set; }

		public DateTime? LichPhongVan { get; set; }

		public int? TrangThai { get; set; }

		public bool? IsEmailSent { get; set; }

		public bool? IsEmailExaminer { get; set; }

		public bool? IsMoved { get; set; }

		public bool? IsMarkUpdate { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
