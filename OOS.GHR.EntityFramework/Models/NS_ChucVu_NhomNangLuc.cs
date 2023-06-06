namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_NhomNangLuc
	{
		[Key]
		public int ChucVuNhomNangLucID { get; set; }

		public int? ChucVuID { get; set; }

		public int? NhomNangLucID { get; set; }

		public int? TrongSo { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
