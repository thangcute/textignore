namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_DotTuyenDung
	{
		[Key]
		public int DotTuyenDungID { get; set; }

		public string TenDotTuyenDung { get; set; }

		public string SoQuyetDinh { get; set; }

		public int? SoLuong { get; set; }

		public byte? TrangThai { get; set; }

		public string GhiChu { get; set; }

		public int? ChiPhiTuyenDung { get; set; }

		public int? KeHoachTuyenDungID { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public DateTime? NgayKetThucThucTe { get; set; }

		public DateTime? NgayNhanHoSo { get; set; }

		public DateTime? NgayDuKienDiLam { get; set; }

		public int? ViTriTuyenDungID { get; set; }

		public int? HinhThucLamViecID { get; set; }

		public int CongTyID { get; set; }

		public int? NhuCauTuyenDungID { get; set; }

		public int? PhongBanYeuCauID { get; set; }

		public int? NguoiYeuCauID { get; set; }

		public string NganhNgheID { get; set; }

		public int? NgoaiNguID { get; set; }

		public int? NguoiPhuTrachID { get; set; }

		public int? TrinhDoID { get; set; }

		public int? KhuVucID { get; set; }

		public string MucLuong { get; set; }

		public string GioiTinh { get; set; }

		public string DoTuoi { get; set; }

		public string TinhTrangHonNhan { get; set; }

		public int? SoNamKinhNghiem { get; set; }

		public string DiaDiemLamViec { get; set; }

		public string DonViDangTuyen { get; set; }

		public string MoTaCongViec { get; set; }

		public string MoTaTuyenDung { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
