namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTQuanHeGiaDinh
	{
		[Key]
		public int QTQuanHeGiaDinhID { get; set; }

		public int? NhanVienID { get; set; }

		public int? ThiSinhID { get; set; }

		public string QuanHe { get; set; }

		public string HoTen { get; set; }

		public DateTime? NgaySinh { get; set; }

		public int? NamSinh { get; set; }

		public string NgheNghiepHienNay { get; set; }

		public string DiaChiThuongTru { get; set; }

		public string GhiChu { get; set; }

		public string GioiTinh { get; set; }

		public string DienThoaiLienHe { get; set; }

		public bool? GiamTru { get; set; }

		public DateTime? NgayBatDauGiamTru { get; set; }

		public DateTime? NgayKetThucGiamTru { get; set; }

		public string MaSoThue { get; set; }

		public string CMTND { get; set; }

		public string QuocTich { get; set; }

		public string GiayKhaiSinhSo { get; set; }

		public string QuyenSo { get; set; }

		public string NoiDangKyGiayKhaiSinh { get; set; }

		public int? XetDuyet { get; set; }

		public bool? IsDied { get; set; }

		public bool? IsLienLacKhanCap { get; set; }

		public DateTime? NgayHieuLucNuoiConNho { get; set; }

		public DateTime? NgayHieuLucTroCapConNho { get; set; }

		public int? ThonXomID { get; set; }

		public int? XaPhuongID { get; set; }

		public int? QuanHuyenID { get; set; }

		public int? TinhID { get; set; }

		public string SoNha { get; set; }

		public string QuanHe_ChuHo { get; set; }

		public string MaSoBHXH { get; set; }

		public string SoSoBHXH { get; set; }

		public int? LoaiGiayToID { get; set; }

		public string SoHoKhau { get; set; }

		public string NgaySinh_Loai { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string MaNhanVien_ThanNhan { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
