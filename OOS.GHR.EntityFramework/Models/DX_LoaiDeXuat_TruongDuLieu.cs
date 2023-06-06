namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class DX_LoaiDeXuat_TruongDuLieu
	{
		[Key]
		public int TruongDuLieuID { get; set; }

		public int? LoaiDeXuatID { get; set; }

		public string TenTruongDuLieu { get; set; }

		public string TenTiengAnh { get; set; }

		public string KieuDuLieu { get; set; }

		public string QueryData { get; set; }

		public string ControlType { get; set; }

		public string FormatString { get; set; }

		public string GiaTriMacDinh { get; set; }

		public string TableName { get; set; }

		public string TextField { get; set; }

		public string ValueField { get; set; }

		public int? STT { get; set; }

		public string MoTaNhapLieu { get; set; }

		public string MoTaNgan { get; set; }

		public int? Width { get; set; }

		public int? Height { get; set; }

		public bool? AllowNull { get; set; }

		public bool? LayThongTinMoTa { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
