namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NhomXetDuyet
	{
		[Key]
		public int NhomXetDuyetID { get; set; }

		public string TenNhomXetDuyet { get; set; }

		public string MaNhomXetDuyet { get; set; }

		public string Email { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
