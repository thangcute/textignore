namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_KeHoachTuyenDung
	{
		[Key]
		public int KeHoachTuyenDungID { get; set; }

		public int? CongTyID { get; set; }

		public int? KeHoachChaID { get; set; }

		public string Ten { get; set; }

		public string Ma { get; set; }

		public string YeuCau { get; set; }

		public string GhiChu { get; set; }

		public string SoQuyetDinh { get; set; }

		public int? TrangThai { get; set; }

		public decimal? ChiPhiDuKien { get; set; }

		public string NguonDuKien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string TenKeHoachTuyenDungTA { get; set; }

		public DateTime? ThoiGianDangTuyen { get; set; }

		public DateTime? ThoiGianToChucTuyenDung { get; set; }

		public DateTime? ThoiGianHoanThanh { get; set; }

		public DateTime? NgayTao { get; set; }
	}
}
