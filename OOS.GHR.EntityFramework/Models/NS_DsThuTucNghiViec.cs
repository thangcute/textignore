namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsThuTucNghiViec
	{
		[Key]
		public int ThuTucNghiViecID { get; set; }

		public string TenHoanTatNghiViec { get; set; }

		public string NhomThuTuc { get; set; }

		public string NhomThuTucTA { get; set; }

		public string TenHoanTatNghiViecTA { get; set; }

		public string MaHoanTatNghiViec { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
