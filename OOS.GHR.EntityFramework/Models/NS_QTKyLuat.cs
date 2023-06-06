namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTKyLuat
	{
		[Key]
		public int QTKyLuatID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? NhanVienID { get; set; }

		public int? HinhThucKyLuatID { get; set; }

		public int? NguoiKyID { get; set; }

		public int? LyDoKyLuatID { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public DateTime? NgayKyLuat { get; set; }

		public DateTime? NgayHetHan { get; set; }

		public string LyDoKyLuat { get; set; }

		public string CoQuanKyLuat { get; set; }

		public string GhiChu { get; set; }

		public int? XetDuyet { get; set; }

		public decimal? SoTienPhat { get; set; }

		public bool? IsTruVaoLuong { get; set; }

		public DateTime? KyLuongChiTra { get; set; }

		public string SoQuyetDinh { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
