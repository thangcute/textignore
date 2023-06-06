namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsNhomCaLamViec
	{
		[Key]
		public int NhomCaLamViecID { get; set; }

		public string TenNhomCaLamViec { get; set; }

		public string MaNhomCaLamViec { get; set; }

		public string TenNhomCaLamViecTA { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
