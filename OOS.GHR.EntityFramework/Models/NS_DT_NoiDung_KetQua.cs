namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_NoiDung_KetQua
	{
		[Key]
		public int NoiDung_KetQuaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? NoiDungDaoTaoID { get; set; }

		public int? KetQuaDaoTaoID { get; set; }

		public decimal? DiemSo { get; set; }

		public string NhanXet { get; set; }

		public string KetQuaDaoTaoStr { get; set; }

		public string ResetKetQua { get; set; }

		public int? ResetLanThu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
