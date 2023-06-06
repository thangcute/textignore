namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_DatCoc
	{
		[Key]
		public int DatCocID { get; set; }

		public int? NhanVienID { get; set; }

		public DateTime? NgayDatCoc { get; set; }

		public string GhiChu { get; set; }

		public decimal? SoTien { get; set; }

		public int? Type { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
