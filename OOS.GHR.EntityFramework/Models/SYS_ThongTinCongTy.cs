namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class SYS_ThongTinCongTy
	{
		public int ID { get; set; }

		public int? LinhVucKinhDoanhID { get; set; }

		public int? NguoiKyHDLDID { get; set; }

		public int? NguoiDaiDienID { get; set; }

		public string TenCongTy { get; set; }

		public string MaDonVi { get; set; }

		public string MaCK { get; set; }

		public bool? IsCongTyCon { get; set; }

		public string ThuDienTu { get; set; }

		public string DiaChi { get; set; }

		public byte[] Logo { get; set; }

		public string Website { get; set; }

		public string ActiveCode { get; set; }

		public string Version { get; set; }

		public byte[] Data { get; set; }

		public string Fax { get; set; }

		public string DienThoai { get; set; }

		public string SoDKKD { get; set; }

		public string MaSoThue { get; set; }

		public byte[] DataCI { get; set; }

		public string MaKhachHang { get; set; }

		public string ThongTinUyQuyen { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }
	}
}
