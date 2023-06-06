namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_NgoaiNgu
	{
		[Key]
		public int NhanVienNgoaiNguID { get; set; }

		public int? NgoaiNguID { get; set; }

		public int? NhanVienID { get; set; }

		public string TrinhDo { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string NoiDaoTao { get; set; }

		public string GhiChu { get; set; }
	}
}
