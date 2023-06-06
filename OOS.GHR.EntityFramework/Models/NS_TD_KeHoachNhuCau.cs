namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_KeHoachNhuCau
	{
		[Key]
		public int KeHoachNhuCauID { get; set; }

		public int? KeHoachID { get; set; }

		public int? NhuCauID { get; set; }

		public int? SoLuong { get; set; }
	}
}
