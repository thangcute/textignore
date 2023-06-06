namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_XetDuyet_UyQuyen
	{
		[Key]
		public int XetDuyetUyQuyenID { get; set; }

		public int? NguoiUyQuyenID { get; set; }

		public int? NguoiDuocUyQuyenID { get; set; }

		public int? XetDuyetID { get; set; }

		public int? LoaiQuyTrinhID { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? TrangThai { get; set; }

		public DateTime? HieuLuc_TuNgay { get; set; }

		public DateTime? HieuLuc_ToiNgay { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }
	}
}
