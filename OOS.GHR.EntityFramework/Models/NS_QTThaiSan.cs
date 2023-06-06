namespace OOS.GHR.EntityFramework.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

	public partial class NS_QTThaiSan
	{
		[Key]
		public int QTThaiSanID { get; set; }

		public int? NhanVienID { get; set; }

		public string QuyetDinhSo { get; set; }

		public DateTime? NgayQuyetDinh { get; set; }

		public string NguoiKy { get; set; }

		public DateTime? NgayBatDau { get; set; }

		public DateTime? NgayKetThuc { get; set; }

		public DateTime? NgaySinhCon { get; set; }

		public DateTime? NgayLap { get; set; }

		public DateTime? NgayConChet { get; set; }

		public DateTime? NgaySayThai { get; set; }

		public DateTime? NgayDuKienSinh { get; set; }

		public DateTime? NgayDiLamTroLai { get; set; }

		public DateTime? NgayBDHuongCD7Thang { get; set; }

		public DateTime? NgayKTHuongCD7Thang { get; set; }

		public int? SinhConLanThu { get; set; }

		public string GhiChu { get; set; }

		public int? XetDuyet { get; set; }

		public bool? DaDuocThanhToanBHXH { get; set; }

		public decimal? TongSoTienThanhToan { get; set; }

		public int? NguoiThamGiaBHID { get; set; }

		public int? SoQuyetDinhID { get; set; }

		public int? NguoiKyID { get; set; }

		public string TenCon { get; set; }

		public string TinhTrangKhiSinh { get; set; }

		public int? SoNgayNghiTaiGia { get; set; }

		public int? SoNgayNghiTapChung { get; set; }

		public decimal? MucLuongDongBHXH { get; set; }

		public decimal? MucLuongBinhQuan { get; set; }

		public decimal? MucLuongToiThieu { get; set; }

		public string TenConThuHai { get; set; }

		public int? SoLuongCon { get; set; }

		public bool? IsNhanNuoiCon { get; set; }

		public int? NguoiLapID { get; set; }

		public int? TuoiConKhiNhanNuoi { get; set; }

		public bool? IsDie { get; set; }

		public bool? NghiDuongSuc { get; set; }

		public string ThoiGianDongBHXH { get; set; }

		public int? TongSoThang { get; set; }

		public string TenConThuBa { get; set; }

		public string TenConThuTu { get; set; }

		public int? CongTyID { get; set; }

		public bool? DaBaoCao { get; set; }

		public int? DonViID { get; set; }

		public int? MucLuongBinhQuanBD { get; set; }

		public decimal? TongSoTienThanhToanBD { get; set; }

		public DateTime? NgayLapBD { get; set; }

		public int? CreatedByID { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? ModifyByID { get; set; }

		public DateTime? ModifyDate { get; set; }

		public string CreatedBy { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime? ApprovedDate { get; set; }

		public string HoVaTen { get; set; }

		public string TenPhongBan { get; set; }

		public string TenChucVu { get; set; }

		public string TenChucDanh { get; set; }
	}
}
