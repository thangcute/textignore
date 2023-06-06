namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_YeuCau
	{
		[Key]
		public int ChucVuYeuCauID { get; set; }

		public int? ChucVuID { get; set; }

		public string TenYeuCau { get; set; }

		public string MoTa { get; set; }

		public string DoiTuongYeuCau { get; set; }

		public int? GiaTri { get; set; }
	}
}
