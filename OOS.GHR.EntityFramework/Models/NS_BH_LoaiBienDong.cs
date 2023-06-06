namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_LoaiBienDong
	{
		[Key]
		public int LoaiBienDongID { get; set; }

		public string Ten { get; set; }

		public string STTText { get; set; }

		public int? TangGiam { get; set; }

		public int? STT { get; set; }

		public int? DonViID { get; set; }
	}
}
