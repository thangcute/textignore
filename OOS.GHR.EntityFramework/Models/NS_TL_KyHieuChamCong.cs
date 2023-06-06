namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_KyHieuChamCong
	{
		[Key]
		public int KyHieuChamCongID { get; set; }

		public int? LoaiChamCongID { get; set; }

		public string KyHieu { get; set; }

		public string MoTa { get; set; }

		public string GhiChu { get; set; }

		public bool? HienThiDangKyNghi { get; set; }

		public bool? IsNghiPhep { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
