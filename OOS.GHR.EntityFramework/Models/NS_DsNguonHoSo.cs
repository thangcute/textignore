namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsNguonHoSo
	{
		[Key]
		public int NguonHoSoID { get; set; }

		public string TenNguonHoSo { get; set; }

		public string MoTa { get; set; }

		public string TenNguonHoSoTA { get; set; }

		public string Keywords { get; set; }

		public string ParseXML { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
