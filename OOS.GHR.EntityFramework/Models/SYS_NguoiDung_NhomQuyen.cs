namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NguoiDung_NhomQuyen
	{
		[Key]
		public int NguoiDungNhomQuyenID { get; set; }

		public string NhomQuyen { get; set; }

		public int? NguoiDungID { get; set; }

		public int? DoiTuongID { get; set; }

		public int? Quyen { get; set; }

		public string DoiTuongStr { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
