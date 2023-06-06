namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_NhomXepLoaiDanhGia
	{
		[Key]
		public int NhomXepLoaiDanhGiaID { get; set; }

		public string TenNhomXepLoai { get; set; }

		public string MaNhomXepLoai { get; set; }

		public string TenNhomXepLoaiTA { get; set; }

		public string Mota { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
