namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TLBD_QTDienBienLuong
	{
		[Key]
		public int QTDienBienLuongID { get; set; }

		public int? DienBienLuongID { get; set; }

		public int? NhanVienID { get; set; }

		public int? CongTyID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? NhomTinhLuongID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? LyDoThayDoiLuongID { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public DateTime? NgayHetHan { get; set; }

		public DateTime? NgayHuong { get; set; }

		public string SoQuyetDinh { get; set; }

		public string NoiLamViec { get; set; }

		public string LyDo { get; set; }

		public bool? LayCongTheoNhomCongThuc { get; set; }

		public DateTime? NgayHuongBackdate { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string CreatedBy { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? XetDuyet { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
