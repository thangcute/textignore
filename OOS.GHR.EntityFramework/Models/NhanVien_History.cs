namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NhanVien_History
	{
		[Key]
		public int NhanVienHistoryID { get; set; }

		public int? NhanVienID { get; set; }

		public string HoVaTen { get; set; }

		public string MaNhanVien { get; set; }

		public int? ViTriID { get; set; }

		public int? ChucVuID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucDanhID { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public DateTime? NgayHetHieuLuc { get; set; }

		public string TenChucVu { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucDanh { get; set; }

		public bool? NghiViec { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string Operation { get; set; }
	}
}
