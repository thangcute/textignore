namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_ThongTinKhac
	{
		[Key]
		public int ThongTinKhacID { get; set; }

		public int? NhanVienID { get; set; }

		public int? LoaiThongTinID { get; set; }

		public string GiaTri { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
