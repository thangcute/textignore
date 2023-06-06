namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_QuyTrinh_BuocThucHien
	{
		[Key]
		public int BuocThucHienID { get; set; }

		public int? QuyTrinhID { get; set; }

		public string TenBuocThucHien { get; set; }

		public string TenBuocThucHienTA { get; set; }

		public int? ThuTu { get; set; }

		public string SQLXetDuyetThanhCong { get; set; }

		public bool? IsViewMode { get; set; }

		public int? LoaiPheDuyetID { get; set; }

		public int? NhanVienPheDuyetID { get; set; }

		public int? CapPhongBanID { get; set; }

		public bool? IsBuocThietLapMucTieu { get; set; }

		public int? RejectActionType { get; set; }

		public int? NhomUserDanhGiaID { get; set; }
	}
}
