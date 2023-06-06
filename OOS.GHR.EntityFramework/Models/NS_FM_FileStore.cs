namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_FM_FileStore
	{
		[Key]
		public int FileStoreID { get; set; }

		public int? FolderID { get; set; }

		public int? NhanVienID { get; set; }

		public string Directory { get; set; }

		public string FileName { get; set; }

		public string FileSourceName { get; set; }

		public long? Size { get; set; }

		public bool? IsMarkStar { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public bool? IsDeleted { get; set; }

		public DateTime? DeletedDate { get; set; }
	}
}
