namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class PhongBan_ChucVu
	{
		[Key]
		public int PhongBanChucVuID { get; set; }

		public string MaViTri { get; set; }

		public string MaChiPhiHachToan { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? NgachID { get; set; }

		public int? BacID { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucVuTA { get; set; }

		public string MaChucVu { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public DateTime? NgayHetHan { get; set; }

		public int? DinhBien { get; set; }

		public bool? IsViTriLanhDao { get; set; }

		public bool? IsDeleted { get; set; }

		public string QuanLyTrucTiepID { get; set; }

		public string QuanLyGianTiepID { get; set; }

		public int? ThuTu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
