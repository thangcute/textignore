namespace OOS.GHR.EntityFramework.Models.Task
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class TSK_CongViec_NguoiPhoiHopThucHien
	{
		[Key]
		public int NguoiPhoiHopThucHienID { get; set; }

		public int? CongViecID { get; set; }

		public int? NhanVienID { get; set; }

		public string ChucVuNguoiPhoiHop { get; set; }

		public bool? IsActive { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
