namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsHoSoThuTuc
	{
		[Key]
		public int HoSoThuTucID { get; set; }

		public string Ten { get; set; }

		public string TenHoSoThuTucTA { get; set; }

		public string TenFile { get; set; }

		public bool? BatBuoc { get; set; }

		public int? LoaiHopDongID { get; set; }

		public int? HanHoanThanh { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
