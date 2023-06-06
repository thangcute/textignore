using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class WorkingModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Số Quyết định không để trống.")]
        public string SoQuyetDinh { get; set; }

        [Required(ErrorMessage = "Ngày Quyết định không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NgayQuyetDinh { get; set; }

        [Required(ErrorMessage = "Ngày Chuyển không để trống.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NgayChuyen { get; set; }

        public DateTime? NgayHetHan { get; set; }
        public string TenPhongBanCu { get; set; }
        public int? PhongBanCuID { get; set; }
        public int? ChucVuCuID { get; set; }
        public int? ChucDanhCuID { get; set; }
        public int? PhongBanMoiID { get; set; }
        public int? ChucVuMoiID { get; set; }
        public int? ChucDanhMoiID { get; set; }
        public string TenLyDoChuyen { get; set; }
        public int? LyDoChuyenID { get; set; }
        public string LyDoChuyen { get; set; }
        public string FileAttachment { get; set; }
        public string FileName { get; set; } = "";
        public int XetDuyet { get; set; }
    }
}
