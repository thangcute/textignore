namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NguoiDung_NhomXetDuyet
	{
		[Key]
		public int NguoiDungNhomXetDuyetID { get; set; }

		public int? NguoiDungID { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public int? NhanVienID { get; set; }

		public decimal? HeSo { get; set; }

		public bool? IsTruongNhom { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
