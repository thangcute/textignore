namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTKhenThuong
	{
		[Key]
		public int QTKhenThuongID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? NhanVienID { get; set; }

		public int? HinhThucKhenThuongID { get; set; }

		public int? NguoiKyID { get; set; }

		public int? ThiSinhID { get; set; }

		public int? LyDoKhenThuongID { get; set; }

		public DateTime? NgayKhenThuong { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public string SoQuyetDinh { get; set; }

		public string CoQuanKhenThuong { get; set; }

		public string LyDoKhenThuong { get; set; }

		public string GhiChu { get; set; }

		public int? XetDuyet { get; set; }

		public decimal? SoTienThuong { get; set; }

		public bool? IsChiTraVaoLuong { get; set; }

		public DateTime? KyLuongChiTra { get; set; }

		public bool? IsKhauTruThue { get; set; }

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
