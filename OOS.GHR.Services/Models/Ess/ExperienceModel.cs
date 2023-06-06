using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class ExperienceModel
    {
        public int? KinhNghiemID { get; set; }
        public string NoiLamViec { get; set; }
        [Required(ErrorMessage = "Từ ngày không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? TuNgay { get; set; }
        [Required(ErrorMessage = "Tới ngày không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ToiNgay { get; set; }
        public string ChucVu { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn Ngành nghề !")]
        public int? NganhNgheID { get; set; }
        public decimal? MucLuong { get; set; }
        public string LyDoNghiViec { get; set; }
    }
}