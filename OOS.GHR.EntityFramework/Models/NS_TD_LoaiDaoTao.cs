namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_LoaiDaoTao
	{
		[Key]
		public int LoaiDaoTaoID { get; set; }

		public string TenLoai { get; set; }

		public string MaLoai { get; set; }

		public int? STT { get; set; }

		public bool? ChiLoadXepLoai { get; set; }

		public bool? ChoPhepChuyenTrungTuyen { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string TenLoaiDaoTaoTA { get; set; }
	}
}
