namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_OnBoardingItem
	{
		[Key]
		public int NhanVienOnBoardingID { get; set; }

		public int? NhanVienID { get; set; }

		public int? OnBoardingItemID { get; set; }

		public int? TrangThai { get; set; }

		public string OnBoardingItemName { get; set; }

		public string OnBoardingGroupName { get; set; }

		public string NguoiXetDuyetTiepTheo { get; set; }

		public string NguoiDuyetCuoi { get; set; }

		public DateTime? NgayDuyetCuoi { get; set; }

		public string HoVaTen { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }

		public string TenPhongBan { get; set; }
	}
}
