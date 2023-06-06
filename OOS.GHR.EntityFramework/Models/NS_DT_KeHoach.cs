namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_KeHoach
	{
		[Key]
		public int KeHoachID { get; set; }

		public string MaKeHoachDaoTao { get; set; }

		public string TenKeHoachDaoTao { get; set; }

		public string TenKeHoachDaoTaoTA { get; set; }

		public int? NguoiLapID { get; set; }

		public DateTime? NgayLap { get; set; }

		public int? PhongBanLapID { get; set; }

		public int? HinhThucDaoTaoID { get; set; }

		public int? DoiTuongDaoTaoID { get; set; }

		public int? CongTyID { get; set; }

		public int? SoLuongHocVien { get; set; }

		public long? NganSachDaoTao { get; set; }

		public string NoiDungDaoTao { get; set; }

		public string DienGiaiChiPhi { get; set; }

		public string DiaDiemDaoTao { get; set; }

		public string DonViDaoTao { get; set; }

		public string MucTieuDaoTao { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public string GhiChu { get; set; }

		public int? TrangThai { get; set; }

		public string ImageURL { get; set; }

		public string NhomChucVuStrID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
