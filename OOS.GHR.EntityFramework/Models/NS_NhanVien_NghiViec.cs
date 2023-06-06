namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_NghiViec
	{
		[Key]
		public int NhanVienNghiViecID { get; set; }

		public int? NhanVienID { get; set; }

		public string SoQuyetDinh { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public int? LyDoNghiViecID { get; set; }

		public int? HinhThucNghiViecID { get; set; }

		public string NguoiQuyetDinh { get; set; }

		public int? NguoiQuyetDinhID { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public string GhiChu { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgayTao { get; set; }

		public DateTime? NgayDiLamLai { get; set; }

		public bool? DaCoNguoiThayThe { get; set; }

		public int? XetDuyet { get; set; }

		public int? NhanVienThayTheID { get; set; }

		public bool? IsChuyenQuanLyTrucTiep { get; set; }

		public bool? IsChuyenQuyTrinhDangChoDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
