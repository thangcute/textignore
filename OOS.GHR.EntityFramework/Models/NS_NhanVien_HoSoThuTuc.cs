namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_HoSoThuTuc
	{
		[Key]
		public int NhanVien_HoSoThuTucID { get; set; }

		public int? NhanVienID { get; set; }

		public int? HoSoThuTucID { get; set; }

		public DateTime? NgayNop { get; set; }

		public string NguoiNhan { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgayTao { get; set; }

		public string GhiChu { get; set; }

		public DateTime? HanNop { get; set; }

		public bool? DaNop { get; set; }

		public int? NguoiNhanID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string NguoiTra { get; set; }

		public DateTime? NgayTra { get; set; }
	}
}
