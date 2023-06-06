namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_LichSuThayDoiThongTin
	{
		[Key]
		public int LichSuThayDoiThongTinID { get; set; }

		public string TenBang { get; set; }

		public string KeyFieldName { get; set; }

		public int? IDValue { get; set; }

		public string Mota { get; set; }

		public DateTime? NgayThayDoi { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
