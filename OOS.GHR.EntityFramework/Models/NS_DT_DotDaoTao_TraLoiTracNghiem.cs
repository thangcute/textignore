namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_TraLoiTracNghiem
	{
		[Key]
		public int TraLoiTracNghiemID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? CauHoiTracNghiemID { get; set; }

		public int? NoiDungDaoTaoID { get; set; }

		public int? ThiTracNghiemID { get; set; }

		public int? DapAn { get; set; }

		public string DapAnStr { get; set; }

		public bool? IsCorrect { get; set; }

		public int? QuestionIndex { get; set; }

		public decimal? DiemSo { get; set; }

		public int? ChuDeTracNghiemID { get; set; }
	}
}
