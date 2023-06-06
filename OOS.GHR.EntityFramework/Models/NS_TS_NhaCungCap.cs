namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_NhaCungCap
	{
		[Key]
		public int NhaCungCapID { get; set; }

		public string TenNhaCungCap { get; set; }

		public string TenNguoiDaiDien { get; set; }

		public string DiaChi { get; set; }

		public string DienThoai { get; set; }

		public string Fax { get; set; }

		public string ThuDienTu { get; set; }

		public string Ghichu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
