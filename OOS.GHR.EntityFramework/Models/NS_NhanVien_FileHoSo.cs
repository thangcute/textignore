namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_NhanVien_FileHoSo
	{
		[Key]
		public int HoSoID { get; set; }

		public string TenFile { get; set; }

		public byte[] Data { get; set; }

		public int? NhanVienID { get; set; }

		public int? NguoiThemID { get; set; }

		public int? HoSoThuTucID { get; set; }

		public DateTime? FileDate { get; set; }

		public DateTime? NgayUpload { get; set; }

		public string FileNameOnDisk { get; set; }

		public string Directory { get; set; }

		public long? Size { get; set; }

		public string FileStoreID { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
