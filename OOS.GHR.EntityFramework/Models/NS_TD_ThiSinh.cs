namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_TD_ThiSinh
	{
		[Key]
		public int ThiSinhID { get; set; }

		public int? CongTyID { get; set; }

		public int? DanTocID { get; set; }

		public int? NgoaiNguID { get; set; }

		public int? TinHocID { get; set; }

		public int? NganhNgheID { get; set; }

		public int? TrinhDoID { get; set; }

		public int? TonGiaoID { get; set; }

		public int? NhomThiSinhID { get; set; }

		public int? HeDaoTaoID { get; set; }

		public int? HangTotNghiepID { get; set; }

		public int? NguonHoSoID { get; set; }

		public string NguonHoSoWebID { get; set; }

		public string TruongDaoTao { get; set; }

		public string TrinhDoNgoaiNgu { get; set; }

		public string SoBaoDanh { get; set; }

		public string SoCMTND { get; set; }

		public string TenHo { get; set; }

		public string Ten { get; set; }

		public string DienThoai { get; set; }

		public string Email { get; set; }

		public string DiaChiLienHe { get; set; }

		public DateTime? NgaySinh { get; set; }

		public byte[] Anh { get; set; }

		public string GioiTinh { get; set; }

		public string NoiSinh { get; set; }

		public DateTime? NgayVaoDang { get; set; }

		public byte? SoNamKinhNghiem { get; set; }

		public string KinhNghiemLamViec { get; set; }

		public string HocVan { get; set; }

		public string KyNang { get; set; }

		public string MucTieuNgheNghiep { get; set; }

		public string NguoiThamKhao { get; set; }

		public int? MucLuongHienTai { get; set; }

		public string MucLuongMongMuon { get; set; }

		public int? TrangThai { get; set; }

		public string GhiChu { get; set; }

		public DateTime? NgayTaoHoSo { get; set; }

		public DateTime? NgayCap { get; set; }

		public string NoiCap { get; set; }

		public string NguyenQuan { get; set; }

		public string HoKhauThuongTru { get; set; }

		public DateTime? NamTotNghiep { get; set; }

		public DateTime? NgaySanSangDiLam { get; set; }

		public string TinhTrangHonNhan { get; set; }

		public string DeXuat { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public int? ViTriDuTuyenID { get; set; }

		public int? ViTriDuTuyen2ID { get; set; }

		public int? ViTriDuTuyen3ID { get; set; }

		public string NguoiThamKhao1 { get; set; }

		public string NguoiThamKhao2 { get; set; }

		public string NguoiThamKhao3 { get; set; }

		public string DiaChi_SoNha { get; set; }

		public int? DiaChi_ThonXomID { get; set; }

		public int? DiaChi_XaPhuongID { get; set; }

		public int? DiaChi_QuanHuyenID { get; set; }

		public int? DiaChi_TinhID { get; set; }

		public string DiaChiTT_SoNha { get; set; }

		public int? DiaChiTT_ThonXomID { get; set; }

		public int? DiaChiTT_XaPhuongID { get; set; }

		public int? DiaChiTT_QuanHuyenID { get; set; }

		public int? DiaChiTT_TinhID { get; set; }

		public int? PhongBanID { get; set; }

		public int? NhanVienID { get; set; }

		public string CapBacHienTai { get; set; }

		public string CapBacMongMuon { get; set; }

		public string FileSVSource { get; set; }

		public string MatKhau { get; set; }

		public bool? IsBlackList { get; set; }

		public string FileAttachmentContent { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? VongThiID { get; set; }

		public string ThongTinDotTuyenDung { get; set; }
	}
}
