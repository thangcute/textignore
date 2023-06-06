namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_ThiSinh_DaoTao
	{
		[Key]
		public int DaoTaoID { get; set; }

		public int? ThiSinhID { get; set; }

		public string NoiDaoTao { get; set; }

		public string GhiChu { get; set; }

		public int? NghanhHocID { get; set; }

		public int? VanBangID { get; set; }

		public int? NuocDaoTaoID { get; set; }

		public int? ChungChiID { get; set; }

		public int? HangTotNghiepID { get; set; }

		public int? TruongDaoTaoID { get; set; }

		public int? HeDaoTaoID { get; set; }

		public int? ChuyenNganhID { get; set; }

		public string LopDaoTao { get; set; }

		public DateTime? TuNgay { get; set; }

		public DateTime? ToiNgay { get; set; }

		public string DiemSo { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
