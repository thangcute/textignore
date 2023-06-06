namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_XetDuyet
	{
		[Key]
		public int XetDuyetID { get; set; }

		public string TenXetDuyet { get; set; }

		public int? QuyTrinhID { get; set; }

		public int? StepIndex { get; set; }

		public int? NhanVienID { get; set; }

		public int? ObjectID { get; set; }

		public string ObjectCode { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgayTao { get; set; }

		public string GhiChu { get; set; }

		public string TenNguoiTao { get; set; }

		public string MaNVNguoiTao { get; set; }

		public string ChucVu_NguoiTao { get; set; }

		public string PhongBan_NguoiTao { get; set; }

		public string ApproveKey { get; set; }

		public int? TrangThai { get; set; }

		public string NguoiDangChoDuyet { get; set; }

		public string NguoiDuyetCuoi { get; set; }

		public DateTime? NgayDuyetCuoi { get; set; }

		public bool? HienThiTrungTamPheDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
