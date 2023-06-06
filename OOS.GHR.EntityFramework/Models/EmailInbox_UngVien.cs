namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class EmailInbox_UngVien
	{
		[Key]
		public int UngVienID { get; set; }

		public DateTime? NgayNhanHoSo { get; set; }

		public int? InboxID { get; set; }

		public int? ProfileID { get; set; }

		public int? ThiSinhID { get; set; }

		public int? NguonHoSoID { get; set; }

		public string ViTriTuyenDung { get; set; }

		public int? ChucVuID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? NhomUngVienID { get; set; }

		public string Subject { get; set; }

		public string HoVaTen { get; set; }

		public string DienThoai { get; set; }

		public string Email { get; set; }

		public DateTime? NgaySinh { get; set; }

		public string DiaChi { get; set; }

		public string DienThoaiTemp { get; set; }

		public string HoVaTenTemp { get; set; }

		public string NgaySinhTemp { get; set; }

		public string EmailTemp { get; set; }

		public bool? IsRead { get; set; }

		public bool? IsIgnore { get; set; }

		public string KinhNghiemLamViec { get; set; }

		public string TrinhDoHocVan { get; set; }

		public string SoThich { get; set; }

		public string UuDiem { get; set; }

		public string NhuocDiem { get; set; }

		public byte[] PictureAvatar { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedByID { get; set; }

		public string MucLuongMongMuon { get; set; }

		public decimal? SoNamKinhNghiem { get; set; }

		public string GioiTinh { get; set; }

		public string TinhTrangHonNhan { get; set; }

		public string CapBacMongMuon { get; set; }

		public string DiaDiemLamViec { get; set; }

		public string NgoaiNgu { get; set; }

		public string CapBacHienTai { get; set; }

		public string GhiChu { get; set; }

		public string NguonHoSo { get; set; }

		public int? FileStoreID { get; set; }

		public string FileAttachmentContent { get; set; }
	}
}
