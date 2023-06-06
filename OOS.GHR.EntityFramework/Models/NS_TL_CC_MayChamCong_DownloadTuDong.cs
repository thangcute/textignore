namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_CC_MayChamCong_DownloadTuDong
	{
		[Key]
		public int DownloadTuDongID { get; set; }

		public int? MayChamCongID { get; set; }

		public TimeSpan? ThoiGian { get; set; }

		public bool? TuDongTongHopCong { get; set; }

		public bool? TuDongXoaDuLieu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
