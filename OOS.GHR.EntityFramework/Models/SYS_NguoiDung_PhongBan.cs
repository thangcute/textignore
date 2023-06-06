namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NguoiDung_PhongBan
	{
		[Key]
		public int NguoiDungPhongBanID { get; set; }

		public int? NguoiDungID { get; set; }

		public int? PhongBanID { get; set; }

		public int? XetDuyet { get; set; }

		public int? CongTyID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
