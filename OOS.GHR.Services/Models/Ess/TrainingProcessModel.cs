using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class TrainingProcessModel
    {
        public int? QTDaoTaoID { get; set; }
        public string LopDaoTao { get; set; }
        public string NoiDungDaoTao { get; set; }
        public int? KhoaDaoTaoID { get; set; }

        [Required(ErrorMessage = "Từ ngày không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? TuNgay { get; set; }

        [Required(ErrorMessage = "Từ ngày không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ToiNgay { get; set; }
        public string NoiDaoTao { get; set; }
        public int? HeDaoTaoID { get; set; }
        public int? NganhHocID { get; set; }
        public int? ChuyenNganhID { get; set; }
        public int? VanBangID { get; set; }
        public int? ChungChiID { get; set; }
        public int? KetQuaDaoTaoID { get; set; }
        public decimal? DiemSo { get; set; }
        public string FileAttachment { get; set; }
        public string FileName { get; set; } = "";
    }
}
