namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DsNguoiThamKhao
	{
		[Key]
		public int NguoiThamKhaoID { get; set; }

		public string Ten { get; set; }

		public string DienThoai { get; set; }

		public string ChucVu { get; set; }

		public string NoiLamViec { get; set; }
	}
}
