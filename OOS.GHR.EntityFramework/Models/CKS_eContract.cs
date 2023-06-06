namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class CKS_eContract
	{
		[Key]
		public int eContractID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? LanhDaoID { get; set; }

		public int? CKSNhanVienID { get; set; }

		public int? CKSLanhDaoID { get; set; }

		public int? TrangThai { get; set; }

		public DateTime? NgayTrinhKy { get; set; }

		public DateTime? NhanVien_NgayKy { get; set; }

		public DateTime? LanhDao_NgayKy { get; set; }

		public string NhanVien_MaNhanVien { get; set; }

		public string NhanVien_HoVaTen { get; set; }

		public string NhanVien_ChucVu { get; set; }

		public string NhanVien_TenPhongBan { get; set; }

		public string LanhDao_HoVaTen { get; set; }

		public string LanhDao_ChucVu { get; set; }

		public bool? IsNhanVienDaKy { get; set; }

		public bool? IsLanhDaoDaKy { get; set; }

		public string MoTa { get; set; }

		public string FileStoreID { get; set; }

		public string URLDownloadFile { get; set; }

		public string LoaiHopDong { get; set; }

		public DateTime? NgayKyHopDong { get; set; }

		public string SoHopDong { get; set; }

		public int? HopDongID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
