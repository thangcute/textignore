using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class TimeSheetModel
    {
        public long? TongHopTheoNgayID { get; set; }
        public string Thu { get; set; }
        public string CaLamViec { get; set; }
        public DateTime NgayThang { get; set; }
        public TimeSpan? TG_Den { get; set; }
        public TimeSpan? TG_Ra { get; set; }
        public TimeSpan? TG_Vao { get; set; }
        public TimeSpan? TG_Ve { get; set; }
        public string NghiPhep_CongTac { get; set; }
        public string DangKy { get; set; }
        public decimal? TG_LamViec { get; set; }
        public decimal? TG_LamThem { get; set; }
        public decimal? DiMuon { get; set; }
        public decimal? VeSom { get; set; }
        public string GiaiTrinhDiMuon { get; set; }
        public bool? DisableGiaiTrinhDiMuon { get; set; }
        public string GiaiTrinhVeSom { get; set; }
        public bool? DisableGiaiTrinhVeSom { get; set; }
        public string GiaiTrinhThemGio { get; set; }
        public bool? DisableGiaiThemGio { get; set; }
        public bool? Lock { get; set; }
        public bool? IsLamThubay { get; set; }
        public bool? IsLamChuNhat { get; set; }
        public int? XetDuyet { get; set; }
        public string Approver { get; set; }
        public string ApprovedDate { get; set; }
        public string ApproveComment { get; set; }
    }

    public class ExplainModel
    {
        [Required(ErrorMessage = "Ngày tháng không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgayThang { get; set; }

        public bool IsDimuon { get; set; } = true;

        [Required(ErrorMessage = "Lý do không để trống.")]
        public string LyDo { get; set; }
    }

    public class LostFingerModel
    {
        public long TongHopTheoNgayID { get; set; } = 0;
        [Required(ErrorMessage = "Ngày tháng không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime NgayThang { get; set; }
        public string TG_Den { get; set; }
        public string TG_Ra { get; set; }
        public string TG_Vao { get; set; }
        public string TG_Ve { get; set; }
        [Required(ErrorMessage = "Lý do  không để trống.")]
        public string Lydo { get; set; }
    }

    public class TimeSheetSummaryModel
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
