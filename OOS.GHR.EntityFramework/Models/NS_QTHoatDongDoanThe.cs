namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTHoatDongDoanThe
	{
		[Key]
		public int QTHoatDongDoanTheID { get; set; }

		public int NhanVienID { get; set; }

		public DateTime? NgayThamGia { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string TenDoanThe { get; set; }

		public string ChucVuCaoNhat { get; set; }

		public string LyDoThamGia { get; set; }

		public string NhanXet { get; set; }

		public int? GhiChuID { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public DateTime? NgayChinhThuc { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }
	}
}
