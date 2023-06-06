namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_LopDaoTao
	{
		[Key]
		public int LopDaoTaoID { get; set; }

		public string TenLop { get; set; }

		public int? GiaoVienChuNhiemID { get; set; }

		public DateTime? NgayKhaiGiang { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public int? TrangThai { get; set; }

		public int? NguoiTaoID { get; set; }

		public DateTime? NgayTao { get; set; }

		public int? NguoiSuaID { get; set; }

		public DateTime? NgaySua { get; set; }

		public decimal? TongDiemYeuCau { get; set; }

		public int? LoaiLopID { get; set; }

		public int? SoLuongHocVien { get; set; }

		public int? CongTyID { get; set; }

		public string GhiChu { get; set; }

		public string BoPhan { get; set; }

		public string DiaDiem { get; set; }

		public string DoiTac { get; set; }

		public string DonViToChuc { get; set; }

		public string MucDich { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string TenLopDaoTaoTA { get; set; }
	}
}
