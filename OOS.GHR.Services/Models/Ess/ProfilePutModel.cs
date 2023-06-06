using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OOS.GHR.Services.Models.Ess
{
    public class ProfilePutModel
    {
        public string QueQuan { get; set; }
        public string GioiTinh { get; set; }
        public int? QuocTichID { get; set; }
        public int? DanTocID { get; set; }
        public int? TonGiaoID { get; set; }
        public decimal? SoPhepTheoQuyDinh { get; set; }
        public decimal? SoPhepDaNghi { get; set; }
        public string TinhTrangHonNhan { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public DateTime? NgayKyHopDongLD { get; set; }
        public DateTime? NgaybatDauLam { get; set; }
        public string CMTND { get; set; }
        public DateTime? NgayCapCMT { get; set; }
        public string NoiCapCMT { get; set; }
        public string ThuongTruSoNha { get; set; }      //
        public int? ThuongTruTinhID { get; set; }
        public int? ThuongTruQuanHuyenID { get; set; }
        public int? ThuongTruXaPhuongID { get; set; }   //
        public string DiaChiThuongTru { get; set; }
        public string HienTaiSoNha { get; set; }        //
        public int? HienTaiTinhID { get; set; }
        public int? HienTaiQuanHuyenID { get; set; }
        public int? HienTaiXaPhuongID { get; set; }     //
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string EmailCaNhan { get; set; }
        public string MaSoThue { get; set; }
        public string MaBHXH { get; set; }
        public int? TrinhDoID { get; set; }
        public int? TruongDaoTaoID { get; set; }
        public int? ChuyenNganhID { get; set; }
        public int? NamTotNghiep { get; set; }
        public int? HangTotNghiepID { get; set; }
        public string TaiKhoanNganHang { get; set; }
        public string TenNganHang { get; set; }
        //public HttpPostedFileBase FileAttachment { get; set; }

        //[LimitFileSize(ErrorMessage = "Dung lượng file không được quá 1MB!")]
        public string Picture { get; set; }
        public string FileAttachment { get; set; }
        public string FileName { get; set; } = "";
        public int LoaiHopDongID { get; set; }
        public int ChucDanhID { get; set; }
        public int ChucVuID { get; set; }
        public int PhongBanID { get; set; }
    }

    public class LimitFileSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (ProfilePutModel)validationContext.ObjectInstance;
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            var characterCount = value.ToString().Length;
            var paddingCount = value.ToString().Substring(characterCount - 2, 2).Count(c => c == '=');
            int fileSize = (3 * (characterCount / 4)) - paddingCount; // Bytes

            if(fileSize > ((1024* 1024) / 2))
                return new ValidationResult("Dung lượng file không được quá 1MB!");

            return ValidationResult.Success;
        }
    }
}
