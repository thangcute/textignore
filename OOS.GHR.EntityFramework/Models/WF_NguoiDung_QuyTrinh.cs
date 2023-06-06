namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class WF_NguoiDung_QuyTrinh
	{
		[Key]
		public int NguoiDungQuyTrinhID { get; set; }

		public int? NguoiDungID { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? ChucVuID { get; set; }

		public int? ChucDanhID { get; set; }

		public int? CongTyID { get; set; }

		public int? LevelMin { get; set; }

		public int? LevelMax { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? NhomXetDuyetID { get; set; }
	}
}
