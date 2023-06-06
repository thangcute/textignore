namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsXaPhuong
	{
		[Key]
		public int XaPhuongID { get; set; }

		public int? QuanHuyenID { get; set; }

		public string TenXaPhuong { get; set; }

		public string MaXaPhuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string Keyword { get; set; }

		public int? TinhID { get; set; }

		public string XaHuyenTinh { get; set; }
	}
}
