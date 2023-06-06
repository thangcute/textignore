namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTDaoTao
	{
		[Key]
		public int QTDaoTaoID { get; set; }

		public int? NhanVienID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public string NoiDaoTao { get; set; }

		public string GhiChu { get; set; }

		public int? HinhThucDaoTaoID { get; set; }

		public int? NganhHocID { get; set; }

		public int? VanBangID { get; set; }

		public int? NuocDaoTaoID { get; set; }

		public int? ChungChiID { get; set; }

		public int? KetQuaDaoTaoID { get; set; }

		public int? NoiDungDaoTaoID { get; set; }

		public int? DanhMucDaoTaoID { get; set; }

		public int? DoiTacDaoTaoID { get; set; }

		public int? TruongDaoTaoID { get; set; }

		public int? HeDaoTaoID { get; set; }

		public int? ChuyenNganhID { get; set; }

		public string LopDaoTao { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public int? ChiPhiCongTy { get; set; }

		public int? ChiPhiCaNhan { get; set; }

		public int? ChiPhiKhac { get; set; }

		public string SoQuyetDinh { get; set; }

		public string DiemSo { get; set; }

		public string NoiDungDaoTao { get; set; }

		public bool? SauKhiVaoCongTy { get; set; }

		public decimal? SoThangCamKetSauDaoTao { get; set; }

		public int? KhoaDaoTaoID { get; set; }

		public DateTime? NgayHetHan { get; set; }

		public DateTime? NgayCapChungChi { get; set; }

		public int? CongTyID { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }
	}
}
