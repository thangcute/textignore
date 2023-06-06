namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class DX_DeXuat
	{
		[Key]
		public int DeXuatID { get; set; }

		public string TieuDe { get; set; }

		public int? LoaiDeXuatID { get; set; }

		public int? QuyTrinhID { get; set; }

		public DateTime? NgayDeXuat { get; set; }

		public int? NguoiDeXuatID { get; set; }

		public string NguoiDeXuat_HoVaTen { get; set; }

		public string NguoiDeXuat_MaNhanVien { get; set; }

		public string NguoiDeXuat_ChucVu { get; set; }

		public string NguoiDeXuatTao_PhongBan { get; set; }

		public string NoiDung_ThuGon { get; set; }

		public int? XetDuyet { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
