namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_FM_FileShare
	{
		[Key]
		public int FileShareID { get; set; }

		public int? FileStoreID { get; set; }

		public int? FolderID { get; set; }

		public int? ShareToNhanVienID { get; set; }

		public int? ShareToPhongBanID { get; set; }

		public string FromUserName { get; set; }

		public string Name { get; set; }

		public string ShareToName { get; set; }

		public int? PermissionType { get; set; }

		public bool? IsPublicFolder { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }
	}
}
