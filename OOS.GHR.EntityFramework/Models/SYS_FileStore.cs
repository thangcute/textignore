namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_FileStore
	{
		[Key]
		public string FileStoreID { get; set; }

		public string FileName { get; set; }

		public string FileSourceName { get; set; }

		public string Directory { get; set; }

		public long? Size { get; set; }

		public long? ObjectID { get; set; }

		public int? NhanVienID { get; set; }

		public string Module { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }
	}
}
