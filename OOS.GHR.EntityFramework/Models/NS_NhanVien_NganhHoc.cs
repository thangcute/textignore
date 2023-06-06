namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_NganhHoc
	{
		[Key]
		public int NhanVienNganhHocID { get; set; }

		public int NganhHocID { get; set; }

		public int NhanVienID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string GhiChu { get; set; }

		public int? TruongDaoTaoID { get; set; }

		public int? NamTotNghiep { get; set; }
	}
}
