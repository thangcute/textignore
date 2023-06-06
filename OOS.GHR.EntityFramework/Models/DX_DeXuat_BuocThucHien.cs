namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class DX_DeXuat_BuocThucHien
	{
		[Key]
		public int DeXuatBuocThucHienID { get; set; }

		public int? QuyTrinhBuocThucHienID { get; set; }

		public int? DeXuatID { get; set; }

		public int? NguoiDungXetDuyetID { get; set; }

		public int? TrangThai { get; set; }

		public DateTime? NgayThucHien { get; set; }

		public DateTime? NgayChuyenYeuCau { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public string ApproveKey { get; set; }

		public string NguoiXetDuyet { get; set; }

		public string ChucVuNguoiXetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
