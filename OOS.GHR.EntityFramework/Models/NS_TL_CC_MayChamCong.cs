namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_CC_MayChamCong
	{
		[Key]
		public int MayChamCongID { get; set; }

		public string Ten { get; set; }

		public string IP { get; set; }

		public string Serial { get; set; }

		public string Key { get; set; }

		public string ProviderName { get; set; }

		public string DiaChi_KhuVuc { get; set; }

		public bool? ChinhVao { get; set; }

		public int? Loai { get; set; }

		public int? CongGiaoTiep { get; set; }

		public int? BaudRate { get; set; }

		public int? Port { get; set; }

		public int? KieuKetNoi { get; set; }

		public DateTime? DuLieuDocLanCuoi { get; set; }

		public bool? XoaDuLieuSauKhiImport { get; set; }

		public bool? DuLieuKhongTongHop { get; set; }

		public TimeSpan? LichDownload { get; set; }

		public bool IsDownloading { get; set; }

		public long? SoBanGhi { get; set; }

		public int? SoLuongBanGhiCuoiCung { get; set; }

		public int? SoLuongLuuVaoDB { get; set; }

		public int? CongTyID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
