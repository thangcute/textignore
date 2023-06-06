namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_HopDong
	{
		[Key]
		public int HopDongID { get; set; }

		public string MaHopDong { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? NguoiKyID { get; set; }

		public int? ChucDanhID { get; set; }

		public int? HopDongChinhID { get; set; }

		public int? QTDienBienLuongID { get; set; }

		public int? NguoiKyChucDanhID { get; set; }

		public int? ChucVuID { get; set; }

		public int? LoaiHopDongID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public DateTime? NgayKyHD { get; set; }

		public int? MucLuong { get; set; }

		public int? KyHDLanThu { get; set; }

		public string NguoiKyHD { get; set; }

		public string NoiDung { get; set; }

		public string GhiChu { get; set; }

		public string CacLoaiPhuCap { get; set; }

		public string BacLuong { get; set; }

		public string HeSoLuong { get; set; }

		public string TenFileHD { get; set; }

		public string DiaDiemLamViec { get; set; }

		public string ThoiGianLamViec { get; set; }

		public byte[] FileHopDong { get; set; }

		public byte[] PDFFile { get; set; }

		public int? XetDuyet { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public DateTime? ApprovedDate { get; set; }

		public string ApprovedBy { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public int? eContractID { get; set; }

		public string iSignFileStoreID { get; set; }

		public int? MauHopDongID { get; set; }
	}
}
