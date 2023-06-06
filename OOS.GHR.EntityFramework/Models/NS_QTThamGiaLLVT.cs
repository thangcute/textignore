namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTThamGiaLLVT
	{
		[Key]
		public int QTThamGiaLLVTID { get; set; }

		public int? NhanVienID { get; set; }

		public DateTime? NgayNhapNgu { get; set; }

		public DateTime? NgayXuatNgu { get; set; }

		public string QuanHam { get; set; }

		public string NgheNghiepChucVu { get; set; }

		public int? GhiChuID { get; set; }

		public int? QuanHamID { get; set; }

		public int? ChucVuQuanDoiID { get; set; }

		public int? DonViCongTacID { get; set; }

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
