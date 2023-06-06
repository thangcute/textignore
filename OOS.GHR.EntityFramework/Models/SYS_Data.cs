namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Data
	{
		[Key]
		public int DataID { get; set; }

		public string DataCode { get; set; }

		public byte[] Data { get; set; }

		public string TenBaoCao { get; set; }

		public int? Type { get; set; }

		public byte[] DataLayout { get; set; }

		public string DataType { get; set; }

		public string FormLayout { get; set; }

		public string ReportCode { get; set; }
	}
}
