namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTQuanHeThanNhanNuocNgoai
	{
		[Key]
		public int QTQuanHeThanNhanNuocNgoaiID { get; set; }

		public int? NhanVienID { get; set; }

		public string HoTen { get; set; }

		public int? NamSinh { get; set; }

		public string QuanHe { get; set; }

		public string NuocDinhCu { get; set; }

		public string QuocTich { get; set; }

		public int? NamDinhCu { get; set; }

		public int? GhiChuID { get; set; }

		public bool? GioiTinh { get; set; }

		public int? NuocDinhCuID { get; set; }

		public int? QuocTichID { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }
	}
}
