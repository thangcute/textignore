namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_BangLuong
	{
		[Key]
		public int BangLuongID { get; set; }

		public int NhanVienID { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public string NgayQuyetDinh { get; set; }

		public string GhiChu { get; set; }

		public int? QTDienBienLuongID { get; set; }

		public int? ChucDanhID { get; set; }

		public int? NhomLuongID { get; set; }

		public int? CongTyID { get; set; }

		public bool? Lock { get; set; }

		public int? XetDuyet { get; set; }

		public string MaDotTinhLuong { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string TenNhomLuong { get; set; }

		public bool? IsEmailSent { get; set; }

		public string UniqueID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }
	}
}
