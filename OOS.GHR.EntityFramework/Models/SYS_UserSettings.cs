namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_UserSettings
	{
		[Key]
		public int UserSettingID { get; set; }

		public int? NguoiDungID { get; set; }

		public byte[] Settings { get; set; }

		public string GhiChu { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string Code { get; set; }
	}
}
