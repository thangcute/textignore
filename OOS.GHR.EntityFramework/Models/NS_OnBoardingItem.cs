namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_OnBoardingItem
	{
		[Key]
		public int OnBoardingItemID { get; set; }

		public int? OnBoardingGroupID { get; set; }

		public string OnBoardingItemCode { get; set; }

		public string OnBoardingItemName { get; set; }

		public string OnBoardingItemNameTA { get; set; }

		public string XetDuyetCode { get; set; }

		public string Note { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
