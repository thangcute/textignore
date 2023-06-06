namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_MauBaoCao
	{
		[Key]
		public int MauBaoCaoID { get; set; }

		public string TenBaoCao { get; set; }

		public int? Type { get; set; }

		public string MaBaoCao { get; set; }

		public string NhomBaoCao { get; set; }

		public string SQLCommand { get; set; }

		public byte[] Data { get; set; }

		public string LoaiBaoCao { get; set; }
	}
}
