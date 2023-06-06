namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_ThongSo
	{
		[Key]
		public int ThongSoID { get; set; }

		public int? NhomThongSoID { get; set; }

		public string MaSo { get; set; }

		public string TenThongSo { get; set; }

		public string MoTa { get; set; }

		public string GiaTri { get; set; }

		public string GiaTriMacDinh { get; set; }

		public string KieuDuLieu { get; set; }

		public string Nhom { get; set; }

		public string TenTA { get; set; }

		public string XMLDes { get; set; }

		public string SQLQueryData { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? CongTyID { get; set; }

		public int? STT { get; set; }

		public string FormatString { get; set; }
	}
}
