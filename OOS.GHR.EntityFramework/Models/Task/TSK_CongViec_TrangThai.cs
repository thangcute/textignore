namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_CongViec_TrangThai
	{
		[Key]
		public int TrangThaiID { get; set; }

		public string MaTrangThai { get; set; }

		public string TenTrangThai { get; set; }

		public int? STT { get; set; }

		public byte? Status { get; set; }

		public string MoTa { get; set; }

		public string Color { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
