namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao
	{
		[Key]
		public int DotDaoTaoID { get; set; }

		public string MaDotDaoTao { get; set; }

		public string TenDotDaoTao { get; set; }

		public string TenDotDaoTaoTA { get; set; }

		public int? CongTyID { get; set; }

		public int? KhoaDaoTaoID { get; set; }

		public int? HinhThucDaoTaoID { get; set; }

		public int? NganhHocID { get; set; }

		public int? VanBangID { get; set; }

		public int? KeHoachID { get; set; }

		public int? ChungChiID { get; set; }

		public int? YeuCauKhoaHocID { get; set; }

		public int? NguoiPhuTrachID { get; set; }

		public string NguoiPhuTrach { get; set; }

		public DateTime? NgayCapChungChi { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public DateTime? NgayGuiBaoCaoDaoTao { get; set; }

		public DateTime? DangKyTuNgay { get; set; }

		public DateTime? DangKyToiNgay { get; set; }

		public DateTime? NgayHetHanChungChi { get; set; }

		public string LopDaoTao { get; set; }

		public string NoiDaoTao { get; set; }

		public string NguoiLienHe { get; set; }

		public string NuocDaoTao { get; set; }

		public int? ChiPhiCongTy { get; set; }

		public int? ChiPhiCaNhan { get; set; }

		public int? ChiPhiKhac { get; set; }

		public int? TrangThai { get; set; }

		public int? TrangThaiHocVien { get; set; }

		public string MoTa { get; set; }

		public string SoQuyetDinh { get; set; }

		public string DoiTuongDaoTao { get; set; }

		public bool? ChoPhepNhanVienDangKy { get; set; }

		public int? SoLuongHocVien { get; set; }

		public int? SoLuongHocVienDangKy { get; set; }

		public bool? IsBatBuocDangKy { get; set; }

		public decimal? ThoiLuongDuKien { get; set; }

		public decimal? ThoiLuong_SoBuoiDuKien { get; set; }

		public decimal? SoThangCamKetDaoTao { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string NoiDungChuanBi { get; set; }
	}
}
