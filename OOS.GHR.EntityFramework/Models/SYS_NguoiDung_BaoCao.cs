namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NguoiDung_BaoCao
	{
		[Key]
		public int NguoiDungBaoCaoID { get; set; }

		public int? NguoiDungID { get; set; }

		public int? BaoCaoID { get; set; }

		public int? XetDuyet { get; set; }

		public int? Quyen { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
