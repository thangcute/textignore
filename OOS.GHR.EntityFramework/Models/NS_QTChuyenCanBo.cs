namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTChuyenCanBo
	{
		[Key]
		public int QTChuyenCanBoID { get; set; }

		public int? CongTyID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanCuID { get; set; }

		public int? PhongBanMoiID { get; set; }

		public int? ChucVuCuID { get; set; }

		public int? ChucVuMoiID { get; set; }

		public int? ChucDanhCuID { get; set; }

		public int? ChucDanhMoiID { get; set; }

		public int? QTDienBienLuongID { get; set; }

		public int? NguoiKyID { get; set; }

		public DateTime? NgayChuyen { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public DateTime? NgayHetHan { get; set; }

		public string TenPhongBanCu { get; set; }

		public string TenPhongBanMoi { get; set; }

		public bool? NgoaiCongTy { get; set; }

		public string LyDoChuyen { get; set; }

		public int? XetDuyet { get; set; }

		public string TenChucVuMoi { get; set; }

		public string TenChucVuCu { get; set; }

		public string SoQuyetDinh { get; set; }

		public string TenChucDanhCu { get; set; }

		public string TenChucDanhMoi { get; set; }

		public int? LyDoChuyenID { get; set; }

		public bool? IsDeleted { get; set; }

		public int? Type { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }
	}
}
