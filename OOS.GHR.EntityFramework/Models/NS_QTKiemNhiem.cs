namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTKiemNhiem
	{
		[Key]
		public int QTKiemNhiemID { get; set; }

		public int? NhanVienID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? ChucVuKiemNhiemID { get; set; }

		public string ChucVuKhac { get; set; }

		public int? PhongBanKiemNhiemID { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public string SoQuyetDinh { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string GhiChu { get; set; }

		public string TenChucVuKiemNhiem { get; set; }

		public int? SoCPKiemNhiem { get; set; }

		public int? ThuLaoKiemNhiem { get; set; }

		public int? ChucVuPhongBanID { get; set; }

		public string MaViTri { get; set; }

		public int? XetDuyet { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }

		public string CreatedByUser { get; set; }

		public string ApprovedByUser { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public DateTime? NgayQuyetDinh_KetThuc { get; set; }

		public string SoQuyetDinh_KetThuc { get; set; }
	}
}
