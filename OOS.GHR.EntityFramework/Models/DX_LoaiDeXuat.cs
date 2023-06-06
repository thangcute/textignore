namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class DX_LoaiDeXuat
	{
		[Key]
		public int LoaiDeXuatID { get; set; }

		public string MaLoaiDeXuat { get; set; }

		public string TenLoaiDeXuat { get; set; }

		public string XetDuyetCode { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
