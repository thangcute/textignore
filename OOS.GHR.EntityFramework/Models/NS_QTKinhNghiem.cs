namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTKinhNghiem
	{
		[Key]
		public int KinhNghiemID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NganhNgheID { get; set; }

		public int? ThiSinhID { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public string TenNganhNghe { get; set; }

		public string NoiLamViec { get; set; }

		public string ChucVu { get; set; }

		public string GhiChu { get; set; }

		public int? XetDuyet { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string LyDoNghiViec { get; set; }

		public decimal? MucLuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
