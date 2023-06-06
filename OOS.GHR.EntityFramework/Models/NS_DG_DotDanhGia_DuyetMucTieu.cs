namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_DotDanhGia_DuyetMucTieu
	{
		[Key]
		public int DuyetMucTieuID { get; set; }

		public int? DotDanhGiaID { get; set; }

		public int? NguoiDuyetMucTieuID { get; set; }

		public DateTime? NgayGuiYeuCau { get; set; }

		public DateTime? NgayDuyet { get; set; }

		public string YKienPheDuyet { get; set; }

		public int? XetDuyet { get; set; }

		public string HoTenNguoiDuyet { get; set; }

		public string ChucVuNguoiDuyet { get; set; }

		public string PhongBanNguoiDuyet { get; set; }
	}
}
