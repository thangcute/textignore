namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class Poll_Poll
	{
		[Key]
		public int PollID { get; set; }

		public string DisplayText { get; set; }

		public string Description { get; set; }

		public bool? IsActive { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? ExpiredDate { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string PostFor { get; set; }

		public int? PostForNhanVienID { get; set; }

		public int? PostForPhongBanID { get; set; }

		public int? PostForChucVuID { get; set; }

		public int? PostForChucDanhID { get; set; }

		public int? PostForLoaiHopDongID { get; set; }
	}
}
