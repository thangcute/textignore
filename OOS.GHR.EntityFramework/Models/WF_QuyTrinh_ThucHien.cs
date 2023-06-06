namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class WF_QuyTrinh_ThucHien
	{
		[Key]
		public int QuyTrinhThucHienID { get; set; }

		public int? QuyTrinhBuocThucHienID { get; set; }

		public int? NguoiDungXetDuyetID { get; set; }

		public int? NhomXetDuyetID { get; set; }

		public int? XetDuyetID { get; set; }

		public int? TrangThai { get; set; }

		public int? ThuTuThucHien { get; set; }

		public DateTime? NgayThucHien { get; set; }

		public DateTime? NgayChuyenYeuCau { get; set; }

		public string ApproveKey { get; set; }

		public string GhiChu { get; set; }

		public string LyDoTuChoi { get; set; }

		public string NguoiXetDuyet { get; set; }

		public string ChucVuNguoiXetDuyet { get; set; }

		public DateTime? ThoiHanDuyetYeuCau { get; set; }

		public string TenNguoiDuyetTiepTheo { get; set; }

		public string TenNhomDuyetTiepTheo { get; set; }

		public string TenBuocDuyetTiepTheo { get; set; }

		public string ChucVuNguoiDuyetTiepTheo { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
