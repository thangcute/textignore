namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_CauHoiTracNghiem
	{
		[Key]
		public int CauHoiTracNghiemID { get; set; }

		public int? BoCauHoiTracNghiemID { get; set; }

		public int? Type { get; set; }

		public string MaCauHoi { get; set; }

		public string TenCauHoi { get; set; }

		public string DapAn1 { get; set; }

		public string DapAn2 { get; set; }

		public string DapAn3 { get; set; }

		public string DapAn4 { get; set; }

		public string DapAn5 { get; set; }

		public int? DapAnDung { get; set; }

		public string DienGiai { get; set; }

		public int? ChuDeTracNghiemID { get; set; }
	}
}
