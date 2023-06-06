namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DGNL_DotDanhGia_ChucVu_DiemChuan
	{
		[Key]
		public int NangLucChucVuID { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? NangLucID { get; set; }

		public int? ChucVuID { get; set; }

		public int? DiemChuan { get; set; }

		public string TenNangLucChuan { get; set; }
	}
}
