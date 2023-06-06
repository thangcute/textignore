namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class DX_DeXuat_DuLieuDeXuat
	{
		[Key]
		public int DuLieuDeXuatID { get; set; }

		public int? DeXuatID { get; set; }

		public int? LoaiDeXuatID { get; set; }

		public int? TruongDuLieuID { get; set; }

		public string TenTruongDuLieu { get; set; }

		public string GiaTri { get; set; }

		public byte[] GiaTriBinary { get; set; }

		public DateTime? GiaTriDateTime { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
