namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_Quyen
	{
		[Key]
		public int QuyenID { get; set; }

		public string NhomQuyenID { get; set; }

		public string MaSo { get; set; }

		public string TenQuyen { get; set; }

		public string MoTa { get; set; }
	}
}
