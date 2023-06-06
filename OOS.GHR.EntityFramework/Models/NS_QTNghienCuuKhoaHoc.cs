namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTNghienCuuKhoaHoc
	{
		[Key]
		public int QTNgienCuuKHID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? CongTyID { get; set; }

		public int? NhanVienID { get; set; }

		public string SoQuyetDinh { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public string CapQuanLy { get; set; }

		public string CapChuTri { get; set; }

		public string ChucDanhThamGia { get; set; }

		public string TenDeTaiDuAn { get; set; }

		public int? GhiChuID { get; set; }

		public string GhiChu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }
	}
}
