namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_KetQuaDaoTao
	{
		[Key]
		public int KetQuaDaoTaoID { get; set; }

		public string TenKetQuaDaoTao { get; set; }

		public string TenKetQuaDaoTaoTA { get; set; }

		public string GhiChu { get; set; }

		public int? STT { get; set; }

		public bool? IsDat { get; set; }

		public bool? IsKhongDat { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
