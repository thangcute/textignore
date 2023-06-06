namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_BH_DonViDangKyBHXH
	{
		[Key]
		public int DonViDangKyBHXHID { get; set; }

		public string TenDonVi { get; set; }

		public string DiaChi { get; set; }

		public string DienThoai { get; set; }

		public byte[] Logo { get; set; }

		public string ThuDienTu { get; set; }

		public string MaDonVi { get; set; }

		public string MaSoThue { get; set; }

		public string TenTaiKhoan { get; set; }

		public string TaiNganHang { get; set; }

		public string SoTaiKhoan { get; set; }

		public string TaiKhoanNganHang { get; set; }

		public string Ten { get; set; }
	}
}
