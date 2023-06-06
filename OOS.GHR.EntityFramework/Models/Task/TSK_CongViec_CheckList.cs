namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_CongViec_CheckList
	{
		[Key]
		public int CheckListID { get; set; }

		public int? CongViecID { get; set; }

		public string MoTa { get; set; }

		public int? Tyle { get; set; }

		public bool? IsHoanThanh { get; set; }
	}
}
