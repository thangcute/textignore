namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_FileUpdate
	{
		[Key]
		public int FileUpdateID { get; set; }

		public string TenFile { get; set; }

		public byte[] Data { get; set; }

		public DateTime? FileDate { get; set; }

		public DateTime? NgayUpload { get; set; }

		public int? NguoiThemID { get; set; }

		public string Path { get; set; }
	}
}
