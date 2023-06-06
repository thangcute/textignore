namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class PhongBan
	{
		[Key]
		public int PhongBanID { get; set; }

		public int? PhongBanChaID { get; set; }

		public int? CongTyID { get; set; }

		public int? KhoiID { get; set; }

		public int? ChucVuLanhDaoID { get; set; }

		public int? CapPhongBanID { get; set; }

		public int? NhomNghiepVuID { get; set; }

		public string MaPhongBan { get; set; }

		public string Ten { get; set; }

		public string TenPhongBanTA { get; set; }

		public string Keyword { get; set; }

		public string Mota { get; set; }

		public long? ThuTu { get; set; }

		public string ChucNang { get; set; }

		public string NhiemVu { get; set; }

		public byte[] Icon { get; set; }

		public bool? IsDeleted { get; set; }

		public DateTime? NgayHieuLuc { get; set; }

		public DateTime? NgayHetHieuLuc { get; set; }

		public string MaPBHoachToan { get; set; }

		public string MaChiNhanhHoachToan { get; set; }

		public int? Level { get; set; }

		public string OrgPath { get; set; }

		public int? SoLuongNhanSu { get; set; }

		public int? SoLuongViTri { get; set; }

		public int? NhanVienPhuTrachID { get; set; }

		public string HoVaTen_NhanSuPhuTrach { get; set; }

		public int? SoLuongDinhBien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
