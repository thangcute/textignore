using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOS.GHR.Services.Models.Ess
{
    public class BusinessTripModel
    {
    }

    public class BusinessTripPostModel
    {
        public int? Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Phải chọn Lý do công tác !")]
        public int? LyDoCongTacID { get; set; }
        [Required(ErrorMessage = "Ngày bắt đầu không để trống.")]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc không để trống.")]
        public DateTime NgayKetThuc { get; set; }
        public string NoiCongTac { get; set; }
        [Required(ErrorMessage = "Công việc củ thể không để trống.")]
        public string CongViecCuThe { get; set; }
    }
}
