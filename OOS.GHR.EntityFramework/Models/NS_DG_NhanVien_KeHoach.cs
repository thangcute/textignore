namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DG_NhanVien_KeHoach
	{
		[Key]
		public int NhanVienKeHoachID { get; set; }

		public int? NhomDanhGiaID { get; set; }

		public int? NhanVienID { get; set; }

		public int? TieuChiID { get; set; }

		public decimal? MucTieu { get; set; }

		public DateTime? Deadline { get; set; }

		public decimal? TrongSo { get; set; }

		public int? Thang { get; set; }

		public int? Quy { get; set; }

		public int? Nam { get; set; }

		public string MoTa { get; set; }

		public int? XetDuyet { get; set; }

		public int? NguoiXetDuyetID { get; set; }

		public DateTime? NgayXetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? BoTieuChiID { get; set; }

		public string TenTieuChi { get; set; }
	}
}
