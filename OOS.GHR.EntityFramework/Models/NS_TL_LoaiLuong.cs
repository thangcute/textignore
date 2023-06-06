namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TL_LoaiLuong
	{
		[Key]
		public int LoaiLuongID { get; set; }

		public int? NhomLuongID { get; set; }

		public string MaSo { get; set; }

		public string Ten { get; set; }

		public string GhiChu { get; set; }

		public bool? NhapQuyetDinh { get; set; }

		public int? LamTron { get; set; }

		public int? ThuTu { get; set; }

		public int? ThuTuHienThi { get; set; }

		public long? color { get; set; }

		public int? TinhTaiDong { get; set; }

		public bool? ChoPhepImport { get; set; }

		public bool? ChoPhepLuuDuLieu { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
