namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_MonThi_ThiSinh
	{
		[Key]
		public int MonThiThiSinhID { get; set; }

		public int? CongTyID { get; set; }

		public int? MonThiID { get; set; }

		public int? ThiSinhID { get; set; }

		public decimal? DiemThi { get; set; }

		public string LoaiKetQua { get; set; }

		public int? VongThiID { get; set; }

		public int? NguoiChamDiemID { get; set; }

		public DateTime? NgayChamThi { get; set; }

		public int? DotTuyenDungID { get; set; }

		public string GhiChu { get; set; }
	}
}
