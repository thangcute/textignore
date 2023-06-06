namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_NhuCauTuyenDung
	{
		[Key]
		public int NhuCauTuyenDungID { get; set; }

		public int? PhongBanChucVuID { get; set; }

		public int? ViTriTuyenDungID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public DateTime? NgayLap { get; set; }

		public DateTime? NgayCan { get; set; }

		public DateTime? NgayXetDuyet { get; set; }

		public DateTime? NgayYeuCau { get; set; }

		public int? PhongBanYeuCauID { get; set; }

		public int? NguoiYeuCauID { get; set; }

		public string NganhNgheID { get; set; }

		public int? NgoaiNguID { get; set; }

		public int? NguoiXetDuyetID { get; set; }

		public int? TrinhDoID { get; set; }

		public string TenViTri { get; set; }

		public string TenPhongBan { get; set; }

		public string NguoiYeuCau { get; set; }

		public string MucLuong { get; set; }

		public string GioiTinh { get; set; }

		public string DoTuoi { get; set; }

		public string TinhTrangHonNhan { get; set; }

		public int? SoLuongYC { get; set; }

		public int? SoLuongXD { get; set; }

		public int? SoLuongTD { get; set; }

		public int? SoLuongDB { get; set; }

		public int? SoNamKinhNghiem { get; set; }

		public string MoTaCongViec { get; set; }

		public string YeuCauKhac { get; set; }

		public string LyDoTuyen { get; set; }

		public string LyDoYeuCauHuyOrTamDung { get; set; }

		public string GhiChu { get; set; }

		public string DiaDiemLamViec { get; set; }

		public int? KhuVucID { get; set; }

		public int? CongTyID { get; set; }

		public string FileStoreID { get; set; }

		public string FileStoreName { get; set; }

		public string ThayTheNhanVienIDStr { get; set; }

		public string YKienPheDuyet { get; set; }

		public byte? TrangThai { get; set; }

		public byte? TrangThai_Old { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }
	}
}
