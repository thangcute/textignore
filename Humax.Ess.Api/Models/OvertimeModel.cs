using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Humax.Ess.Api.Models
{
    public class OvertimeModel
    {
        public int Id { get; set; }
        public int? ReasonId { get; set; }
        [NotMapped]
        public string ReasonName { get; set; }
        public string DatetimeStart { get; set; }
        public string DatetimeEnd { get; set; }
        public int? totalDays { get; set; }
        public string RestPlace { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string FilePath { get; set; }
        public int? CreateUserId { get; set; }
        [NotMapped]
        public string CreateUserFullname { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ApprovalStatus { get; set; }
        [NotMapped]
        public string ApprovalStatusName { get; set; }
        public string ApprovalNote { get; set; }
        public int? ApprovalUserId { get; set; }
        [NotMapped]
        public string ApprovalUserFullname { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
    }

    public class OtPostModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Ngày đăng ký không để trống.")]
        public DateTime NgayDangKy { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn Ca làm việc !")]
        
        public int? CaLamViecID { get; set; }
        
        public int? KyHieuChamCongID { get; set; }
        
        public decimal? SoGioLamThemTruocCa { get; set; }
        
        public decimal? SoGioLamThemSauCa { get; set; }
        
        public string BDLamThemTruocCa { get; set; }
        
        public string KTLamThemTruocCa { get; set; }
        
        public string BDLamThemSauCa { get; set; }
        
        public string KTLamThemSauCa { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn Lý do !")]        
        public int? LyDoTangCaID { get; set; }
        
        public string LyDoTangCa { get; set; }
        
        public bool? AnTangCa { get; set; }

        public bool? IsMoiTruongDacBiet { get; set; }

        public string TuGio { get; set; }

        public string ToiGio { get; set; }
    }

    public class OvertimePostModel
    {
        public int? Id { get; set; }
        public string Date { get; set; }
        public int WorkShiftId { get; set; }        
        public string BeforeFrom { get; set; }
        public string BeforeTo { get; set; }
        public string AfterFrom { get; set; }
        public string AfterTo { get; set; }
        public decimal? BeforeHourPlus { get; set; }
        public decimal? AfterHourPlus { get; set; }
        public int ReasonId { get; set; }
        public string Description { get; set; }
    }
}