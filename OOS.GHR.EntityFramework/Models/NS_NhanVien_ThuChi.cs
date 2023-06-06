namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_ThuChi
	{
		[Key]
		public int ThuChiID { get; set; }

		public int? NhanVienID { get; set; }

		public int? KhoanMucThuChiID { get; set; }

		public DateTime? Ngay { get; set; }

		public decimal? SoTien { get; set; }

		public int? NguoiThucHienID { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgayTao { get; set; }

		public string DienGiai { get; set; }

		public int? Status { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
