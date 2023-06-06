namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_BoTieuChi
	{
		[Key]
		public int NhanVienBoTieuChiID { get; set; }

		public int? NhanVienID { get; set; }

		public string BoTieuChiStr { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public DateTime? NgayTao_DotDanhGia { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
