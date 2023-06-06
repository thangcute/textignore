namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_CongThucLuongHeThong
	{
		[Key]
		public int CongThucLuongHeThongID { get; set; }

		public string MaSo { get; set; }

		public string Ten { get; set; }

		public string GhiChu { get; set; }

		public string CongThuc { get; set; }

		public bool? IsUse { get; set; }

		public bool? TraVeKieuText { get; set; }

		public string TextFormat { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
