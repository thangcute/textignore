namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_BangChamCong
	{
		[Key]
		public int BangChamCongID { get; set; }

		public int NhanVienID { get; set; }

		public int? KyHieuChamCongID { get; set; }

		public DateTime NgayChamCong { get; set; }

		public int? LoaiChamCongID { get; set; }

		public bool? Lock { get; set; }

		public int? NhomTinhLuongID { get; set; }

		public int? CongTyID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }
	}
}
