namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_BHXHTongHop
	{
		[Key]
		public int BHXHTongHopID { get; set; }

		public int? NguoiThamGiaID { get; set; }

		public decimal? MucDongBH_Cu { get; set; }

		public decimal? MucDongBH_Moi { get; set; }

		public string TuThangNam { get; set; }

		public string ToiThangNam { get; set; }

		public decimal? TyleDong { get; set; }

		public bool? DaCoSoBHXH { get; set; }

		public string GhiChu { get; set; }

		public int? TangGiam { get; set; }

		public string Nhom { get; set; }

		public string LoaiNhom { get; set; }

		public int? Thang { get; set; }

		public int? Nam { get; set; }

		public DateTime? NgayLap { get; set; }

		public bool? IsChinhSua { get; set; }

		public DateTime? NgayDeNghiCapSo { get; set; }

		public DateTime? NgayNghiViec { get; set; }

		public string ThangNamDongBH { get; set; }

		public string ThangNamNghiViec { get; set; }

		public string MaTinh { get; set; }

		public string MaBV { get; set; }

		public string KeyID { get; set; }

		public string TenChucVu { get; set; }

		public int? NoiDongBHXHID { get; set; }

		public string LyDo { get; set; }
	}
}
