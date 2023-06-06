namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_SanPhamCongDoanLSX
	{
		[Key]
		public int SanPhamCongDoanLSXID { get; set; }

		public int? SanPhamID { get; set; }

		public int? CongDoanID { get; set; }

		public int? LenhSanXuatID { get; set; }

		public decimal? DonGia { get; set; }

		public int? XetDuyet { get; set; }
	}
}
