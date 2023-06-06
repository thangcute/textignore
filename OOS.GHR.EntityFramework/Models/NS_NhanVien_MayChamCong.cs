namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_MayChamCong
	{
		[Key]
		public int NhanVienMayChamCongID { get; set; }

		public int? NhanVienID { get; set; }

		public int? MayChamCongID { get; set; }

		public string MaChamCong { get; set; }

		public bool? IsUploaded { get; set; }
	}
}
