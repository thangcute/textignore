namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_ThiSinh_QuanHeGiaDinh
	{
		[Key]
		public int QuanHeGiaDinhID { get; set; }

		public int? ThiSinhID { get; set; }

		public string QuanHe { get; set; }

		public string HoTen { get; set; }

		public DateTime? NgaySinh { get; set; }

		public string NgheNghiepHienNay { get; set; }

		public string DiaChiThuongTru { get; set; }

		public string GhiChu { get; set; }

		public string GioiTinh { get; set; }

		public string DienThoaiLienHe { get; set; }

		public string CMTND { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public bool? IsLienLacKhanCap { get; set; }
	}
}
