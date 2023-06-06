namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_ChuHoKhau
	{
		[Key]
		public int ChuHoKhauID { get; set; }

		public int? NhanVienID { get; set; }

		public string HoVaTen { get; set; }

		public string DienThoai { get; set; }

		public DateTime? NgaySinh { get; set; }

		public string GhiChu { get; set; }

		public string SoHoKhau { get; set; }

		public int? LoaiGiayToID { get; set; }

		public string DiaChi { get; set; }

		public string SoNha { get; set; }

		public int? ThonXomID { get; set; }

		public int? XaPhuongID { get; set; }

		public int? QuanHuyenID { get; set; }

		public int? TinhID { get; set; }

		public string MoiQuanHe { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
