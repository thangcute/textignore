namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_FM_Folder
	{
		[Key]
		public int FolderID { get; set; }

		public int? ParentFolderID { get; set; }

		public string FolderName { get; set; }

		public string Icon { get; set; }

		public int? TotalFiles { get; set; }

		public int? NhanVienID { get; set; }

		public bool? IsDeleted { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public DateTime? DeletedDate { get; set; }
	}
}
