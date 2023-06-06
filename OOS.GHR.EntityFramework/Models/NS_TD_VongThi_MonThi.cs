namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_VongThi_MonThi
	{
		[Key]
		public int VongThi_MonThiID { get; set; }

		public int? VongThiID { get; set; }

		public int? MonThiID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public string GhiChu { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string KetQua { get; set; }

		public int? TinhTrang { get; set; }

		public float? HeSo { get; set; }

		public float? DiemKyVong { get; set; }

		public float? DiemToiDa { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
