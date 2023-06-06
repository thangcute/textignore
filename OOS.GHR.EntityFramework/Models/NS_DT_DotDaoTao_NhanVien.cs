namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_NhanVien
	{
		[Key]
		public int DotDaoTao_NhanVienID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? NhanVienID { get; set; }

		public int? KetQuaDaoTaoID { get; set; }

		public int? NguoiThemID { get; set; }

		public DateTime? NgayDangKy { get; set; }

		public DateTime? NgayGuiBaoCaoDaoTao { get; set; }

		public string HoVaTen { get; set; }

		public string TenChucVu { get; set; }

		public string TenPhongBan { get; set; }

		public string GhiChu { get; set; }

		public string NhanXet { get; set; }

		public string DoiTuong { get; set; }

		public string TrangThaiThamGia { get; set; }

		public decimal? TongDiem { get; set; }

		public int? XetDuyet { get; set; }

		public int? TrangThai { get; set; }

		public string SoChungChi { get; set; }

		public DateTime? NgayCapChungChi { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
