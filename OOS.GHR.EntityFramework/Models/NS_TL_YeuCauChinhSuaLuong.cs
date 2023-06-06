namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_YeuCauChinhSuaLuong
	{
		[Key]
		public int YeuCauChinhSuaLuongID { get; set; }

		public int? ChiTietLuongID { get; set; }

		public int? NguoiYeuCauID { get; set; }

		public DateTime? NgayYeuCau { get; set; }

		public decimal? GiaTri { get; set; }

		public string GiaTriStr { get; set; }

		public int? XetDuyet { get; set; }

		public decimal? GiaTriGoc { get; set; }

		public string GiaTriGocStr { get; set; }

		public string LyDoYeuCau { get; set; }
	}
}
