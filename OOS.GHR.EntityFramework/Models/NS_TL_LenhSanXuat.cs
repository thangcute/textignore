namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_LenhSanXuat
	{
		[Key]
		public int LenhSanXuatID { get; set; }

		public string MaLenhSanXuat { get; set; }

		public string TenLenhSanXuat { get; set; }

		public DateTime? NgayThang { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
