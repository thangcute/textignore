namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class DX_DeXuat_FollowUser
	{
		[Key]
		public int FollowUserID { get; set; }

		public int? DeXuatID { get; set; }

		public int? UserID { get; set; }

		public bool? Block { get; set; }

		public string HoVaTen { get; set; }

		public string ChucVu { get; set; }

		public string PhongBan { get; set; }

		public int? PhongBanID { get; set; }

		public int? NhanVienID { get; set; }

		public int? ChucVuID { get; set; }
	}
}
