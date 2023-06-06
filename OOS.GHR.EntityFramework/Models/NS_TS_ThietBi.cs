namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TS_ThietBi
	{
		[Key]
		public int ThietBiID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public string MaThietBi { get; set; }

		public string TenThietBi { get; set; }

		public string MoTa { get; set; }

		public string DonViTinh { get; set; }

		public string TinhTrang { get; set; }

		public byte[] Image { get; set; }

		public string Ghichu { get; set; }

		public string NhaCungCap { get; set; }

		public decimal? ThoiGianKhauHao { get; set; }

		public DateTime? NgayTinhKhauHao { get; set; }

		public decimal? GiaTri { get; set; }

		public int? NhomThietBiID { get; set; }

		public DateTime? NgayDuaVaoSuDung { get; set; }

		public int? ThietBiChaID { get; set; }

		public int? NhaCungCapID { get; set; }

		public int? NguoiKyID { get; set; }

		public string SoQuyetDinh { get; set; }

		public int? CaNhanYeuCauID { get; set; }

		public string MaPhu { get; set; }

		public string Day { get; set; }

		public int? Cot { get; set; }

		public int? Tang { get; set; }

		public string FileSo { get; set; }

		public DateTime? NgayInTaiLieu { get; set; }

		public DateTime? NgayGui { get; set; }

		public string MaDuAn { get; set; }

		public string KhoGiayTaiLieu { get; set; }

		public int? SoLuong { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
