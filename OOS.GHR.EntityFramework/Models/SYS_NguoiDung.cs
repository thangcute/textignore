namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_NguoiDung
	{
		[Key]
		public int NguoiDungID { get; set; }

		public int? NhanVienID { get; set; }

		public int? NhomNguoiDungID { get; set; }

		public int? NhomQuyenID { get; set; }

		public string TenDangNhap { get; set; }

		public string MatKhau { get; set; }

		public DateTime? LastLogin { get; set; }

		public DateTime? LastLogout { get; set; }

		public DateTime? DueDate { get; set; }

		public bool? Active { get; set; }

		public bool? ActiveMobileApp { get; set; }

		public bool? IsPortalAccount { get; set; }

		public bool? IsADAccount { get; set; }

		public string ActiveModule { get; set; }

		public bool? IsDeleted { get; set; }

		public string AID { get; set; }

		public string DeviceId { get; set; }

		public string BrowserToken { get; set; }

		public string MobileToken { get; set; }

		public string Token { get; set; }

		public int? HRIS_TuCapBac { get; set; }

		public int? CB_TuCapBac { get; set; }

		public string EmailAccount { get; set; }

		public string EmailPassword { get; set; }

		public string ND_HoVaTen { get; set; }

		public string ND_MaNhanVien { get; set; }

		public bool? MustChangePassword { get; set; }

		public long? ActivitySaw { get; set; }

		public int? TwoFactorAuthenticationMode { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
