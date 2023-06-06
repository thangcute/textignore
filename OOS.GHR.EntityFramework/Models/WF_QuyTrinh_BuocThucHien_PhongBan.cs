namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class WF_QuyTrinh_BuocThucHien_PhongBan
	{
		[Key]
		public int BuocThucHienPhongBanID { get; set; }

		public int? QuyTrinhBuocThucHienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? NguoiXetDuyetID { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
