namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_ChiTietCongThucLuong_LichSu
	{
		[Key]
		public int LichSuCongThucLuongID { get; set; }

		public int? LoaiLuongID { get; set; }

		public string CongThucTinh { get; set; }

		public string CongThucTinhBackdate { get; set; }

		public DateTime? NgayChinhSua { get; set; }

		public int? NhomCongThucLuongID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
