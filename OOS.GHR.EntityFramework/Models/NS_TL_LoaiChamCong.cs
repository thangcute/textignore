namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_LoaiChamCong
	{
		[Key]
		public int LoaiChamCongID { get; set; }

		public string MaSo { get; set; }

		public string TenLoai { get; set; }

		public int? ThuTu { get; set; }

		public int? Type { get; set; }

		public bool? HienThiPortal { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
