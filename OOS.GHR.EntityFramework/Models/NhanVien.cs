namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NhanVien
	{
		[Key]
		public int NhanVienID { get; set; }

		public int? NhanVienCuID { get; set; }

		public int? CaLVID { get; set; }

		public int? PhongBanID { get; set; }

		public int? ChucVuID { get; set; }

		public int? PhongBanChucVuID { get; set; }

		public int? QuocTichID { get; set; }

		public int? DotTuyenDungID { get; set; }

		public int? NganhHocID { get; set; }

		public int? LoaiLaoDongID { get; set; }

		public int? TinhThanhID { get; set; }

		public int? QuanHuyenID { get; set; }

		public int? TrinhDoID { get; set; }

		public int? TrinhDoVanHoaID { get; set; }

		public int? TrinhDoTinHocID { get; set; }

		public int? HocHamID { get; set; }

		public int? HocViID { get; set; }

		public int? TrangThaiID { get; set; }

		public int? TonGiaoID { get; set; }

		public int? DanTocID { get; set; }

		public int? TrinhDoNgoaiNguID { get; set; }

		public int? ThiSinhID { get; set; }

		public int? LoaiHopDongID { get; set; }

		public int? ChucDanhID { get; set; }

		public int? CongTyID { get; set; }

		public int? TruongDaoTaoID { get; set; }

		public int? HangTotNghiepID { get; set; }

		public int? HeDaoTaoID { get; set; }

		public int? ChuyenNganhID { get; set; }

		public int? TrinhDoNgoaiNgu2ID { get; set; }

		public string MaNhanVien { get; set; }

		public string MaTapDoan { get; set; }

		public string Ho { get; set; }

		public string HoTen { get; set; }

		public string DiaChi { get; set; }

		public string DienThoai { get; set; }

		public string GhiChu { get; set; }

		public string CMTND { get; set; }

		public DateTime? NgayBatDauLam { get; set; }

		public string TenThuongDung { get; set; }

		public string QueQuan { get; set; }

		public string DiaChiThuongTru { get; set; }

		public string NoiCapCMT { get; set; }

		public string GioiTinh { get; set; }

		public string DienThoaiNha { get; set; }

		public string DienThoaiCoQuan { get; set; }

		public string Email { get; set; }

		public string Fax { get; set; }

		public string TinhTrangHonNhan { get; set; }

		public byte[] Anh { get; set; }

		public string SoHopDong { get; set; }

		public string TaiKhoanNganHang { get; set; }

		public string TenNganHang { get; set; }

		public string DiaChiNganHang { get; set; }

		public string HinhThucTuyenDung { get; set; }

		public string TinhTrangSucKhoe { get; set; }

		public DateTime? NgayHetHanHopDong { get; set; }

		public DateTime? NgayCapCMT { get; set; }

		public DateTime? NgaySinh { get; set; }

		public DateTime? NgayNghiViec { get; set; }

		public DateTime? NgayUpdate { get; set; }

		public DateTime? NgayKyHopDongLD { get; set; }

		public DateTime? NgayTinhLuongCoBan { get; set; }

		public DateTime? NgayHetHanHoChieu { get; set; }

		public int? XaPhuongID { get; set; }

		public DateTime? NgayHetHanThuViec { get; set; }

		public string NhomMau { get; set; }

		public string MaTheChamCong { get; set; }

		public int? XetDuyet { get; set; }

		public bool? NghiViec { get; set; }

		public string NoiSinh { get; set; }

		public string MaSoThue { get; set; }

		public string MaNVCu { get; set; }

		public string MoiQuanHe { get; set; }

		public string ChungChi { get; set; }

		public string TrinhDoNgoaiNgu { get; set; }

		public string XepLoaiNgoaiNgu { get; set; }

		public string TrinhDoTinHoc { get; set; }

		public string XepLoaiTinHoc { get; set; }

		public string SoHoChieu { get; set; }

		public decimal? SoPhepChuyenTuNamTruoc { get; set; }

		public decimal? SoPhepBanDau { get; set; }

		public decimal? SoPhepTheoQuyDinh { get; set; }

		public decimal? SoPhepDaNghi { get; set; }

		public decimal? SoPhepConLai { get; set; }

		public decimal? ThamNien { get; set; }

		public int? NamTotNghiep { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string Yahoo { get; set; }

		public string Facebook { get; set; }

		public string Skype { get; set; }

		public string Twitter { get; set; }

		public string Zalo { get; set; }

		public bool? IsDeleted { get; set; }

		public int? NhomCaLamViecID { get; set; }

		public int? BaoCaoTrucTiepID { get; set; }

		public int? BaoCaoGianTiepID { get; set; }

		public string Keyword { get; set; }

		public DateTime? NgayCap_HoChieu { get; set; }

		public DateTime? NgayHetHan_HoChieu { get; set; }

		public string Visa { get; set; }

		public DateTime? NgayCap_Visa { get; set; }

		public DateTime? NgayHetHan_Visa { get; set; }

		public int? LoaiThiThucID { get; set; }

		public string GiayPhepLaoDong { get; set; }

		public DateTime? NgayCap_GiayPhepLaoDong { get; set; }

		public DateTime? NgayHetHan_GiayPhepLaoDong { get; set; }

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

		public string QueQuan_SoNha { get; set; }

		public int? QueQuan_ThonXomID { get; set; }

		public int? QueQuan_XaPhuongID { get; set; }

		public int? QueQuan_QuanHuyenID { get; set; }

		public int? QueQuan_TinhID { get; set; }

		public string NoiSinh_SoNha { get; set; }

		public int? NoiSinh_ThonXomID { get; set; }

		public int? NoiSinh_XaPhuongID { get; set; }

		public int? NoiSinh_QuanHuyenID { get; set; }

		public int? NoiSinh_TinhID { get; set; }

		public bool? IsChuHoKhau { get; set; }

		public string NoiCap_HoChieu { get; set; }

		public string NoiCap_Visa { get; set; }

		public string NoiCap_GiayPhepLaoDong { get; set; }

		public string EmailCaNhan { get; set; }

		public string TaiKhoanLuong { get; set; }

		public string SoBHXH { get; set; }

		public string MaBHXH { get; set; }

		public int? Status { get; set; }

		public int? KhuVucID { get; set; }

		public string TT_TenChucVu { get; set; }

		public string TT_TenPhongBan { get; set; }

		public string TT_TenChucDanh { get; set; }

		public string TT_TenLoaiHopDong { get; set; }

		public decimal? NghiBuTonNamTruoc { get; set; }

		public decimal? NghiBuDuocHuong { get; set; }

		public decimal? NghiBuDaSuDung { get; set; }

		public decimal? NghiBuConLai { get; set; }

		public string CanNang { get; set; }

		public string ChieuCao { get; set; }

		public string ThanhPhanGiaDinh { get; set; }

		public string MayChamCongStr { get; set; }

		public string TenChuTaiKhoan { get; set; }

		public string GhiChu_TrinhDoChuyenMon { get; set; }
	}
}
