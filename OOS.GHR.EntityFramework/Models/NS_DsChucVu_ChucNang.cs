namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsChucVu_ChucNang
	{
		[Key]
		public int ChucVu_ChucNangID { get; set; }

		public int? ChucVuID { get; set; }

		public string MoTa { get; set; }

		public string TieuChiDanhGia { get; set; }
	}
}
