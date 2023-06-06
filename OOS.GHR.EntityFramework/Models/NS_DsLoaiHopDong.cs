namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsLoaiHopDong
	{
		[Key]
		public int LoaiHopDongID { get; set; }

		public string TenLoaiHopDong { get; set; }

		public string TenLoaiHopDongTA { get; set; }

		public string Keyword { get; set; }

		public string MaLoaiHD { get; set; }

		public int? Ngay { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public int? SoNgay { get; set; }

		public int? ThoiGianBaoTruoc { get; set; }

		public int? TrangThaiNhanSuID { get; set; }

		public bool? IsThamGiaBHXH { get; set; }

		public bool? IsLoaiHopDongThueLuyTien { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
