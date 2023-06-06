namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class CKS_NhanVien
	{
		[Key]
		public int CKSNhanVienID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public string HoVaTen { get; set; }

		public DateTime? NgaySinh { get; set; }

		public string MaNhanVien { get; set; }

		public string GioiTinh { get; set; }

		public string DienThoai { get; set; }

		public string Email { get; set; }

		public string CMTND { get; set; }

		public string CMTND_NoiCap { get; set; }

		public string CCCD { get; set; }

		public string CCCD_NoiCap { get; set; }

		public string HoChieu { get; set; }

		public string HoChieu_NoiCap { get; set; }

		public string QuocGia { get; set; }

		public string DiaChi { get; set; }

		public string TenCongTy { get; set; }

		public string TenPhongBan { get; set; }

		public string TinhThanh { get; set; }

		public string ChucVu { get; set; }

		public int? TrangThai { get; set; }

		public string CKS_NhaCungCap { get; set; }

		public DateTime? CKS_TuNgay { get; set; }

		public DateTime? CKS_ToiNgay { get; set; }

		public string CKS_Serial { get; set; }

		public string CKS_LoaiChuKy { get; set; }

		public DateTime? NgayYeuCau { get; set; }

		public string Reason { get; set; }

		public string Location { get; set; }

		public string ImgSignaturePath { get; set; }
	}
}
