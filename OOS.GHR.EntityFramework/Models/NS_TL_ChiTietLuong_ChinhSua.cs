namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_ChiTietLuong_ChinhSua
	{
		[Key]
		public int ChiTietLuongChinhSuaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public int? LoaiLuongID { get; set; }

		public decimal? TienLuong { get; set; }

		public decimal? GiaTriGoc { get; set; }

		public string TienLuongStr { get; set; }

		public string LyDoChinhSua { get; set; }

		public int? XetDuyet { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }

		public int? BangLuongID { get; set; }

		public string KeyToSearch { get; set; }
	}
}
