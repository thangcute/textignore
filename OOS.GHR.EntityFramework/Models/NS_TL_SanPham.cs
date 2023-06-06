namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_SanPham
	{
		[Key]
		public int SanPhamID { get; set; }

		public string TenSanPham { get; set; }

		public string MaSanPham { get; set; }

		public string DonViTinh { get; set; }

		public decimal? DonGia { get; set; }

		public int? NganhHangID { get; set; }

		public int? CongTyID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
