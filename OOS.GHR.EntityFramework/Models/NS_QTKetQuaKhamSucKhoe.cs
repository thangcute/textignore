namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTKetQuaKhamSucKhoe
	{
		[Key]
		public int QTKetQuaKhamSucKhoeID { get; set; }

		public string KetQua { get; set; }

		public string GhiChu { get; set; }

		public int? DotKhamSucKhoeID { get; set; }

		public int? LoaiKhamSucKhoeID { get; set; }

		public int? NhanVienID { get; set; }

		public string Ten { get; set; }

		public int? KhamSucKhoeID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }
	}
}
