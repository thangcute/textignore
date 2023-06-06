namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_NhuCau_NhanVien
	{
		[Key]
		public int NhuCauNhanVienID { get; set; }

		public int? NhuCauDaoTaoID { get; set; }

		public int? NhanVienID { get; set; }

		public string HoVaTen { get; set; }

		public string TenChucVu { get; set; }

		public string TenPhongBan { get; set; }

		public string GhiChu { get; set; }

		public DateTime? NgayDangKy { get; set; }

		public int? TrangThai { get; set; }

		public string DoiTuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
