namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_DotTuyenDung_NguonUngVien
	{
		[Key]
		public int DotTuyenDungNguonUngVienID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? NguonHoSoID { get; set; }

		public string LinkUngVien { get; set; }

		public bool? DownloadUngVien { get; set; }
	}
}
