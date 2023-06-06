namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NguoiDung_Quyen
	{
		[Key]
		public int NguoiDungQuyenID { get; set; }

		public int? NguoiDungID { get; set; }

		public string QuyenID { get; set; }

		public int? ThaoTac { get; set; }

		public string Action { get; set; }

		public string Controller { get; set; }

		public int? XetDuyet { get; set; }

		public string TenQuyen { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
