namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_VongThi
	{
		[Key]
		public int VongThiID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public string TenVongThi { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string MoTa { get; set; }

		public bool? DaThucHien { get; set; }

		public int? STT { get; set; }

		public int? TongDiemYeuCau { get; set; }

		public string TenVongThiTA { get; set; }

		public bool? IsDaoTao { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
