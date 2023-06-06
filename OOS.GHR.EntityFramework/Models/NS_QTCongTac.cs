namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTCongTac
	{
		[Key]
		public int QTCongTacID { get; set; }

		public string SoQuyetDinh { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public int? CongTyID { get; set; }

		public int? NhanVienID { get; set; }

		public int? LyDoCongTacID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public decimal? SoNgayCongTac { get; set; }

		public string NoiCongTac { get; set; }

		public string ChucVuCaoNhat { get; set; }

		public string CongViecCuThe { get; set; }

		public string GhiChu { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public int? XetDuyet { get; set; }

		public string LyDoHuyDangKy { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
