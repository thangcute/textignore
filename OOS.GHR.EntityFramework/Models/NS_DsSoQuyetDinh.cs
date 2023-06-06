namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsSoQuyetDinh
	{
		[Key]
		public int SoQuyetDinhID { get; set; }

		public string Ten { get; set; }

		public string SoQuyetDinh { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public string ChucVu { get; set; }

		public string TomTatNoiDung { get; set; }

		public int? LoaiQuyetDinhID { get; set; }

		public DateTime? NgayBatDauHieuLuc { get; set; }

		public DateTime? NgayHetHieuLuc { get; set; }

		public DateTime? NgayTao { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgaySua { get; set; }

		public int? NguoiSuaID { get; set; }

		public int? TrangThai { get; set; }

		public int? NguoiQuyetDinhID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public byte[] FileQuyetDinh { get; set; }

		public string TenFile { get; set; }
	}
}
