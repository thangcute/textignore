namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_LopDaoTao_ThiSinh
	{
		[Key]
		public int LopDaoTaoThiSinhID { get; set; }

		public int? LopDaoTaoID { get; set; }

		public int? ThiSinhID { get; set; }

		public string NhanXet { get; set; }

		public decimal? TongDiem { get; set; }

		public int? XepLoaiID { get; set; }

		public string KetLuan { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? TrangThai { get; set; }

		public decimal? ViTinh { get; set; }

		public int? NguoiChuyenID { get; set; }

		public DateTime? NgayChuyen { get; set; }

		public decimal? DiemKhac { get; set; }

		public int? TrangThaiNghiID { get; set; }

		public int? KetQuaDanhGiaDaoTaoID { get; set; }

		public string MaSo { get; set; }

		public decimal? Diem4 { get; set; }

		public decimal? Diem5 { get; set; }

		public decimal? Diem6 { get; set; }

		public string GhiChu { get; set; }
	}
}
