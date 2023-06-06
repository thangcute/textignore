namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_ChucVu_OnBoarding
	{
		[Key]
		public int ChucVuOnBoardingID { get; set; }

		public int? ChucVuID { get; set; }

		public int? OnBoardingItemID { get; set; }

		public string MaXetDuyet { get; set; }

		public bool? Active { get; set; }

		public string MoTa { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
