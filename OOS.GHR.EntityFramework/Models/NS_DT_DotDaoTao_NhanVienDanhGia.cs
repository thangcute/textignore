namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_NhanVienDanhGia
	{
		[Key]
		public int NhanVienDanhGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NoiDungDaoTaoID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? TieuChiDanhGiaID { get; set; }

		public string TenTieuChiDanhGia { get; set; }

		public string DanhGia { get; set; }

		public string GhiChu { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }
	}
}
