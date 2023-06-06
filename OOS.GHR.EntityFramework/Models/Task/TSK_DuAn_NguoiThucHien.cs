namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_DuAn_NguoiThucHien
	{
		[Key]
		public int NguoiThucHienID { get; set; }

		public int? DuAnID { get; set; }

		public int? NhanVienID { get; set; }

		public string ChucVuDuAn { get; set; }

		public DateTime? NgayGiaoNhiemVu { get; set; }

		public bool? IsActive { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
