namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_DT_DotDaoTao_DangKyThamGia
	{
		[Key]
		public int DotDaoTaoDangKyThamGiaID { get; set; }

		public int? DotDaoTaoID { get; set; }

		public int? ChucVuID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucDanhID { get; set; }
	}
}
