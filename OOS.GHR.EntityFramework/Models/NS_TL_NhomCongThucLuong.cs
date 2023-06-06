namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_NhomCongThucLuong
	{
		[Key]
		public int NhomCongThucLuongID { get; set; }

		public string TenNhom { get; set; }

		public string MaNhom { get; set; }

		public bool? NhomChinh { get; set; }

		public int? NhomLuongBackDateID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
