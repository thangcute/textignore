namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NhomThongSo
	{
		[Key]
		public int NhomThongSoID { get; set; }

		public int? STT { get; set; }

		public string MaNhomThongSo { get; set; }

		public string TenNhomThongSo { get; set; }

		public string MoTa { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
