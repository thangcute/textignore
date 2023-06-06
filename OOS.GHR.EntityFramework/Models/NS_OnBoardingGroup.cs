namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_OnBoardingGroup
	{
		[Key]
		public int OnBoardingGroupID { get; set; }

		public string OnBoardingGroupCode { get; set; }

		public string OnBoardingGroupName { get; set; }

		public string OnBoardingGroupNameTA { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
