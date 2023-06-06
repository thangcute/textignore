namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_LichSuThayDoiThongTin
	{
		[Key]
		public int LichSuThayDoiThongTinID { get; set; }

		public int? NguoiThamGiaID { get; set; }

		public string NoiDungDieuChinh { get; set; }

		public string GiaTriCu { get; set; }

		public string GiaTriMoi { get; set; }

		public DateTime? NgayThang { get; set; }

		public string CanCuDieuChinh { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgayTao { get; set; }
	}
}
