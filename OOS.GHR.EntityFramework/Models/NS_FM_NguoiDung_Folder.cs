namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_FM_NguoiDung_Folder
	{
		[Key]
		public int NguoiDungFolderID { get; set; }

		public int? NhanVienID { get; set; }

		public int? PhongBanID { get; set; }

		public int? FolderID { get; set; }

		public int? PermissionType { get; set; }

		public int? XetDuyet { get; set; }
	}
}
