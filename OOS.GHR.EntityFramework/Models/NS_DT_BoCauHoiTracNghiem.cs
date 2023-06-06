namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_BoCauHoiTracNghiem
	{
		[Key]
		public int BoCauHoiTracNghiemID { get; set; }

		public string TenBoCauHoiTracNghiem { get; set; }

		public int? ThoiGianHoanThanh { get; set; }

		public int? TongSoCauHoi { get; set; }

		public int? DiemDat { get; set; }

		public bool? IsUse { get; set; }

		public string Mota { get; set; }
	}
}
