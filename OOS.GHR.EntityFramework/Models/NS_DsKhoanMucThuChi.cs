namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsKhoanMucThuChi
	{
		[Key]
		public int KhoanMucThuChiID { get; set; }

		public string Ma { get; set; }

		public string Ten { get; set; }

		public int? Type { get; set; }

		public bool? HienThiBangLuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string TenKhoanMucThuChiTA { get; set; }
	}
}
