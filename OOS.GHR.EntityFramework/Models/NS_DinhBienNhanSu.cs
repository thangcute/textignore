namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DinhBienNhanSu
	{
		[Key]
		public int DinhBienNhanSuID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? Nam { get; set; }

		public string GhiChu { get; set; }

		public int? Thang { get; set; }

		public int? SoLanThayDoiDinhBien { get; set; }

		public int? SoLuong { get; set; }

		public int? SLBanDau { get; set; }

		public string MaViTri { get; set; }

		public int? PhongBanChucVuID { get; set; }

		public int? XetDuyet { get; set; }

		public int? SoLuongTuyenMoi { get; set; }

		public int? SoLuongTuyenNoiBo { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? SLNhanSuDuKien { get; set; }
	}
}
