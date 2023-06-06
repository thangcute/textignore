namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_KhoaDaoTao
	{
		[Key]
		public int KhoaDaoTaoID { get; set; }

		public string TenKhoaDaoTao { get; set; }

		public string MaKhoaDaoTao { get; set; }

		public string TenKhoaDaoTaoTA { get; set; }

		public int? BoCauHoiTracNghiemID { get; set; }

		public string DonViDaoTao { get; set; }

		public int? TrangThai { get; set; }

		public int? DoiTuongDaoTaoID { get; set; }

		public string NhomChucVuStrID { get; set; }

		public int? ThoiLuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
